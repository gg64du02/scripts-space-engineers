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
double alt = 0f;
double last_alt = 0f;
double alt_speed_ms_1 = 0f;
double last_alt_speed_ms_1 = 0f;
double alt_acc_ms_2 = 0f;
double last_alt_acc_ms_2 = 0f;
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
    //double dir = altRegulator.Control(altitudeError, dts);

    Echo("elev:" + elev);
    //PhysicalMass	Gets the physical mass of the ship, which accounts for inventory multiplier.
    var physMass_kg = myCurrentCockpit.CalculateShipMass().PhysicalMass;
    debugString += " " + "physMass_kg:" + physMass_kg;

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
    debugString += "\n" + "maxEffectiveThrust_N:" + maxEffectiveThrust_N;
    debugString += "\n" + "currentThrust_N:" + currentThrust_N;

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

    double thrustLeft_N = currentThrust_N - physMass_N;
    debugString += "\n" + "thrustLeft_N:" + thrustLeft_N;

    double a_z_ms_2 = thrustLeft_N / physMass_kg;
    debugString += "\n" + "a_z_ms_2:" + a_z_ms_2;

    alt = elev;
    debugString += "\n" + "dts:" + dts;

    //errorDerivative = (error - lastError) / timeStep;
    alt_speed_ms_1 = (alt - last_alt) / dts;
    debugString += "\n" + "alt_speed_ms_1:" + alt_speed_ms_1;

    alt_acc_ms_2 = (alt_speed_ms_1 - last_alt_speed_ms_1) / dts;
    debugString += "\n" + "alt_acc_ms_2:" + alt_acc_ms_2;

    last_alt = alt;
    last_alt_speed_ms_1 = alt_speed_ms_1;
    //TODO code here

    //public double Control(double error, double timeStep)
    // double speedError = 0;
    double speedError = alt_speed_ms_1 - 0;
    double controlSpeed = altRegulator.Control(speedError, dts);

    debugString += "\n" + "controlSpeed:" + controlSpeed;

    var massOfShip = myCurrentCockpit.CalculateShipMass().PhysicalMass;
    debugString += "\n" + "massOfShip:" + massOfShip;


    //applying what the pid processed
    //var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        if (c.GridThrustDirection.Y == -1)
        {
            //c.ThrustOverridePercentage = 0.01f * Convert.ToSingle(dir);
            //c.ThrustOverride = Convert.ToSingle(physMass_N + a_z_ms_2 * physMass_kg);
            //debugString += "\n" + "0.25f * currentThrust_N:" + 0.25f * currentThrust_N;
            //c.ThrustOverride = Convert.ToSingle(2f*(2-Math.Cos()));

            //c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N * 1.01f);
            //debugString += "\n" + "0.25f * physMass_N:" + 0.25f * physMass_N * 1.01f;

            //c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N);
            //debugString += "\n" + "0.25f * physMass_N:" + 0.25f * physMass_N;

            //debugString += "\n" + "0.25f * physMass_N:" + Convert.ToSingle(0.25f * physMass_N + a_z_ms_2 * physMass_kg);
            //c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N + a_z_ms_2 * physMass_kg);

            //debugString += "\n" + "0.25f * physMass_N:" + Convert.ToSingle(0.25f * physMass_N + controlSpeed * physMass_kg / dts);
            //c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N + controlSpeed * physMass_kg / dts);

            //debugString += "\n" + "0.25f * physMass_N:" + Convert.ToSingle(0.25f * physMass_N );
            //c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N + controlSpeed * physMass_kg / dts);

            c.ThrustOverride = Convert.ToSingle(0.25f * physMass_N);
            debugString += "\n" + "0.25f * physMass_N:" + 0.25f * physMass_N;
            debugString += "\n" + "c.ThrustOverride:" + c.ThrustOverride;
        }
    }


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