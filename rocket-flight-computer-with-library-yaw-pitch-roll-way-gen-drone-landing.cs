//From https://steamcommunity.com/sharedfiles/filedetails/?id=1390966561

// config
string[] flightIndicatorsLcdNames = { "" };
string flightIndicatorsControllerName = "";
const bool stalizableYaw = false; // do you want to stablize yaw to 0°
const bool isPlanetWorld = true; // this should be true for every easy start or star system scenario, false if no planet in your scenario

// end of config

enum FlightMode { STABILIZATION, STANDY };
FlightMode flightIndicatorsFlightMode = FlightMode.STANDY;
List<IMyTextPanel> flightIndicatorsLcdDisplay = new List<IMyTextPanel>();
IMyShipController flightIndicatorsShipController = null;
/*
//default constant
const double pidP = 0.06f;
const double pidI = 0.0f;
const double pidD = 0.01f;
*/
/*
const double pidP = 0.06f;
const double pidI = 0.0f;
const double pidD = 0.0f;
*/


const double pidP = 0.06f;
const double pidI = 0.00f;
const double pidD = 0.01f;


BasicLibrary basicLibrary;
LCDHelper lcdHelper;
FlightIndicators flightIndicators;
FightStabilizator fightStabilizator;





PIDController altRegulator = new PIDController(0.06f, .00f, 0.01f);
double wantedAltitude = 150;
double altitudeError = 0f;
bool altSettingChanged = false;
Vector3D shipAcceleration = new Vector3D(0, 0, 0);
Vector3D prevLinearSpeedsShip = new Vector3D(0, 0, 0);
PIDController downwardSpeedAltRegulator = new PIDController(0.06f, .00f, 0.06f);
double g_constant = 9.8f;
double alt = 0f;
double last_alt = 0f;
double alt_speed_ms_1 = 0f;
double last_alt_speed_ms_1 = 0f;
double alt_acc_ms_2 = 0f;
double last_alt_acc_ms_2 = 0f;

System.DateTime lastTime = System.DateTime.UtcNow;
System.DateTime lastRunTs = System.DateTime.UtcNow;

bool firstMainLoop = true;

//drone landing
//PIDController angleRollPID = new PIDController(0.06f, 0f, 0.06f);
PIDController angleRollPID = new PIDController(1f, 0f, 0f);


public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
    basicLibrary = new BasicLibrary(GridTerminalSystem, Echo);
    lcdHelper = new LCDHelper(basicLibrary, new Color(0, 255, 0), 1.5f);
}

public void Main(string argument, UpdateType updateSource)
{
    if (!TryInit())
    {
        return;
    }

    /*
    BasicLibrary basicLibrary = new BasicLibrary(GridTerminalSystem, Echo);
    bool stalizableRoll = true;
    bool stalizablePitch = true;
    bool stalizableYaw = false;

    // PID values in case you want to ajust them, but you should not need to do it
    const double pidP = 0.06f;
    const double pidI = 0.0f;
    const double pidD = 0.01f;

    FightStabilizator fightStabilizator = new FightStabilizator(flightIndicators, controller, pidP, pidI, pidD, basicLibrary);

    // optional : set desired angles
    fightStabilizator.pitchDesiredAngle = 0f;
    fightStabilizator.yawDesiredAngle = 0f;
    fightStabilizator.rollDesiredAngle = 0f;

    // reset before each new stabilization (for example after stopping stabilization and when you want to start a new one). DO NOT call it during stabilization
    fightStabilizator.Reset();

    // call this next line at each run
    fightStabilizator.Stabilize(stalizableRoll, stalizablePitch, stalizableYaw);

    // release gyros when you stop stabilization
    fightStabilizator.Release();
    */

    /*
    flightIndicatorsFlightMode = FlightMode.STABILIZATION;
    fightStabilizator.Reset();
    flightIndicators.Compute();
    fightStabilizator.Stabilize(true, true, stalizableYaw);
    */



    //if (argument != null && argument.ToLower().Equals("stabilize_on"))
    /*
    flightIndicatorsFlightMode = FlightMode.STABILIZATION;
    fightStabilizator.Reset();
    */

    //USE aaa_needs_testing bp

    System.DateTime now = System.DateTime.UtcNow;
    Echo("now:" + now);

    var deltaTime = (float)(now - lastTime).Milliseconds / 1000f;
    Echo("deltaTime = now - lastTime:" + deltaTime);

    lastTime = now;

    Echo("now - lastRunTs:" + (now - lastRunTs).Milliseconds / 1000f);

    DateTime d = new DateTime(1970, 01, 01);
    var temp = d.Ticks; // == 621355968000000000

    Echo("temp:" + temp);

    var temp2 = now.Ticks;

    Echo("temp2:" + temp2);

    Echo("temp2/10**6:" + (temp2 / 1000000f));

    DateTime dt1970 = new DateTime(1970, 1, 1);
    DateTime current = DateTime.Now;//DateTime.UtcNow for unix timestamp
    TimeSpan span = current - dt1970;
    Echo("span:" + span.TotalMilliseconds.ToString());

    List<IMyShipController> listRemoteController = new List<IMyShipController>();
    GridTerminalSystem.GetBlocksOfType<IMyShipController>(listRemoteController);

    if (listRemoteController == null)
    { Echo("no IMyShipController available"); return; }
    //ship controlller GetTotalGravity()
    IMyShipController myCurrentCockpit = listRemoteController[0];
    Vector3D totalGravityVect3D = myCurrentCockpit.GetTotalGravity();
    Echo("\n\ntotalGravityVect3D:\n" + totalGravityVect3D);
    Vector3D totalGravityVect3Dnormalized = Vector3D.Normalize(totalGravityVect3D);
    Echo("\n\ntotalGravityVect3Dnormalized:\n" + totalGravityVect3Dnormalized);
    MyBlockOrientation cockpitOrientation = myCurrentCockpit.Orientation;
    var leftCockpitOrientation = cockpitOrientation.Left;
    Echo("leftCockpitOrientation:" + leftCockpitOrientation);

    //getting vectors to help with angles proposals
    Vector3D shipForwardVector = myCurrentCockpit.WorldMatrix.Forward;
    Vector3D shipLeftVector = myCurrentCockpit.WorldMatrix.Left;
    Vector3D shipDownVector = myCurrentCockpit.WorldMatrix.Down;
    Echo("shipForwardVector:" + shipForwardVector);
    Echo("shipLeftVector:" + shipLeftVector);
    Echo("shipDownVector:" + shipDownVector);

    //Getting the ship/pb postion
    Vector3D myPos = Me.GetPosition();
    Echo("myPos:\n" + myPos);

    //note:
    //https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/VRage.Math/Vector3D.cs
    //var targetGpsString = "";
    //Echo("targetGpsString:" + targetGpsString);
    MyWaypointInfo myWaypointInfoTarget = new MyWaypointInfo("lol", 0, 0, 0);
    MyWaypointInfo.TryParse("GPS:/// #4:53590.85:-26608.05:11979.08:", out myWaypointInfoTarget);
    //MyWaypointInfo.TryParse("GPS:/// #5:57250.86:-21636.06:8383.28:", out myWaypointInfoTarget);
    //MyWaypointInfo.TryParse("GPS:/// #6:53613.98:-26613.76:11979.21:", out myWaypointInfoTarget);
    //MyWaypointInfo.TryParse("GPS:4 reversed:-53590.85:26608.05:-11979.08:", out myWaypointInfoTarget);
    //MyWaypointInfo.TryParse("GPS:/// #7:51179.25:-30228.41:13047.45:", out myWaypointInfoTarget);



    //x,y,z coords
    Vector3D vec3Dtarget = myWaypointInfoTarget.Coords;


    //targetGravityVectorNormalized
    Vector3D earthLikeCenter = new Vector3D(0, 0, 0);
    Vector3D vec3DtargetNegate;
    Vector3D.Negate(ref vec3Dtarget, out vec3DtargetNegate);
    Vector3D targetGravityVectorNormalized = Vector3D.Normalize(Vector3D.Add(vec3DtargetNegate, earthLikeCenter));
    Echo("\ntargetGravityVectorNormalized:\n" + targetGravityVectorNormalized);

    //distance to target TODO
    Vector3D VectToTarget = Vector3D.Add(vec3DtargetNegate, myPos);
    double distToTarget = VectToTarget.Length();
    Echo("\ndistToTarget:" + distToTarget);

    //totalGravityVect3Dnormalized cross targetGravityVectorNormalized
    Vector3D crossCurrentTargetGravityNormalized = Vector3D.Cross(targetGravityVectorNormalized, totalGravityVect3Dnormalized);
    Echo("\ncrossCurrentTargetGravityNormalized:\n" + crossCurrentTargetGravityNormalized);

    //math pitch
    Echo("\n=====================================");
    //TODO:Check all axis one by one and then the signs.
    //CHECKED: long range roll and pitch length and sign changes are ok
    Vector3D vectorPitchCalcedSetting = Vector3D.Cross(shipForwardVector, crossCurrentTargetGravityNormalized);
    Echo("\nvectorPitchCalcedSetting:\n" + vectorPitchCalcedSetting);
    //math roll : + clock wise | - clock wise 
    Vector3D vectorRollCalcedSetting = Vector3D.Cross(shipLeftVector, crossCurrentTargetGravityNormalized);
    Echo("\nvectorRollCalcedSetting:\n" + vectorRollCalcedSetting);
    //math yaw
    Vector3D vectorYawCalcedSetting = Vector3D.Cross(shipDownVector, crossCurrentTargetGravityNormalized);
    Echo("\n\nvectorYawCalcedSetting:\n" + vectorYawCalcedSetting);

    double pitchFowardOrBackward = (Vector3D.Dot(vectorPitchCalcedSetting, shipDownVector) < 0) ? -vectorPitchCalcedSetting.Length() : vectorPitchCalcedSetting.Length();
    double rollLeftOrRight = (Vector3D.Dot(vectorRollCalcedSetting, shipForwardVector) > 0) ? -vectorRollCalcedSetting.Length() : vectorRollCalcedSetting.Length();
    double yawCWOrAntiCW = (Vector3D.Dot(vectorYawCalcedSetting, shipLeftVector) < 0) ? -vectorYawCalcedSetting.Length() : vectorYawCalcedSetting.Length(); ;
    Echo("\npitchFowardOrBackward:\n" + pitchFowardOrBackward);
    double pitchTmp = pitchFowardOrBackward;
    double rollTmp = rollLeftOrRight;
    double yawTmp = yawCWOrAntiCW;


    double elev;
    myCurrentCockpit.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);

    //change the wantedAltitude BEFORE THIS LINE
    altitudeError = wantedAltitude - elev;

    MyShipVelocities myShipVel = myCurrentCockpit.GetShipVelocities();
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
    IMyShipController shipController = listRemoteController[0];

    Vector3D gravityVector = shipController.GetNaturalGravity();
    Vector3D planetRelativeLeftVector = shipForwardVector.Cross(gravityVector);

    Vector3D absoluteNorthVector;
    absoluteNorthVector = absoluteNorthPlanetWorldsVector;

    Vector3D relativeEastVector = gravityVector.Cross(absoluteNorthVector);
    Vector3D relativeNorthVector = relativeEastVector.Cross(gravityVector);

    shipForwardVector = VectToTarget;
    Vector3D forwardProjectUp = VectorHelper.VectorProjection(shipForwardVector, gravityVector);
    Vector3D forwardProjPlaneVector = shipForwardVector - forwardProjectUp;

    yawCWOrAntiCW = VectorHelper.VectorAngleBetween(forwardProjPlaneVector, relativeNorthVector) * rad2deg;
    if (shipForwardVector.Dot(relativeEastVector) < 0)
    {
        yawCWOrAntiCW = 360.0d - yawCWOrAntiCW; //because of how the angle is measured                                                                          
    }
    //End of the part from the library (and mainly from it)========================

    /*
    bool stalizablePitch = true;
    bool stalizableRoll = true;
    bool stalizableYaw = true;

    if (distToTarget < 1500)
    {
        if (linearSpeedsShip.Length() > 1)
        {
            pitchFowardOrBackward = Vector3D.Dot(linearSpeedsShipNormalized, FowardPorMNormalized);
            rollLeftOrRight = Vector3D.Dot(linearSpeedsShipNormalized, LeftPorMNormalized);

            //debug
            double surfaceSpeed = Math.Sqrt(pitchFowardOrBackward * pitchFowardOrBackward + rollLeftOrRight * rollLeftOrRight);


            Echo("\n=====================================");
            Echo("\n" + "pitchFowardOrBackward:\n" + pitchFowardOrBackward);
            Echo("\n" + "rollLeftOrRight:\n" + rollLeftOrRight);


            pitchFowardOrBackward *= 0.01f;
            rollLeftOrRight *= 0.01f;
            //yaw setting can't put the rocket upside down

            stalizablePitch = true;
            stalizableRoll = true;
            stalizableYaw = false;

        }

        if (elev < 5)
        {
            //general:
            List<IMyFunctionalBlock> listIMyFunctionalBlock = new List<IMyFunctionalBlock>();
            GridTerminalSystem.GetBlocksOfType(listIMyFunctionalBlock);
            //disabling everything but the PB
            foreach (var l in listIMyFunctionalBlock)
            {
                if (!(l is IMyBatteryBlock))
                {
                    if (!(l is IMyProgrammableBlock))
                    {
                        l.Enabled = false;
                    }
                }
            }
            //disabling the PB
            foreach (var l in listIMyFunctionalBlock)
            {
                if (l is IMyProgrammableBlock)
                {
                    l.Enabled = false;
                }
            }
        }
        
        if (distToTarget < 1500)
        {
            //issue once the reset for the PID
            if (wantedAltitude != 100)
            {
                wantedAltitude = 100;
                altRegulator.Reset();
            }
        }

        if (elev < 100)
        {
            //issue once the reset for the PID
            if (wantedAltitude != 15)
            {
                wantedAltitude = 15;
                altRegulator.Reset();
            }
        }

    }

    if (altSettingChanged == true)
    {
        altSettingChanged = false;
        //altRegulator.Reset();
    }

    double finalPitchSetting = Convert.ToSingle(-pitchFowardOrBackward * 3000f);
    finalPitchSetting = MyMath.Clamp(Convert.ToSingle(finalPitchSetting), -30f, 30f);
    double finalRollSetting = Convert.ToSingle(rollLeftOrRight * 3000f);
    finalRollSetting = MyMath.Clamp(Convert.ToSingle(finalRollSetting), -30f, 30f);
    double finalYawSetting = Convert.ToSingle(yawCWOrAntiCW);


    BasicLibrary basicLibrary = new BasicLibrary(GridTerminalSystem, Echo);
    
    // call this next line at each run
    fightStabilizator.Stabilize(stalizableRoll, stalizablePitch, stalizableYaw);

    //+ pitch go foward
    fightStabilizator.pitchDesiredAngle = Convert.ToSingle(finalPitchSetting);
    fightStabilizator.rollDesiredAngle = Convert.ToSingle(finalRollSetting);
    fightStabilizator.yawDesiredAngle = Convert.ToSingle(finalYawSetting);
    */

    List<IMyRadioAntenna> listAntenna = new List<IMyRadioAntenna>();
    GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);


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
    Echo("posInterpolation:\n" + posInterpolation);


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
    
    /*
    bool stalizablePitch = true;
    bool stalizableRoll = true;
    bool stalizableYaw = false;
    */

    flightIndicators.Compute();
    if (flightIndicatorsFlightMode == FlightMode.STABILIZATION)
    {
        //fightStabilizator.Stabilize(true, true, stalizableYaw);
        fightStabilizator.Stabilize(true, true, false);
        //just do one axis gyro axis maximum if stuck
    }

    lcdHelper.ClearMessageBuffer();
    lcdHelper.AppendMessageBuffer(flightIndicators.DisplayText());
    if (flightIndicatorsFlightMode == FlightMode.STABILIZATION)
    {
        lcdHelper.AppendMessageBuffer(fightStabilizator.DisplayText());
    }
    lcdHelper.DisplayMessageBuffer(flightIndicatorsLcdDisplay);


    var debugString = "";
    /*
    var myCurrentCockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    var listShipController = new List<IMyShipController>();
    if (listShipController == null)
    { Echo("nope"); return; }
    var myCurrentCockpit = listShipController[0];
    */
    //var listRemoteController = new List<IMyRemoteControl>();
    //IMyRemoteControl

    /*
    if (altitudeError > 5)
    {
        flightIndicatorsFlightMode = FlightMode.STABILIZATION;
        fightStabilizator.Reset();
    }
    else
    {
        flightIndicatorsFlightMode = FlightMode.STANDY;
        fightStabilizator.Release();
    }
    */

    double dts = Runtime.TimeSinceLastRun.TotalSeconds;

    shipAcceleration = Vector3D.Add(Vector3D.Negate(linearSpeedsShip), prevLinearSpeedsShip) / dts;

    prevLinearSpeedsShip = linearSpeedsShip;

    Echo("shipAcceleration:"+ shipAcceleration);

    //public double Control(double error, double timeStep)
    //todo change this
    //double dir = altRegulator.Control(altitudeError, dts);

    Echo("elev:" + elev);
    //PhysicalMass	Gets the physical mass of the ship, which accounts for inventory multiplier.
    var physMass_kg = myCurrentCockpit.CalculateShipMass().PhysicalMass;
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

    double physMass_N = physMass_kg * g_constant;
    debugString += "\n" + "physMass_N:" + physMass_N;


    //BaseMass Gets the base mass of the ship.
    //totalMass Gets the total mass of the ship, including cargo.
    //PhysicalMass Gets the physical mass of the ship, which accounts for inventory multiplier.


    var totalMass_kg = myCurrentCockpit.CalculateShipMass().TotalMass;
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

    var massOfShip = myCurrentCockpit.CalculateShipMass().PhysicalMass;
    debugString += "\n" + "massOfShip:" + massOfShip;
    
    var control = altRegulator.Control(altitudeError, dts);
    /*
    double downwardSpeedAlt = Vector3D.Dot(totalGravityVect3Dnormalized,linearSpeedsShip);
    Vector3D downwardSpeedAltVector3D = linearSpeedsShip.Dot(totalGravityVect3D) / totalGravityVect3D.LengthSquared() * totalGravityVect3D;
    double downwardSpeedAltError = -10- downwardSpeedAltVector3D.Length();
    
    var control = downwardSpeedAltRegulator.Control(downwardSpeedAltError, dts);

    listAntenna[0].HudText = "elev:" + Math.Round((elev), 0)+"downwardSpeedAltError:"+ Math.Round((downwardSpeedAltError), 2) + "control:"+ Math.Round((control),2);
    Me.CubeGrid.CustomName = "elev:" + Math.Round((elev), 0) + "downwardSpeedAltError:" + Math.Round((downwardSpeedAltError), 2) + "control:" + Math.Round((control), 2);
    */


    //double TWR = 4.59;
    double TWR = thr_to_weight_ratio;
    double V_max = 100;
    double AngleRollMaxAcc = Math.Atan((TWR - 1)/ 1) * 180 / Math.PI;

    double g = 9.8;
    double MaxSurfaceAcc = (TWR - 1) * g;

    double brakingTime = V_max / MaxSurfaceAcc;
    double distWhenToStartBraking = brakingTime * V_max;

    //double wantedSpeedRoll = Vector3D.Dot(shipLeftVector,shipVelocities);
    //TODO
    //double distRoll = Vector3D.Dot(shipLeftVector, distToTarget);

    Vector3D leftProjectUp = VectorHelper.VectorProjection(shipLeftVector, gravityVector);
    Vector3D leftProjPlaneVector = shipLeftVector - leftProjectUp;

    double leftProjPlaneVectorLength = leftProjPlaneVector.Length();

    double distRoll = Vector3D.Dot(leftProjPlaneVector, VectToTarget);
    double clampedDistRoll = MyMath.Clamp(Convert.ToSingle(distRoll), Convert.ToSingle(-distWhenToStartBraking), Convert.ToSingle(distWhenToStartBraking));
    double wantedSpeedRoll = (V_max / distWhenToStartBraking) * clampedDistRoll;

    double speedRoll = Vector3D.Dot(leftProjPlaneVector, linearSpeedsShip);

    double speedRollError = wantedSpeedRoll - speedRoll;
    double angleRoll = angleRollPID.Control(speedRollError, dts);
    angleRoll = MyMath.Clamp(Convert.ToSingle(angleRoll), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));

    double finalPitchSetting = 0f;
    double finalYawSetting = 0f;
    double finalRollSetting =- angleRoll * 180 / Math.PI;

    //+ pitch go foward
    fightStabilizator.pitchDesiredAngle = Convert.ToSingle(finalPitchSetting);
    fightStabilizator.rollDesiredAngle = Convert.ToSingle(finalRollSetting);
    fightStabilizator.yawDesiredAngle = Convert.ToSingle(finalYawSetting);

    bool stalizablePitch = true;
    bool stalizableRoll = true;
    bool stalizableYaw = false;

    //debug roll\
    //var str_to_display = "distRoll:" + Math.Round((distRoll), 2);
    //var str_to_display = "distRoll:" + Math.Round((distRoll), 2) + "|" + "clampedDistRoll:" + Math.Round((clampedDistRoll), 2);
    //var str_to_display = "AngleRollMaxAcc:" + Math.Round((AngleRollMaxAcc), 2);
    //var str_to_display = "distRoll:" + Math.Round((distRoll), 2);
    //var str_to_display = "angleRoll:" + Math.Round((angleRoll), 2) + "|" + "wantedSpeedRoll:" + Math.Round((wantedSpeedRoll), 2) + "|" + "distRoll:" + Math.Round((distRoll), 2);
    var str_to_display =  Math.Round((distRoll), 2) + "|" + Math.Round((wantedSpeedRoll), 2) + "|"+ Math.Round((angleRoll), 2) + "|" + Math.Round((leftProjPlaneVectorLength), 2);
    //str_to_display = "finalPitchSetting:" + Math.Round((finalPitchSetting), 2) + "finalRollSetting:" + Math.Round((finalRollSetting), 2) + "finalYawSetting:" + Math.Round((finalYawSetting), 2);
    listAntenna[0].HudText = str_to_display;
    Me.CubeGrid.CustomName = str_to_display;

    debugString += "\n" + "control:" + control;

    //applying what the pid processed
    //var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    Echo(cs.ToString());

    foreach (var c in cs)
    {
        double temp_thr_n = 1f * physMass_N * c.MaxThrust / c.MaxEffectiveThrust + physMass_N * control;
        c.ThrustOverride = Convert.ToSingle(temp_thr_n);
        c.ThrustOverride = Convert.ToSingle(temp_thr_n*(1+1/(1-Math.Sin(angleRoll*3.14/180))));
        //debug:disabled the thruster
        //c.ThrustOverride = Convert.ToSingle(0f);
    }
}

public class FlightIndicators
{
    IMyShipController shipController;
    readonly Action<string> Echo;
    readonly List<IMyTextPanel> lcdDisplays = null;
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

        Echo("|Pitch " + Math.Round(Pitch, 2) + "\n|Roll " + Math.Round(Roll, 2) + "\n|Yaw " + Math.Round(Yaw, 2));

        return stringBuilder.ToString();
    }

    public void Display()
    {
        if (lcdHelper == null || lcdDisplays == null)
        {
            Echo("Can't diplay, LCD or LCDHelper not set");
            return;
        }

        lcdHelper.DisplayMessage(DisplayText(), lcdDisplays);
    }

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
        yawPid = new PIDController(pidP/6, pidI, pidD);
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
            if (textPanel != null)
            {
                flightIndicatorsLcdDisplay.Add(textPanel);
            }
            else
            {
                Echo("Cound not find any LCD");
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
                lcdHelper.DisplayMessage(message, flightIndicatorsLcdDisplay);
                return false;
            }
            flightIndicatorsShipController = (IMyShipController)namedController;
        }
        else
        {
            List<IMyShipController> shipControllers = new List<IMyShipController>();
            GridTerminalSystem.GetBlocksOfType<IMyShipController>(shipControllers);

            if (shipControllers.Count != 0)
            {
                flightIndicatorsShipController = shipControllers[0];
            }
            else
            {
                string message = "No controller found.";
                Echo(message);
                lcdHelper.DisplayMessage(message, flightIndicatorsLcdDisplay);
                return false;
            }
        }
    }

    if (flightIndicators == null)
    {
        flightIndicators = new FlightIndicators(flightIndicatorsShipController, Echo, isPlanetWorld, flightIndicatorsLcdDisplay, lcdHelper);
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
        myTextPanel.WritePublicText(message, append);
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
        myTextPanel.ShowPublicTextOnScreen();
        myTextPanel.FontColor = color;
        myTextPanel.FontSize = fontSize;
        myTextPanel.ApplyAction("OnOff_On");
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