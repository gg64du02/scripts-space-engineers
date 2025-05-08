using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.

        List<IMyShipConnector> listShipConnector = new List<IMyShipConnector>();
        IMyShipConnector theShipConnector = null;
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


            GridTerminalSystem.GetBlocksOfType<IMyShipConnector>(listShipConnector);

            if (listShipConnector.Count != 0)
            {
                foreach (IMyShipConnector connector in listShipConnector)
                {
                    if ((connector.CubeGrid == Me.CubeGrid))
                    {
                        if(connector.CustomName =="stv sgic")
                            theShipConnector = connector;
                    }
                }
            }


            if (theShipConnector != null)
            {
                Echo("is not null");
                Echo(theShipConnector.Name+"");
                Echo(theShipConnector.GetType() + "");
                Echo(theShipConnector.CustomName + "");
                //"Small Inset Connector"
            }
            else {
                Echo("is null");
            
            }

            Vector3D abs_conn = Vector3D.Zero;

            abs_conn = theShipConnector.GetPosition() + theShipConnector.WorldMatrix.Forward * (2.5 / 2); 

            Me.CustomData = "GPS:debug:" + abs_conn.X + ":" + abs_conn.Y + ":" + abs_conn.Z + ":" + "#FF75C9F1:";
        }
    }
}
