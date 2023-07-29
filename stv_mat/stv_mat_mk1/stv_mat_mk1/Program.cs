using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
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

        IMyCockpit theCockpit = null;

        //using the visual debugging API:
        DebugAPI Debug;

        int YellowLengthId;
        const double YellowLengthDefault = 5;



        //x,y,z coords
        Vector3D vec3Dtarget = new Vector3D(0, 0, 0);

        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project, 
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
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
            //Wheels = Blocks.FindAll(x => x.IsSameConstructAs(Me) && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();
            Gyros = Blocks.FindAll(x => x.IsSameConstructAs(Me) && x is IMyGyro).Select(x => x as IMyGyro).ToList();

            RemoteControl = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRemoteControl) as IMyRemoteControl;
            
            theAntenna = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRadioAntenna) as IMyRadioAntenna;

            Debug = new DebugAPI(this);

            Debug.PrintChat("Hello there.");

            // This allows local player to hold R and using mouse scroll, change that initial 5 by 0.05 per scroll. It will show up on HUD too when you do this.
            // Then the returned id can be used to retrieve this value.
            // For simplicity sake you should only call AddAdjustNumber() in the constructor here.
            Debug.DeclareAdjustNumber(out YellowLengthId, YellowLengthDefault, 0.05, DebugAPI.Input.R, "Yellow line length");

            Runtime.UpdateFrequency = UpdateFrequency.Update10;
        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {

            Vector3D vectorToAlignToward = new Vector3D(0, 0, 0);

            //Vector3D V3D_zero = new Vector3D(0, 0, 0);
            Vector3D V3D_zero = Vector3D.Zero;

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

            Vector3D gravityVector = RemoteControl.GetNaturalGravity();

            Vector3D VTT = vec3Dtarget - (Vector3D)RemoteControl.Position;

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
                maxEffectiveThrust_N += c.MaxEffectiveThrust; currentThrust_N += c.CurrentThrust;
            }
            //debugString += "\n" + "maxEffectiveThrust_N:" + maxEffectiveThrust_N;
            var thr_to_weight_ratio = maxEffectiveThrust_N / physMass_N;
            //debugString += "\n" + "thr_to_weight_ratio:" + thr_to_weight_ratio;
            double TWR = thr_to_weight_ratio;
            double V_max = 55;

            double leftOverTrust_N = maxEffectiveThrust_N - physMass_N;

            Echo("TWR:" + Math.Round(TWR, 2));

            double elev;
            RemoteControl.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);



            Vector3D RC_WP = RemoteControl.GetPosition();

            Echo("vec3Dtarget:" + Vector3D.Round(vec3Dtarget, 1));

            Vector3D displayMeV3D = Vector3D.Zero;



            Vector3D shipSettingVelProj = Vector3D.Zero;
            Vector3D wanted_sideway_speed = Vector3D.Zero;

            Vector3D error_sideways_speed = Vector3D.Zero;

            //thresholds for the vectorToAlignToward preping:
            if (vec3Dtarget == V3D_zero)
            {
                //Got a target
                //TODO: try to go there and land 
                Echo("if (vec3Dtarget == V3D_zero)");

                vectorToAlignToward = -(-gravityVector);

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
                Vector3D shipVelProjError = shipVelocities - shipVelOnGravPlane;

                Vector3D shipVelProjErrorNorm = Vector3D.Normalize(shipVelProjError);


                Vector3D VTToffset = vec3Dtarget - RemoteControl.GetPosition();

                //debug
                //displayMeV3D = VTToffset;

                //Vector3D shipSettingVel = 1 * Vector3D.Normalize(VTToffset);
                Vector3D shipSettingVel = VTToffset;

                Vector3D shipSettingVelOnGravPlane = VectorHelper.VectorProjection(shipSettingVel, gravNorm);
                //Vector3D shipSettingVelProj = shipSettingVel - shipSettingVelOnGravPlane;
                 shipSettingVelProj = shipSettingVel - shipSettingVelOnGravPlane;

                double lenV3D = shipSettingVelProj.Length();

                /*if (lenV3D > 55)
                {
                    shipSettingVelProj = 55 * Vector3D.Normalize(shipSettingVelProj);
                }*/

                //Vector3D shipSTV = (-shipSettingVelProj);

                /*
                wanted_sideway_speed = MyMath.Clamp((float)shipSettingVelProj.Length(), 0f, 55f)
                    * Vector3D.Normalize(-shipSettingVelProj);
                */

                wanted_sideway_speed = MyMath.Clamp((float)shipSettingVelProj.Length(), 0f, 100f)
                   * Vector3D.Normalize(shipSettingVelProj);

                error_sideways_speed =  shipVelProjError - wanted_sideway_speed;

                //displayMeV3D = shipSTV;

                //vectorToAlignToward = gravNorm * (1) + Vector3D.Normalize(wanted_sideway_speed) * 1;

                //vectorToAlignToward = (TWR - 1) * Vector3D.Normalize(error_sideways_speed) + 1 * gravNorm;

                //vectorToAlignToward = (1) * Vector3D.Normalize(error_sideways_speed) + (1) * gravNorm;
                //vectorToAlignToward = (1) * error_sideways_speed + (1) * gravNorm;
                //vectorToAlignToward = (1) * error_sideways_speed + (TWR-1) * gravNorm;


                //vectorToAlignToward = (1) * error_sideways_speed + (TWR - 1) * gravNorm;
                vectorToAlignToward = (1) * error_sideways_speed + (TWR - 1) * gravityVector;
                //vectorToAlignToward = shipSettingVelProj;

                //debug
                //displayMeV3D = shipVelProjError;

                //debug
                //displayMeV3D = vectorToAlignToward;

                double trust_to_apply_N = vectorToAlignToward.Length();

                Echo("else");

            }


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


            str_to_display = "";

            Vector3D worldDirection = gravityVector;
            //Vector3D worldDirection = vectorToAlignToward;

            // Convert worldDirection into a local direction
            Vector3D bodyVectorLocal = Vector3D.TransformNormal(worldDirection, MatrixD.Transpose(RemoteControl.WorldMatrix)); // Note that we transpose to go from world -> body

            Vector3D bodyVectorWorld = Vector3D.TransformNormal(bodyVectorLocal, RemoteControl.WorldMatrix); // Note that we transpose to go from world -> body


            //str_to_display = "" + Math.Round(shipForwardVector.Dot(vectorToAlignToward), 2);
            //str_to_display = "" + Math.Round(shipForwardVector.Dot(bodyVector), 2);
            //if we got a local vector, use a local matrix/vector to do the product
            str_to_display = "" + Math.Round(RemoteControl.WorldMatrix.Backward.Dot(bodyVectorLocal), 2);

            //help for debugging
            if (theAntenna != null)
            {
                theAntenna.HudText = str_to_display;
            }

            //ADD THE MOD TO THE WORLD YOU ARE TESTING ON
            try
            {
                Debug.RemoveDraw();
                //Debug.PrintChat("test");
                //Debug.PrintHUD($"Time is now: {DateTime.Now.ToLongTimeString()}");
                Debug.PrintChat(str_to_display);

                //Debug.PrintChat("" + Me.Position);
                Debug.PrintChat("=========");
                //Debug.PrintChat("" + bodyVectorLocal.Length());
                //Debug.PrintChat("" + RemoteControl.WorldMatrix.Up.Cross(gravityVector.Normalized()).Dot(RemoteControl.WorldMatrix.Right));
                //Debug.PrintChat("" + RemoteControl.WorldMatrix.Up.Cross(gravityVector.Normalized()).Dot(RemoteControl.WorldMatrix.Forward));


                Debug.PrintChat("pitch:" + (float)RemoteControl.WorldMatrix.Down.Cross(vectorToAlignToward.Normalized()).Dot(RemoteControl.WorldMatrix.Left));
                Debug.PrintChat("roll:" + (float)RemoteControl.WorldMatrix.Down.Cross(vectorToAlignToward.Normalized()).Dot(RemoteControl.WorldMatrix.Forward));

                Echo("vec3Dtarget:" + Vector3D.Round(vectorToAlignToward, 1));


                //Debug.DrawLine(RC_WP, RC_WP + displayMeV3D * 1, Color.Red, thickness: 0.11f, onTop: true);


                //gravity vector display
                Debug.DrawLine(RC_WP, RC_WP + gravityVector, Color.Red, thickness: 0.11f, onTop: true);

                //to target
                Debug.DrawLine(RC_WP,  vec3Dtarget , Color.Green, thickness: 0.01f, onTop: true);

                //ship wanted direction
                Debug.DrawLine(RC_WP, RC_WP + shipSettingVelProj, Color.Yellow, thickness: 0.01f, onTop: true);
                //ok
                
                // wanted sideways speed
                Debug.DrawLine(RC_WP, RC_WP + wanted_sideway_speed, Color.Purple, thickness: 0.02f, onTop: true);
                //ok

                // error sideways speed
                Debug.DrawLine(RC_WP + new Vector3D(0, 0, 1), RC_WP + error_sideways_speed, Color.White, thickness: 0.03f, onTop: true);


                //actual thrust vector
                Debug.DrawLine(RC_WP + new Vector3D(0, 0, 2), RC_WP + vectorToAlignToward, Color.Pink, thickness: 0.03f, onTop: true);


                Echo("RC_WP:" + RC_WP);
                Echo("vec3Dtarget:" + vec3Dtarget);
                Echo("vec3Dtarget.Length():" + vec3Dtarget.Length());

                Echo("wanted_sideway_speed:" + wanted_sideway_speed);
                Echo("error_sideways_speed:" + error_sideways_speed);



                Debug.PrintChat("drawing done");
            }
            catch (Exception e)
            {
                // example way to get notified on error then allow PB to stop (crash)
                Debug.PrintChat($"{e.Message}\n{e.StackTrace}", font: DebugAPI.Font.Red);
                Me.CustomData = e.ToString();
                throw;
            }

            int factor
                = 3;

            float pitchStg = factor * (float)RemoteControl.WorldMatrix.Down.Cross(vectorToAlignToward.Normalized()).Dot(RemoteControl.WorldMatrix.Left);
            float rollStg = factor * (float)RemoteControl.WorldMatrix.Down.Cross(vectorToAlignToward.Normalized()).Dot(RemoteControl.WorldMatrix.Forward);


            foreach (IMyGyro gyro in Gyros)
            {
                gyro.GyroOverride = true;
                //gyro.Roll = (float)speedForGyros.X;
                //gyro.Pitch = (float)speedForGyros.Y;
                gyro.Roll = rollStg;
                gyro.Pitch = pitchStg;
            }

            //end main
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
