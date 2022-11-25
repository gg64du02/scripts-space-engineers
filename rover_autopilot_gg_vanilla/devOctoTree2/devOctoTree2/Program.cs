using System;

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


        }

        public double dist(octoNode a, octoNode b, int dim)
        {
            double t, d = 0;
            while (dim!=0)
            {
                t = a.x[dim] - b.x[dim];
                d += t * t;
                dim = dim - 1;
            }
            return d;
        }

        public void swap(ref octoNode x, ref octoNode y)
        {
            double[] tmp = new double[3];

            tmp = x.x;
            x.x = y.x;
            y.x = tmp;
        }


        int visited;


        void nearest(octoNode root, octoNode nd, int i, int dim,
        ref octoNode best,ref double best_dist)
        {
            double d, dx, dx2;

            if (root==null) return;
            d = dist(root, nd, dim);
                dx = root.x[i] - nd.x[i];
            dx2 = dx* dx;

                visited ++;

            if ((best==null) || d< best_dist) {
                best_dist = d;
                best = root;
            }

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




        public static octoNode maketree2(List<Vector3D> listToBeSorted, int i, int dim)
        {
            octoNode n = new octoNode();

            List<Vector3D> listSorted = sortingOnSpecificAxises(listToBeSorted, i);


            int intIndexPoint = (listSorted.Count - 1) / 2;

            int startLeft = 0;
            int endLeft = intIndexPoint - 1;

            int startRight = intIndexPoint + 1;
            int endtRight = listSorted.Count - 1;

            List<Vector3D> subListLeft = listSorted.GetRange(startLeft, endLeft - startLeft + 1);
            List<Vector3D> subListRight = listSorted.GetRange(startRight, endtRight - startRight + 1);

            i = (i + 1) % dim;
            if (subListLeft.Count != 0)
            {
                n.left = maketree2(subListLeft, i, dim);
            }
            if (subListRight.Count != 0)
            {
                n.right = maketree2(subListRight, i, dim);
            }
            //storing the point
            n.x[0] = listSorted[intIndexPoint].X;
            n.x[1] = listSorted[intIndexPoint].Y;
            n.x[2] = listSorted[intIndexPoint].Z;

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


         static void Main(string[] args)
        {
            octoNode rootOctoNode;

            
            Random rnd = new Random(0);
            //Random rnd = new Random();

            //N = (N + rnd.Next()) % 1000;
            //N = (N + 10) % 1000;
            int N = 35;

            List<Vector3D>  listPointsNotSorted = new List<Vector3D>();
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

            rootOctoNode = maketree2(listPointsNotSorted, 0, 3);




            Console.WriteLine("Hello World!");
        }
    }
}
