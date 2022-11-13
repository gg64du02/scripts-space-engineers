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
        Node rootNode = new Node();

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

        public class Node
        {

            int axisDepth = -1;
            //0 is x
            //1 is y
            //2 is z
            int intAxis = -1;

            Bound bounds;

            Vector3D Point = new Vector3D(0, 0, 0);

            bool leaf = false;
            Node left;
            Node right;
            public Node(Bound bound)
            {

            }



            public Node(Vector3D pointLeaf)
            {
                Point = pointLeaf;
                leaf = true;
            }
            public Node()
            {
                //placeholder
            }
            public Node(List<Vector3D> listToBeSorted)
            {
                //TODO: listToBeSorted sort this on an axis (0 for root)
                //Echo("" + listToBeSorted.Count);
                axisDepth = axisDepth + 1;
                intAxis = axisDepth % 3;
                if (listToBeSorted.Count <= 2)
                {
                    if(listToBeSorted.Count == 1)
                    {
                        //TODO: store a point
                        //left bias
                        Point = listToBeSorted[0];
                    }
                    else
                    {
                        //store a point and make a leaf (Node) (left bias)
                        Point = listToBeSorted[0];
                        left = new Node(listToBeSorted[1]);
                    }
                }
                else
                {
                    //TODO: store a point and make 2 Node

                    int intIndexPoint = (listToBeSorted.Count - 1) / 2;

                    int startLeft = 0;
                    int endLeft = intIndexPoint - 1;

                    int startRight = intIndexPoint + 1;
                    int endtRight = listToBeSorted.Count - 1;

                    List<Vector3D> subListLeft = listToBeSorted.GetRange(startLeft, endLeft);
                    List<Vector3D> subListRight = listToBeSorted.GetRange(startRight, endtRight);

                    Point = listToBeSorted[intIndexPoint];
                    left = new Node(subListLeft);
                    right = new Node(subListRight);


                }
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

            //rootNode = new Node();
            rootNode = new Node(listPointsNotSorted);

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
        }
    }
}
