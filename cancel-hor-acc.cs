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

double prev_a_x = 0;
double prev_a_y = 0;


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

    //storing current speeds for next loop
    prev_v_x = currentVelocity.LinearVelocity.X;
    prev_v_y = currentVelocity.LinearVelocity.Y;


    debugString += "\n" + "myCurrentCockpit.RotationIndicator:\n" + myCurrentCockpit.RotationIndicator;
    debugString += "\n" + " currentVelocity.LinearVelocity.X\n currentVelocity.LinearVelocity.Y:\n" + currentVelocity.LinearVelocity.X + "\n"+ currentVelocity.LinearVelocity.Y;
    debugString += "\n" + "a_x,a_y:\n" + a_x + ",\n" + a_y;
    //useless
    //debugString += "\n" + "myCurrentCockpit.Orientation:\n" + myCurrentCockpit.Orientation;

    //devrive acceleration
    var d_a_x = (prev_a_x - a_x) / dts;
    var d_a_y = (prev_a_y - a_y) / dts;
    debugString += "\n" + "d_a_x,d_a_y:\n" + d_a_x + ",\n" + d_a_y;

    //storing current acc for next loop
    prev_a_x = a_x;
    prev_a_y = a_y;

    
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
                if (d_a_x == 0)
                {
                    if (d_a_y == 0)
                    {
                        //gyro.GyroOverride = true;
                        //.1f pitch brakes (pitch backward)
                        //gyro.Pitch = .1f;
                        //.1f yaw goes right (normal to gravity)
                    }
                }
                else
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

                    double a_angle = Math.Atan(d_a_x / a_y);

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

                    double v_angle = Math.Atan(currentVelocity.LinearVelocity.X/currentVelocity.LinearVelocity.Y);
                    debugString += "\n" + "v_angle:" + a_angle;

                    double result = a_angle + v_angle;

                    if (turnMeAround == true)
                    {
                        gyro.GyroOverride = true;
                        if (gyro.Pitch == 0)
                            gyro.Pitch = .1f;
                        if(result>0)
                            gyro.Yaw = 1f;
                        else
                            gyro.Yaw = -1f;
                    }
                    else
                    {
                        gyro.Pitch = 0f;
                        gyro.Yaw = 0;
                        gyro.GyroOverride = false;
                    }

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

        debugString += "\n" + "gyro.Pitch:\n" + gyro.Pitch;

    }


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