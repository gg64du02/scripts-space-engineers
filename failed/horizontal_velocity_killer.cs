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

double r_t = 1000;
double e_t = 0;
double y_t = 0;
double p_t = 0;
double i_t = 0;
double d_t = 0;
double u_t = 0;

new List<double> i_t_list_points = new List<double>();
double i_t_num_points = 10;

//previous value of e(t)
double d_t_m_1 = 0;
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

    double elev;
    var myCurrentCockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    myCurrentCockpit.TryGetPlanetElevation(MyPlanetElevation.Surface, out elev);

    Echo("myCurrentCockpit.RotationIndicator:" + myCurrentCockpit.RotationIndicator);
    var currentVelocity = myCurrentCockpit.GetShipVelocities();
    Echo("currentVelocity.LinearVelocity:" + currentVelocity.LinearVelocity);
    Echo("myCurrentCockpit.Orientation:" + myCurrentCockpit.Orientation);
    //var currentVelocityString = currentVelocity.ToString();
    //Echo("myCurrentCockpit currentVelocity:" + currentVelocityString);

    int[] earthPos = new int[3] { 0,0,0 };

    Echo("myCurrentCockpit.GetPosition():"+myCurrentCockpit.GetPosition());
    /*
    x = block.GetPosition().GetDim(0);
    y = block.GetPosition().GetDim(1);
    z = block.GetPosition().GetDim(2);
    */

    //Gyros
    //Roll
    //Pitch
    //Yaw
    // IMyGyro
    var gyros = new List<IMyGyro>();
    GridTerminalSystem.GetBlocksOfType(gyros);
    foreach(var gyro in gyros)
    {

        var roll = 0;
        var yaw = 0;
        var pitch = 0;

        
        Echo(" " + gyro.Roll + " " + gyro.Pitch + " " + gyro.Yaw);
        //range of those is -2pi +2pi
        
        if (elev > 500)
        {
            
            gyro.GyroOverride = true;
            /*
            gyro.Yaw += 0.001f * Convert.ToSingle(currentVelocity.LinearVelocity.Y);
            if (gyro.Yaw > 0.1f)
                gyro.Yaw = 0.1f;
            if (gyro.Yaw < -0.1f)
                gyro.Yaw = -0.1f;
                */
            /*
            //gyro.Roll += 0.0001f * Convert.ToSingle(currentVelocity.LinearVelocity.X);
            //gyro.Pitch += 0.0001f * Convert.ToSingle(currentVelocity.LinearVelocity.Y);
            if (gyro.Yaw > 0.1f)
                gyro.Yaw = 0.1f;
                */

            var maxOuput = .2f;
            gyro.Pitch += 0.001f * Convert.ToSingle(currentVelocity.LinearVelocity.X);
            if (gyro.Pitch > maxOuput)
                gyro.Pitch = maxOuput;
            if (gyro.Pitch < -maxOuput)
                gyro.Pitch = -maxOuput;

            gyro.Roll += 0.001f * Convert.ToSingle(currentVelocity.LinearVelocity.Y);
            if (gyro.Roll > maxOuput)
                gyro.Roll = maxOuput;
            if (gyro.Roll < -maxOuput)
                gyro.Roll = -maxOuput;
        }
        else
        {

            gyro.GyroOverride = false;
        }
        

        //Echo(Convert.ToSingle(currentVelocity.LinearVelocity.Y));

    //here is the speed of the cockpit relative to itslef
        //X slide right left roll
        //Y foward backward pitch
        //Z turn left right yaw
    }

    y_t = elev;
    Echo("y_t:" + y_t.ToString());

    e_t = r_t - y_t;

    //integral 
    //first approach from 0 to now (bad results, assume the settings would never change)
    //i_t += e_t;
    if (i_t_list_points.Count < i_t_num_points) {
        i_t_list_points.Add(e_t);
    }
    else
    {
        i_t_list_points.RemoveAt(0);
        i_t_list_points.Add(e_t);
    }
    i_t = 0;
    foreach (var it in i_t_list_points)
    {
        i_t += it;
    }
    i_t = i_t / 10;
    Echo("i_t:" + i_t.ToString());

    //derivative
    //TODO: figure the time delta
    var dts = Runtime.TimeSinceLastRun.TotalSeconds;
    Echo("dts:" + dts.ToString());
    d_t = (d_t_m_1 - e_t) / dts;
    d_t_m_1 = e_t;
    Echo("d_t:" + d_t.ToString());


    //TODO:need update for i and d
    u_t = K_p * e_t + T_i * i_t + T_d * d_t;
    Echo("u_t:" + u_t.ToString());

    //applying what the pid processed
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        //Echo(c.CustomName + " " + c.GridThrustDirection);
        //Vector3I upVect = new 
        if (c.GridThrustDirection.Y == -1)
        {
            //c.ThrustOverridePercentage += Convert.ToSingle(Math.Pow(Convert.ToSingle(Math.Abs(minAlt - elev)),2)); 
            c.ThrustOverridePercentage += Convert.ToSingle(u_t);
        }
        /*
        //disabled by default, prone to ship crashes !!!
        if (c.GridThrustDirection.Y == 1)
        {
            c.ThrustOverridePercentage -= Convert.ToSingle(u_t);
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