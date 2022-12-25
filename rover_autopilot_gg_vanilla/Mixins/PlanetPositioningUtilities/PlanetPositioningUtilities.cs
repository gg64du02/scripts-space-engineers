using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {

        //check from down6 gps to down3 gps
        //GPS:down3:-3957044.93:-55637.06:-748943.78:#FF75C9F1:
        //GPS:down6:-3971849:-53922.84:-787513.96:#FF75C9F1:
        public void faceAndPointOnPlanetsCalculated(IMyRemoteControl sc, out int facenumber, out Point pixelPos, bool debugMode, Vector3D testedV3D)
        {

            // Echo(Me.GetPosition()+"");
            Vector3D myPos = sc.GetPosition();
            if (debugMode == true)
            {
                myPos = testedV3D;
            }

            // foreach	(Point point in tmpTestNextPoints){
            // Echo("point"+point);
            // }

            Vector3D centerFacePositionOffset = new Vector3D(0, 0, 0);
            double planet_radius = 60000;

            Vector3D planetCenter = new Vector3D(0, 0, 0);

            bool planetDetected = sc.TryGetPlanetPosition(out planetCenter);

            //Echo("planetCenter:" + planetCenter);

            // planet_radius = (int) (planetCenter-myPos).Length();
            planet_radius = (int)(myPos - planetCenter).Length();

            Echo("planet_radius:" + planet_radius);

            Vector3D myPosRelToCenter = (myPos - planetCenter);

            double myPosXAbs = Math.Abs(myPosRelToCenter.X);
            double myPosYAbs = Math.Abs(myPosRelToCenter.Y);
            double myPosZAbs = Math.Abs(myPosRelToCenter.Z);

            Vector3D projectedSphereVector = new Vector3D(0, 0, 0);

            int faceNumber = -1;

            double pixelScalingToIGW = (2 * planet_radius / 2048);

            //shorter names formulas
            double intX = 0;
            double intY = 0;
            double intZ = 0;

            Point extractedPoint = new Point(0, 0);
            double extractionX_pointRL = 0;
            double extractionY_pointRL = 0;

            if (myPosXAbs > myPosYAbs)
            {
                if (myPosXAbs > myPosZAbs)
                {
                    projectedSphereVector = (planet_radius / myPosXAbs) * myPosRelToCenter;
                    intY = projectedSphereVector.Y;
                    intZ = projectedSphereVector.Z;
                    if (myPosRelToCenter.X > 0)
                    {
                        faceNumber = 3;
                        extractionX_pointRL = planet_radius - intY;
                        extractionY_pointRL = planet_radius - intZ;
                    }
                    else
                    {
                        faceNumber = 4;
                        extractionX_pointRL = planet_radius - intY;
                        extractionY_pointRL = planet_radius + intZ;
                    }
                }
            }

            if (myPosYAbs > myPosXAbs)
            {
                if (myPosYAbs > myPosZAbs)
                {
                    projectedSphereVector = (planet_radius / myPosYAbs) * myPosRelToCenter;
                    intX = projectedSphereVector.X;
                    intZ = projectedSphereVector.Z;
                    if (myPosRelToCenter.Y > 0)
                    {
                        faceNumber = 5;
                        extractionY_pointRL = planet_radius - intX;
                        extractionX_pointRL = planet_radius - intZ;
                    }
                    else
                    {
                        faceNumber = 1;
                        //extractionY_pointRL = planet_radius + intX;
                        //extractionX_pointRL = planet_radius - intZ;
                        extractionY_pointRL = planet_radius - intX;
                        extractionX_pointRL = planet_radius + intZ;
                    }
                }
            }

            if (myPosZAbs > myPosXAbs)
            {
                if (myPosZAbs > myPosYAbs)
                {
                    projectedSphereVector = (planet_radius / myPosZAbs) * myPosRelToCenter;
                    intX = projectedSphereVector.X;
                    intY = projectedSphereVector.Y;
                    if (myPosRelToCenter.Z > 0)
                    {
                        faceNumber = 0;
                        extractionY_pointRL = planet_radius + intX;
                        extractionX_pointRL = planet_radius - intY;
                    }
                    else
                    {
                        faceNumber = 2;
                        extractionY_pointRL = planet_radius - intX;
                        extractionX_pointRL = planet_radius - intY;
                    }
                }
            }

            if (extractionX_pointRL == 0)
            {
                //out-ing
                facenumber = faceNumber;
                pixelPos = new Point(0, 0);

                return;
            }

            if (extractionY_pointRL == 0)
            {
                //out-ing
                facenumber = faceNumber;
                pixelPos = new Point(0, 0);
                return;
            }

            double tmpCalcX = extractionX_pointRL / pixelScalingToIGW;
            double tmpCalcY = extractionY_pointRL / pixelScalingToIGW;

            extractedPoint = new Point((int)tmpCalcX, (int)tmpCalcY);

            //Echo("extractedPoint:"+extractedPoint);
            //Echo("faceNumber:"+faceNumber);
            //Echo("projectedSphereVector:"+projectedSphereVector);

            Point calculatedPoint = new Point(-1, -1);


            //out-ing
            facenumber = faceNumber;
            pixelPos = extractedPoint;

        }

        public void whichFileShouldIlook(int facenumber)
        {

            string tmpStr = "" + facenumber + " is ";

            if (facenumber == 0)
            {
                tmpStr += "back";
            }
            if (facenumber == 1)
            {
                tmpStr += "down";
            }

            if (facenumber == 2)
            {
                tmpStr += "front";
            }
            if (facenumber == 3)
            {
                tmpStr += "left";
            }

            if (facenumber == 4)
            {
                tmpStr += "right";
            }
            if (facenumber == 5)
            {
                tmpStr += "up";
            }

            Echo(tmpStr);

            // 0 is back
            // 1 is down

            // 2 is front
            // 3 is left

            // 4 is right
            // 5 is up
        }
    }
}
