using System;
//using Sandbox.Game.EntityComponents;
//using Sandbox.ModAPI.Ingame;
//using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
//using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;


namespace debugOctoTreeCS
{
    class Program
    {

        class SorterByAxisesOnVector3Dx : IComparer<Vector3D>
        {
            public int Compare(Vector3D x, Vector3D y)
            {
                return x.X.CompareTo(y.X);
            }
        }

        class SorterByAxisesOnVector3Dy : IComparer<Vector3D>
        {
            public int Compare(Vector3D x, Vector3D y)
            {
                return x.Y.CompareTo(y.Y);
            }
        }
        class SorterByAxisesOnVector3Dz : IComparer<Vector3D>
        {
            public int Compare(Vector3D x, Vector3D y)
            {
                return x.Z.CompareTo(y.Z);
            }
        }

        public static double distanceBetweenPointsOnSpecificAxis(Vector3D a, Vector3D b, int axisN)
        {

            double distanceTmp = 1;
            if (axisN % 3 == 0) { distanceTmp = a.X - b.X; }
            if (axisN % 3 == 1) { distanceTmp = a.Y - b.Y; }
            if (axisN % 3 == 2) { distanceTmp = a.Z - b.Z; }

            return distanceTmp;
        }

        public class OctoTree
        {

            int axisDepth = -1;
            //0 is x
            //1 is y
            //2 is z
            int intAxis = -1;

            public Vector3D Point = new Vector3D(0, 0, 0);

            public OctoTree left = null;
            public OctoTree right = null;

            public Vector3D minBB = new Vector3D(0, 0, 0);
            public Vector3D maxBB = new Vector3D(0, 0, 0);

            public OctoTree(Vector3D pointLeaf, int axisD)
            {
                Point = pointLeaf;
                axisDepth = axisD;
                intAxis = 0;
            }
            public OctoTree()
            {
                //placeholder
            }
            public OctoTree(List<Vector3D> listToBeSorted, int tmpAxisDepth)
            {

                axisDepth = tmpAxisDepth;

                List<Vector3D> listSortedX = sortingOnSpecificAxises(listToBeSorted, 0);
                List<Vector3D> listSortedY = sortingOnSpecificAxises(listToBeSorted, 1);
                List<Vector3D> listSortedZ = sortingOnSpecificAxises(listToBeSorted, 2);

                double minX = listSortedX[0].X;
                double maxX = listSortedX[listSortedX.Count - 1].X;

                double minY = listSortedY[0].Y;
                double maxY = listSortedY[listSortedY.Count - 1].Y;

                double minZ = listSortedZ[0].Z;
                double maxZ = listSortedZ[listSortedZ.Count - 1].Z;

                minBB = new Vector3D(minX, minY, minZ);
                maxBB = new Vector3D(maxX, maxY, maxZ);


                int axisChoiceNumber = -1;

                double distBB_x = Math.Abs(maxX - minX);
                double distBB_y = Math.Abs(maxY - minY);
                double distBB_z = Math.Abs(maxZ - minZ);

                List<double> maxDists = new List<double>();
                maxDists.Add(distBB_x);
                maxDists.Add(distBB_y);
                maxDists.Add(distBB_z);

                double maxDistValue = maxDists.Max();

                int maxIndexDistValue = maxDistValue == maxDists[0] ? maxDistValue == maxDists[1] ? 0 : 1 : 2;

                intAxis = maxIndexDistValue;

                axisChoiceNumber = maxIndexDistValue;

                List<Vector3D> listSorted = sortingOnSpecificAxises(listToBeSorted, axisChoiceNumber);
                if (listSorted.Count <= 2)
                {
                    if (listSorted.Count == 1)
                    {
                        //store a point
                        Point = listSorted[0];
                    }
                    else
                    {
                        //store a point and make a leaf (OctoTree) (left bias)
                        Point = listSorted[1];
                        left = new OctoTree(listSorted[0], axisDepth + 1);
                    }
                }
                else
                {
                    //store a point and make 2 OctoTree

                    int intIndexPoint = (listSorted.Count - 1) / 2;

                    int startLeft = 0;
                    int endLeft = intIndexPoint - 1;

                    int startRight = intIndexPoint + 1;
                    int endtRight = listSorted.Count - 1;

                    List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft - startLeft + 1);
                    List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight - startRight + 1);

                    Point = listSorted[intIndexPoint];
                    left = new OctoTree(subListLeft, axisDepth + 1);
                    right = new OctoTree(subListRight, axisDepth + 1);


                }
            }

            public List<Vector3D> sortingOnSpecificAxises(List<Vector3D> listToSort, int axisOnWhichToSort)
            {
                List<Vector3D> newResult = new List<Vector3D>();

                if (axisOnWhichToSort == 0)
                {
                    SorterByAxisesOnVector3Dx storerX = new SorterByAxisesOnVector3Dx();
                    listToSort.Sort(storerX);
                }
                if (axisOnWhichToSort == 1)
                {
                    SorterByAxisesOnVector3Dy storerY = new SorterByAxisesOnVector3Dy();
                    listToSort.Sort(storerY);
                }
                if (axisOnWhichToSort == 2)
                {
                    SorterByAxisesOnVector3Dz storerZ = new SorterByAxisesOnVector3Dz();
                    listToSort.Sort(storerZ);
                }


                //debug
                return listToSort;

                //return newResult;
            }


            public string listOfPointsInBranch(Vector3D testAgainst, int axisN)
            {
                //assuming your are on a root:
                //available Vector3D Point, OctoTree right left

                string debugging = "";
                debugging = debugging + "listOfPointsInBranch\n";

                //current OctoTree tested
                OctoTree tmpOctoTree = new OctoTree();
                //starting position
                Vector3D tmpPoint = Point;
                OctoTree rightTmp = right;
                OctoTree leftTmp = left;

                Vector3D tmpRightPoint = right.Point;
                Vector3D tmpLeftPoint = left.Point;

                //store the distance
                Dictionary<Vector3D, double> distanceToPoint = new Dictionary<Vector3D, double>();
                int tmpAxisD = 0;

                tmpAxisD = intAxis;

                List<Vector3D> resultListV3D = new List<Vector3D>();
                resultListV3D.Add(tmpPoint);

                List<OctoTree> previousNodes = new List<OctoTree>();

                bool continueRunning = true;
                while (continueRunning == true)
                {

                    debugging = debugging + "Point:" + tmpPoint + "\n";
                    if (rightTmp != null) debugging = debugging + "right:" + rightTmp.Point + "\n";
                    if (leftTmp != null) debugging = debugging + "left:" + leftTmp.Point + "\n";

                    double tmpDistanceToPoint = distanceBetweenPointsOnSpecificAxis(tmpPoint, testAgainst, tmpAxisD);
                    distanceToPoint[tmpPoint] = tmpDistanceToPoint;
                    debugging = debugging + "tmpDistanceToPoint:" + tmpDistanceToPoint + "\n";
                    debugging = debugging + "tmpOctoTree.axisDepth:" + tmpOctoTree.axisDepth + "\n";
                    debugging = debugging + "tmpOctoTree.intAxis:" + tmpOctoTree.intAxis + "\n";
                    debugging = debugging + "tmpOctoTree.minBB:" + tmpOctoTree.minBB + "\n";
                    debugging = debugging + "tmpOctoTree.maxBB:" + tmpOctoTree.maxBB + "\n";

                    if (tmpDistanceToPoint > 0)
                    {
                        double tmpDistanceToLeft = distanceBetweenPointsOnSpecificAxis(tmpLeftPoint, testAgainst, tmpAxisD);
                        distanceToPoint[tmpLeftPoint] = tmpDistanceToLeft;

                        if (tmpOctoTree.left == null)
                        {
                            tmpOctoTree = left;
                        }
                        else
                        {
                            previousNodes.Add(tmpOctoTree);
                            tmpOctoTree = tmpOctoTree.left;
                        }

                        debugging = debugging + "going left:" + "\n";
                    }
                    else
                    {
                        double tmpDistanceToRight = distanceBetweenPointsOnSpecificAxis(tmpRightPoint, testAgainst, tmpAxisD);
                        distanceToPoint[tmpRightPoint] = tmpDistanceToRight;

                        if (tmpOctoTree.right == null)
                        {
                            tmpOctoTree = right;
                        }
                        else
                        {
                            previousNodes.Add(tmpOctoTree);
                            tmpOctoTree = tmpOctoTree.right;
                        }
                        debugging = debugging + "going right:" + "\n\n";
                    }
                    if (tmpOctoTree != null)
                    {
                        tmpPoint = tmpOctoTree.Point;
                        rightTmp = tmpOctoTree.right;
                        leftTmp = tmpOctoTree.left;
                        resultListV3D.Add(tmpOctoTree.Point);
                    }

                    if (tmpAxisD == 0)
                    {
                        tmpRightPoint = right.Point;
                        tmpLeftPoint = left.Point;
                    }
                    else
                    {
                        if (rightTmp != null) tmpRightPoint = rightTmp.Point;
                        if (leftTmp != null) tmpLeftPoint = leftTmp.Point;
                    }


                    tmpAxisD = tmpAxisD + 1;


                    debugging = debugging + "\n";

                    if ((tmpOctoTree.right == null) && (tmpOctoTree.left == null))
                    {
                        continueRunning = false;
                        break;
                    }


                    double distanceXToNeighbors = distanceBetweenPointsOnSpecificAxis(tmpRightPoint, testAgainst, 0);
                    debugging = debugging + "dXToN:" + distanceXToNeighbors + "|";

                    double distanceYToNeighbors = distanceBetweenPointsOnSpecificAxis(tmpRightPoint, testAgainst, 1);
                    debugging = debugging + "dYToN:" + distanceYToNeighbors + "|";

                    double distanceZToNeighbors = distanceBetweenPointsOnSpecificAxis(tmpRightPoint, testAgainst, 2);
                    debugging = debugging + "dZToN:" + distanceZToNeighbors + "\n";



                    if (tmpAxisD == 14)
                    {
                        //fail safe
                        continueRunning = false;

                        debugging = debugging + "if (tmpAxisD == 12) {\n\n";
                    }
                }

                debugging = debugging + GetDebuggerDisplayWithLeafsRec(previousNodes[previousNodes.Count - 3]) + "\n";

                //return resultListV3D;
                return debugging;
            }


            public string GetDebuggerDisplay()
            {
                //string resultStr = "GetDebuggerDisplay";
                string resultStr = "";
                resultStr = resultStr + "Point" + Point + "\n";
                resultStr = resultStr + "axisDepth" + axisDepth + "\n";
                resultStr = resultStr + "intAxis" + intAxis + "\n";
                resultStr = resultStr + "minBB" + minBB + "\n";
                resultStr = resultStr + "maxBB" + maxBB + "\n";
                return resultStr;
            }
            public string GetDebuggerDisplayWithLeafs()
            {
                //string resultStr = "GetDebuggerDisplayWithLeafs";
                string resultStr = "";
                resultStr = resultStr + GetDebuggerDisplay() + "\n";
                if (left != null) resultStr = resultStr + "left" + left.GetDebuggerDisplay() + "\n";
                if (right != null) resultStr = resultStr + "right" + right.GetDebuggerDisplay() + "\n";
                return resultStr;
            }
            public string GetDebuggerDisplayWithLeafsRec()
            {
                //string resultStr = "GetDebuggerDisplayWithLeafsRec";
                string resultStr = "";
                resultStr = resultStr + GetDebuggerDisplay() + "\n";
                if (left != null) resultStr = resultStr + "left" + left.GetDebuggerDisplayWithLeafsRec() + "\n";
                if (right != null) resultStr = resultStr + "right" + right.GetDebuggerDisplayWithLeafsRec() + "\n";
                return resultStr;
            }
            public string GetDebuggerDisplayWithLeafsRec(OctoTree recOctree)
            {
                string resultStr = "GetDebuggerRec(recOctree)\n";
                //string resultStr = "";
                resultStr = resultStr + GetDebuggerDisplay() + "\n";
                if (recOctree.left != null) resultStr = resultStr + "left" + recOctree.left.GetDebuggerDisplayWithLeafsRec() + "\n";
                if (recOctree.right != null) resultStr = resultStr + "right" + recOctree.right.GetDebuggerDisplayWithLeafsRec() + "\n";
                return resultStr;
            }
        }

        public Program()
        {

        }

        public void Save()
        {

        }

        static void Main(string[] args)
        {

            //int N = 6;
            //int N = 31;
            int N = 34;
            //int N = 127;
            //fail
            //int N = 3000;

            OctoTree rootOctoTree;

            List<Vector3D> listPointsNotSorted = new List<Vector3D>();
            List<Vector3D> listPointsSortedForRoot = new List<Vector3D>();

            string toCustomStr = "";

            Random rnd = new Random(0);
            //Random rnd = new Random();

            N = (N + 1) % 1000;

            listPointsNotSorted = new List<Vector3D>();
            foreach (int testInt in Enumerable.Range(0, N))
            {
                int numCoordx = -512 + rnd.Next() % 1024;
                int numCoordy = -512 + rnd.Next() % 1024;
                int numCoordz = -512 + rnd.Next() % 1024;
                listPointsNotSorted.Add(new Vector3D(numCoordx, numCoordy, numCoordz));
            }

            toCustomStr = toCustomStr + "listPointsNotSorted.Count:0 to " + (listPointsNotSorted.Count - 1) + "\n";

            rootOctoTree = new OctoTree(listPointsNotSorted, 0);

            toCustomStr = toCustomStr + "rootOctoTree init done" + "\n";

            toCustomStr = toCustomStr + "\n" + "==================\n" + "displaying the whole tree:\n" + "\n";

            toCustomStr = toCustomStr + "" + rootOctoTree.GetDebuggerDisplayWithLeafsRec(rootOctoTree) + "\n";
            /*
            toCustomStr = toCustomStr + "==================\n";

            Vector3D positionToTest = new Vector3D(11.9, 11.9, 11.9);

            toCustomStr = toCustomStr + "=========================\n";

            //Echo("" + rootOctoTree.listOfPointsInBranch(positionToTest, 0));
            string listOfPointsInBranchStr = rootOctoTree.listOfPointsInBranch(positionToTest, 0);
            //Echo("" + listOfPointsInBranchStr);
            toCustomStr = toCustomStr + listOfPointsInBranchStr;
            //Me.CustomData = listOfPointsInBranchStr;

            toCustomStr = toCustomStr + "=========================\n";


            double minDistanceForeach = 500000;
            Vector3D closestPointForeach = new Vector3D(0, 0, 0);
            foreach (Vector3D vect in listPointsNotSorted)
            {
                double tmpDistance = (vect - positionToTest).Length();
                toCustomStr = toCustomStr + ">tmpDistance:" + Math.Round(tmpDistance,1) + "\n";
                toCustomStr = toCustomStr + "vect:" + vect + "\n";

                double distanceXToNeighbors = distanceBetweenPointsOnSpecificAxis(vect, positionToTest, 0);
                toCustomStr = toCustomStr + "dXToN:" + distanceXToNeighbors + "|";

                double distanceYToNeighbors = distanceBetweenPointsOnSpecificAxis(vect, positionToTest, 1);
                toCustomStr = toCustomStr + "dYToN:" + distanceYToNeighbors + "|";

                double distanceZToNeighbors = distanceBetweenPointsOnSpecificAxis(vect, positionToTest, 2);
                toCustomStr = toCustomStr + "dZToN:" + distanceZToNeighbors + "\n";

                if (tmpDistance < minDistanceForeach)
                {
                    minDistanceForeach = tmpDistance;
                    closestPointForeach = vect;
                }
            }

            toCustomStr = toCustomStr + "=========================\n";

            toCustomStr = toCustomStr + "minDistanceForeach:" + Math.Round(minDistanceForeach, 2) + "\n";
            toCustomStr = toCustomStr + "closestPointForeach: " + closestPointForeach + "\n";
            */
            //Me.CustomData = toCustomStr;

            Console.WriteLine(toCustomStr);
        }

    }
}
