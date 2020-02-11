#if DEBUG
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using VRageMath;
using VRage.Game;
using VRage.Collections;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game.EntityComponents;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;

namespace SpaceEngineers
{
    public sealed class Program : MyGridProgram
    {
#endif
//=======================================================================
//////////////////////////BEGIN//////////////////////////////////////////
//=======================================================================

public Program()
{
            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script.
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
            //This makes the program automatically run every 10 ticks.
}
public void Main()
{
    var debugString = "";

    double elev;
    var myCurrentCockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    myCurrentCockpit.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);

    Echo("myCurrentCockpit.RotationIndicator:" + myCurrentCockpit.RotationIndicator);
    var currentVelocity = myCurrentCockpit.GetShipVelocities();
    Echo("currentVelocity.LinearVelocity:" + currentVelocity.LinearVelocity);
    Echo("myCurrentCockpit.Orientation:" + myCurrentCockpit.Orientation);

    debugString += "\n" + "myCurrentCockpit.RotationIndicator:\n" +  myCurrentCockpit.RotationIndicator;
    debugString += "\n" + "currentVelocity.LinearVelocity:\n" + currentVelocity.LinearVelocity;
    debugString += "\n" + "myCurrentCockpit.Orientation:\n" + myCurrentCockpit.Orientation;



    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    textPanel.WritePublicText("debugString", false);
    textPanel.WritePublicTitle("debugString", false);
    textPanel.WriteText(debugString, false);
}

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means.

            // This method is optional and can be removed if not
            // needed.
        }

        //=======================================================================
        //////////////////////////END////////////////////////////////////////////
        //=======================================================================
#if DEBUG
    }
}
#endif