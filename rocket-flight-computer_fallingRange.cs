//STV script on the Steam Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=2167483644

//library used for roll pitch yaw https://steamcommunity.com/sharedfiles/filedetails/?id=1390966561

// config
string[] flightIndicatorsLcdNames = { "" };
string flightIndicatorsControllerName = "";
const bool stalizableYaw = false; // do you want to stablize yaw to 0°
const bool isPlanetWorld = true; // this should be true for every easy start or star system scenario, false if no planet in your scenario

// end of config

enum FlightMode { STABILIZATION, STANDY };
FlightMode flightIndicatorsFlightMode = FlightMode.STANDY;
List<IMyTextPanel> flightIndicatorsLcdDisplay = new List<IMyTextPanel>();
List<IMyTextSurface> flightIndicatorsSurfaceDisplay =  new List<IMyTextSurface>();
IMyShipController flightIndicatorsShipController = null;

//default constant
const double pidP = 0.06f;
const double pidI = 0.00f;
const double pidD = 0.01f;

BasicLibrary basicLibrary;
LCDHelper lcdHelper;
FlightIndicators flightIndicators;
FightStabilizator fightStabilizator;


        DebugAPI Debug;
//x,y,z coords
Vector3D vec3Dtarget = new Vector3D(0, 0, 0);

PIDController altRegulator = new PIDController(0.06f, .00f, 0.01f);
double wantedAltitude = 1500;
double altitudeError = 0f;
Vector3D shipAcceleration = new Vector3D(0, 0, 0);
Vector3D prevLinearSpeedsShip = new Vector3D(0, 0, 0);

PIDController downwardSpeedAltRegulator = new PIDController(1f, .00f, 0.0f);
double alt = 0f;
double last_alt = 0f;
double alt_speed_ms_1 = 0f;
double last_alt_speed_ms_1 = 0f;
double alt_acc_ms_2 = 0f;

double derivateDistToPlanetCenter = 0f;
double lastDistToPlanetCenter = 0f;

System.DateTime lastTime = System.DateTime.UtcNow;
System.DateTime lastRunTs = System.DateTime.UtcNow;

bool firstMainLoop = true;

//drone landing
PIDController angleRollPID = new PIDController(1f, 0f, 0f);

PIDController anglePitchPID = new PIDController(1f, 0f, 0f);

IMyShipController myRemoteControl = null;

List<IMyRadioAntenna> listAntenna = new List<IMyRadioAntenna>();

IMyRadioAntenna theAntenna = null;
	
public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
    basicLibrary = new BasicLibrary(GridTerminalSystem, Echo);
    //lcdHelper = new LCDHelper(basicLibrary, new Color(0, 255, 0), 1.5f);
    lcdHelper = new LCDHelper(basicLibrary, new Color(255, 255, 255), 1.5f);
	
	GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);
	
	
    if (listAntenna.Count != 0){
		foreach (IMyRadioAntenna antenna in listAntenna){
			if(antenna.IsSameConstructAs(Me)){
				theAntenna = antenna;
			}
		}
	}
            Debug = new DebugAPI(this);
}

public void Main(string argument)
{
	
    if (!TryInit())
    {
        return;
    }

    System.DateTime now = System.DateTime.UtcNow;
    var deltaTime = (float)(now - lastTime).Milliseconds / 1000f;
    lastTime = now;
    DateTime d = new DateTime(1970, 01, 01);
    var temp = d.Ticks; // == 621355968000000000
    var temp2 = now.Ticks;

    DateTime dt1970 = new DateTime(1970, 1, 1);
    DateTime current = DateTime.Now;//DateTime.UtcNow for unix timestamp
    TimeSpan span = current - dt1970;
	//Echo("temp:" + temp);
    Echo("now:" + now);
    Echo("deltaTime = now - lastTime:" + deltaTime);
    Echo("now - lastRunTs:" + (now - lastRunTs).Milliseconds / 1000f);
    //Echo("temp2:" + temp2);
    //Echo("temp2/10**6:" + (temp2 / 1000000f));
    Echo("span:" + span.TotalMilliseconds.ToString());

    if (flightIndicatorsShipController == null)
    { Echo("no IMyShipController available"); return; }
    //ship controlller GetTotalGravity()
    if (myRemoteControl == null)
    {
        myRemoteControl = flightIndicatorsShipController;
    }
    Vector3D totalGravityVect3D = myRemoteControl.GetTotalGravity();
    //Echo("\n\ntotalGravityVect3D:\n" + totalGravityVect3D);
    Vector3D totalGravityVect3Dnormalized = Vector3D.Normalize(totalGravityVect3D);
    Echo("\n\ntotalGravityVect3Dnormalized:\n" + totalGravityVect3Dnormalized);
    MyBlockOrientation cockpitOrientation = myRemoteControl.Orientation;
    var leftCockpitOrientation = cockpitOrientation.Left;
    Echo("leftCockpitOrientation:" + leftCockpitOrientation);

    //getting vectors to help with angles proposals
    Vector3D shipForwardVector = myRemoteControl.WorldMatrix.Forward;
    Vector3D shipLeftVector = myRemoteControl.WorldMatrix.Left;
    Vector3D shipDownVector = myRemoteControl.WorldMatrix.Down;
    //Echo("shipForwardVector:" + shipForwardVector);
    //Echo("shipLeftVector:" + shipLeftVector);
    //Echo("shipDownVector:" + shipDownVector);

    //Getting the ship/pb postion
    Vector3D myPos = myRemoteControl.GetPosition();
    //Echo("myPos:\n" + myPos);

    //note:
    //https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/VRage.Math/Vector3D.cs
    //var targetGpsString = "";
    //Echo("targetGpsString:" + targetGpsString);
    MyWaypointInfo myWaypointInfoTarget = new MyWaypointInfo("lol", 0, 0, 0);
    //MyWaypointInfo.TryParse("GPS:/// #4:53590.85:-26608.05:11979.08:", out myWaypointInfoTarget);

    if (argument != null)
    {
        if (argument != "")
        {
            Echo("argument:" + argument);
            if (argument.Contains(":#") == true)
            {
                Echo("if (argument.Contains(:#) == true)");
                MyWaypointInfo.TryParse(argument.Substring(0, argument.Length - 10), out myWaypointInfoTarget);
            }
            else
            {
                Echo("not if (argument.Contains(:#) == true)");
                MyWaypointInfo.TryParse(argument, out myWaypointInfoTarget);
            }
            if (myWaypointInfoTarget.Coords != new Vector3D(0, 0, 0))
            {
                //x,y,z coords is global to remember between each loop
                vec3Dtarget = myWaypointInfoTarget.Coords;
            }
        }
    }

    if (vec3Dtarget == new Vector3D(0, 0, 0))
    {
        //using the expected remote control to give us the center of the current planet
        flightIndicatorsShipController.TryGetPlanetPosition(out vec3Dtarget);
    }

    //Echo("vec3Dtarget:" + vec3Dtarget);

    //targetGravityVectorNormalized
    Vector3D earthLikeCenter = new Vector3D(0, 0, 0);
    Vector3D vec3DtargetNegate;
    Vector3D.Negate(ref vec3Dtarget, out vec3DtargetNegate);
    Vector3D targetGravityVectorNormalized = Vector3D.Normalize(Vector3D.Add(vec3DtargetNegate, earthLikeCenter));
    //Echo("\ntargetGravityVectorNormalized:\n" + targetGravityVectorNormalized);

    //distance to target TODO
    Vector3D VectToTarget = Vector3D.Add(vec3DtargetNegate, myPos);
    double distToTarget = VectToTarget.Length();
    //Echo("\ndistToTarget:" + distToTarget);

    //totalGravityVect3Dnormalized cross targetGravityVectorNormalized
    Vector3D crossCurrentTargetGravityNormalized = Vector3D.Cross(targetGravityVectorNormalized, totalGravityVect3Dnormalized);
    //Echo("\ncrossCurrentTargetGravityNormalized:\n" + crossCurrentTargetGravityNormalized);

    double elev;
    myRemoteControl.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);

    //Best we have is Runtime.LastRunTime and Runtime.TimeSinceLastRun
    double dts = Runtime.TimeSinceLastRun.TotalSeconds;
    Echo("dts:" + dts);
    double dts2 = Runtime.LastRunTimeMs;
    Echo("dts2:" + dts2);

    if (dts == 0)
    {
        return;
    }

    //everything necessary to know if it is underground (sign change will be used)
    //derivative of elev
    //known as alt_speed_ms_1
    //generating a vector from the current position to the center of the planet
    Vector3D VecPlanetCenter = new Vector3D(0, 0, 0);
    flightIndicatorsShipController.TryGetPlanetPosition(out VecPlanetCenter);
    Vector3D negateVecPlanetCenter = new Vector3D(0, 0, 0);
    Vector3D.Negate(ref VecPlanetCenter, out negateVecPlanetCenter);
    Vector3D vecToPlanetCenter = Vector3D.Add(myPos, negateVecPlanetCenter);
    double distToPlanetCenter = vecToPlanetCenter.Length();
    //derivative of distance to planet center
    derivateDistToPlanetCenter = (distToPlanetCenter - lastDistToPlanetCenter) / dts;
    lastDistToPlanetCenter = distToPlanetCenter;
    var isNegativeMeanUnderground = alt_speed_ms_1 * derivateDistToPlanetCenter;
    //Echo("isNegativeMeanUnderground:" + isNegativeMeanUnderground);


    //change the wantedAltitude BEFORE THIS LINE
    altitudeError = wantedAltitude - elev;

    MyShipVelocities myShipVel = myRemoteControl.GetShipVelocities();
    Vector3D linearSpeedsShip = myShipVel.LinearVelocity;
    Vector3D linearSpeedsShipNormalized = Vector3D.Normalize(linearSpeedsShip);


    Vector3D first3D = Vector3D.Cross(totalGravityVect3Dnormalized, shipForwardVector);
    //first3D is going to be Left or -Left
    //it will be perpendicular to the gravity vector
    Vector3D second3D = Vector3D.Normalize(Vector3D.Cross(totalGravityVect3Dnormalized, first3D));
    //second3D is going to be Forward or -Forward
    //it will be perpendicular to the gravity vector
    //Those vectors are used for the pitch and roll control to cancel speed
    Vector3D LeftPorMNormalized = first3D;
    Vector3D FowardPorMNormalized = second3D;


    //find angle from abs north to projected forward vector measured clockwise  
    //yawCWOrAntiCW = VectorHelper.VectorAngleBetween(forwardProjPlaneVector, relativeNorthVector) * rad2deg;

    //Comply with the library (and mainly from it)========================
    const double rad2deg = 180 / Math.PI;
    // thanks whip for those vectors
    Vector3D absoluteNorthPlanetWorldsVector = new Vector3D(0, -1, 0);
    Vector3D absoluteNorthNotPlanetWorldsVector = new Vector3D(0.342063708833718, -0.704407897782847, -0.621934025954579);

    //from gg
    Vector3D gravityVector = myRemoteControl.GetNaturalGravity();
    Vector3D planetRelativeLeftVector = shipForwardVector.Cross(gravityVector);

    Vector3D absoluteNorthVector;
    absoluteNorthVector = absoluteNorthPlanetWorldsVector;

    Vector3D relativeEastVector = gravityVector.Cross(absoluteNorthVector);
    Vector3D relativeNorthVector = relativeEastVector.Cross(gravityVector);


    //VTT: VectorToTarget
    Vector3D normVTT = Vector3D.Normalize(VectToTarget);
    Vector3D normVTTProjectUp = VectorHelper.VectorProjection(normVTT, gravityVector);
    Vector3D normVTTProjPlaneVector = normVTTProjectUp - normVTT;

    double yawCWOrAntiCW = VectorHelper.VectorAngleBetween(normVTTProjPlaneVector, relativeNorthVector) * rad2deg;
    if (normVTT.Dot(relativeEastVector) < 0)
    {
        yawCWOrAntiCW = 360.0d - yawCWOrAntiCW; //because of how the angle is measured                     
    }

    if (firstMainLoop == true)
    {
        flightIndicatorsFlightMode = FlightMode.STABILIZATION;
        fightStabilizator.Reset();
        // optional : set desired angles
        fightStabilizator.pitchDesiredAngle = 0f;
        fightStabilizator.yawDesiredAngle = 0f;
        fightStabilizator.rollDesiredAngle = 0f;
        lastRunTs = System.DateTime.UtcNow;
        firstMainLoop = false;
    }


    var posInterpolation = Vector3D.Add(myPos / 2, vec3Dtarget / 2);
    //Echo("posInterpolation:\n" + posInterpolation);


    if (argument != null && argument.ToLower().Equals("on"))
    {
        flightIndicatorsFlightMode = FlightMode.STABILIZATION;
        fightStabilizator.Reset();
        // optional : set desired angles
        fightStabilizator.pitchDesiredAngle = 10f;
        fightStabilizator.yawDesiredAngle = 0f;
        fightStabilizator.rollDesiredAngle = 0f;
        lastRunTs = System.DateTime.UtcNow;
    }
    //else if (argument != null && argument.ToLower().Equals("stabilize_off"))
    else if (argument != null && argument.ToLower().Equals("off"))
    {
        flightIndicatorsFlightMode = FlightMode.STANDY;
        fightStabilizator.Release();
    }


    flightIndicators.Compute();
    if (flightIndicatorsFlightMode == FlightMode.STABILIZATION)
    {
        //fightStabilizator.Stabilize(true, true, false);
        //just do one axis gyro axis maximum if stuck
    }

    lcdHelper.ClearMessageBuffer();
    lcdHelper.AppendMessageBuffer(flightIndicators.DisplayText());
    if (flightIndicatorsFlightMode == FlightMode.STABILIZATION)
    {
        lcdHelper.AppendMessageBuffer(fightStabilizator.DisplayText());
    }
    //lcdHelper.DisplayMessageBuffer(flightIndicatorsLcdDisplay);
    lcdHelper.DisplayMessageBuffer(flightIndicatorsSurfaceDisplay);


    var debugString = "";

    shipAcceleration = Vector3D.Add(Vector3D.Negate(linearSpeedsShip), prevLinearSpeedsShip) / dts;

    prevLinearSpeedsShip = linearSpeedsShip;

    //Echo("shipAcceleration:"+ shipAcceleration);

    //public double Control(double error, double timeStep)
    //todo change this
    //double dir = altRegulator.Control(altitudeError, dts);

    Echo("elev:" + elev);
    //PhysicalMass	Gets the physical mass of the ship, which accounts for inventory multiplier.
    var physMass_kg = myRemoteControl.CalculateShipMass().PhysicalMass;
    debugString += " " + "physMass_kg:" + physMass_kg;
    debugString += "\n" + "elev:" + elev;

    //figuring out the available thrust
    //IMyThrust.MaxEffectiveThrust
    //IMyThrust.CurrentThrust_N
    double maxEffectiveThrust_N = 0;
    double currentThrust_N = 0;
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        maxEffectiveThrust_N += c.MaxEffectiveThrust; currentThrust_N += c.CurrentThrust;
    }
    debugString += "\n" + "maxEffectiveThrust_N:" + maxEffectiveThrust_N;
    debugString += "\n" + "currentThrust_N:" + currentThrust_N;

    debugString += "\n" + "physMass_kg:" + physMass_kg;

    //double g = 9.8;
    double g = gravityVector.Length();

    double physMass_N = physMass_kg * g;
    debugString += "\n" + "physMass_N:" + physMass_N;


    //BaseMass Gets the base mass of the ship.
    //totalMass Gets the total mass of the ship, including cargo.
    //PhysicalMass Gets the physical mass of the ship, which accounts for inventory multiplier.


    var totalMass_kg = myRemoteControl.CalculateShipMass().TotalMass;
    debugString += "\n" + "totalMass_kg:" + totalMass_kg;

    var thr_to_weight_ratio = maxEffectiveThrust_N / physMass_N;
    debugString += "\n" + "thr_to_weight_ratio:" + thr_to_weight_ratio;

    double thrustLeft_N = currentThrust_N - physMass_N;
    debugString += "\n" + "thrustLeft_N:" + thrustLeft_N;

    double a_z_ms_2 = thrustLeft_N / physMass_kg;
    debugString += "\n" + "a_z_ms_2:" + a_z_ms_2;

    alt = elev;
    debugString += "\n" + "dts:" + dts;

    //errorDerivative = (error - lastError) / timeStep;
    alt_speed_ms_1 = (alt - last_alt) / dts;
    debugString += "\n" + "alt_speed_ms_1:" + alt_speed_ms_1;

    alt_acc_ms_2 = (alt_speed_ms_1 - last_alt_speed_ms_1) / dts;
    debugString += "\n" + "alt_acc_ms_2:" + alt_acc_ms_2;

    last_alt = alt;
    last_alt_speed_ms_1 = alt_speed_ms_1;

    var massOfShip = myRemoteControl.CalculateShipMass().PhysicalMass;
    debugString += "\n" + "massOfShip:" + massOfShip;

    double control = altRegulator.Control(altitudeError, dts);

    Echo("altitudeError:" + Math.Round((altitudeError), 3));
    Echo("alt_speed_ms_1:" + Math.Round((alt_speed_ms_1), 3));


    //double TWR = 4.59;
    double TWR = thr_to_weight_ratio;
    double V_max = 55;
    //double AngleRollMaxAcc = Math.Atan((TWR - 1) / 1) * 180 / Math.PI / 8;
    double AngleRollMaxAcc = Math.Atan((TWR - 1) / 1) * 180 / Math.PI / 2;
    //double AngleRollMaxAcc = Math.Atan((TWR - 1) / 1) * 180 / Math.PI / 4;

    //double MaxSurfaceAcc = (TWR - 1) * g;
    double MaxSurfaceAcc = (Math.Sin(Math.PI * AngleRollMaxAcc / 180)) * g;

    double brakingTime = V_max / MaxSurfaceAcc;
    double distWhenToStartBraking = brakingTime * V_max;

    //******Roll************start
    //double wantedSpeedRoll = Vector3D.Dot(shipLeftVector,shipVelocities);
    //TODO
    //double distRoll = Vector3D.Dot(shipLeftVector, distToTarget);

    Vector3D leftProjectUp = VectorHelper.VectorProjection(shipLeftVector, gravityVector);
    Vector3D leftProjPlaneVector = shipLeftVector - leftProjectUp;

    double leftProjPlaneVectorLength = leftProjPlaneVector.Length();

    //double distRoll = Vector3D.Dot(leftProjPlaneVector, VectToTarget);
    double distRoll = -Vector3D.Dot(Vector3D.Normalize(leftProjPlaneVector), VectToTarget);
    double clampedDistRoll = MyMath.Clamp(Convert.ToSingle(distRoll), Convert.ToSingle(-distWhenToStartBraking), Convert.ToSingle(distWhenToStartBraking));
    double wantedSpeedRoll = (V_max / distWhenToStartBraking) * clampedDistRoll;

    //double speedRoll = Vector3D.Dot(leftProjPlaneVector, linearSpeedsShip);
    double speedRoll = Vector3D.Dot(Vector3D.Normalize(leftProjPlaneVector), linearSpeedsShip);

    double speedRollError = wantedSpeedRoll - speedRoll;
    //if speedRollError is => 35.182m/s2 apply AngleRollMaxAcc
    //else todo
    // clamp(speedRollError , -MaxSurfaceAcc , MaxSurfaceAcc)
    // Atan2(speedRollError * 2,1)
    //0.01, 0, 2
    double angleRoll = 0;
	
    double tmpAngleRollPID = angleRollPID.Control(speedRollError, dts);
	angleRoll = tmpAngleRollPID;
    //angleRoll = tmpAngleRollPID;
    if (vec3Dtarget != new Vector3D(0, 0, 0))
    {
        if (Math.Abs(distRoll) < 8)
        {
            angleRoll = angleRoll + 0.2 * angleRoll * angleRoll * angleRoll;
        }
    }
    angleRoll = MyMath.Clamp(Convert.ToSingle(angleRoll), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));

    //******Roll*************end
    //******Pitch************Start

    Vector3D forwardProjectUp = VectorHelper.VectorProjection(shipForwardVector, gravityVector);
    Vector3D forwardProjPlaneVector = shipForwardVector - forwardProjectUp;


    double forwardProjPlaneVectorLength = forwardProjPlaneVector.Length();


    //double distRoll = Vector3D.Dot(leftProjPlaneVector, VectToTarget);
    double distPitch = -Vector3D.Dot(Vector3D.Normalize(forwardProjPlaneVector), VectToTarget);
    double clampedDistPitch = MyMath.Clamp(Convert.ToSingle(distPitch), Convert.ToSingle(-distWhenToStartBraking), Convert.ToSingle(distWhenToStartBraking));
    double wantedSpeedPitch = (V_max / distWhenToStartBraking) * clampedDistPitch;

    //double speedRoll = Vector3D.Dot(leftProjPlaneVector, linearSpeedsShip);
    double speedPitch = Vector3D.Dot(Vector3D.Normalize(forwardProjPlaneVector), linearSpeedsShip);

    ////to reach 100m/s
    //double currentSurfaceSpeedSquared = speedPitch * speedPitch + speedRoll * speedRoll;
    ////double multiplyingFactorForSpeeds = V_max * V_max / currentSurfaceSpeedSquared;
    //double multiplyingFactorForSpeeds = 100 * 100 / currentSurfaceSpeedSquared;
    //Echo("multiplyingFactorForSpeeds:" + multiplyingFactorForSpeeds);

    double speedPitchError = wantedSpeedPitch - speedPitch;
    //if speedRollError is => 35.182m/s2 apply AngleRollMaxAcc
    //else todo
    // clamp(speedRollError , -MaxSurfaceAcc , MaxSurfaceAcc)
    // Atan2(speedRollError * 2,1)
    //0.01, 0, 2
    double anglePitch = 0;
	
    double tmpAnglePitchPID = anglePitchPID.Control(speedPitchError, dts);
	anglePitch = tmpAnglePitchPID;
    //anglePitch = tmpAnglePitchPID;*
    if (vec3Dtarget != new Vector3D(0, 0, 0))
    {
        if (Math.Abs(distPitch) < 8)
        {
            anglePitch = anglePitch + 0.2 * anglePitch * anglePitch * anglePitch;
        }
    }

    anglePitch = MyMath.Clamp(Convert.ToSingle(anglePitch), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));

    //******Pitch************end
	if(Math.Abs(speedRoll)<1){
		if(Math.Abs(speedPitch)<1){
            anglePitch *= 10f;
            angleRoll *= 10f;
			anglePitch = MyMath.Clamp(Convert.ToSingle(anglePitch), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
			angleRoll = MyMath.Clamp(Convert.ToSingle(angleRoll), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
		}
	}
	if(Math.Abs(speedRoll)<.01){
		if(Math.Abs(speedPitch)<.01){
            anglePitch *= 10f;
            angleRoll *= 10f;
			anglePitch = MyMath.Clamp(Convert.ToSingle(anglePitch), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
			angleRoll = MyMath.Clamp(Convert.ToSingle(angleRoll), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
		}
	}


    double pitchFowardOrBackward = Vector3D.Dot(linearSpeedsShipNormalized, FowardPorMNormalized);
    double rollLeftOrRight = Vector3D.Dot(linearSpeedsShipNormalized, LeftPorMNormalized);

    pitchFowardOrBackward *= 0.01f;
    rollLeftOrRight *= 0.01f;

    speedPitchError = wantedSpeedPitch - pitchFowardOrBackward;
    speedRollError = wantedSpeedRoll - rollLeftOrRight;

    //anglePitch = speedPitchError;
    //angleRoll = speedRollError;
    //angleRoll = 0f;

    //Echo("elev:"+ elev);
    //Echo("distWhenToStartBraking:" + distWhenToStartBraking);
    //Echo("distPitch:" + distPitch);
    //Echo("anglePitch:" + anglePitch);
    //Echo("wantedSpeedPitch:" + wantedSpeedPitch);
    //Echo("AngleRollMaxAcc:" + AngleRollMaxAcc);
    //Echo("speedPitch:" + speedPitch);

    //Echo("wantedSpeedRoll:" + wantedSpeedRoll);
    //Echo("AngleRollMaxAcc:" + AngleRollMaxAcc);
    //Echo("speedRoll:" + speedRoll);
    Echo("anglePitch:" + Math.Round((anglePitch), 3));
    Echo("angleRoll:" + Math.Round((angleRoll), 3));

    Vector3D gravityNormalized = totalGravityVect3Dnormalized;

    double scaleForThetaRegardingGravity = Vector3D.Dot(gravityNormalized, shipDownVector);
    double thetaMustBe = 180 * Math.Acos(scaleForThetaRegardingGravity) / Math.PI;
    Echo("thetaMustBe:" + Math.Round((thetaMustBe), 3));
    Echo("wantedAltitude:" + Math.Round((wantedAltitude), 3));
    Echo("altitudeError:" + Math.Round((altitudeError), 3));

    //altitude management and downward speed management========================== start
    double wantedAlitudeSpeed = 0;
    double altitudeSpeedError = 0;
    double controlAltSpeed = 0;

    double V_max_altSpeed = 110;
    double V_min_altSpeed = -100;
    //wantedAltitude 
    //change the wantedAltitude BEFORE THIS LINE
    altitudeError = wantedAltitude - elev;

    //double h_max_alt = (V_max_altSpeed * V_max_altSpeed) / (2 * gravityVector.Length());
    //compute the spare thrust to change the change of altitude speed, cap it at 1 to avoid "spectacular landing" aka ship crash if lag occurs
    //it helps avoid hitting the ground quick while the ship is loaded with a 1 < thr_to_weight_ratio < 2
    double spareThrustToWeightRatio = MyMath.Clamp(Convert.ToSingle(thr_to_weight_ratio - 1), Convert.ToSingle(0), Convert.ToSingle(1));
    double h_max_alt = (V_max_altSpeed * V_max_altSpeed) / (2 * g * spareThrustToWeightRatio);
    Echo("h_max_alt:" + Math.Round((h_max_alt), 3));

    double clampAltError = MyMath.Clamp(Convert.ToSingle(altitudeError), Convert.ToSingle(-h_max_alt), Convert.ToSingle(h_max_alt));
    Echo("clampAltError:" + Math.Round((clampAltError), 3));

    //TODO: make it asymetric
    wantedAlitudeSpeed = (clampAltError / h_max_alt) * V_max_altSpeed;
    Echo("wantedAlitudeSpeed:" + Math.Round((wantedAlitudeSpeed), 3));

    double clampWantedAlitudeSpeed = MyMath.Clamp(Convert.ToSingle(wantedAlitudeSpeed), Convert.ToSingle(V_min_altSpeed), Convert.ToSingle(V_max_altSpeed));


    //wantedAlitudeSpeed = -10;
    //if (elev < 50)
    //{
    //    wantedAlitudeSpeed = -1;
    //}
    //alt_speed_ms_1 is referenced to the actual ground elevation not the GPS marker elevation
    altitudeSpeedError = (clampWantedAlitudeSpeed - alt_speed_ms_1);
    Echo("altitudeSpeedError:" + Math.Round((altitudeSpeedError), 3));

    controlAltSpeed = downwardSpeedAltRegulator.Control(altitudeSpeedError, dts);
    Echo("controlAltSpeed:" + Math.Round((controlAltSpeed), 3));

    Echo("thr_to_weight_ratio:" + Math.Round((thr_to_weight_ratio), 3));

    //feedback loop to counter the wrong speed
    control = controlAltSpeed;

    //double surfaceSpeedSquared = wantedSpeedPitch * wantedSpeedPitch + wantedSpeedRoll * wantedSpeedRoll;
    //double descSurfaceSpeed = 10;
    //Echo("surfaceSpeedSquared:" + surfaceSpeedSquared);


    if (distPitch * distPitch + distRoll * distRoll > 100 * 100)
    {
        wantedAltitude = 1500;
    }

    Echo("dts:" + dts);
    if (Math.Abs(distPitch) < 1)
    {
        if (Math.Abs(distRoll) < 1)
        {
            if (dts > 0)
            {
                //if (surfaceSpeedSquared < descSurfaceSpeed * descSurfaceSpeed)
                //{
                wantedAltitude = 125;

                if (elev < 140)
                {
                    clampWantedAlitudeSpeed = -5;
                }

                //wantedAlitudeSpeed = -10;
                //if (elev < 50)
                //{
                //    wantedAlitudeSpeed = -1;
                //}
                //alt_speed_ms_1 is referenced to the actual ground elevation not the GPS marker elevation
                altitudeSpeedError = (clampWantedAlitudeSpeed - alt_speed_ms_1);
                Echo("altitudeSpeedError:" + Math.Round((altitudeSpeedError), 3));

                controlAltSpeed = downwardSpeedAltRegulator.Control(altitudeSpeedError, dts);
                Echo("controlAltSpeed:" + Math.Round((controlAltSpeed), 3));

                //feedback loop to counter the wrong speed
                control = controlAltSpeed;
                //}
            }
        }
    }
	
	if(dts > 0 ) {
		if (vec3Dtarget != new Vector3D(0, 0, 0)){
			
			//fallingRange(gravityVector, linearSpeedsShip, dts, myPos);
			fallingRange(gravityVector, linearSpeedsShip, dts, myPos, vec3Dtarget, VecPlanetCenter  );
		}
	}
	
	
    //altitude management and downward speed management========================== end



    if (Double.IsNaN(controlAltSpeed) == true)
    {
        downwardSpeedAltRegulator.Reset();
    }

    if (Double.IsNaN(control) == true)
    {
        altRegulator.Reset();
    }


    bool stalizablePitch = true;
    bool stalizableRoll = true;
    bool stalizableYaw = false;

    //+ pitch go foward
    fightStabilizator.pitchDesiredAngle = Convert.ToSingle(-anglePitch);
    fightStabilizator.rollDesiredAngle = Convert.ToSingle(angleRoll);
    fightStabilizator.yawDesiredAngle = Convert.ToSingle(0f);

    // call this next line at each run
    fightStabilizator.Stabilize(stalizableRoll, stalizablePitch, stalizableYaw);


    //debug roll
    var str_to_display = "\n1|" + Math.Round((distPitch), 0) + "|1|" + Math.Round((distRoll), 0)
        + "\n2|" + Math.Round((clampedDistPitch), 0) + "|2|" + Math.Round((clampedDistRoll), 0)
        + "\n3|" + Math.Round((wantedSpeedPitch), 0) + "|3|" + Math.Round((wantedSpeedRoll), 0)
        + "\n4|" + Math.Round((speedPitchError), 0) + "|4|" + Math.Round((speedRollError), 0)
        + "\n5|" + Math.Round((anglePitch), 2) + "|5|" + Math.Round((angleRoll), 2)
        + "\n6|" + Math.Round((forwardProjectUp.Length()), 2) + "|6|" + Math.Round((leftProjectUp.Length()), 2)
        + "\n7|" + Math.Round((forwardProjPlaneVectorLength), 2) + "|7|" + Math.Round((leftProjPlaneVectorLength), 2);
    //str_to_display = "\n8|elev|" + Math.Round((elev), 0)
    //    + "\n9|elevD|" + Math.Round((alt_speed_ms_1), 0)
    //    + "\n10|" + Math.Round((0.0f), 0)
    //    + "\n11|wAS|" + Math.Round((wantedAlitudeSpeed), 0)
    //    + "\n12|wA|" + Math.Round((wantedAltitude), 0)
    //    + "\n13|con|" + Math.Round((control), 0);
    //var str_to_display = "lol";
	//Echo("myRemoteControl.CubeGrid.CustomName:"+myRemoteControl.CubeGrid.CustomName);
    // if(myRemoteControl.CubeGrid.CustomName.Contains("\n|") == true){
			// myRemoteControl.CubeGrid.CustomName = "stv ship controlled";
	// }
	if(theAntenna != null){
		theAntenna.HudText = str_to_display;
	}

    debugString += "\n" + "control:" + control;
    Echo("control:" + Math.Round((control), 2));

    //applying what the pid processed
    //var cs = new List<IMyThrust>();
    //GridTerminalSystem.GetBlocksOfType(cs);
    //Echo(cs.ToString());

    double remainingThrustToApply = -1;
    double temp_thr_n = -1;

    foreach (var c in cs)
    {
        if (c.IsFunctional == true)
        {
            if (c.IsSameConstructAs(flightIndicatorsShipController))
            {
                if (remainingThrustToApply == -1)
                {
                    remainingThrustToApply = (1f * physMass_N * c.MaxThrust / c.MaxEffectiveThrust + (physMass_N * control * 1));
                }
                //Echo("physMass_N" + physMass_N);
                //Echo("c.MaxThrust"+c.MaxThrust);
                //Echo("c.MaxEffectiveThrust"+c.MaxEffectiveThrust);
                //(1f * physMass_N * c.MaxThrust / c.MaxEffectiveThrust + (physMass_N * control))
                if (c.MaxThrust < remainingThrustToApply)
                {
                    temp_thr_n = c.MaxThrust;
                    remainingThrustToApply = remainingThrustToApply - c.MaxThrust;
                }
                else
                {
                    temp_thr_n = remainingThrustToApply;
                    remainingThrustToApply = 0;
                }
                //Echo("temp_thr_n:" + temp_thr_n);
                //Echo("remainingThrustToApply:" + remainingThrustToApply);
                if (temp_thr_n < 0)
                {
                    c.ThrustOverride = Convert.ToSingle(200f);
                }
                else
                {
                    c.ThrustOverride = Convert.ToSingle(temp_thr_n);
                }
				
				if(remainingThrustToApply==0){
					c.ThrustOverridePercentage = 0.00001f;
				}
            }
        }
    }
	
	Echo("distRoll:"+Math.Round(distRoll,2));
	Echo("distPitch:"+Math.Round(distPitch,2));

}

public class FlightIndicators
{
    IMyShipController shipController;
    readonly Action<string> Echo;
    readonly List<IMyTextPanel> lcdDisplays = null;
    readonly List<IMyTextSurface> surfaceDisplays = null;
    private LCDHelper lcdHelper;
    Vector3D absoluteNorthVector;

    public double CurrentSpeed { get; private set; } = 0;
    public double Pitch { get; private set; } = 0;
    public double Roll { get; private set; } = 0;
    public double Yaw { get; private set; } = 0;
    public double Elevation { get; private set; } = 0;

    const double rad2deg = 180 / Math.PI;
    // thanks whip for those vectors
    static Vector3D absoluteNorthPlanetWorldsVector = new Vector3D(0, -1, 0);
    static Vector3D absoluteNorthNotPlanetWorldsVector = new Vector3D(0.342063708833718, -0.704407897782847, -0.621934025954579);

    public FlightIndicators(IMyShipController shipController, Action<String> Echo, bool isPlanetWorld = true, List<IMyTextPanel> lcdDisplays = null, LCDHelper lcdHelper = null)
    {
        this.shipController = shipController;
        this.Echo = Echo;
        this.lcdDisplays = lcdDisplays;
        this.lcdHelper = lcdHelper;

        if (isPlanetWorld)
        {
            absoluteNorthVector = absoluteNorthPlanetWorldsVector;
        }
        else
        {
            absoluteNorthVector = absoluteNorthNotPlanetWorldsVector;
        }


    }
	
	
    public FlightIndicators(IMyShipController shipController, Action<String> Echo, bool isPlanetWorld = true, List<IMyTextSurface> surfaceDisplays = null, LCDHelper lcdHelper = null)
    {
        this.shipController = shipController;
        this.Echo = Echo;
		this.surfaceDisplays = surfaceDisplays;
        this.lcdHelper = lcdHelper;

        if (isPlanetWorld)
        {
            absoluteNorthVector = absoluteNorthPlanetWorldsVector;
        }
        else
        {
            absoluteNorthVector = absoluteNorthNotPlanetWorldsVector;
        }


    }

    public void Compute()
    {
        // speed
        var velocityVector = shipController.GetShipVelocities().LinearVelocity;
        //CurrentSpeed = velocityVec.Length(); //raw speed of ship 
        CurrentSpeed = shipController.GetShipSpeed();

        // roll pitch yaw
        Vector3D shipForwardVector = shipController.WorldMatrix.Forward;
        Vector3D shipLeftVector = shipController.WorldMatrix.Left;
        Vector3D shipDownVector = shipController.WorldMatrix.Down;
        Vector3D gravityVector = shipController.GetNaturalGravity();
        Vector3D planetRelativeLeftVector = shipForwardVector.Cross(gravityVector);

        if (gravityVector.LengthSquared() == 0)
        {
            Echo("No natural gravity field detected");
            Pitch = 0;
            Roll = 0;
            Yaw = 0;
            Elevation = 0;
            return;
        }
        // Roll
        Roll = VectorHelper.VectorAngleBetween(shipLeftVector, planetRelativeLeftVector) * rad2deg * Math.Sign(shipLeftVector.Dot(gravityVector));
        /*if (Roll > 90 || Roll < -90)
        {
            Roll = 180 - Roll;
        }*/
        // Pitch
        Pitch = VectorHelper.VectorAngleBetween(shipForwardVector, gravityVector) * rad2deg; //angle from nose direction to gravity 
        Pitch -= 90; // value computed is 90 degrees if pitch = 0
                     // Yaw
        Vector3D relativeEastVector = gravityVector.Cross(absoluteNorthVector);
        Vector3D relativeNorthVector = relativeEastVector.Cross(gravityVector);
        Vector3D forwardProjectUp = VectorHelper.VectorProjection(shipForwardVector, gravityVector);
        Vector3D forwardProjPlaneVector = shipForwardVector - forwardProjectUp;

        //find angle from abs north to projected forward vector measured clockwise  
        Yaw = VectorHelper.VectorAngleBetween(forwardProjPlaneVector, relativeNorthVector) * rad2deg;
        if (shipForwardVector.Dot(relativeEastVector) < 0)
        {
            Yaw = 360.0d - Yaw; //because of how the angle is measured                                                                          
        }

        double tempElevation = 0;
        if (!shipController.TryGetPlanetElevation(MyPlanetElevation.Surface, out tempElevation))
        {
            Elevation = -1; //error, no gravity field is detected earlier, so it's another kind of problem
        }
        else
        {
            Elevation = tempElevation;
        }

    }

    public string DisplayText()
    {
        StringBuilder stringBuilder = new StringBuilder();

        BasicLibrary.AppendFormattedNewLine(stringBuilder, "Speed     {0} m/s", Math.Round(CurrentSpeed, 2));
        BasicLibrary.AppendFormattedNewLine(stringBuilder, "Pitch       {0}°", Math.Round(Pitch, 2));
        BasicLibrary.AppendFormattedNewLine(stringBuilder, "Roll         {0}°", Math.Round(Roll, 2));
        BasicLibrary.AppendFormattedNewLine(stringBuilder, "Yaw        {0}°", Math.Round(Yaw, 2));
        BasicLibrary.AppendFormattedNewLine(stringBuilder, "Elevation {0} m", Math.Round(Elevation, 0));

        //Echo("|Pitch " + Math.Round(Pitch, 2) + "\n|Roll " + Math.Round(Roll, 2) + "\n|Yaw " + Math.Round(Yaw, 2));

        return stringBuilder.ToString();
    }

    public void Display()
    {
        // if (lcdHelper == null || lcdDisplays == null)
        // {
            // Echo("Can't diplay, LCD or LCDHelper not set");
            // return;
        // }

        //lcdHelper.DisplayMessage(DisplayText(), lcdDisplays);
		lcdHelper.DisplayMessage(DisplayText(),surfaceDisplays);
    }

}


//distance made before touching the ground without thrust
//public float fallingRange(Vector3D gravity , Vector3D shipSpeed , 
//double timeStep, Vector3D shipPosition){
public float fallingRange(Vector3D gravity , Vector3D shipSpeed , 
double timeStep, Vector3D shipPosition, Vector3D target, Vector3D centerPlanet){
	//resultHorizontal
	float resultHorizontal = 0;
	float resultVertical = 0;
	
	float fallingRange = 0;
	
	Vector3D resultShipPosition = shipPosition;
	
	//parameter
	float maxSpeed = 100.0f;
	
	float secondToLookInFutur = 100.0f;
	
	//int maximumInt = 100;
	int maximumInt = (int) (secondToLookInFutur / (float) timeStep);
	
	float maximumFallingAltitude = 1500;
	
	//local variables
	float thesholdToStopAt = 1f;
	
	float lastStepDoneHor = 10.0f;
	float lastStepDoneVer = 10.0f;
	
	Vector3D tmpShipSeed = shipSpeed;
	
	Vector3D tmpLocalTarget = target - centerPlanet;
	Vector3D tmpLocalPOI = new Vector3D(0,0,0);
	
	Echo("timeStep:" + timeStep);
	Echo("maximumInt:" + maximumInt);
	
	
	//trying to look in the futur where it would land
	while(true){
		
		if(lastStepDoneHor < thesholdToStopAt){break;}
		
		tmpShipSeed += timeStep * gravity;
		
		if(tmpShipSeed.LengthSquared() > 10000 ){
			tmpShipSeed = maxSpeed * Vector3D.Normalize(tmpShipSeed);
		}
		
		resultShipPosition += tmpShipSeed * timeStep;
		
		
		Vector3D VectorProjFuturHor = tmpShipSeed - VectorHelper.VectorProjection(tmpShipSeed, gravity);
		Vector3D VectorProjFuturVer = VectorHelper.VectorProjection(tmpShipSeed, gravity);
	
		
		//last speed made on the plane normal to gravity (on the ground)
		lastStepDoneHor = (float) VectorProjFuturHor.Length();
		lastStepDoneVer = (float) VectorProjFuturVer.Length();
		
		//adding delta distance made during the last timestep
		resultHorizontal += (float) lastStepDoneHor * (float) timeStep;
		
		resultVertical += (float) lastStepDoneVer * (float) timeStep ;
		
		if(maximumInt % 100 == 0 ) {
			Echo("lastStepDoneHorInter:" + Math.Round(lastStepDoneHor,3));
			Echo("resultHorInter:" + Math.Round(resultHorizontal,3));
			Echo("resultVerInter:" + Math.Round(resultVertical,3));
		}
		
		if(maximumFallingAltitude<resultVertical){
			break;
		}
		
		
		//fail safe
		maximumInt -= 1 ;
		if(maximumInt<0){
			break;
		}
		
		tmpLocalPOI = resultShipPosition - centerPlanet	;
		
		Vector3D tmpDistancePOI_target = 
		tmpLocalTarget.Length()*Vector3D.Normalize(tmpLocalPOI) - 
		tmpLocalTarget;
		
		float dist_POI_target = (float)tmpDistancePOI_target.Length();
		
		fallingRange = dist_POI_target;
		
		Echo("dist_POI_target:" + dist_POI_target );
		
		if(dist_POI_target<20.0f){break;}
		
		if(lastStepDoneHor < thesholdToStopAt){break;}
	}
	
	Echo("maximumInt:" + maximumInt );
	Echo("resultHor:"+ resultHorizontal);
	Echo("resultVer:"+ resultVertical);
	Echo("resultShipPosition:"+ Vector3D.Round(resultShipPosition,3));
	
	//GPS:OuiOuiOui #2:1076588.43:114319.88:1670351.32:#FF82F175:
	
	string debugOK = "GPS:debugOK:" ;
	debugOK += Math.Round(resultShipPosition.X,0) + ":";
	debugOK += Math.Round(resultShipPosition.Y,0) + ":";
	debugOK += Math.Round(resultShipPosition.Z,0) + ":";
	
	Echo(debugOK);
	
	Me.CustomData = debugOK;
	Debug.RemoveAll();
	
	float cellSize = Me.CubeGrid.GridSize;
	MatrixD pbm = Me.WorldMatrix;
	//Debug.DrawGPS("I'm here!", pbm.Translation + pbm.Backward * (cellSize / 2), Color.Blue);
	
	Debug.DrawGPS("ship", shipPosition + pbm.Backward * (cellSize / 2), Color.Blue);
	Debug.DrawGPS("POI!", resultShipPosition + pbm.Backward * (cellSize / 2), Color.Red);
	
	return fallingRange;
	
}


/// <summary>
/// Create an instance of this and hold its reference.
/// </summary>
public class DebugAPI
{
	public readonly bool ModDetected;

	/// <summary>
	/// Changing this will affect OnTop draw for all future draws that don't have it specified.
	/// </summary>
	public bool DefaultOnTop;

	/// <summary>
	/// Recommended to be used at start of Main(), unless you wish to draw things persistently and remove them manually.
	/// <para>Removes everything except AdjustNumber and chat messages.</para>
	/// </summary>
	public void RemoveDraw() => _removeDraw?.Invoke(_pb);
	Action<IMyProgrammableBlock> _removeDraw;

	/// <summary>
	/// Removes everything that was added by this API (except chat messages), including DeclareAdjustNumber()!
	/// <para>For calling in Main() you should use <see cref="RemoveDraw"/> instead.</para>
	/// </summary>
	public void RemoveAll() => _removeAll?.Invoke(_pb);
	Action<IMyProgrammableBlock> _removeAll;

	/// <summary>
	/// You can store the integer returned by other methods then remove it with this when you wish.
	/// <para>Or you can not use this at all and call <see cref="RemoveDraw"/> on every Main() so that your drawn things live a single PB run.</para>
	/// </summary>
	public void Remove(int id) => _remove?.Invoke(_pb, id);
	Action<IMyProgrammableBlock, int> _remove;

	public int DrawPoint(Vector3D origin, Color color, float radius = 0.2f, float seconds = DefaultSeconds, bool? onTop = null) => _point?.Invoke(_pb, origin, color, radius, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, Vector3D, Color, float, float, bool, int> _point;

	public int DrawLine(Vector3D start, Vector3D end, Color color, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _line?.Invoke(_pb, start, end, color, thickness, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, Vector3D, Vector3D, Color, float, float, bool, int> _line;

	public int DrawAABB(BoundingBoxD bb, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _aabb?.Invoke(_pb, bb, color, (int)style, thickness, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, BoundingBoxD, Color, int, float, float, bool, int> _aabb;

	public int DrawOBB(MyOrientedBoundingBoxD obb, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _obb?.Invoke(_pb, obb, color, (int)style, thickness, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, MyOrientedBoundingBoxD, Color, int, float, float, bool, int> _obb;

	public int DrawSphere(BoundingSphereD sphere, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, int lineEveryDegrees = 15, float seconds = DefaultSeconds, bool? onTop = null) => _sphere?.Invoke(_pb, sphere, color, (int)style, thickness, lineEveryDegrees, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, BoundingSphereD, Color, int, float, int, float, bool, int> _sphere;

	public int DrawMatrix(MatrixD matrix, float length = 1f, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _matrix?.Invoke(_pb, matrix, length, thickness, seconds, onTop ?? DefaultOnTop) ?? -1;
	Func<IMyProgrammableBlock, MatrixD, float, float, float, bool, int> _matrix;

	/// <summary>
	/// Adds a HUD marker for a world position.
	/// <para>White is used if <paramref name="color"/> is null.</para>
	/// </summary>
	public int DrawGPS(string name, Vector3D origin, Color? color = null, float seconds = DefaultSeconds) => _gps?.Invoke(_pb, name, origin, color, seconds) ?? -1;
	Func<IMyProgrammableBlock, string, Vector3D, Color?, float, int> _gps;

	/// <summary>
	/// Adds a notification center on screen. Do not give 0 or lower <paramref name="seconds"/>.
	/// </summary>
	public int PrintHUD(string message, Font font = Font.Debug, float seconds = 2) => _printHUD?.Invoke(_pb, message, font.ToString(), seconds) ?? -1;
	Func<IMyProgrammableBlock, string, string, float, int> _printHUD;

	/// <summary>
	/// Shows a message in chat as if sent by the PB (or whoever you want the sender to be)
	/// <para>If <paramref name="sender"/> is null, the PB's CustomName is used.</para>
	/// <para>The <paramref name="font"/> affects the fontface and color of the entire message, while <paramref name="senderColor"/> only affects the sender name's color.</para>
	/// </summary>
	public void PrintChat(string message, string sender = null, Color? senderColor = null, Font font = Font.Debug) => _chat?.Invoke(_pb, message, sender, senderColor, font.ToString());
	Action<IMyProgrammableBlock, string, string, Color?, string> _chat;

	/// <summary>
	/// Used for realtime adjustments, allows you to hold the specified key/button with mouse scroll in order to adjust the <paramref name="initial"/> number by <paramref name="step"/> amount.
	/// <para>Add this once at start then store the returned id, then use that id with <see cref="GetAdjustNumber(int)"/>.</para>
	/// </summary>
	public void DeclareAdjustNumber(out int id, double initial, double step = 0.05, Input modifier = Input.Control, string label = null) => id = _adjustNumber?.Invoke(_pb, initial, step, modifier.ToString(), label) ?? -1;
	Func<IMyProgrammableBlock, double, double, string, string, int> _adjustNumber;

	/// <summary>
	/// See description for: <see cref="DeclareAdjustNumber(double, double, Input, string)"/>.
	/// <para>The <paramref name="noModDefault"/> is returned when the mod is not present.</para>
	/// </summary>
	public double GetAdjustNumber(int id, double noModDefault = 1) => _getAdjustNumber?.Invoke(_pb, id) ?? noModDefault;
	Func<IMyProgrammableBlock, int, double> _getAdjustNumber;

	/// <summary>
	/// Gets simulation tick since this session started. Returns -1 if mod is not present.
	/// </summary>
	public int GetTick() => _tick?.Invoke() ?? -1;
	Func<int> _tick;

	/// <summary>
	/// Gets time from Stopwatch which is accurate to nanoseconds, can be used to measure code execution time.
	/// Returns TimeSpan.Zero if mod is not present.
	/// </summary>
	public TimeSpan GetTimestamp() => _timestamp?.Invoke() ?? TimeSpan.Zero;
	Func<TimeSpan> _timestamp;

	/// <summary>
	/// Use with a using() statement to measure a chunk of code and get the time difference in a callback.
	/// <code>
	/// using(Debug.Measure((t) => Echo($"diff={t}")))
	/// {
	///    // code to measure
	/// }
	/// </code>
	/// This simply calls <see cref="GetTimestamp"/> before and after the inside code.
	/// </summary>
	public MeasureToken Measure(Action<TimeSpan> call) => new MeasureToken(this, call);

	/// <summary>
	/// <see cref="Measure(Action{TimeSpan})"/>
	/// </summary>
	public MeasureToken Measure(string prefix) => new MeasureToken(this, (t) => PrintHUD($"{prefix} {t.TotalMilliseconds} ms"));

	public struct MeasureToken : IDisposable
	{
		DebugAPI API;
		TimeSpan Start;
		Action<TimeSpan> Callback;

		public MeasureToken(DebugAPI api, Action<TimeSpan> call)
		{
			API = api;
			Callback = call;
			Start = API.GetTimestamp();
		}

		public void Dispose()
		{
			Callback?.Invoke(API.GetTimestamp() - Start);
		}
	}

	public enum Style { Solid, Wireframe, SolidAndWireframe }
	public enum Input { MouseLeftButton, MouseRightButton, MouseMiddleButton, MouseExtraButton1, MouseExtraButton2, LeftShift, RightShift, LeftControl, RightControl, LeftAlt, RightAlt, Tab, Shift, Control, Alt, Space, PageUp, PageDown, End, Home, Insert, Delete, Left, Up, Right, Down, D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NumPad0, NumPad1, NumPad2, NumPad3, NumPad4, NumPad5, NumPad6, NumPad7, NumPad8, NumPad9, Multiply, Add, Separator, Subtract, Decimal, Divide, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12 }
	public enum Font { Debug, White, Red, Green, Blue, DarkBlue }

	const float DefaultThickness = 0.02f;
	const float DefaultSeconds = -1;

	IMyProgrammableBlock _pb;

	/// <summary>
	/// NOTE: if mod is not present then methods will simply not do anything, therefore you can leave the methods in your released code.
	/// </summary>
	/// <param name="program">pass `this`.</param>
	/// <param name="drawOnTopDefault">set the default for onTop on all objects that have such an option.</param>
	public DebugAPI(MyGridProgram program, bool drawOnTopDefault = false)
	{
		if(program == null) throw new Exception("Pass `this` into the API, not null.");

		DefaultOnTop = drawOnTopDefault;
		_pb = program.Me;

		var methods = _pb.GetProperty("DebugAPI")?.As<IReadOnlyDictionary<string, Delegate>>()?.GetValue(_pb);
		if(methods != null)
		{
			Assign(out _removeAll, methods["RemoveAll"]);
			Assign(out _removeDraw, methods["RemoveDraw"]);
			Assign(out _remove, methods["Remove"]);
			Assign(out _point, methods["Point"]);
			Assign(out _line, methods["Line"]);
			Assign(out _aabb, methods["AABB"]);
			Assign(out _obb, methods["OBB"]);
			Assign(out _sphere, methods["Sphere"]);
			Assign(out _matrix, methods["Matrix"]);
			Assign(out _gps, methods["GPS"]);
			Assign(out _printHUD, methods["HUDNotification"]);
			Assign(out _chat, methods["Chat"]);
			Assign(out _adjustNumber, methods["DeclareAdjustNumber"]);
			Assign(out _getAdjustNumber, methods["GetAdjustNumber"]);
			Assign(out _tick, methods["Tick"]);
			Assign(out _timestamp, methods["Timestamp"]);

			RemoveAll(); // cleanup from past compilations on this same PB

			ModDetected = true;
		}
	}

	void Assign<T>(out T field, object method) => field = (T)method;
}

public class FightStabilizator
{
    private FlightIndicators flightIndicators;
    //public FlightIndicators flightIndicators;
    public Action<string> Echo;
    BasicLibrary basicLibrary;
    IMyShipController shipController;
    PIDController pitchPid;
    PIDController rollPid;
    PIDController yawPid;

    public double gyroscopeOverridedRoll { get; private set; } = 0;
    public double gyroscopeOverridedPitch { get; private set; } = 0;
    public double gyroscopeOverridedYaw { get; private set; } = 0;

    bool firstRun = true;
    double lastTime = 0;

    float gyroscopeMaximumGyroscopePower = 1.0f;
    float gyroscopeMaximumErrorMargin = 0.001f;

    public float pitchDesiredAngle = 0;
    public float yawDesiredAngle = 0;
    public float rollDesiredAngle = 0;

    string WarningMessage = null;
    List<IMyGyro> gyroscopes = new List<IMyGyro>();

    public FightStabilizator(FlightIndicators flightIndicators, IMyShipController shipController, double pidP, double pidI, double pidD, BasicLibrary basicLibrary)
    {
        this.flightIndicators = flightIndicators;
        this.Echo = basicLibrary.Echo;
        this.basicLibrary = basicLibrary;
        this.shipController = shipController;

        pitchPid = new PIDController(pidP, pidI, pidD);
        rollPid = new PIDController(pidP, pidI, pidD);
        yawPid = new PIDController(pidP, pidI, pidD);
    }

    public void Reset()
    {
        firstRun = true;
        FindAndInitGyroscopesOverdrive();

        pitchPid.Reset();
        rollPid.Reset();
        yawPid.Reset();
    }

    public void Release()
    {
        ReleaseGyroscopes();
    }

    public void Stabilize()
    {
        Stabilize(true, true, false);
    }

    public void Stabilize(bool stabilizeRoll, bool stabilizePitch, bool stabilizeYaw)
    {
        if (gyroscopes.Count == 0)
        {
            WarningMessage = "Warning no gyro found.\nCan't stabilize ship.";
            Echo(WarningMessage);
            return;
        }

        float maxGyroValue = gyroscopes[0].GetMaximum<float>("Yaw") * gyroscopeMaximumGyroscopePower;

        // center yaw at origin
        double originCenteredYaw = flightIndicators.Yaw;
        if (originCenteredYaw > 180)
        {
            originCenteredYaw -= 360;
        }

        double currentTime = BasicLibrary.GetCurrentTimeInMs();
        double timeStep = currentTime - lastTime;

        // sometimes time difference is 0 (because system is caching getTime calls), skip computing for this time
        if (timeStep == 0)
        {
            return;
        }

        if (!firstRun)
        {
            double pitchCommand = (stabilizePitch) ? ComputeCommand(flightIndicators.Pitch - pitchDesiredAngle, pitchPid, timeStep, maxGyroValue) : 0;
            double yawCommand = (stabilizeYaw) ? ComputeCommand(originCenteredYaw - yawDesiredAngle, yawPid, timeStep, maxGyroValue) : 0;
            double rollCommand = (stabilizeRoll) ? ComputeCommand(flightIndicators.Roll - rollDesiredAngle, rollPid, timeStep, maxGyroValue) : 0;
            // + rollCommand because of the way we compute it
            ApplyGyroOverride(-pitchCommand, -yawCommand, rollCommand, gyroscopes, shipController);
        }
        else
        {
            firstRun = false;
        }

        // compute overriden gyro values into controller coordonates
        Vector3D overrideData = new Vector3D(-gyroscopes[0].Pitch, gyroscopes[0].Yaw, gyroscopes[0].Roll);
        MatrixD gyroscopeWorldMatrix = gyroscopes[0].WorldMatrix;
        MatrixD controllerWorldMatrix = shipController.WorldMatrix;
        Vector3D overrideDataInControllerView = Vector3D.TransformNormal(Vector3D.TransformNormal(overrideData, gyroscopeWorldMatrix), Matrix.Transpose(controllerWorldMatrix));

        gyroscopeOverridedPitch = overrideDataInControllerView.X;
        gyroscopeOverridedYaw = overrideDataInControllerView.Y;
        gyroscopeOverridedRoll = -overrideDataInControllerView.Z; // negative because of the way we compute roll           


        lastTime = BasicLibrary.GetCurrentTimeInMs();
    }

    double ComputeCommand(double error, PIDController pid, double timeStep, double maxGyroValue)
    {
        if (Math.Abs(error) > gyroscopeMaximumErrorMargin)
        {
            double command = pid.Control(error, timeStep / 1000);
            command = MathHelper.Clamp(command, -maxGyroValue, maxGyroValue);
            return command;
        }
        else
        {
            return 0.0d;
        }

    }

    public string DisplayText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        BasicLibrary.AppendFormattedNewLine(stringBuilder, WarningMessage);
        if (gyroscopes.Count > 0)
        {
            BasicLibrary.AppendFormattedNewLine(stringBuilder, "Auto-correcting roll and pitch");
            BasicLibrary.AppendFormattedNewLine(stringBuilder, "Pitch overdrive {0}", Math.Round(gyroscopeOverridedPitch, 4));
            BasicLibrary.AppendFormattedNewLine(stringBuilder, "Roll overdrive  {0}", Math.Round(gyroscopeOverridedRoll, 4));
            BasicLibrary.AppendFormattedNewLine(stringBuilder, "Yaw overdrive   {0}", Math.Round(gyroscopeOverridedYaw, 4));
        }
        return stringBuilder.ToString();
    }

    void SetGyroscopesYawOverride(float overrride)
    {
        foreach (IMyGyro gyroscope in gyroscopes)
        {
            gyroscope.Yaw = overrride;
        }
    }

    void FindAndInitGyroscopesOverdrive()
    {
        if (gyroscopes.Count == 0)
        {
            basicLibrary.GetBlocksOfType(gyroscopes);

            //using just one gyroscope on the same grid as the shipController
            IMyGyro tmpGyroscope = null;
            foreach (var gyro in gyroscopes)
            {
                if (gyro.IsSameConstructAs(shipController))
                {
                    tmpGyroscope = gyro;
                }
            }
            gyroscopes = new List<IMyGyro>();
            if (tmpGyroscope != null)
            {
                gyroscopes.Add(tmpGyroscope);
            }
            if (gyroscopes.Count == 0)
            {
                WarningMessage = "Warning no gyro found.";
                Echo(WarningMessage);
                return;
            }
            InitGyroscopesOverride();
        }
    }

    void InitGyroscopesOverride()
    {
        foreach (IMyGyro gyroscope in gyroscopes)
        {
            gyroscope.GyroPower = 1.0f; // set power to 100%
            gyroscope.GyroOverride = true;
            gyroscope.Pitch = 0;
            gyroscope.Roll = 0;
            gyroscope.Yaw = 0;
            gyroscope.ApplyAction("OnOff_On");
        }
    }

    void ReleaseGyroscopes()
    {
        foreach (IMyGyro gyroscope in gyroscopes)
        {
            gyroscope.Roll = 0;
            gyroscope.Pitch = 0;
            gyroscope.Yaw = 0;
            gyroscope.GyroOverride = false;
            gyroscope.GyroPower = 1.0f;
        }
    }

    // thanks Whip for your help
    // Whip's ApplyGyroOverride Method v9 - 8/19/17
    void ApplyGyroOverride(double pitch_speed, double yaw_speed, double roll_speed, List<IMyGyro> gyro_list, IMyTerminalBlock reference)
    {
        var rotationVec = new Vector3D(-pitch_speed, yaw_speed, roll_speed); //because keen does some weird stuff with signs
        //var rotationVec = new Vector3D(pitch_speed, yaw_speed, roll_speed); //because keen does some weird stuff with signs
        var shipMatrix = reference.WorldMatrix;
        var relativeRotationVec = Vector3D.TransformNormal(rotationVec, shipMatrix);
        foreach (var thisGyro in gyro_list)
        {
            var gyroMatrix = thisGyro.WorldMatrix;
            var transformedRotationVec = Vector3D.TransformNormal(relativeRotationVec, Matrix.Transpose(gyroMatrix));
            thisGyro.Pitch = (float)transformedRotationVec.X;
            thisGyro.Yaw = (float)transformedRotationVec.Y;
            thisGyro.Roll = (float)transformedRotationVec.Z;
            thisGyro.GyroOverride = true;
        }
    }
}

public static class VectorHelper
{
    // in radians
    public static double VectorAngleBetween(Vector3D a, Vector3D b)
    {
        if (Vector3D.IsZero(a) || Vector3D.IsZero(b))
            return 0;
        else
            return Math.Acos(MathHelper.Clamp(a.Dot(b) / Math.Sqrt(a.LengthSquared() * b.LengthSquared()), -1, 1));
    }

    public static Vector3D VectorProjection(Vector3D vectorToProject, Vector3D projectsToVector)
    {
        if (Vector3D.IsZero(projectsToVector))
            return Vector3D.Zero;

        return vectorToProject.Dot(projectsToVector) / projectsToVector.LengthSquared() * projectsToVector;
    }

}



bool TryInit()
{
	
	IMyTextSurface pbTextSurface = Me.GetSurface(0);
	if(flightIndicatorsSurfaceDisplay.Count == 0){
		flightIndicatorsSurfaceDisplay.Add(pbTextSurface);
	}
	if(flightIndicatorsSurfaceDisplay.Count == 0){
        Echo("Cound not find any Surface");
	}
	else{
		//TODO add fonts settings put to Text and Image , size, color,
		flightIndicatorsSurfaceDisplay[0].ContentType = ContentType.TEXT_AND_IMAGE;
        flightIndicatorsSurfaceDisplay[0].FontColor = new Color(255, 255, 255);
        flightIndicatorsSurfaceDisplay[0].FontSize = 1.5f;
	}
	
    // LCD
    if (flightIndicatorsLcdDisplay.Count == 0)
    {
        if (flightIndicatorsLcdNames != null && flightIndicatorsLcdNames.Length > 0 && flightIndicatorsLcdNames[0].Length > 0)
        {
            flightIndicatorsLcdDisplay.AddList(lcdHelper.Find(flightIndicatorsLcdNames));
        }
        else
        {
            IMyTextPanel textPanel = lcdHelper.FindFirst();
			//if no LCD is specified use the included screen onto the PB
			
            if (textPanel != null)
            {
                flightIndicatorsLcdDisplay.Add(textPanel);
            }
            else
            {
                //Echo("Cound not find any LCD");
            }
        }
        if (flightIndicatorsLcdDisplay.Count == 0)
        {
            //return false;
        }

    }


    // Controller
    if (flightIndicatorsShipController == null)
    {
        if (flightIndicatorsControllerName != null && flightIndicatorsControllerName.Length != 0)
        {
            IMyTerminalBlock namedController = GridTerminalSystem.GetBlockWithName(flightIndicatorsControllerName);
            if (namedController == null)
            {
                string message = "No controller named \n" + flightIndicatorsControllerName + " found.";
                Echo(message);
                //lcdHelper.DisplayMessage(message, flightIndicatorsLcdDisplay);
                lcdHelper.DisplayMessage(message, flightIndicatorsSurfaceDisplay);
                return false;
            }
            flightIndicatorsShipController = (IMyShipController)namedController;
        }
        else
        {
            List<IMyRemoteControl> remoteControllers = new List<IMyRemoteControl>();
            GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(remoteControllers);

            if (remoteControllers.Count != 0)
            {
                foreach (var sc in remoteControllers)
                {
                    if (sc.IsSameConstructAs(Me))
                    {
                        flightIndicatorsShipController = (IMyShipController)sc;
                    }
                }
            }
            else
            {
                string message = "No remote control found on the same grid.";
                Echo(message);
                //lcdHelper.DisplayMessage(message, flightIndicatorsLcdDisplay);
                lcdHelper.DisplayMessage(message, flightIndicatorsSurfaceDisplay);
                return false;
            }
        }
    }

    if (flightIndicators == null)
    {
        //flightIndicators = new FlightIndicators(flightIndicatorsShipController, Echo, isPlanetWorld, flightIndicatorsLcdDisplay, lcdHelper);
        flightIndicators = new FlightIndicators(flightIndicatorsShipController, Echo, isPlanetWorld, flightIndicatorsSurfaceDisplay, lcdHelper);
    }

    if (fightStabilizator == null)
    {
        fightStabilizator = new FightStabilizator(flightIndicators, flightIndicatorsShipController, pidP, pidI, pidD, basicLibrary);
        /*
        //testing
        // optional : set desired angles
        fightStabilizator.pitchDesiredAngle = 0f;
        fightStabilizator.yawDesiredAngle = 0f;
        fightStabilizator.rollDesiredAngle = 30f;
        /*
        */
    }

    return true;
}



public class PIDController
{
    double p = 0;
    double i = 0;
    double d = 0;

    double errorIntegral = 0;
    double lastError = 0;

    bool firstRun = true;

    public PIDController(double p, double i, double d)
    {
        this.p = p;
        this.i = i;
        this.d = d;
    }

    public double Control(double error, double timeStep)
    {
        double errorDerivative;

        if (firstRun)
        {
            errorDerivative = 0;
            firstRun = false;
        }
        else
        {
            errorDerivative = (error - lastError) / timeStep;
        }

        lastError = error;

        errorIntegral += error * timeStep;
        return p * error + i * errorIntegral + d * errorDerivative;
    }

    public void Reset()
    {
        errorIntegral = 0;
        lastError = 0;
        firstRun = true;
    }
}


//
// Neyna LCD LIBRARY
// Free to use library for space engineers modders. Just credit me and link to this library in your creations workshop pages.
// https://steamcommunity.com/workshop/filedetails/?id=1404290522
//

public class LCDHelper
{
    public Color defaultFontColor = new Color(255, 255, 255);
    public float defaultSize = 2;
    BasicLibrary basicLibrary;
    StringBuilder messageBuffer = new StringBuilder();

    public LCDHelper(BasicLibrary basicLibrary)
    {
        this.basicLibrary = basicLibrary;
    }

    public LCDHelper(BasicLibrary basicLibrary, Color defaultFontColor, float defaultSize = 2)
    {
        this.basicLibrary = basicLibrary;
        this.defaultFontColor = defaultFontColor;
        this.defaultSize = defaultSize;
    }

    public void DisplayMessage(string message, List<IMyTextPanel> myTextPanels, bool append = false)
    {
        foreach (IMyTextPanel myTextPanel in myTextPanels)
        {
            DisplayMessage(message, myTextPanel, append);
        }
    }

    public void DisplayMessage(string message, IMyTextPanel myTextPanel, bool append = false)
    {
    }
	

    public void DisplayMessage(string message, List<IMyTextSurface> myTextSurfaces, bool append = false)
    {
        foreach (IMyTextSurface myTextSurface in myTextSurfaces)
        {
            DisplayMessage(message, myTextSurface, append);
        }
    }

    public void DisplayMessage(string message, IMyTextSurface myTextSurface, bool append = false)
    {
        myTextSurface.WriteText(message, append);
    }

    // return null if no lcd
    public IMyTextPanel FindFirst()
    {
        IMyTextPanel lcd = basicLibrary.FindFirstBlockByType<IMyTextPanel>();
        if (lcd != null)
        {
            InitDisplay(lcd);
        }
        return lcd;
    }

    // return all lcd in groups + all lcd by names
    public List<IMyTextPanel> Find(string[] lcdGoupsAndNames)
    {
        List<IMyTextPanel> lcds = basicLibrary.FindBlocksByNameAndGroup<IMyTextPanel>(lcdGoupsAndNames, "LCD");
        InitDisplays(lcds);
        return lcds;
    }


    public void InitDisplays(List<IMyTextPanel> myTextPanels)
    {
        InitDisplays(myTextPanels, defaultFontColor, defaultSize);
    }

    public void InitDisplay(IMyTextPanel myTextPanel)
    {
        InitDisplay(myTextPanel, defaultFontColor, defaultSize);
    }

    public void InitDisplays(List<IMyTextPanel> myTextPanels, Color color, float fontSize)
    {
        foreach (IMyTextPanel myTextPanel in myTextPanels)
        {
            InitDisplay(myTextPanel, color, fontSize);
        }
    }

    public void InitDisplay(IMyTextPanel myTextPanel, Color color, float fontSize)
    {
        myTextPanel.FontColor = color;
        myTextPanel.FontSize = fontSize;
        myTextPanel.ApplyAction("OnOff_On");
    }

    public void InitDisplay(IMyTextSurface myTextSurface, Color color, float fontSize)
    {
        myTextSurface.FontColor = color;
        myTextSurface.FontSize = fontSize;
    }

    public void ClearMessageBuffer()
    {
        messageBuffer.Clear();
    }

    public void AppendMessageBuffer(string text)
    {
        messageBuffer.Append(text);
    }

    // this method does not have append boolean parameter because the plan is to use it only with a complete screen message to prevent flickering
    public void DisplayMessageBuffer(List<IMyTextPanel> myTextPanels)
    {
        DisplayMessage(messageBuffer.ToString(), myTextPanels);
    }
	
    //pb screen
    public void DisplayMessageBuffer(List<IMyTextSurface> myTextSurfaces)
    {
        DisplayMessage(messageBuffer.ToString(), myTextSurfaces);
    }
}


//
// END LCD LIBRARY CODE
//


//
// Neyna BASIC LIBRARY
// Free to use library for space engineers modders. Just credit me and link to this library in your creations workshop pages.
// https://steamcommunity.com/workshop/filedetails/?id=1404290522
//

public class BasicLibrary
{
    IMyGridTerminalSystem GridTerminalSystem;
    public Action<string> Echo;

    public BasicLibrary(IMyGridTerminalSystem GridTerminalSystem, Action<string> Echo)
    {
        this.GridTerminalSystem = GridTerminalSystem;
        this.Echo = Echo;
    }

    public T FindFirstBlockByType<T>() where T : class
    {
        List<T> temporaryList = new List<T>();
        GridTerminalSystem.GetBlocksOfType(temporaryList);
        if (temporaryList.Count > 0)
        {
            return temporaryList[0];
        }
        return null;
    }

    public List<T> FindBlocksByNameAndGroup<T>(string[] names, string typeOfBlockForMessage) where T : class
    {
        List<T> result = new List<T>();

        List<T> temporaryList = new List<T>();
        List<T> allBlockList = new List<T>();
        GridTerminalSystem.GetBlocksOfType(allBlockList);

        if (names == null) return result;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Length == 0)
            {
                break;
            }
            IMyBlockGroup blockGroup = GridTerminalSystem.GetBlockGroupWithName(names[i]);
            if (blockGroup != null)
            {
                temporaryList.Clear();
                blockGroup.GetBlocksOfType(temporaryList);
                if (temporaryList.Count == 0)
                {
                    Echo($"Warning : group {names[i]} has no {typeOfBlockForMessage}.");
                }
                result.AddList(temporaryList);
            }
            else
            {
                bool found = false;
                foreach (T block in allBlockList)
                {
                    if (((IMyTerminalBlock)block).CustomName == names[i])
                    {
                        result.Add(block);
                        found = true;
                        break;
                    }

                }
                if (!found)
                {
                    Echo($"Warning : {typeOfBlockForMessage} or group named\n{names[i]} not found.");
                }
            }
        }
        return result;
    }

    public void GetBlocksOfType<T>(List<T> list, Func<T, bool> collect = null) where T : class
    {
        GridTerminalSystem.GetBlocksOfType(list, collect);
    }

    public static void AppendFormatted(StringBuilder stringBuilder, string stringToFormat, params object[] args)
    {
        if (stringToFormat != null && stringToFormat.Length > 0)
        {
            stringBuilder.Append(string.Format(stringToFormat, args));
        }
    }

    public static void AppendFormattedNewLine(StringBuilder stringBuilder, string stringToFormat, params object[] args)
    {
        AppendFormatted(stringBuilder, stringToFormat, args);
        stringBuilder.Append('\n');
    }

    static readonly DateTime dt1970 = new DateTime(1970, 1, 1);
    public static double GetCurrentTimeInMs()
    {
        DateTime time = System.DateTime.Now;
        TimeSpan timeSpan = time - dt1970;
        return timeSpan.TotalMilliseconds;
    }
}

//
// END OF BASIC LIBRARY
//