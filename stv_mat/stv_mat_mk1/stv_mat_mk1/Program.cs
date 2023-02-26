using Sandbox.Game.EntityComponents;
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

        public IMyRemoteControl RemoteControl;

        public Vector3D myTerrainTarget = new Vector3D(0, 0, 0);

        MyWaypointInfo myWaypointInfoTerrainTarget = new MyWaypointInfo("target", 0, 0, 0);

        IMyRadioAntenna theAntenna = null;

        IMyCockpit theCockpit = null;

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
            //Sensor = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMySensorBlock) as IMySensorBlock;


            theAntenna = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRadioAntenna) as IMyRadioAntenna;

            //theCockpit = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyCockpit) as IMyCockpit;

            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            //Runtime.UpdateFrequency = UpdateFrequency.Update100;
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

            Vector3D gravityVector = RemoteControl.GetNaturalGravity();

            Vector3D VTT = vec3Dtarget - (Vector3D) RemoteControl.Position;

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

            //thresholds for the vectorToAlignToward preping:
            if (vec3Dtarget != V3D_zero)
            {
                Echo("if (vec3Dtarget != V3D_zero)");
                //todo
                vectorToAlignToward = (-gravityVector);


                //vectorToAlignToward = ;

            }
            else
            {
                Echo("!if (vec3Dtarget != V3D_zero)");
                //WIP
                vectorToAlignToward = (-gravityVector);

                //minus the ship velocities projected on the plane normal to gravity vector
                //vectorToAlignToward += ;

                Vector3D gravNorm = Vector3D.Normalize(gravityVector);

                Vector3D shipVelOnGravPlane = VectorHelper.VectorProjection(shipVelocities, gravNorm);

                Vector3D oppShipVelOnGravPlane = -shipVelOnGravPlane;

                Vector3D oppShipVelOnGravPlaneNorm = Vector3D.Normalize(oppShipVelOnGravPlane);


                Vector3D v3d_grav_N = - gravityVector * physMass_kg ;


                //to be done
                Vector3D shipVelNormProjError = Vector3D.Zero - shipVelOnGravPlane;


                vectorToAlignToward = -v3d_grav_N + leftOverTrust_N * shipVelNormProjError;

                double trust_to_apply_N = vectorToAlignToward.Length();



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

    }
}
