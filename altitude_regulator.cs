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
    //ok
    double elev;
    var myCurrentCockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    myCurrentCockpit.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);
    Echo(elev.ToString());
    var thrUp = GridTerminalSystem.GetBlockWithName("thrUp") as IMyThrust;
    double minAlt = 20;
    /*
    if (elev < minAlt)
    {
    thrUp.ThrustOverridePercentage += 0.2f;
    }
    else
    {
    thrUp.ThrustOverridePercentage -= 0.2f;
    }*/
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach(var c in cs)
    {
        Echo(c.CustomName + " " + c.GridThrustDirection);
        //Vector3I upVect = new 
        if (c.GridThrustDirection.Y == -1) { 
            Echo("hello");
            if (elev < minAlt)
            {
                //c.ThrustOverridePercentage += 0.05f;
                //c.ThrustOverridePercentage += System.Convert.ToDouble((1 / (Math.Abs(minAlt - elev))));
                //c.ThrustOverridePercentage += (1 / (Convert.ToSingle(Math.Abs(minAlt - elev))));
                c.ThrustOverridePercentage += Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)),2)); 
            }
            
            else
            {
                //c.ThrustOverridePercentage -= 0.05f;
                //c.ThrustOverridePercentage -= (1 / (Convert.ToSingle(Math.Abs(minAlt - elev))));
                //c.ThrustOverridePercentage -= Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)), 2);
                c.ThrustOverridePercentage -= Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)), 2));
            }
        }
        
        if (c.GridThrustDirection.Y == 1)
        {
            Echo("hello");
            if (elev < minAlt)
            {
                //c.ThrustOverridePercentage -= 0.05f;
                //c.ThrustOverridePercentage -= (1 / (Convert.ToSingle(Math.Abs(minAlt - elev))));
                c.ThrustOverridePercentage -= Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)), 2));
            }
            else
            {
                //c.ThrustOverridePercentage += 0.05f;
                //c.ThrustOverridePercentage += (1 / (Convert.ToSingle(Math.Abs(minAlt - elev))));
                c.ThrustOverridePercentage += 0.0000001f*Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)), 2));
            }
            
        }
        
    }
                
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