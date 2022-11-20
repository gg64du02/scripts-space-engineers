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
    partial class Program : MyGridProgram
    {
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project, 
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
        // 
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.


        //int N = 6;
        //int N = 7;
        //int N = 8;
        //int N = 9;
        //int N = 10;
        //int N = 15;
        //int N = 16;
        //int N = 14;
        //int N = 31;
        //int N = 31;
        int N = 127;
        //int N = 126;
        //int N = 55;
        //int N = 500;
        //int N = 503;

        //ok
        //int N = 375;

        //ok
        //int N = 750;
        //int N = 850;

        //int N = 860;
        //fail
        //int N = 897;

        //int N = 889;
        //fail?
        //int N = 899;//or 900

        //int N = 901;

        //int N = 1000;

        //fail
        //int N = 1500;

        //int N = 2000;
        //int N = 2500;

        //fail
        //int N = 3000;

        OctoTree rootOctoTree;

        List<Vector3D> listPointsNotSorted = new List<Vector3D>();
        List<Vector3D> listPointsSortedForRoot = new List<Vector3D>();

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

            public OctoTree(Vector3D pointLeaf, int axisD)
            {
                Point = pointLeaf;
                axisDepth = axisD;
                intAxis = axisDepth % 3;
            }
            public OctoTree()
            {
                //placeholder
            }
            public OctoTree(List<Vector3D> listToBeSorted, int axisD)
            {
                //TODO: listToBeSorted sort this on an axis (0 for root)
                //Echo("" + listToBeSorted.Count);
                axisDepth = axisD;
                intAxis = axisDepth % 3;
                List<Vector3D> listSorted = sortingOnSpecificAxises(listToBeSorted, intAxis);
                if (listSorted.Count <= 2)
                {
                    if(listSorted.Count == 1)
                    {
                        //TODO: store a point
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
                    //TODO: store a point and make 2 OctoTree

                    int intIndexPoint = (listSorted.Count - 1) / 2;

                    int startLeft = 0;
                    int endLeft = intIndexPoint - 1;

                    int startRight = intIndexPoint + 1;
                    int endtRight = listSorted.Count - 1;
                    
                    //List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft);
                    //List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight);
                    //public System.Collections.Generic.List<T> GetRange (int index, int count);

                    List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft - startLeft + 1);
                    List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight - startRight + 1);
                    
                    Point = listSorted[intIndexPoint];
                    left = new OctoTree(subListLeft, axisDepth + 1);
                    right = new OctoTree(subListRight, axisDepth + 1);

                    
                }
            }

            public List<Vector3D> sortingOnSpecificAxises(List<Vector3D> listToSort, int axisOnWhichToSort) {
                //
                //
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

            public double distanceBetweenPointsOnSpecificAxis(Vector3D a, Vector3D b, int axisN)
            {

                double distanceTmp = 1;
                if (axisN % 3 == 0) { distanceTmp = a.X - b.X; }
                if (axisN % 3 == 1) { distanceTmp = a.Y - b.Y; }
                if (axisN % 3 == 2) { distanceTmp = a.Z - b.Z; }

                return distanceTmp;
            }

            //public List<Vector3D> listOfPointsInBranch(Vector3D testAgainst, int axisN)
            public string listOfPointsInBranch(Vector3D testAgainst, int axisN)
            {
                //assuming your are on a root:
                //available Vector3D Point, OctoTree right left

                string debugging = "";
                debugging = debugging+"listOfPointsInBranch\n";

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

                //int debugCount = 0

                List<Vector3D> resultListV3D = new List<Vector3D>();
                resultListV3D.Add(tmpPoint);

                //throw new InvalidOperationException("break my point");

                List<OctoTree> previousNodes = new List<OctoTree>();

                bool continueRunning = true;
                while (continueRunning == true)
                {

                    debugging = debugging +"Point:" + tmpPoint + "\n";
                    if(rightTmp != null) debugging = debugging + "right:" + rightTmp.Point + "\n";
                    if (leftTmp != null) debugging = debugging + "left:" + leftTmp.Point + "\n";

                    //double tmpDistanceToPoint = distanceBetweenPointsOnSpecificAxis(Point, testAgainst, tmpAxisD);
                    double tmpDistanceToPoint = distanceBetweenPointsOnSpecificAxis(tmpPoint, testAgainst, tmpAxisD);
                    distanceToPoint[tmpPoint] = tmpDistanceToPoint;
                    debugging = debugging + "tmpDistanceToPoint:" + tmpDistanceToPoint + "\n";
                    debugging = debugging + "tmpOctoTree.axisDepth.:" + tmpOctoTree.axisDepth + "\n";
                    if (tmpDistanceToPoint > 0)
                    {
                        double tmpDistanceToLeft = distanceBetweenPointsOnSpecificAxis(tmpLeftPoint, testAgainst, tmpAxisD + 1);
                        distanceToPoint[tmpLeftPoint] = tmpDistanceToLeft;
                        //updating to the leaf

                        //tmpOctoTree = tmpOctoTree.left;
                        
                        if (tmpOctoTree.left == null)
                        {
                            tmpOctoTree = left;
                        }
                        else
                        {
                            previousNodes.Add(tmpOctoTree);
                            tmpOctoTree = tmpOctoTree.left;
                        }

                        debugging = debugging + "going left:" +"\n";
                    }
                    else
                    {
                        double tmpDistanceToRight = distanceBetweenPointsOnSpecificAxis(tmpRightPoint, testAgainst, tmpAxisD + 1);
                        distanceToPoint[tmpRightPoint] = tmpDistanceToRight;
                        //updating to the leaf
                        //tmpOctoTree = tmpOctoTree.right;
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
                        if(rightTmp != null) tmpRightPoint = rightTmp.Point;
                        if (leftTmp != null) tmpLeftPoint = leftTmp.Point;
                    }


                    tmpAxisD = tmpAxisD + 1;

                    //stop searching
                    //if ((tmpOctoTree.right == null) || (tmpOctoTree.left == null))
                    /*
                    if ((tmpOctoTree.right == null))
                        {
                        continueRunning = false;
                    }*/
                    if (tmpOctoTree == null) { debugging = debugging + "tmpOctoTree == null\n"; }
                    if (tmpOctoTree.right == null) { debugging = debugging + "tmpOctoTree.right == null\n"; }
                    if (tmpOctoTree.left == null) { debugging = debugging + "tmpOctoTree.left == null\n"; }

                    debugging = debugging + "\n";

                    if ((tmpOctoTree.right == null) && (tmpOctoTree.left == null))
                    {
                        continueRunning = false;
                        break;
                    }

                    if(tmpOctoTree.axisDepth == 3)
                    {
                        debugging = debugging + "tmpOctoTree.Point:"+ tmpOctoTree.Point + "\n";
                        debugging = debugging + "-1:" + previousNodes[previousNodes.Count - 1].Point + "\n";
                        debugging = debugging + "-2:" + previousNodes[previousNodes.Count - 2].Point + "\n";
                    }



                    if (tmpAxisD == 14) {
                        //fail safe
                        continueRunning = false;

                        debugging = debugging + "if (tmpAxisD == 12) {\n\n";
                    }
                }

                debugging = debugging + GetDebuggerDisplayWithLeafsRec(previousNodes[previousNodes.Count - 3]) + "\n";

                //return resultListV3D;
                return debugging;
            }

            public OctoTree getLeafOctoTree(Vector3D testAgainst,int axisN)
            {
                int axisDtmp = axisN % 3;
                double distanceTmp = -1;
                distanceTmp = distanceBetweenPointsOnSpecificAxis(Point, testAgainst, axisDtmp);

                if (distanceTmp > 0)
                {
                    //check the left leaf
                }
                else
                {
                    //check the right leaf
                }

                if((left == null) && (right == null))
                {
                    return new OctoTree(Point,axisDtmp);
                }
                return new OctoTree();
            }

            public Vector3D closestOctoTree(Vector3D testAgainst)
            {
                Vector3D resultClosest = new Vector3D(0, 0, 0);

                Vector3D pointTmp = Point;
                OctoTree rightTmp = right;
                OctoTree leftTmp = left;

                int axisDepthTmp = 0;
                int intAxisTmp = axisDepthTmp%3;

                double distanceTmp = -1;
                distanceTmp = distanceBetweenPointsOnSpecificAxis(Point, testAgainst, intAxisTmp);

                if (distanceTmp > 0)
                {
                    //check the left leaf
                }
                else
                {
                    //check the right leaf
                }

                return resultClosest;
            }
            public string GetDebuggerDisplay()
            {
                //string resultStr = "GetDebuggerDisplay";
                string resultStr = "";
                resultStr = resultStr + "Point" + Point + "\n";
                resultStr = resultStr + "axisDepth" + axisDepth + "\n";
                resultStr = resultStr + "intAxis" + intAxis + "\n";
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
                string resultStr = "GetDebuggerRec(recOctree)";
                //string resultStr = "";
                resultStr = resultStr + GetDebuggerDisplay() + "\n";
                if (recOctree.left != null) resultStr = resultStr + "left" + recOctree.left.GetDebuggerDisplayWithLeafsRec() + "\n";
                if (recOctree.right != null) resultStr = resultStr + "right" + recOctree.right.GetDebuggerDisplayWithLeafsRec() + "\n";
                return resultStr;
            }
        }

        public Program()
        {
            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set Runtime.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.
        }

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.
        }

        public void Main(string argument, UpdateType updateSource)
        {
            // The main entry point of the script, invoked every time
            // one of the programmable block's Run actions are invoked,
            // or the script updates itself. The updateSource argument
            // describes where the update came from. Be aware that the
            // updateSource is a  bitfield  and might contain more than 
            // one update type.
            // 
            // The method itself is required, but the arguments above
            // can be removed if not needed.

            Vector3D point1 = new Vector3D(654, 566, 422);
            Vector3D point2 = new Vector3D(-20, 20, 0);
            Vector3D point3 = new Vector3D(-51, 21, 24);
            Vector3D point4 = new Vector3D(21, 452, 32);
            Vector3D point5 = new Vector3D(651, 782, 45);
            Vector3D point6 = new Vector3D(-651, 128, 123);

            listPointsNotSorted.Add(point1);
            listPointsNotSorted.Add(point2);
            listPointsNotSorted.Add(point3);
            listPointsNotSorted.Add(point4);
            listPointsNotSorted.Add(point5);
            listPointsNotSorted.Add(point6);


            Echo("test");


            /*
            listPointsNotSorted = new List<Vector3D>();
            foreach (int testInt in Enumerable.Range(0, N))
            {
                listPointsNotSorted.Add(new Vector3D(testInt, testInt, testInt));
            }

            Random rnd = new Random();
            listPointsNotSorted = new List<Vector3D>();
            foreach (int testInt in Enumerable.Range(0, N))
            {
                int numCoord = rnd.Next() % 1024;
                listPointsNotSorted.Add(new Vector3D(numCoord, numCoord, numCoord));
            }
            
            */
            //N = N * 2;

            //Random rnd = new Random(0);
            Random rnd = new Random();

            //N = (N + rnd.Next()) % 1000;
            //N = (N + 10) % 1000;
            N = (N + 1) % 1000;

            listPointsNotSorted = new List<Vector3D>();
            foreach (int testInt in Enumerable.Range(0, N))
            {
                //int numCoordx = rnd.Next() % 1024;
                //int numCoordy = rnd.Next() % 1024;
                //int numCoordz = rnd.Next() % 1024;
                int numCoordx = -512 + rnd.Next() % 1024;
                int numCoordy = -512 + rnd.Next() % 1024;
                int numCoordz = -512 + rnd.Next() % 1024;
                listPointsNotSorted.Add(new Vector3D(numCoordx, numCoordy, numCoordz));
            }


            Echo("listPointsNotSorted.Count:0 to " + (listPointsNotSorted.Count - 1));


            rootOctoTree = new OctoTree(listPointsNotSorted,0);
            Echo("rootOctoTree init done");

            /*
            Echo("" + rootOctoTree.GetDebuggerDisplay());
            Echo("======================");
            Echo("" + rootOctoTree.left.GetDebuggerDisplay());
            Echo("" + rootOctoTree.right.GetDebuggerDisplay());
            
            Echo("======================");
            Echo("" + rootOctoTree.right.right.right.GetDebuggerDisplay());
            Echo("" + rootOctoTree.right.right.right.right.right.GetDebuggerDisplay());
            
            
            Echo("======================");
            
            Echo("" + rootOctoTree.GetDebuggerDisplayWithLeafs());
            */
            //Echo("" + rootOctoTree.GetDebuggerDisplayWithLeafsRec());
            /*
            Echo("" + rootOctoTree.left.GetDebuggerDisplayWithLeafs());
            Echo("" + rootOctoTree.right.GetDebuggerDisplayWithLeafs());
            Echo("======================");
            Echo("" + rootOctoTree.GetDebuggerDisplayWithLeafsRec());
            */



            //List<Vector3D> testClosestneighbor = rootOctoTree.listOfPointsInBranch(new Vector3D(4.3, 4.3, 4.3), 0);
            //List<Vector3D> testClosestneighbor = rootOctoTree.listOfPointsInBranch(new Vector3D(4, 4, 4), 0);
            //List<Vector3D> testClosestneighbor = rootOctoTree.listOfPointsInBranch(new Vector3D(0, 0, 0), 0);
            /*
            List<Vector3D> testClosestneighbor = rootOctoTree.listOfPointsInBranch(new Vector3D(12, 12, 12), 0);

            foreach (Vector3D points in testClosestneighbor)
            {
                Echo("points:" + points);
            }
            */

            Vector3D positionToTest = new Vector3D(11.9, 11.9, 11.9);
            //Vector3D positionToTest = new Vector3D(12, 12, 12);
            //Vector3D positionToTest = new Vector3D(12.1, 12.1, 12.1);
            //Vector3D positionToTest = new Vector3D(11.9, 11.9, 11.9);
            //Vector3D positionToTest = new Vector3D(4.3, 4.3, 4.3);

            //Echo("" + rootOctoTree.GetDebuggerDisplayWithLeafsRec());
            //Echo("" + rootOctoTree.listOfPointsInBranch(positionToTest, 0));
            //Echo("" + rootOctoTree.listOfPointsInBranch(positionToTest, 0));
            Echo("" + rootOctoTree.listOfPointsInBranch(positionToTest, 0));
            //Echo("" + rootOctoTree.listOfPointsInBranch(positionToTest, 0));




            
            double minDistanceForeach = 500000;
            Vector3D closestPointForeach = new Vector3D(0,0,0);
            foreach (Vector3D vect in listPointsNotSorted)
            {
                double tmpDistance = (vect - positionToTest).Length();
                if(tmpDistance< minDistanceForeach)
                {
                    minDistanceForeach = tmpDistance;
                    closestPointForeach = vect;
                }
            }

            Echo("minDistanceForeach:" + minDistanceForeach);
            Echo("closestPointForeach:" + closestPointForeach);
            
        }
    }
}
