﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
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
    partial class Program : MyGridProgram
    {
        public List<IMyMotorSuspension> Wheels = new List<IMyMotorSuspension>();

        public List<IMyGyro> Gyros = new List<IMyGyro>();

        public IMyShipController RemoteControl = null;

        public Vector3D myTerrainTarget = new Vector3D(0, 0, 0);

        MyWaypointInfo myWaypointInfoTerrainTarget = new MyWaypointInfo("target", 0, 0, 0);

        IMyRadioAntenna theAntenna = null;

        string str_to_display = "";

        //using the visual debugging API:
        DebugAPI Debug;

        int YellowLengthId;
        const double YellowLengthDefault = 5;



        float control = 0;

        double altitude_m = 0;
        double altitude_error_m = 0;
        double altitude_settings_m = 0;
        double last_altitude_m = 0;

        double altitude_error_m_s = 0;
        double altitude_settings_m_s = 0;
        double altitude_speed_m_s = 0;

        //x,y,z coords
        Vector3D vec3Dtarget = new Vector3D(0, 0, 0);


        bool slow_landing_now = false;

        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.

        public Program()
        {
            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set Runtime.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.

            var Blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocks(Blocks);
            //Wheels = Blocks.FindAll(x => (x.CubeGrid == Me.CubeGrid) && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();
            Gyros = Blocks.FindAll(x => (x.CubeGrid == Me.CubeGrid) && x is IMyGyro).Select(x => x as IMyGyro).ToList();

            RemoteControl = Blocks.Find(x => (x.CubeGrid == Me.CubeGrid) && x is IMyRemoteControl) as IMyRemoteControl;

            theAntenna = Blocks.Find(x => (x.CubeGrid == Me.CubeGrid) && x is IMyRadioAntenna) as IMyRadioAntenna;

            Debug = new DebugAPI(this);

            Debug.PrintChat("Hello there.");

            // This allows local player to hold R and using mouse scroll, change that initial 5 by 0.05 per scroll. It will show up on HUD too when you do this.
            // Then the returned id can be used to retrieve this value.
            // For simplicity sake you should only call AddAdjustNumber() in the constructor here.
            Debug.DeclareAdjustNumber(out YellowLengthId, YellowLengthDefault, 0.05, DebugAPI.Input.R, "Yellow line length");

            //Runtime.UpdateFrequency = UpdateFrequency.Update10;
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.
        }

        public void Main(string argument, UpdateType updateSource)
        {
            // The main entry point of the script, invoked every time
            // one of the programmable block's Run actions are invoked,
            // or the script updates itself. The updateSource argument
            // describes where the update came from. Be aware that the
            // updateSource is a  bitfield  and might contain more than 
            // one update type.
            // 
            // The method itself is required, but the arguments above
            // can be removed if not needed.

            Vector3D vectorToAlignToward = new Vector3D(0, 0, 0);

            //Vector3D V3D_zero = new Vector3D(0, 0, 0);
            Vector3D V3D_zero = Vector3D.Zero;

            //Echo("targetGpsString:" + targetGpsString);
            MyWaypointInfo myWaypointInfoTarget = new MyWaypointInfo("lol", 0, 0, 0);

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
                        //no need to hit Recompile anymore, would take off if drifting because of wheels though
                        slow_landing_now = false;
                    }
                }
            }

            Vector3D gravityVector = RemoteControl.GetNaturalGravity();

            Vector3D RC_WP = RemoteControl.GetPosition();

            //Vector3D RC_WP = RemoteControl.CenterOfMass;

            Vector3D VTT = vec3Dtarget - RC_WP;

            Vector3D shipVelocities = RemoteControl.GetShipVelocities().LinearVelocity;


            double g = gravityVector.Length();

            var physMass_kg = RemoteControl.CalculateShipMass().PhysicalMass;
            double physMass_N = physMass_kg * g;
            double maxEffectiveThrust_N = 0;
            double currentThrust_N = 0;
            var cs = new List<IMyThrust>();
            GridTerminalSystem.GetBlocksOfType(cs);
            foreach (var c in cs)
            {
                if (c.CubeGrid == Me.CubeGrid)
                {
                    maxEffectiveThrust_N += c.MaxEffectiveThrust; currentThrust_N += c.CurrentThrust;
                }
            }

            var thr_to_weight_ratio = maxEffectiveThrust_N / physMass_N;

            double TWR = thr_to_weight_ratio;

            double leftOverTrust_N = maxEffectiveThrust_N - physMass_N;

            Echo("TWR:" + Math.Round(TWR, 2));

            double elev;
            RemoteControl.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);



            Echo("vec3Dtarget:" + Vector3D.Round(vec3Dtarget, 1));

            Vector3D displayMeV3D = Vector3D.Zero;


            double dts = Runtime.TimeSinceLastRun.TotalSeconds;
            Echo("dts:" + dts);
            double dts2 = Runtime.LastRunTimeMs;
            Echo("dts2:" + dts2);

            if (dts == 0)
            {
                return;
            }


            Vector3D shipSettingVelProj = Vector3D.Zero;
            Vector3D wanted_sideway_speed = Vector3D.Zero;

            Vector3D error_sideways_speed = Vector3D.Zero;


            //generating a vector from the current position to the center of the planet
            Vector3D VecPlanetCenter = new Vector3D(0, 0, 0);
            RemoteControl.TryGetPlanetPosition(out VecPlanetCenter);
            MyShipVelocities myShipVel = RemoteControl.GetShipVelocities();
            Vector3D linearSpeedsShip = myShipVel.LinearVelocity;


            //thresholds for the vectorToAlignToward preping:
            if (vec3Dtarget == V3D_zero)
            {
                //Got a target//
                // try to go there and land 
                Echo("if (vec3Dtarget == V3D_zero)");

                vectorToAlignToward = -(-gravityVector);
                //minus the ship velocities projected on the plane normal to gravity vector
                //vectorToAlignToward += ;

                Vector3D gravNorm = Vector3D.Normalize(gravityVector);

                Vector3D shipVelOnGravPlane = VectorHelper.VectorProjection(shipVelocities, gravNorm);

                //debug
                //displayMeV3D = oppShipVelOnGravPlaneNorm;

                Vector3D v3d_grav_N = -gravityVector * physMass_kg;

                //ship velocities projected normal to the gravity
                Vector3D shipVelProj = shipVelocities - shipVelOnGravPlane;

                vectorToAlignToward = -(-gravityVector) + shipVelProj;



                Echo("if end");
            }
            else
            {
                //No target
                //TODO: try to stop and land 
                Echo("!if (vec3Dtarget == V3D_zero)");
                Echo("vec3Dtarget:" + Vector3D.Round(vec3Dtarget, 3));
                //WIP
                vectorToAlignToward = -(-gravityVector);

                //minus the ship velocities projected on the plane normal to gravity vector
                //vectorToAlignToward += ;

                Vector3D gravNorm = Vector3D.Normalize(gravityVector);

                Vector3D shipVelOnGravPlane = VectorHelper.VectorProjection(shipVelocities, gravNorm);

                //debug
                //displayMeV3D = oppShipVelOnGravPlaneNorm;

                Vector3D v3d_grav_N = -gravityVector * physMass_kg;

                //ship velocities projected normal to the gravity
                Vector3D shipVelProj = shipVelocities - shipVelOnGravPlane;

                Vector3D shipVelProjErrorNorm = Vector3D.Normalize(shipVelProj);


                Vector3D VTToffset = vec3Dtarget - RC_WP;


                //debug
                //displayMeV3D = VTToffset;

                Vector3D VTToffsetOnGravPlane = VectorHelper.VectorProjection(VTToffset, gravNorm);
                Vector3D VTToffsetProj = VTToffset - VTToffsetOnGravPlane;


                Vector3D shipSettingVel = VTToffset;

                Vector3D shipSettingVelOnGravPlane = VectorHelper.VectorProjection(shipSettingVel, gravNorm);

                shipSettingVelProj = shipSettingVel - shipSettingVelOnGravPlane;

                double lenV3D = shipSettingVelProj.Length();


                wanted_sideway_speed = MyMath.Clamp((float)shipSettingVelProj.Length(), 0f, 100f)
                   * Vector3D.Normalize(shipSettingVelProj);


                float temp_speed_math = (float)Math.Sqrt((float)VTToffset.Length() * 2 * (1) * 9.8);

                //float temp_speed_math_res = MyMath.Clamp(temp_speed_math, -100f, 100f);
                float temp_speed_math_res = MyMath.Clamp(temp_speed_math, -98f, 98f);

                //shipVelOnGravPlane is the vertical vel
                //shipVelProj is the sideways/surface speed

                //error_sideways_speed = shipVelProj; //cancel out the surface speed

                //error_sideways_speed = Vector3D.Normalize(-VTT) * temp_speed_math_res;


                //error_sideways_speed =  Vector3D.Normalize(-VTToffsetProj) * temp_speed_math_res  ; //pointing at the target starting from zero


                if (VTToffsetProj.Length() < 400)
                {
                    if (VTToffsetProj.Length() > 100)
                    {
                        temp_speed_math_res = temp_speed_math_res / 2;
                    }
                }

                if (VTToffsetProj.Length() < 100)
                {
                    temp_speed_math_res = temp_speed_math_res / 10;
                }
                if (VTToffsetProj.Length() < 4)
                {
                    temp_speed_math_res = temp_speed_math_res / 10;
                }

                error_sideways_speed = Vector3D.Normalize(-VTToffsetProj) * temp_speed_math_res + shipVelProj;



                error_sideways_speed = MyMath.Clamp((float)error_sideways_speed.Length(), 0f, 11f)
                //error_sideways_speed = MyMath.Clamp((float)error_sideways_speed.Length(), 0f, .1f)
                   * Vector3D.Normalize(error_sideways_speed);

                //vectorToAlignToward = error_sideways_speed * Vector3D.Normalize(shipSettingVelProj);
                //vectorToAlignToward = error_sideways_speed;

                vectorToAlignToward = error_sideways_speed + gravityVector;

                //vectorToAlignToward = vectorToAlignToward + shipVelOnGravPlane;


                double trust_to_apply_N = vectorToAlignToward.Length();

                Echo("else");


                altitude_m = elev;


                Echo("altitude_settings_m:" + altitude_settings_m);



                Vector3D myPos = RC_WP;



                if (dts > 0)
                {
                    if (vec3Dtarget != new Vector3D(0, 0, 0))
                    {

                        //fallingRange(gravityVector, linearSpeedsShip, dts, myPos);
                        float falling_range = 0;
                        //falling_range = fallingRange(gravityVector, linearSpeedsShip, dts, myPos, vec3Dtarget, VecPlanetCenter);
                        float speedFactor = 10.0f;
                        falling_range = fallingRange(gravityVector, linearSpeedsShip, speedFactor * dts, myPos, vec3Dtarget, VecPlanetCenter);

                        //TODO: use a spreasheet to make an array of the rules for the controls

                        float offset_alt = 30;

                        if (falling_range < 30.0f)
                        {
                            altitude_settings_m = 35 + offset_alt;
                            if (VTToffsetProj.Length() < 600)
                            {
                                //altitude_settings_m = 50 + offset_alt;
                            }
                            if (VTToffsetProj.Length() < 30)
                            {
                                altitude_settings_m = 50 + offset_alt;
                            }
                            if (VTToffsetProj.Length() < 1)
                            {
                                altitude_settings_m = 0;
                                slow_landing_now = true;
                            }
                            //vectorToAlignToward = vectorToAlignToward - gravityVector;
                        }
                        else
                        {
                            altitude_settings_m = 125;
                            //altitude_settings_m = 25;
                            if (VTToffsetProj.Length() > 1500.0f)
                            {
                                altitude_settings_m = 1250;
                            }
                            if (VTToffsetProj.Length() < 30)
                            {
                                altitude_settings_m = 50 + offset_alt;
                            }
                            if (VTToffsetProj.Length() < .5)
                            {
                                altitude_settings_m = 0;
                            }

                        }

                    }
                }


            }
            bool hitRecompile = false;
            if (vec3Dtarget == Vector3D.Zero)
            {
                altitude_settings_m = 125;
                // 10 * 10 must be above the slow landing speed !!! (-3 squared right now)
                if(linearSpeedsShip.LengthSquared()<10 * 10)
                {
                    hitRecompile = true;
                }
            }

            altitude_m = elev;

            altitude_error_m = altitude_settings_m - altitude_m;

            altitude_speed_m_s = (altitude_m - last_altitude_m) / dts;



            //alti

            // .5 * m * v^2 = m g h
            // v^2 = 2gh
            // v = sqrt (2gh)

            if (slow_landing_now == false)
            {
                altitude_settings_m_s = Math.Sign(altitude_error_m) *
                Math.Sqrt((double)(2 * gravityVector.Length() * (float)Math.Abs(altitude_error_m)));
            }
            else
            {
                altitude_settings_m_s = -3;
            }

            if (hitRecompile == true)
            {
                altitude_settings_m_s = -3;
            }

            altitude_error_m_s = altitude_settings_m_s - altitude_speed_m_s;

            //altitude_settings_m_s = MyMath.Clamp( (float) altitude_settings_m_s, -100f, 100f);

            control = (float)altitude_error_m_s;

            //;ini;u; set to one to avoid bad crashes during debugging
            //control = MyMath.Clamp((float)altitude_error_m_s, 1f, 100f); ;

            //survival settings
            control = MyMath.Clamp((float)altitude_error_m_s, 0f, 100f); ;
            //control = MyMath.Clamp((float)altitude_error_m_s, -100f, 100f); ;
            //control = 1f;

            Echo("control:" + control);

            //control = MyMath.Clamp((float)altitude_error_m_s, 0f, 4f); ;

            last_altitude_m = elev;

            //str_to_display = "" + "control:" + control;
            str_to_display = "";
            //str_to_display += "\n1:" + slow_landing_now;
            //str_to_display += "\n1:" + hitRecompile;
            //str_to_display += "\n1:" + Math.Round(linearSpeedsShip.LengthSquared(), 1);
            str_to_display += "\n1:" + Math.Round(altitude_settings_m, 1);
            str_to_display += "\n2:" + Math.Round(altitude_error_m, 1);
            str_to_display += "\n31:" + Math.Round(altitude_m, 3);
            //str_to_display += "\n32:" + Math.Round(last_altitude_m, 3);
            str_to_display += "\n5:" + Math.Round(altitude_error_m_s, 1);
            str_to_display += "\n6:" + Math.Round(altitude_speed_m_s, 1);
            //str_to_display += "\n7:" + "====";
            str_to_display += "\n8:" + Math.Round(control, 3);

            Echo("str_to_display:" + str_to_display);


            /*
            //Me.CustomData = debugOK;
            Debug.RemoveAll();

            float cellSize = Me.CubeGrid.GridSize;
            MatrixD pbm = Me.WorldMatrix;
            //Debug.DrawGPS("I'm here!", pbm.Translation + pbm.Backward * (cellSize / 2), Color.Blue);

                
            Debug.DrawGPS("ship", Me.GetPosition() + pbm.Backward * (cellSize / 2), Color.Blue);
            //Debug.DrawGPS("POI!", resultShipPosition + pbm.Backward * (cellSize / 2), Color.Red);
            Debug.DrawLine(Me.GetPosition(), Me.GetPosition() + vectorToAlignToward, Color.Red, 0.1f, 0.016f);
            Debug.DrawLine(Me.GetPosition(), Me.GetPosition() + RemoteControl.WorldMatrix.Left,
                Color.Yellow, 0.1f, 0.016f);
            Debug.DrawLine(Me.GetPosition(), Me.GetPosition() + RemoteControl.WorldMatrix.Forward,
                Color.Purple, 0.1f, 0.016f);
            */
            //}



            //getting vectors to help with angles proposals
            Vector3D shipForwardVector = RemoteControl.WorldMatrix.Forward;
            Vector3D shipLeftVector = RemoteControl.WorldMatrix.Left;
            Vector3D shipDownVector = RemoteControl.WorldMatrix.Down;


            //todo: extract angles from vectorToAlignToward to apply to gyros:

            Vector3D VectorToPointToward = Vector3D.Normalize(vectorToAlignToward);

            //todo: use the matrix of the block not the world matrix
            MatrixD RCWorldMatrix = RemoteControl.WorldMatrix;
            //MatrixD RCWorldMatrix = RemoteControl.;


            //transform VectorToPointToward to local position ?
            Vector3D anglesForGyros = Vector3D.TransformNormal(VectorToPointToward, RCWorldMatrix);

            Echo("VectorToPointToward:" + Vector3D.Round(VectorToPointToward, 3));
            Echo("gravityVector:" + Vector3D.Round(gravityVector, 3));
            Echo("anglesForGyros:" + Vector3D.Round(anglesForGyros, 3));
            Echo("shipForwardVector:" + Vector3D.Round(shipForwardVector, 3));
            Vector3D shipRightVector = RemoteControl.WorldMatrix.Right;
            Echo("shipRightVector:" + Vector3D.Round(shipRightVector, 3));

            int max_rpm = 1;

            Vector3D speedForGyros = anglesForGyros * max_rpm;

            Echo("speedForGyros:" + Vector3D.Round(speedForGyros, 3));



            Vector3D worldDirection = gravityVector;
            //Vector3D worldDirection = vectorToAlignToward;

            // Convert worldDirection into a local direction
            Vector3D bodyVectorLocal = Vector3D.TransformNormal(worldDirection, MatrixD.Transpose(RemoteControl.WorldMatrix)); // Note that we transpose to go from world -> body

            Vector3D bodyVectorWorld = Vector3D.TransformNormal(bodyVectorLocal, RemoteControl.WorldMatrix); // Note that we transpose to go from world -> body



            //help for debugging
            if (theAntenna != null)
            {
                theAntenna.HudText = str_to_display;
            }


            int factor = 1;



            //TODO: warning mind the axises of gyros and remote control axises

            foreach (IMyGyro gyro in Gyros)
            {
                gyro.GyroOverride = true;

                //both must be 1 or 0.98

                //float rollStg = factor * (float)vectorToAlignToward.Normalized().
                //    Dot(RemoteControl.WorldMatrix.Forward);
                float rollStg = factor * (float)vectorToAlignToward.Normalized().
                    Dot(gyro.WorldMatrix.Right);
                Echo("gyroFor and RCLeft aligned:" + gyro.WorldMatrix.Forward.
                    Dot(RemoteControl.WorldMatrix.Left));
                gyro.Roll = -rollStg;

                //Roll is on the Left/Right axis


                //float pitchStg = factor * (float)vectorToAlignToward.Normalized().
                //   Dot(RemoteControl.WorldMatrix.Left);
                float pitchStg = factor * (float)vectorToAlignToward.Normalized().
                    Dot(gyro.WorldMatrix.Forward);
                Echo("gyroLeft and RCBack aligned:" + gyro.WorldMatrix.Left.
                    Dot(RemoteControl.WorldMatrix.Backward));

                gyro.Pitch = -pitchStg;

                //Pitch is on the Forward/Backward axis

                //rollStg is Right
                //pitch is Forward
            }

            //end main



            double remainingThrustToApply = -1;
            double temp_thr_n = -1;

            foreach (var c in cs)
            {
                if (c.CubeGrid == Me.CubeGrid)
                {
                    if (c.IsFunctional == true)
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

                        if (remainingThrustToApply == 0)
                        {
                            c.ThrustOverridePercentage = 0.00001f;
                        }
                    }
                }
            }


            //trust control end
        }

        //distance made before touching the ground without thrust
        //public float fallingRange(Vector3D gravity , Vector3D shipSpeed , 
        //double timeStep, Vector3D shipPosition){
        public float fallingRange(Vector3D gravity, Vector3D shipSpeed,
        double timeStep, Vector3D shipPosition, Vector3D target, Vector3D centerPlanet)
        {
            //resultHorizontal
            float resultHorizontal = 0;
            float resultVertical = 0;

            float fallingRange = 0;

            Vector3D resultShipPosition = shipPosition;

            //parameter
            float maxSpeed = 100.0f;

            float secondToLookInFutur = 100.0f;

            //int maximumInt = 100;
            int maximumInt = (int)(secondToLookInFutur / (float)timeStep);

            float maximumFallingAltitude = 2000;

            //local variables
            float lastStepDoneHor = 10.0f;
            float lastStepDoneVer = 10.0f;

            Vector3D tmpShipSpeed = shipSpeed;

            Vector3D tmpLocalTarget = target - centerPlanet;
            Vector3D tmpLocalPOI = new Vector3D(0, 0, 0);

            Echo("timeStep:" + timeStep);
            Echo("maximumInt:start:" + maximumInt);


            Debug.RemoveAll();


            //trying to look in the futur where it would land
            while (true)
            {

                tmpShipSpeed += timeStep * gravity;

                if (tmpShipSpeed.LengthSquared() > 10000)
                {
                    tmpShipSpeed = maxSpeed * Vector3D.Normalize(tmpShipSpeed);
                }

                //display as a "field" of vector
                //Debug.DrawLine(Me.GetPosition(), Me.GetPosition() + tmpShipSpeed * timeStep, Color.Red, 0.01f, 0.016f);


                resultShipPosition += tmpShipSpeed * timeStep;


                //display the curve
                Debug.DrawLine(resultShipPosition, resultShipPosition + tmpShipSpeed * timeStep, Color.Red, 1.11f, 0.016f);

                Vector3D VectorProjFuturHor = tmpShipSpeed - VectorHelper.VectorProjection(tmpShipSpeed, gravity);
                Vector3D VectorProjFuturVer = VectorHelper.VectorProjection(tmpShipSpeed, gravity);


                //last speed made on the plane normal to gravity (on the ground)
                lastStepDoneHor = (float)VectorProjFuturHor.Length();
                lastStepDoneVer = (float)VectorProjFuturVer.Length();

                //adding delta distance made during the last timestep
                resultHorizontal += (float)lastStepDoneHor * (float)timeStep;

                resultVertical += (float)lastStepDoneVer * (float)timeStep;


                if (maximumFallingAltitude < resultVertical)
                {
                    break;
                }


                //fail safe
                maximumInt -= 1;
                if (maximumInt < 0)
                {
                    break;
                }

                tmpLocalPOI = resultShipPosition - centerPlanet;

                Vector3D tmpDistancePOI_target =
                ((float)tmpLocalTarget.Length()) * Vector3D.Normalize(tmpLocalPOI) -
                tmpLocalTarget;

                float dist_POI_target = (float)tmpDistancePOI_target.Length();

                //POI distance to target
                fallingRange = (float)Math.Round((resultShipPosition - target).Length(), 1);

                if (fallingRange < 5.0f)
                {
                    // Debug.PrintChat("if (fallingRange < 5.0f)");
                    break;
                }

                //stop looping if the trajectory overshoot
                if (tmpLocalTarget.Length() > tmpLocalPOI.Length() + 30.0f)
                {
                    //Debug.PrintChat("if (tmpLocalTarget.Length() > tmpLocalPOI.Length() + 30.0f)");
                    break;
                }




            }

            Echo("maximumInt:end:" + maximumInt);
            Echo("resultHor:" + resultHorizontal);
            Echo("resultVer:" + resultVertical);
            Echo("resultShipPosition:" + Vector3D.Round(resultShipPosition, 1));

            Echo("fallingRange:" + Math.Round(fallingRange, 1));



            //GPS:OuiOuiOui #2:1076588.43:114319.88:1670351.32:#FF82F175:

            string debugOK = "GPS:debugOK:";
            debugOK += Math.Round(resultShipPosition.X, 0) + ":";
            debugOK += Math.Round(resultShipPosition.Y, 0) + ":";
            debugOK += Math.Round(resultShipPosition.Z, 0) + ":";

            //Echo(debugOK);

            Me.CustomData = debugOK;

            float cellSize = Me.CubeGrid.GridSize;
            MatrixD pbm = Me.WorldMatrix;
            //Debug.DrawGPS("I'm here!", pbm.Translation + pbm.Backward * (cellSize / 2), Color.Blue);

            //Debug.DrawGPS("ship", shipPosition + pbm.Backward * (cellSize / 2), Color.Blue);
            Debug.DrawGPS("POI!", resultShipPosition + pbm.Backward * (cellSize / 2), Color.Red);
            //Debug.DrawGPS("" + Math.Round(fallingRange, 1), resultShipPosition + pbm.Backward * (cellSize / 2), Color.Red);
            //Debug.DrawGPS("" + Math.Round((resultShipPosition - target).Length(), 1), resultShipPosition + pbm.Backward * (cellSize / 2), Color.OrangeRed);


            //Debug.PrintChat("IC:" + Runtime.CurrentInstructionCount);
            Echo("IC:" + Runtime.CurrentInstructionCount);

            /*
            public float fallingRange(Vector3D gravity, Vector3D shipSpeed,
            double timeStep, Vector3D shipPosition, Vector3D target, Vector3D centerPlanet)
            */

            //gravity is the vector normal to the plane we to get the intersect of the curve with
            //Plane equation is : ax + by + cz + d = 0
            //with (a,b,c) normal to the plane

            //target belongs to this plane

            float offsetD = 0.0f;
            /*
             *  float interX1, interY1, interZ1 = 0.0f;
            gravity.X * (interX1 - target.X) + gravity.Y * (interY1 - target.Y)
                + gravity.Z * (interZ1 - target.Z) + offsetD = 0;
            */

            //make a point that belong to the gravity plane (to compute D):
            Vector3D anyVector = gravity.Cross(tmpShipSpeed) + target;//todo check this

            offsetD = (float)-(gravity.X * (anyVector.X - target.X) + gravity.Y * (anyVector.Y - target.Y) + gravity.Z * (anyVector.Z - target.Z));//

            //Line is resultShipPosition + tmpShipSpeed * timeStep
            //a point belong to resultShipPosition

            Vector3D endOfCurveLine = tmpShipSpeed * timeStep;
            Vector3D endOfCurvePoint = resultShipPosition;

            // x = endOfCurveLine.X * t + endOfCurvePoint.X
            // y = endOfCurveLine.Y * t + endOfCurvePoint.Y
            // z = endOfCurveLine.Z * t + endOfCurvePoint.Z

            /*
            gravity.X * (endOfCurveLine.X * t + endOfCurvePoint.X - target.X) 
                + gravity.Y * (endOfCurveLine.Y * t + endOfCurvePoint.Y - target.Y)
                + gravity.Z * (endOfCurveLine.Z * t + endOfCurvePoint.Z - target.Z) + offsetD = 0;
            */


            /*            gravity.X * (endOfCurveLine.X * t) 
                + gravity.Y * (endOfCurveLine.Y * t)
                + gravity.Z * (endOfCurveLine.Z * t)  = 
            - offsetD 
            - gravity.X * (endOfCurvePoint.X - target.X)
            - gravity.Y * (endOfCurvePoint.Y - target.Y)
            - gravity.Z * (endOfCurvePoint.Z - target.Z);
             */
            float t = 0.0f;

            float top = (float)
            (offsetD + gravity.X * (endOfCurvePoint.X - target.X) + gravity.Y * (endOfCurvePoint.Y - target.Y) + gravity.Z * (endOfCurvePoint.Z - target.Z));
            float bottom = (float)
                (gravity.X * endOfCurveLine.X + gravity.Y * endOfCurveLine.Y + gravity.Z * endOfCurveLine.Z);

            t = (float)(-top / bottom);

            Echo("t:" + t);

            //Plugging t in the line equation:

            Vector3D pointOfIntersect = new Vector3D(
                endOfCurveLine.X * t + endOfCurvePoint.X,
                endOfCurveLine.Y * t + endOfCurvePoint.Y,
                endOfCurveLine.Z * t + endOfCurvePoint.Z);


            //Debug.DrawGPS("POI!2", pointOfIntersect + pbm.Backward * (cellSize / 2), Color.Purple);


            PlaneD planeGrav = new PlaneD(target, gravity);

            RayD rayCurve = new RayD(resultShipPosition, tmpShipSpeed);

            double? interReturn = rayCurve.Intersects(planeGrav);

            if (interReturn.HasValue == true)
            {
                Vector3D result = rayCurve.Position + ((double)interReturn) * rayCurve.Direction;

                Debug.DrawGPS("POI!3", result + pbm.Backward * (cellSize / 2), Color.Orange);
            }
            else
            {

                rayCurve = new RayD(resultShipPosition, -tmpShipSpeed);

                interReturn = rayCurve.Intersects(planeGrav);

                Vector3D result = rayCurve.Position + ((double)interReturn) * rayCurve.Direction;

                Debug.DrawGPS("POI!4", result + pbm.Backward * (cellSize / 2), Color.Black);

            }




            return fallingRange;

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



        /// <summary>
        /// Create an instance of this and hold its reference.
        /// </summary>
        public class DebugAPI
        {
            public readonly bool ModDetected;

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

            public int DrawPoint(Vector3D origin, Color color, float radius = 0.2f, float seconds = DefaultSeconds, bool? onTop = null) => _point?.Invoke(_pb, origin, color, radius, seconds, onTop ?? _defaultOnTop) ?? -1;
            Func<IMyProgrammableBlock, Vector3D, Color, float, float, bool, int> _point;

            public int DrawLine(Vector3D start, Vector3D end, Color color, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _line?.Invoke(_pb, start, end, color, thickness, seconds, onTop ?? _defaultOnTop) ?? -1;
            Func<IMyProgrammableBlock, Vector3D, Vector3D, Color, float, float, bool, int> _line;

            public int DrawAABB(BoundingBoxD bb, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _aabb?.Invoke(_pb, bb, color, (int)style, thickness, seconds, onTop ?? _defaultOnTop) ?? -1;
            Func<IMyProgrammableBlock, BoundingBoxD, Color, int, float, float, bool, int> _aabb;

            public int DrawOBB(MyOrientedBoundingBoxD obb, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _obb?.Invoke(_pb, obb, color, (int)style, thickness, seconds, onTop ?? _defaultOnTop) ?? -1;
            Func<IMyProgrammableBlock, MyOrientedBoundingBoxD, Color, int, float, float, bool, int> _obb;

            public int DrawSphere(BoundingSphereD sphere, Color color, Style style = Style.Wireframe, float thickness = DefaultThickness, int lineEveryDegrees = 15, float seconds = DefaultSeconds, bool? onTop = null) => _sphere?.Invoke(_pb, sphere, color, (int)style, thickness, lineEveryDegrees, seconds, onTop ?? _defaultOnTop) ?? -1;
            Func<IMyProgrammableBlock, BoundingSphereD, Color, int, float, int, float, bool, int> _sphere;

            public int DrawMatrix(MatrixD matrix, float length = 1f, float thickness = DefaultThickness, float seconds = DefaultSeconds, bool? onTop = null) => _matrix?.Invoke(_pb, matrix, length, thickness, seconds, onTop ?? _defaultOnTop) ?? -1;
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

            public enum Style { Solid, Wireframe, SolidAndWireframe }
            public enum Input { MouseLeftButton, MouseRightButton, MouseMiddleButton, MouseExtraButton1, MouseExtraButton2, LeftShift, RightShift, LeftControl, RightControl, LeftAlt, RightAlt, Tab, Shift, Control, Alt, Space, PageUp, PageDown, End, Home, Insert, Delete, Left, Up, Right, Down, D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NumPad0, NumPad1, NumPad2, NumPad3, NumPad4, NumPad5, NumPad6, NumPad7, NumPad8, NumPad9, Multiply, Add, Separator, Subtract, Decimal, Divide, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12 }
            public enum Font { Debug, White, Red, Green, Blue, DarkBlue }

            const float DefaultThickness = 0.02f;
            const float DefaultSeconds = -1;

            IMyProgrammableBlock _pb;
            bool _defaultOnTop;

            /// <summary>
            /// NOTE: if mod is not present then methods will simply not do anything, therefore you can leave the methods in your released code.
            /// </summary>
            /// <param name="program">pass `this`.</param>
            /// <param name="drawOnTopDefault">set the default for onTop on all objects that have such an option.</param>
            public DebugAPI(MyGridProgram program, bool drawOnTopDefault = false)
            {
                if (program == null)
                    throw new Exception("Pass `this` into the API, not null.");

                _defaultOnTop = drawOnTopDefault;
                _pb = program.Me;

                var methods = _pb.GetProperty("DebugAPI")?.As<IReadOnlyDictionary<string, Delegate>>()?.GetValue(_pb);
                if (methods != null)
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

                    RemoveAll(); // cleanup from past compilations on this same PB

                    ModDetected = true;
                }
            }

            void Assign<T>(out T field, object method) => field = (T)method;
        }
    }



}
