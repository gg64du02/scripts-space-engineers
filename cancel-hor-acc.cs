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
double prev_v_z = 0;

double a_x = 0;
double a_y = 0;
double a_z = 0;

double prev_a_x = 0;
double prev_a_y = 0;
double prev_a_z = 0;

double prev_r = 0;
double prev_theta = 0;
double prev_varphi = 0;

double d_r_t = 0;
double d_theta_t = 0;
double d_varphi_t = 0;

public Program()
{
    // The constructor, called only once every session and
    // always before any other method is called. Use it to
    // initialize your script.
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
    //This makes the program automatically run every 10 ticks.

    //Reset all gyros


    var gyros = new List<IMyGyro>();
    GridTerminalSystem.GetBlocksOfType(gyros);
    foreach (var gyro in gyros)
    {
        gyro.GyroOverride = true;
        gyro.Pitch = 0f;
        gyro.Roll = 0f;
        gyro.Yaw = 0f;
        gyro.GyroOverride = false;
    }
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
    a_z = (prev_v_z - currentVelocity.LinearVelocity.Z) / dts;

    //storing current speeds for next loop
    prev_v_x = currentVelocity.LinearVelocity.X;
    prev_v_y = currentVelocity.LinearVelocity.Y;
    prev_v_z = currentVelocity.LinearVelocity.Z;


    //debugString += "\n" + "myCurrentCockpit.RotationIndicator:\n" + myCurrentCockpit.RotationIndicator;
    //debugString += "\n" + " currentVelocity.LinearVelocity.X\n currentVelocity.LinearVelocity.Y:\n" + currentVelocity.LinearVelocity.X + "\n" + currentVelocity.LinearVelocity.Y;
    debugString += " currentVelocity.LinearVelocity.X,Y,Z:\n" + Math.Round((currentVelocity.LinearVelocity.X), 2).ToString() + "," + Math.Round((currentVelocity.LinearVelocity.X), 2).ToString() + "," + Math.Round((currentVelocity.LinearVelocity.Z), 2).ToString() + ",";
    //debugString += "\n" + "a_x,a_y:\n" + a_x + ",\n" + a_y;
    debugString += "\na_x,a_y,a_z:" + Math.Round((a_x), 2).ToString() + "," + Math.Round((a_y), 2).ToString()+ "," + Math.Round((a_z), 2).ToString();
    var grav = myCurrentCockpit.GetTotalGravity();

    //debugString += "\n" + "grav:\n" + grav;
    //debugString += "\n" + "grav:\n" + grav.X + ",\n" + grav.Y + ",\n" + grav.Z;
    debugString += "\ngrav.X,Y,Z:" + Math.Round((grav.X), 2).ToString() + "," + Math.Round((grav.Y), 2).ToString() + "," + Math.Round((grav.Z), 2).ToString();
    //useless
    //debugString += "\n" + "myCurrentCockpit.Orientation:\n" + myCurrentCockpit.Orientation;

    //devrive acceleration
    var d_a_x = (prev_a_x - a_x) / dts;
    var d_a_y = (prev_a_y - a_y) / dts;
    var d_a_z = (prev_a_z - a_z) / dts;
    //debugString += "\n" + "d_a_x,d_a_y,d_a_z:\n" + d_a_x + ",\n" + d_a_y+ ",\n" + d_a_z;
    debugString += "\nd_a_x,d_a_y,d_a_z:" + Math.Round((d_a_x), 2).ToString() + "," + Math.Round((d_a_y), 2).ToString() + "," + Math.Round((d_a_z), 2).ToString();

    //storing current acc for next loop
    prev_a_x = a_x;
    prev_a_y = a_y;
    prev_a_z = a_z;

    //TODO: figure out polar coordinates (current xyz pos versus planet center xyz)
    //https://en.wikipedia.org/wiki/Spherical_coordinate_system
    //(r,theta,phi)
    /*
    r &= \sqrt{ x ^ 2 + y ^ 2 + z ^ 2}, \\
    \varphi &= \arctan \frac{ y}{ x}, \\
    \theta &= \arccos\frac{ z} {\sqrt{ x ^ 2 + y ^ 2 + z ^ 2} } 
    = \arccos\frac{ z}{ r}
    =\arctan\frac{\sqrt{ x ^ 2 + y ^ 2} } { z}.
    */
    var myCurPos = myCurrentCockpit.GetPosition();
    var r = Math.Sqrt((myCurPos.X) * (myCurPos.X)
        + (myCurPos.Y) * (myCurPos.Y)
        + (myCurPos.Z) * (myCurPos.Z));

    //debugString += "\nr: " + Math.Round((r), 2).ToString();
    debugString += "\nr: " + r;

    var theta = Math.Atan2(Math.Sqrt((myCurPos.X) * (myCurPos.X)
        + (myCurPos.Y) * (myCurPos.Y)), myCurPos.Z);

   //debugString += "\ntheta: " + Math.Round((theta), 2).ToString();
    debugString += "\ntheta: " + theta;

    var varphi = Math.Atan2(myCurPos.Y , myCurPos.X);

    //debugString += "\nvarphi: " + Math.Round((varphi), 2).ToString();
    debugString += "\nvarphi: " + varphi;

    //TODO: figure out (d theta / dt) and (d varphi / dt) to allow control on ROLL and PITCH?

    d_r_t = (prev_r - r) / dts;
    d_theta_t = (prev_theta - theta) / dts;
    d_varphi_t = (prev_varphi - varphi) / dts;
    /*
    debugString += "\nd_r_t: " + d_r_t;
    debugString += "\nd_theta_t: " + d_theta_t;
    debugString += "\nd_varphi_t: " + d_varphi_t;
    */
    //storing the values for the next loop
    prev_r = r;
    prev_theta = theta;
    prev_varphi = varphi;

    //figuring out the surface vel and angle
    double sqrt_d_theta_varphi_t = Math.Sqrt(d_theta_t * d_theta_t + d_varphi_t * d_varphi_t);
    double sqrt_d_angle_theta_varphi_t = Math.Atan2(d_theta_t, d_varphi_t);

    double ground_speed_ms = sqrt_d_theta_varphi_t * (r-elev);
    debugString += "\nground_speed_ms: " + ground_speed_ms;

    if (sqrt_d_theta_varphi_t > 0)
    {

        //TODO code here

        /*
        //gyro
        //X slide right left roll
        //Y foward backward pitch
        //Z turn left right yaw
        //MAKE SURE THAT ALL GYROS ALL PLACE IN THE SAME DIRECTION
        var gyros = new List<IMyGyro>();
        GridTerminalSystem.GetBlocksOfType(gyros);
        foreach (var gyro in gyros)
        {
            gyro.GyroOverride = true;
            gyro.GyroOverride = false;
            //Math.Atan2(d_a_y , d_a_x);
            //maxing out
            if (Math.Abs(gyro.Pitch) > .1f)
            {
                if (gyro.Pitch > .1f)
                    gyro.Pitch = .1f;
                gyro.Pitch = -.1f;
            }
            if (Math.Abs(gyro.Roll) > .1f)
            {
                if (gyro.Roll > .1f)
                    gyro.Roll = .1f;
                gyro.Roll = -.1f;
            }
        }
        */


        //debugString += "\n" + "gyro.Pitch:\n" + gyro.Pitch;

    }

    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    textPanel.FontSize = 1.58f;
    //textPanel.FontSize = 1f;
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