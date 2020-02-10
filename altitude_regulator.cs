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
    //PID constants
    double K_p = 1;
    double T_i = 0;
    double T_d = 0;

    /*
     * r(t) = define the altitude you want
     * e(t) = r(t) - y(t) // error
     * y(t) = the altitude
     * p(t) = proportionnal
     * i(t) = integral
     * d(t) = derivative
     * u(t) = p + i + d 
     * */

    double r_t = 20;
    double e_t = 0;
    double y_t = 0;
    double p_t = 0;
    double i_t = 0;
    double d_t = 0;
    double u_t = 0;

    double elev;
    var myCurrentCockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    myCurrentCockpit.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);
    y_t = elev;
    Echo("y_t:" + y_t.ToString());

    e_t = r_t - y_t;

    //TODO:need update for i and d
    u_t = K_p * e_t + T_i * i_t + T_d * d_t;
    Echo("u_t:" + u_t.ToString());

    //applying what the pid processed
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        Echo(c.CustomName + " " + c.GridThrustDirection);
        //Vector3I upVect = new 
        if (c.GridThrustDirection.Y == -1)
        {
            //c.ThrustOverridePercentage += Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)),2)); 
            c.ThrustOverridePercentage += Convert.ToSingle(u_t);
        }
        /*
        if (c.GridThrustDirection.Y == 1)
        {
            c.ThrustOverridePercentage -= u_t;
        }
        */

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