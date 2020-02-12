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
    debugString += "\n" + " currentVelocity.LinearVelocity.X,Y,Z:\n" + Math.Round((currentVelocity.LinearVelocity.X), 2).ToString() + "," + Math.Round((currentVelocity.LinearVelocity.X), 2).ToString() + "," + Math.Round((currentVelocity.LinearVelocity.Z), 2).ToString() + ",";
    //debugString += "\n" + "a_x,a_y:\n" + a_x + ",\n" + a_y;
    debugString += "\na_x,a_y,a_z:\n" + Math.Round((a_x), 2).ToString() + "," + Math.Round((a_y), 2).ToString()+ "," + Math.Round((a_z), 2).ToString();
    var grav = myCurrentCockpit.GetTotalGravity();

    //debugString += "\n" + "grav:\n" + grav;
    //debugString += "\n" + "grav:\n" + grav.X + ",\n" + grav.Y + ",\n" + grav.Z;
    debugString += "\ngrav.X,Y,Z:\n" + Math.Round((grav.X), 2).ToString() + "," + Math.Round((grav.Y), 2).ToString() + "," + Math.Round((grav.Z), 2).ToString();
    //useless
    //debugString += "\n" + "myCurrentCockpit.Orientation:\n" + myCurrentCockpit.Orientation;

    //devrive acceleration
    var d_a_x = (prev_a_x - a_x) / dts;
    var d_a_y = (prev_a_y - a_y) / dts;
    var d_a_z = (prev_a_z - a_z) / dts;
    //debugString += "\n" + "d_a_x,d_a_y,d_a_z:\n" + d_a_x + ",\n" + d_a_y+ ",\n" + d_a_z;
    debugString += "\nd_a_x,d_a_y,d_a_z:\n" + Math.Round((d_a_x), 2).ToString() + "," + Math.Round((d_a_y), 2).ToString() + "," + Math.Round((d_a_z), 2).ToString();

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

    var gyros = new List<IMyGyro>();
    GridTerminalSystem.GetBlocksOfType(gyros);
    foreach (var gyro in gyros)
    {
        //gyro
        //X slide right left roll
        //Y foward backward pitch
        //Z turn left right yaw
        //MAKE SURE THAT ALL GYROS ALL PLACE IN THE SAME DIRECTION

        double threshold = .02f;
        if(Math.Abs(a_x) > threshold)
        {
            if (Math.Abs(a_y) > threshold)
            {
                //we want to cancel the speed so we need the acc be the oppsosite sign of the speed
                //tldr: sign(acc) must be !sign(speed)
                bool turnMeAround = false;

                if(a_x>0)
                {
                    if(currentVelocity.LinearVelocity.X>0)
                    {
                        turnMeAround = true;
                    }
                }
                if (a_x < 0)
                {
                    if (currentVelocity.LinearVelocity.X < 0)
                    {
                        turnMeAround = true;
                    }
                }

                double a_angle = Math.Atan2(d_a_y , d_a_x);
                debugString += "\n" + "a_angle:" + a_angle;

                if (a_y > 0)
                {
                    if (currentVelocity.LinearVelocity.Y > 0)
                    {
                        turnMeAround = true;
                    }
                }
                if (a_y < 0)
                {
                    if (currentVelocity.LinearVelocity.Y < 0)
                    {
                        turnMeAround = true;
                    }
                }

                if (a_z > 0)
                {
                    if (currentVelocity.LinearVelocity.Z > 0)
                    {
                        turnMeAround = true;
                    }
                }
                if (a_z < 0)
                {
                    if (currentVelocity.LinearVelocity.Z < 0)
                    {
                        turnMeAround = true;
                    }
                }

                double v_angle = Math.Atan2(currentVelocity.LinearVelocity.Y,currentVelocity.LinearVelocity.X);
                //debugString += "\n" + "v_angle:" + a_angle;

                double result = a_angle + v_angle;
                debugString += "\n" + "result:" + result;

                if (turnMeAround == true)
                {
                    /*
                    gyro.GyroOverride = true;
                    gyro.Yaw = Convert.ToSingle(result) * 4f;
                    /*
                    if (gyro.Pitch == 0)
                        gyro.Pitch = .2f;
                    if (result > 0)
                    {
                        //gyro.Yaw = 1f;
                        gyro.Yaw = Convert.ToSingle(result) * 4f;
                    }
                    else
                    {
                        //gyro.Yaw = -1f;
                        gyro.Yaw = -Convert.ToSingle(result) * 4f;
                    }*/
                }
                else
                {
                    gyro.Pitch = 0f;
                    gyro.Yaw = 0;
                    gyro.GyroOverride = false;
                }
            }
        }
        else
        {
            gyro.Pitch = 0f;
            gyro.GyroOverride = false;
        }

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

        //debugString += "\n" + "gyro.Pitch:\n" + gyro.Pitch;

    }

    //lcd display
    var textPanel = GridTerminalSystem.GetBlockWithName("textPanel") as IMyTextPanel;
    //textPanel.FontSize = 1.58f;
    textPanel.FontSize = 1f;
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