﻿using System;

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

namespace devOctoTree2
{
    class Program
    {
        public class octoNode
        {

            public double[] x = new double[3];

            public octoNode left, right;

            /*
            public octoNode(Vector3D v)
            {
                x[0] = v.X;
                x[1] = v.Y;
                x[2] = v.Z;
            }
            */

        }

        static public double dist(octoNode a, octoNode b, int dim)
        {
            double t, d = 0;
            dim = dim - 1;
            while (dim >= 0)
            {
                t = a.x[dim] - b.x[dim];
                d += t * t;
                dim = dim - 1;
            }
            return d;
        }
        static public double dist2(Vector3D a, Vector3D b)
        {
            double d;
            d = (a - b).LengthSquared();
            return d;
        }


        static int visited=0;


        static void nearest(octoNode root, octoNode nd, int i, int dim, ref octoNode best,ref double best_dist)
        {
            double d, dx, dx2;

            if (root==null) return;
            d = dist(root, nd, dim);
            //d = dist2(convertOctoNodeToV3D(root), convertOctoNodeToV3D(nd));
            dx = root.x[i] - nd.x[i];
            dx2 = dx* dx;

            visited ++;

            if ((best==null) || d< best_dist) {
                best_dist = d;
                best = root;
            }


            Console.WriteLine("best:" + convertOctoNodeToV3D(best));
            Console.WriteLine("root:" + convertOctoNodeToV3D(root));
            Console.WriteLine("=============");

            /* if chance of exact match is high */
            if (best_dist == null) return;

            if (++i >= dim) i = 0;

            nearest(dx > 0 ? root.left : root.right, nd, i, dim, ref best, ref best_dist);
            if (dx2 >= best_dist) return;
            nearest(dx > 0 ? root.right : root.left, nd, i, dim, ref best, ref best_dist);
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


        static int yieldsAmount = 0;

        public static octoNode maketree2(List<Vector3D> listToBeSorted, int i, int dim)
        {
            octoNode n = new octoNode();

            List<Vector3D> listSorted = sortingOnSpecificAxises(listToBeSorted, i);

            yieldsAmount = yieldsAmount + 1 ;


            int intIndexPoint = (listSorted.Count - 1) / 2;

            int startLeft = 0;
            int endLeft = intIndexPoint - 1;

            int startRight = intIndexPoint + 1;
            int endtRight = listSorted.Count - 1;

            List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft - startLeft + 1);
            List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight - startRight + 1);

            i = (i + 1) % dim;

            //storing the point
            n.x[0] = listSorted[intIndexPoint].X;
            n.x[1] = listSorted[intIndexPoint].Y;
            n.x[2] = listSorted[intIndexPoint].Z;

            if (subListLeft.Count != 0)
            {
                n.left = maketree2(subListLeft, i, dim);
            }
            if (subListRight.Count != 0)
            {
                n.right = maketree2(subListRight, i, dim);
            }

            return n;
            
        }

        public static List<Vector3D> sortingOnSpecificAxises(List<Vector3D> listToSort, int axisOnWhichToSort)
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
        
        static public Vector3D convertOctoNodeToV3D(octoNode ON)
        {
            Vector3D v = new Vector3D();
            v.X = ON.x[0];
            v.Y = ON.x[1];
            v.Z = ON.x[2];
            return v;
        }

        public string printNode(octoNode point)
        {
            return ""+convertOctoNodeToV3D(point);
        }

        public string pointTree(octoNode root, int depth)
        {
            string result = "";
            result = result + "depth:" + depth + ":" + printNode(root) + "\n";
            if (root.left != null) result = result + "left:" + pointTree(root.left, depth + 1) + "\n";
            if (root.right != null) result = result + "right:" + pointTree(root.right , depth + 1) + "\n";
            return result;
        }


         static void Main(string[] args)
        {
            octoNode rootOctoNode;
            
            Random rnd = new Random(0);
            //Random rnd = new Random();

            int N = 16;

            List<Vector3D>  listPointsNotSorted = new List<Vector3D>();
            foreach (int testInt in Enumerable.Range(0, N))
            {
                int numCoordx = -512 + rnd.Next() % 1024;
                int numCoordy = -512 + rnd.Next() % 1024;
                int numCoordz = -512 + rnd.Next() % 1024;
                listPointsNotSorted.Add(new Vector3D(numCoordx, numCoordy, numCoordz));
            }

            rootOctoNode = maketree2(listPointsNotSorted, 0, 3);

            //Vector3D v3d = new Vector3D(-49, -140, 107);
            //Vector3D v3d = new Vector3D(-49, -140, 87);
            //Vector3D v3d = new Vector3D(-45, -120, 60);
            //Vector3D v3d = new Vector3D(0,0,0);
            //Vector3D v3d = new Vector3D(11.9, 11.9, 11.9);
            Vector3D v3d = new Vector3D(119, 119, 119);

            octoNode testON = new octoNode();
            octoNode test_Best = new octoNode();

            testON.x[0] = v3d.X;
            testON.x[1] = v3d.Y;
            testON.x[2] = v3d.Z;

            double best_dist = 500000;

            nearest(rootOctoNode, testON, 0, 3, ref test_Best, ref best_dist);

            Vector3D v3d_test_Best = convertOctoNodeToV3D(test_Best);

            string infos_clos = "" + (v3d_test_Best - v3d).Length();

            Vector3D actualClosest = new Vector3D(500000, 500000, 500000);
            double actualClosestDist = 500000;

            foreach(Vector3D VD in listPointsNotSorted)
            {

                double tmpDist = (v3d - VD).Length();
                if(actualClosestDist > tmpDist)
                {
                    actualClosestDist = tmpDist;
                    actualClosest = VD;
                }
            }

            Console.WriteLine("visited:" + visited);
            Console.WriteLine("yieldsAmount:" + yieldsAmount);

            Console.WriteLine("Hello World!");
        }
    }
}
