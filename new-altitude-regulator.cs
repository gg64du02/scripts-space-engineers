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
double g_constant = 9.8f;
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
    var physMass_kg = myCurrentCockpit.CalculateShipMass().PhysicalMass;
    debugString += "\n" + "physMass_kg:" + physMass_kg;
    debugString += "\n"+"dir:" + dir;

    //figuring out the available thrust
    //IMyThrust.MaxEffectiveThrust
    //IMyThrust.CurrentThrust_N
    double maxEffectiveThrust_N = 0;
    double currentThrust_N = 0;
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs){
        maxEffectiveThrust_N += c.MaxEffectiveThrust; currentThrust_N += c.CurrentThrust;
    }
    debugString += "\n" + "currentThrust_N:" + currentThrust_N;

    double a_z = currentThrust_N - physMass_kg;
    debugString += "\n" + "physMass_kg:" + physMass_kg;

    double physMass_N = physMass_kg * g_constant;
    debugString += "\n" + "physMass_N:" + physMass_N;

    /*
    BaseMass Gets the base mass of the ship.
    totalMass Gets the total mass of the ship, including cargo.
    PhysicalMass Gets the physical mass of the ship, which accounts for inventory multiplier.
    */

    var totalMass_kg = myCurrentCockpit.CalculateShipMass().TotalMass;
    debugString += "\n" + "totalMass_kg:" + totalMass_kg;

    var thr_to_weight_ratio = maxEffectiveThrust_N / physMass_N ;
    debugString += "\n" + "thr_to_weight_ratio:" + thr_to_weight_ratio;

    //TODO code here
    debugString += "\n" + "a_z:" + a_z;

    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    //textPanel.FontSize = 1.2f;
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