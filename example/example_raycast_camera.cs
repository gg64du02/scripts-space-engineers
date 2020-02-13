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

//https://forum.keenswh.com/threads/new-camera-raycast-and-sensor-api-update-01-162-dev.7389290/

double SCAN_DISTANCE = 100;
float PITCH = 0;
float YAW = 0;
private IMyCameraBlock camera;
private IMyTextPanel lcd;
private bool firstrun = true;
private MyDetectedEntityInfo info;
private StringBuilder sb = new StringBuilder();

public void Main(string argument)
{
    if (firstrun)
    {
        firstrun = false;
        List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
        GridTerminalSystem.GetBlocks(blocks);

        foreach (var block in blocks)
        {
            if (block is IMyCameraBlock)
                camera = (IMyCameraBlock)block;

            if (block is IMyTextPanel)
                lcd = (IMyTextPanel)block;
        }

        camera.EnableRaycast = true;
    }

    if (camera.CanScan(SCAN_DISTANCE))
        info = camera.Raycast(SCAN_DISTANCE, PITCH, YAW);

    sb.Clear();
    Echo("EntityID: " + info.EntityId);

    Echo("Name: " + info.Name);

    Echo("Type: " + info.Type);

    Echo("Velocity: " + info.Velocity.ToString("0.000"));

    Echo("Relationship: " + info.Relationship);

    Echo("Size: " + info.BoundingBox.Size.ToString("0.000"));

    Echo("Position: " + info.Position.ToString("0.000"));

    if (info.HitPosition.HasValue)
    {

        Echo("Hit: " + info.HitPosition.Value.ToString("0.000"));

        Echo("Distance: " + Vector3D.Distance(camera.GetPosition(), info.HitPosition.Value).ToString("0.00"));
    }


    Echo("Range: " + camera.AvailableScanRange.ToString());
    /*
    lcd.WritePublicText(sb.ToString());
    lcd.ShowPrivateTextOnScreen();
    lcd.ShowPublicTextOnScreen();
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
#if DEBUG
    }
}
#endif