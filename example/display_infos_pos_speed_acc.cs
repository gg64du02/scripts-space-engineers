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

double prev_v_x = 0;
double prev_v_y = 0;

double a_x = 0;
double a_y = 0;

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
    //useless
    //Echo("myCurrentCockpit.Orientation:" + myCurrentCockpit.Orientation);

    //how much time since the last loop
    var dts = Runtime.TimeSinceLastRun.TotalSeconds;

    //figuring out the acc for hor axis
    a_x = (prev_v_x - currentVelocity.LinearVelocity.X) / dts;
    a_y = (prev_v_y - currentVelocity.LinearVelocity.Y) / dts;

    //storing current speeds for next loop
    prev_v_x = currentVelocity.LinearVelocity.X;
    prev_v_y = currentVelocity.LinearVelocity.Y;


    debugString += "\n" + "myCurrentCockpit.RotationIndicator:\n" +  myCurrentCockpit.RotationIndicator;
    debugString += "\n" + "currentVelocity.LinearVelocity:\n" + currentVelocity.LinearVelocity;
    debugString += "\n" + "a_x,a_y:\n" + a_x + "," + a_y;
    //useless
    //debugString += "\n" + "myCurrentCockpit.Orientation:\n" + myCurrentCockpit.Orientation;


    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    textPanel.WriteText(debugString, false);
    //deprecated
    //textPanel.WritePublicText("debugString", false);
    //textPanel.WritePublicTitle("debugString", false);


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