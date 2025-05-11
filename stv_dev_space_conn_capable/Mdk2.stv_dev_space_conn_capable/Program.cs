using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    public partial class Program : MyGridProgram
    {
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
        List<IMyTextSurface> flightIndicatorsSurfaceDisplay = new List<IMyTextSurface>();
        IMyShipController flightIndicatorsShipController = null;

        //default constant
        const double pidP = 0.06f;
        const double pidI = 0.00f;
        const double pidD = 0.01f;

        BasicLibrary basicLibrary;
        LCDHelper lcdHelper;
        FlightIndicators flightIndicators;
        FightStabilizator fightStabilizator;


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

        double PlanetmaxAtmoRadius = 0;
        double PlanetmaxR = 0;
        double PlanetminR = 100000;
        double PlanetCurrentG = 0;
        double PlanetMaxG = 0;
        bool firstRunComingFromSpace = false;
        double maxAtmoAltComingFromSpace = 0;
        bool PlanetisTargetInTheSameGravityWheel = false;

        Vector3D VecPlanetCenter = new Vector3D(0, 0, 0);

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            basicLibrary = new BasicLibrary(GridTerminalSystem, Echo);
            //lcdHelper = new LCDHelper(basicLibrary, new Color(0, 255, 0), 1.5f);
            lcdHelper = new LCDHelper(basicLibrary, new Color(255, 255, 255), 1.5f);

            GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);

            if (listAntenna.Count != 0)
            {
                foreach (IMyRadioAntenna antenna in listAntenna)
                {
                    if ((antenna.CubeGrid == Me.CubeGrid))
                    {
                        theAntenna = antenna;
                    }
                }
            }
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

            Echo("now:" + now);
            Echo("deltaTime = now - lastTime:" + deltaTime);
            Echo("now - lastRunTs:" + (now - lastRunTs).Milliseconds / 1000f);
            //Echo("temp:" + temp);
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
                    String arg = argument;
                    int count = arg.Count(x => x == ':');
                    string filteredString = "";
                    String[] argSplitted = arg.Split(':');
                    Echo("count:" + count);
                    if (count < 5)
                    {
                        //something is wrong with the gps
                        return;
                    }
                    if (count == 5)
                    { //do nothing, ok
                    }
                    if (count == 6)
                    { //trim color
                    }
                    if (count == 7)
                    { //trim color and folder
                    }
                    if (count > 7)
                    {
                        //something is wrong with the gps
                        return;
                    }
                    filteredString = argSplitted[0] + ":" + argSplitted[1] + ":" + argSplitted[2] + ":" + argSplitted[3] + ":" + argSplitted[4] + ":";
                    Echo("filteredString:" + filteredString);

                    MyWaypointInfo.TryParse(filteredString, out myWaypointInfoTarget);

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
            Vector3D tmpCenterPlanet = new Vector3D(0, 0, 0);
            if (flightIndicatorsShipController.TryGetPlanetPosition(out tmpCenterPlanet) == true)
            {
                Echo("==true");
                VecPlanetCenter = tmpCenterPlanet;
            }
            else
            {
                Echo("!=true");

                //GPS: Pertam: -3967231.50:-32231.50:-767231.50:

                int radiusPertam = 48527;

                Vector3D centerPertam = new Vector3D(-3967231.50, -32231.50, -767231.50);


                Vector3D BBPertamMinV3 = centerPertam - radiusPertam;
                Vector3D BBPertamMaxV3 = centerPertam + radiusPertam;

                BoundingBoxD BBPertam = new BoundingBoxD(BBPertamMinV3, BBPertamMaxV3);

                ContainmentType ct = BBPertam.Contains((Vector3)myPos);

                if (ct == ContainmentType.Contains)
                {
                    Echo("==Contains");
                    Vector3D tpmG = myRemoteControl.GetNaturalGravity();
                    if (tpmG != Vector3D.Zero)
                    {
                        Echo("tpmG != ");
                        VecPlanetCenter = centerPertam;
                        elev = (Me.GetPosition() - VecPlanetCenter).Length() - 30230;
                    }
                    else
                    {
                        Echo("tpmG == ");
                    }
                }
                else
                {
                    Echo("!=Contains");
                }
            }
            Vector3D negateVecPlanetCenter = new Vector3D(0, 0, 0);
            Vector3D.Negate(ref VecPlanetCenter, out negateVecPlanetCenter);
            Vector3D vecToPlanetCenter = Vector3D.Add(myPos, negateVecPlanetCenter);
            double distToPlanetCenter = vecToPlanetCenter.Length();
            //Echo("distToPlanetCenter:"+distToPlanetCenter);
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
                if (c.CubeGrid == Me.CubeGrid)
                {
                    if (c.IsWorking == true)
                    {
                        //numCS++;
                        //Echo("numCS:"+numCS);
                        maxEffectiveThrust_N += c.MaxEffectiveThrust; currentThrust_N += c.CurrentThrust;
                    }
                }

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
            if (Math.Abs(speedRoll) < 1)
            {
                if (Math.Abs(speedPitch) < 1)
                {
                    anglePitch *= 10f;
                    angleRoll *= 10f;
                    anglePitch = MyMath.Clamp(Convert.ToSingle(anglePitch), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
                    angleRoll = MyMath.Clamp(Convert.ToSingle(angleRoll), Convert.ToSingle(-AngleRollMaxAcc), Convert.ToSingle(AngleRollMaxAcc));
                }
            }
            if (Math.Abs(speedRoll) < .01)
            {
                if (Math.Abs(speedPitch) < .01)
                {
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

            Vector3D V3Dgoal = -(myPos - vec3Dtarget);


            //detecting when going to space would allow the ship to not come into the current gravity if it uses thrusters in space
            bool isTargetAboveTheHor = false;
            double angleTargetVsGravity = 0;
            angleTargetVsGravity = VectorHelper.VectorAngleBetween(totalGravityVect3Dnormalized, V3Dgoal) * rad2deg;
            Echo("angleTargetVsGravity:" + Math.Round(angleTargetVsGravity, 3));

            if (angleTargetVsGravity > 90)
            {
                isTargetAboveTheHor = true;
            }

            Echo("AboveTheHor:" + isTargetAboveTheHor);
            Echo("isTargetAboveTheHor:" + isTargetAboveTheHor);
            Echo("AboveTheHor:" + isTargetAboveTheHor);

            if (gravityVector.LengthSquared() == 0)
            {
                Echo("In space ?");
                //assuming the g_constant
                g = 9.81;
                thr_to_weight_ratio = maxEffectiveThrust_N / (physMass_kg * g);
                Echo("thr_to_weight_ratioInSpace:" + Math.Round(thr_to_weight_ratio,3) );

                //thr_to_weight_ratio = MyMath.Clamp((float)thr_to_weight_ratio,(float) 0,(float) 0.3);

                double max_g_space = maxEffectiveThrust_N / physMass_kg;

                Echo("max_g_space:" + max_g_space);

                //from 1 to inf
                double safety_k = 5;

                Echo("safety_k:" + safety_k);

                double V_max_space = 100;

                distWhenToStartBraking = safety_k
               * (V_max_space * V_max_space) / (2 * max_g_space);
                //double distWhenToStartBraking  = distToTarget * linearSpeedsShip.Length();

                Echo("distWhenToStartBraking:" + Math.Round(distWhenToStartBraking, 3));

                //vec3Dtarget = new Vector3D(0, 0, 0);

                if (vec3Dtarget == new Vector3D(0, 0, 0))
                {
                    vec3Dtarget = myPos;
                }

                //start thrust control
                double distToGoal = V3Dgoal.Length();

                Echo("distToGoal:" + Math.Round(distToGoal, 3));

                Vector3D V3Dgoal_speed = V_max_space * Vector3D.Normalize(V3Dgoal);

                double approachSpeed = 0f;
                //if(distToGoal<safety_k*distWhenToStartBraking){
                approachSpeed = (distToGoal * V_max_space) / distWhenToStartBraking;
                approachSpeed = MyMath.Clamp((float)approachSpeed, 0, (float)V_max_space);
                V3Dgoal_speed = approachSpeed * Vector3D.Normalize(V3Dgoal_speed);
                //}
                Echo("approachSpeed:" + approachSpeed);


                if (distToGoal < 250)
                {
                    V3Dgoal_speed = 10 * V3Dgoal_speed;
                    V3Dgoal_speed = V3Dgoal_speed.Normalized() * MyMath.Clamp((float)V3Dgoal_speed.Length(), 0, (float)10f); ;
                }


                Vector3D V3D_V_error_space = linearSpeedsShip - V3Dgoal_speed;

                double V_error_space = (linearSpeedsShip - V3Dgoal_speed).Length();

                double dot_linearSpeedsShip_V3Dgoal_speed = linearSpeedsShip.Dot(V3Dgoal_speed);
                Echo("dot_linearSpeedsShip_V3Dgoal_speed:\n" + dot_linearSpeedsShip_V3Dgoal_speed);

                if (distToGoal < 250)
                {
                    V_error_space = 50 * V_error_space;

                    V_error_space = MyMath.Clamp((float)V_error_space, 0, (float)8f);
                }

                Echo("V_error_space:" + Math.Round(V_error_space,3));

                double negIfThrustIsOpp = V3D_V_error_space.Dot(shipDownVector);

                Echo("negIfThrustIsOpp" + Math.Round(negIfThrustIsOpp, 3));

                //===================
                //space support WIP start
                Vector3D leftProjectUp2 = VectorHelper.VectorProjection(shipLeftVector, shipDownVector);
                Vector3D leftProjPlaneVector2 = shipLeftVector - leftProjectUp2;
                double distRoll2 = -Vector3D.Dot(Vector3D.Normalize(leftProjPlaneVector2), Vector3D.Normalize(V3D_V_error_space));

                Echo("distRoll2:" + Math.Round(distRoll2, 2));
                angleRoll = 20 * distRoll2;
                //===================
                Vector3D forwardProjectUp2 = VectorHelper.VectorProjection(shipForwardVector, shipDownVector);
                Vector3D forwardProjPlaneVector2 = shipForwardVector - forwardProjectUp2;
                //double distRoll = Vector3D.Dot(leftProjPlaneVector, VectToTarget);
                double distPitch2 = -Vector3D.Dot(Vector3D.Normalize(forwardProjPlaneVector2), Vector3D.Normalize(V3D_V_error_space));

                Echo("distPitch2:" + Math.Round(distPitch2, 2));
                anglePitch = 20 * distPitch2;
                if (negIfThrustIsOpp < 0)
                {
                    anglePitch *= 10;
                    angleRoll *= 10;
                }

                control = 0;
                if (Math.Abs(V_error_space) > 5)
                {
                    if (Math.Abs(anglePitch) < 1)
                    {
                        if (Math.Abs(angleRoll) < 1)
                        {
                            control = V_error_space;
                        }
                    }
                }
                /*
                if(Math.Abs(V_error_space)>1){
                    if(Math.Abs(anglePitch)<.3){
                        if(Math.Abs(angleRoll)<.3){
                            control =  50*V_error_space;
                        }
                    }
                }

                if(Math.Abs(distToGoal)<70){
                    if(Math.Abs(anglePitch)<.3){
                        if(Math.Abs(angleRoll)<.3){
                            control =  50*V_error_space;
                        }
                    }
                }
                */
                //TODO: set 0,0,0 as a no action coords ?
                //stopping the thrust and 
                /*
                if(distToTarget<5){
                    if(linearSpeedsShip.Length()<0.01){
                        if(distToGoal<safety_k*distWhenToStartBraking){
                            control = 0;
                           // release gyros when you stop stabilization
                           fightStabilizator.Release(); 
                        }
                    }
                }
                */

                //TODO control PID for thrust in space
                //control = 0;


                //control = MyMath.Clamp((float)control,(float) 0,(float) 0.3);

                physMass_N = physMass_kg * g;
                Echo("physMass_N:\n" + physMass_N);

                double remainingThrustToApply = -1;
                double temp_thr_n = -1;
                //space thrust management
                foreach (var c in cs)
                {
                    if (c.IsFunctional == true)
                    {
                        if (c.CubeGrid == flightIndicatorsShipController.CubeGrid)
                        {
                            //Echo("c.MaxEffectiveThrust:"+c.MaxEffectiveThrust);
                            c.ThrustOverride = Convert.ToSingle(200f);
                            if (c.MaxEffectiveThrust == 0)
                            {
                                continue;
                            }
                            if (c.Enabled == false)
                            {
                                continue;
                            }
                            if (remainingThrustToApply == -1)
                            {
                                remainingThrustToApply = (physMass_N * control * 1);
                                Echo("spaceRemainThrust:" + Math.Round(remainingThrustToApply, 3));
                            }
                            //Echo("c.CustomName:"+c.CustomName);
                            // Echo("physMass_N" + physMass_N);
                            // Echo("c.MaxThrust"+c.MaxThrust);
                            // Echo("c.MaxEffectiveThrust"+c.MaxEffectiveThrust);
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

                        }
                    }
                }

                //altitude management and downward speed management========================== end
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

                PlanetmaxAtmoRadius = 0;
                PlanetmaxR = 0;
                PlanetminR = 100000;
                PlanetCurrentG = 0;
                PlanetisTargetInTheSameGravityWheel = false;
                PlanetMaxG = 0;
                firstRunComingFromSpace = true;
                maxAtmoAltComingFromSpace = 0.0;



                //debug roll
                var str_to_display = "space mode\n"
                    + "\n1|" + Math.Round((V3Dgoal_speed.Length()), 1)
                    + "\n2|" + Math.Round((V3D_V_error_space.Length()), 1)
                    + "\n3|" + Math.Round((V_error_space), 1)
                    + "\n4|" + Math.Round((control), 2);

                    //+ "\n" 
                    /*
                    + "\n1|" + Math.Round((distPitch), 0) + "|1|" + Math.Round((distRoll), 0)
                    + "\n2|" + Math.Round((clampedDistPitch), 0) + "|2|" + Math.Round((clampedDistRoll), 0)
                    + "\n3|" + Math.Round((wantedSpeedPitch), 0) + "|3|" + Math.Round((wantedSpeedRoll), 0)
                    + "\n4|" + Math.Round((speedPitchError), 0) + "|4|" + Math.Round((speedRollError), 0)
                    + "\n5|" + Math.Round((anglePitch), 2) + "|5|" + Math.Round((angleRoll), 2)
                    + "\n6|" + Math.Round((forwardProjectUp.Length()), 2) + "|6|" + Math.Round((leftProjectUp.Length()), 2)
                    + "\n7|" + Math.Round((forwardProjPlaneVectorLength), 2) + "|7|" + Math.Round((leftProjPlaneVectorLength), 2);
                    */

                if (theAntenna != null)
                {
                    theAntenna.HudText = str_to_display;
                }
            }
            //space support WIP end
            //===================
            else
            {
                //gravity wheel parameters guessing start
                //Echo("VecPlanetCenter:"+Vector3D.Round(VecPlanetCenter,0));
                //Echo("distToPlanetCenter:"+distToPlanetCenter);

                PlanetCurrentG = (1.0 / 9.81) * g;
                //PlanetCurrentG = (0.1)*g;
                if (PlanetMaxG < g)
                {
                    PlanetMaxG = PlanetCurrentG;
                }
                if (PlanetminR > distToPlanetCenter)
                {
                    PlanetminR = distToPlanetCenter;
                }
                if (PlanetmaxR < distToPlanetCenter)
                {
                    PlanetmaxR = distToPlanetCenter;
                }

                PlanetmaxAtmoRadius = distToPlanetCenter * Math.Pow(PlanetCurrentG / 0.05, (1.0 / 7));

                PlanetisTargetInTheSameGravityWheel = false;
                if (maxAtmoAltComingFromSpace == 0.0)
                {
                    maxAtmoAltComingFromSpace = distToPlanetCenter;
                    PlanetmaxAtmoRadius = maxAtmoAltComingFromSpace;
                }

                Echo("CenterToTarget:" + Math.Round((VecPlanetCenter - vec3Dtarget).Length(), 1));
                Echo("PlanetmaxAtmoRadius:" + Math.Round(PlanetmaxAtmoRadius, 1));
                Echo("formula:" + Math.Round((PlanetmaxR * Math.Pow(PlanetMaxG / 0.05, (1.0 / 7))), 1));
                if ((VecPlanetCenter - vec3Dtarget).Length() < PlanetmaxAtmoRadius)
                {
                    PlanetisTargetInTheSameGravityWheel = true;
                }
                else
                {
                    PlanetisTargetInTheSameGravityWheel = false;
                }

                Echo("AtmoRadius:" + Math.Round(PlanetmaxAtmoRadius, 3));
                //Echo("PlanetmaxAtmoRadius:"+Math.Round(PlanetmaxAtmoRadius,3));
                Echo("PlanetmaxR:" + Math.Round(PlanetmaxR, 3));
                Echo("PlanetminR:" + Math.Round(PlanetminR, 3));
                Echo("PlanetCurrentG:" + Math.Round(PlanetCurrentG, 3));
                //Echo("PlanetisTargetInTheSameGravityWheel:"+PlanetisTargetInTheSameGravityWheel);
                Echo("TargetSameGravityWheel:" + PlanetisTargetInTheSameGravityWheel);
                //gravity wheel parameters guessing end


                Echo("anglePitch:" + Math.Round((anglePitch), 3));
                Echo("angleRoll:" + Math.Round((angleRoll), 3));

                Vector3D gravityNormalized = totalGravityVect3Dnormalized;

                double scaleForThetaRegardingGravity = Vector3D.Dot(gravityNormalized, shipDownVector);
                double thetaMustBe = 180 * Math.Acos(scaleForThetaRegardingGravity) / Math.PI;
                Echo("thetaMustBe3:" + Math.Round((thetaMustBe), 3));
                Echo("wantedAltitude3:" + Math.Round((wantedAltitude), 3));
                Echo("altitudeError3:" + Math.Round((altitudeError), 3));

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
                Echo("h_max_alt3:" + Math.Round((h_max_alt), 3));

                double clampAltError = MyMath.Clamp(Convert.ToSingle(altitudeError), Convert.ToSingle(-h_max_alt), Convert.ToSingle(h_max_alt));
                Echo("clampAltError3:" + Math.Round((clampAltError), 3));

                //TODO: make it asymetric
                wantedAlitudeSpeed = (clampAltError / h_max_alt) * V_max_altSpeed;
                Echo("wantedAlitudeSpeed3:" + Math.Round((wantedAlitudeSpeed), 3));

                double clampWantedAlitudeSpeed = MyMath.Clamp(Convert.ToSingle(wantedAlitudeSpeed), Convert.ToSingle(V_min_altSpeed), Convert.ToSingle(V_max_altSpeed));


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

                //Echo("distToTarget:"+distToTarget);
                //Echo("vec3Dtarget:"+vec3Dtarget);
                if (PlanetisTargetInTheSameGravityWheel == false)
                {
                    Echo("dts:" + dts);
                    if (dts > 0)
                    {
                        if (isTargetAboveTheHor == true)
                        {
                            if (Math.Abs(distPitch) < 500)
                            {
                                if (Math.Abs(distRoll) < 500)
                                {
                                    //very high value to force the control value to maintain about 75 vertical speed
                                    wantedAltitude = 411250;

                                    if (elev > 140)
                                    {
                                        clampWantedAlitudeSpeed = 95;
                                    }

                                }
                            }
                            else
                            {
                                clampWantedAlitudeSpeed = 75;
                            }

                            altitudeSpeedError = (clampWantedAlitudeSpeed - alt_speed_ms_1);
                            Echo("altitudeSpeedError1:" + Math.Round((altitudeSpeedError), 3));

                            controlAltSpeed = downwardSpeedAltRegulator.Control(altitudeSpeedError, dts);
                            Echo("controlAltSpeed1:" + Math.Round((controlAltSpeed), 3));

                            //feedback loop to counter the wrong speed
                            control = controlAltSpeed;
                        }
                    }
                }
                else
                {

                    if (firstRunComingFromSpace == true)
                    {
                        wantedAltitude = 1500;
                        firstRunComingFromSpace = false;
                    }


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
                                Echo("altitudeSpeedError2:" + Math.Round((altitudeSpeedError), 3));

                                controlAltSpeed = downwardSpeedAltRegulator.Control(altitudeSpeedError, dts);
                                Echo("controlAltSpeed2:" + Math.Round((controlAltSpeed), 3));

                                //feedback loop to counter the wrong speed
                                control = controlAltSpeed;
                                //}
                            }
                        }
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

                string isIt = "";
                if (PlanetisTargetInTheSameGravityWheel == true)
                {
                    isIt = "yes";
                }
                else
                {
                    isIt = "no";
                }

                //debug roll
                var str_to_display = Math.Round(PlanetmaxAtmoRadius, 1)
                    + "\n" + isIt
                    //+ "\n" + Math.Round(PlanetMaxG,3)
                    //+ "\n" + Math.Round(PlanetminR,0)
                    //+ "\n" + Math.Round(PlanetmaxR,0)
                    //+ "\n" + Math.Round(PlanetCurrentG,3)
                    //+ "\n" + Math.Round(PlanetCurrentG/0.05,3)

                    //+ "\n" 
                    + "\n1|" + Math.Round((distPitch), 0) + "|1|" + Math.Round((distRoll), 0)
                    + "\n2|" + Math.Round((clampedDistPitch), 0) + "|2|" + Math.Round((clampedDistRoll), 0)
                    + "\n3|" + Math.Round((wantedSpeedPitch), 0) + "|3|" + Math.Round((wantedSpeedRoll), 0)
                    + "\n4|" + Math.Round((speedPitchError), 0) + "|4|" + Math.Round((speedRollError), 0)
                    + "\n5|" + Math.Round((anglePitch), 2) + "|5|" + Math.Round((angleRoll), 2)
                    + "\n6|" + Math.Round((forwardProjectUp.Length()), 2) + "|6|" + Math.Round((leftProjectUp.Length()), 2)
                    + "\n7|" + Math.Round((forwardProjPlaneVectorLength), 2) + "|7|" + Math.Round((leftProjPlaneVectorLength), 2);

                if (theAntenna != null)
                {
                    theAntenna.HudText = str_to_display;
                }

                debugString += "\n" + "control:" + control;
                Echo("control:" + Math.Round((control), 2));

                double remainingThrustToApply = -1;
                double temp_thr_n = -1;

                foreach (var c in cs)
                {
                    if (c.IsFunctional == true)
                    {
                        if (c.CubeGrid == flightIndicatorsShipController.CubeGrid)
                        {
                            c.ThrustOverride = Convert.ToSingle(200f);
                            if (c.MaxEffectiveThrust == 0)
                            {
                                continue;
                            }
                            if (c.Enabled == false)
                            {
                                continue;
                            }
                            if (remainingThrustToApply == -1)
                            {
                                remainingThrustToApply = (1f * physMass_N * c.MaxThrust / c.MaxEffectiveThrust + (physMass_N * control * 1) * 1);
                                Echo("atmoRemainThrust:" + Math.Round(remainingThrustToApply, 3));
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

                            if (remainingThrustToApply == 0)
                            {
                                c.ThrustOverridePercentage = 0.001f;
                            }

                        }
                    }
                }
            }

            Echo("distRoll:" + Math.Round(distRoll, 2));
            Echo("distPitch:" + Math.Round(distPitch, 2));


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
                lcdHelper.DisplayMessage(DisplayText(), surfaceDisplays);
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
                        if (gyro.CubeGrid == shipController.CubeGrid)
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
            if (flightIndicatorsSurfaceDisplay.Count == 0)
            {
                flightIndicatorsSurfaceDisplay.Add(pbTextSurface);
            }
            if (flightIndicatorsSurfaceDisplay.Count == 0)
            {
                Echo("Cound not find any Surface");
            }
            else
            {
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
                            if (sc.CubeGrid == Me.CubeGrid)
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
                //myTextPanel.WritePublicText(message, append);
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
                //myTextPanel.ShowPublicTextOnScreen();
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


    }
}
