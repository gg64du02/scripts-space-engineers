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

        Vector3I overallBounds;
        OctoTree rootOctoTree = new OctoTree();

        List<Vector3D> listPointsNotSorted = new List<Vector3D>();
        List<Vector3D> listPointsSortedForRoot = new List<Vector3D>();

        public class Bound
        {
            Vector3D minBound;
            Vector3D maxBound;
            Vector3I BoundDim;

            int volumeCovered;
            public Bound(Vector3I bound)
            {

            }

        }
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

            Bound bounds;

            public Vector3D Point = new Vector3D(0, 0, 0);

            bool leaf = false;
            public OctoTree left;
            public OctoTree right;
            public OctoTree(Bound bound)
            {

            }
            


            public OctoTree(Vector3D pointLeaf)
            {
                Point = pointLeaf;
                leaf = true;
            }
            public OctoTree()
            {
                //placeholder
            }
            public OctoTree(List<Vector3D> listToBeSorted)
            {
                //TODO: listToBeSorted sort this on an axis (0 for root)
                //Echo("" + listToBeSorted.Count);
                axisDepth = axisDepth + 1;
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
                        Point = listSorted[0];
                        left = new OctoTree(listSorted[1]);
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

                    List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft - startLeft);
                    List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight - startRight);
                    
                    Point = listSorted[intIndexPoint];
                    left = new OctoTree(subListLeft);
                    right = new OctoTree(subListRight);

                    
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
            public string GetDebuggerDisplay()
            {
                string resultStr ="";
                resultStr = resultStr + "right" + right + "\n";
                resultStr = resultStr + "left" + left + "\n";
                resultStr = resultStr + "Point" + Point + "\n";
                resultStr = resultStr + "axisDepth" + axisDepth + "\n";
                resultStr = resultStr + "intAxis" + intAxis + "\n";
                return resultStr;
            }


        }


        /*
                    +------------+------------+
                   /            /            /
                  /            /            /
                 /      5     /      6     /
                /            /            /
               +------------+------------+
              /            /            /
             /    1       /    2       /
            /            /            /
           /            /            /
          +------------+------------+
        
                    +------------+------------+
                   /            /            /
                  /            /            /
                 /      7     /      8     /
                /            /            /
               +------------+------------+
              /            /            /
             /    3       /    4       /
            /            /            /
           /            /            /
          +------------+------------+
         
         */


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

            Vector3D point1 = new Vector3D(0, 0, 0);
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

            //rootOctoTree = new OctoTree();
            rootOctoTree = new OctoTree(listPointsNotSorted);

            Echo("" + rootOctoTree.GetDebuggerDisplay());
        }
    }
}
