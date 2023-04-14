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

        public List<IMyMotorSuspension> Wheels = new List<IMyMotorSuspension>();
        public IMyRemoteControl RemoteControl;

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
            Wheels = Blocks.FindAll(x => x.IsSameConstructAs(Me) && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();


            RemoteControl = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRemoteControl) as IMyRemoteControl;

            Runtime.UpdateFrequency = UpdateFrequency.Update10;

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

            Vector3D speedMe = RemoteControl.GetShipVelocities().LinearVelocity;

            float tmpFriction = 0f;

            tmpFriction = (float)((1f)*(100f - speedMe.Length() * 2.75f));

            Echo("tmpFriction1:"+tmpFriction);

            tmpFriction = MyMath.Clamp(tmpFriction, 27f, 100f);


            Echo("tmpFriction2:" + tmpFriction);

            foreach (IMyMotorSuspension Wheel in Wheels)
            {
                Wheel.SetValue<float>("Friction", tmpFriction);
            }
        }
    }
}
