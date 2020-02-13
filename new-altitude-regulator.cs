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

PIDController altRegulator = new PIDController(1f, 0f, 0f);
double wantedAltitude = 20;
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
    //todo change this
    double dir = altRegulator.Control(altitudeError, dts);

    Echo("elev:" + elev);
    //PhysicalMass	Gets the physical mass of the ship, which accounts for inventory multiplier.
    var physMass_n = myCurrentCockpit.CalculateShipMass().PhysicalMass;
    debugString += "\n" + "physMass_n:\n" + physMass_n;
    debugString += "\n"+"dir:\n" + dir;

    //figuring out the available thrust
    //IMyThrust.MaxEffectiveThrust
    double maxEffectiveThrust = 0;
    double currentThrust = 0;
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs){ maxEffectiveThrust += c.MaxEffectiveThrust; currentThrust += c.CurrentThrust; }
    //IMyThrust.CurrentThrust


    //TODO code here

    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    textPanel.FontSize = 1.3f;
    //textPanel.FontSize = 1f;
    textPanel.WriteText(debugString, false);

    /*
    //applying what the pid processed
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        if (c.GridThrustDirection.Y == -1)
        {
            c.ThrustOverridePercentage = 0.01f* Convert.ToSingle(dir);
        }
    }
    */

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