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


/*
 * r(t) = define the altitude you want
 * e(t) = r(t) - y(t) // error
 * y(t) = the altitude
 * p(t) = proportionnal
 * i(t) = integral
 * d(t) = derivative
 * u(t) = p + i + d 
 * */

PIDController altRegulator = new PIDController(0.06f, 0f, .01f);
double wantedAltitude = 2000;
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

    double altitudeError = wantedAltitude - elev;

    double dts = Runtime.TimeSinceLastRun.TotalSeconds;

    //public double Control(double error, double timeStep)
    double dir = altRegulator.Control(altitudeError, dts);

    Echo("elev:" + elev);

    debugString += "dir:\n" + dir;

    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    textPanel.FontSize = 1.3f;
    //textPanel.FontSize = 1f;
    textPanel.WriteText(debugString, false);

    //altRegulator.Control();


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
//PID class
//=======================================================================

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

#if DEBUG
    }
}
#endif