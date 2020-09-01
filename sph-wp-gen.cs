//script would be based on
//https://www.cmu.edu/biolphys/deserno/pdf/sphere_equi.pdf

//sphere radius in meters
double r = 1;

//Numbers of wanted points
double N = 50;

//numbers of generated points
double N_count = 0;

double a = 0;
double d = 0;

double M_v = 0;
double d_v = 0;
double d_phi = 0;

double planetRadius = 60000;

List<Vector3D> generatedPoints = new List<Vector3D>();

List<double> listOfRange = new List<Double>();

Vector3D currentShipPos = new Vector3D(30000, 30000, 30000);

public List<Vector3D> nClosestPointsToADesignatedPoint(int n, List<Vector3D> pointsToTest, Vector3D pointToGetCloseTo)
{
    List<Vector3D> tmpListOfClosestPoints = new List<Vector3D>();

    if(pointsToTest.Count <= n)
    {
        //Echo("    if(pointsToTest.Count <= n)");
        return pointsToTest;
    }
    else
    {
        //Echo("    !if(pointsToTest.Count <= n)");
        //pointsToTest.Count > n
        foreach (var pt in pointsToTest)
        {
            //Echo("foreach (var pt in pointsToTest)");
            if (tmpListOfClosestPoints.Count< n)
            {
                //Echo("if (tmpListOfClosestPoints.Count<= n)");
                //fill up the list with the start of the list
                tmpListOfClosestPoints.Add(pt);
            }
            else
            {
                //Echo("!if (tmpListOfClosestPoints.Count<= n)");
                //sweep through the list to find anything closer
                foreach (var pt2 in tmpListOfClosestPoints)
                {
                    //Echo("foreach (var pt2 in tmpListOfClosestPoints)");
                    double tmpRangeSquared1 = (pt - pointToGetCloseTo).LengthSquared();
                    double tmpRangeSquared2 = (pt2 - pointToGetCloseTo).LengthSquared();
                    //test if the next pt or pt2 is closer to pointToGetCloseTo
                    if (Math.Min(tmpRangeSquared1, tmpRangeSquared2) == tmpRangeSquared1)
                    {
                        //Echo("if (Math.Min(tmpRangeSquared1, tmpRangeSquared2) == tmpRangeSquared1)");
                        //means pt is closer than pt2
                        //aka pt2 needs to be replaced

                        tmpListOfClosestPoints.Remove(pt2);

                        tmpListOfClosestPoints.Add(pt);
                        break;
                    }
                }
            }
        }
    }

    return tmpListOfClosestPoints;
}

public void generateWaypoints()
{
    for (int m = 0; m < M_v; m++)
    {
        double v = Math.PI * (m + .5) / M_v;
        double M_phi = Math.Round((2 * Math.PI * Math.Sin(v) / d_v), 3);
        for (int n = 0; n < M_phi; n++)
        {
            double phi = 2 * Math.PI * n / M_phi;
            //Create point using Eqn. (1).
            double x = planetRadius * r * Math.Sin(v) * Math.Cos(phi);
            double y = planetRadius * r * Math.Sin(v) * Math.Sin(phi);
            double z = planetRadius * r * Math.Cos(phi);
            Vector3D generatedPoint = new Vector3D(x, y, z);
            //Echo("generatedPoint:"+ generatedPoint);
            generatedPoints.Add(generatedPoint);
            N_count = N_count + 1;
        }
    }

    Vector3D currentShipPos = new Vector3D(30000, 30000, 30000);
    foreach (Vector3D gp in generatedPoints)
    {
        Vector3D tmpV3D = gp - currentShipPos;
        //Echo("tmpV3D:" + tmpV3D);
        double range = tmpV3D.Length();
        //Echo("range:" + range);
        //length squared needed (quicker)
        double rangeSquared = tmpV3D.LengthSquared();
        //Echo("rangeSquared:" + rangeSquared);
    }


    List<Vector3D> pointsPath = new List<Vector3D>();
    
    //copy of generated that can be modified
    List<Vector3D> RemainingPoints = generatedPoints;

    //get the closest point
    Vector3D pointPath = (nClosestPointsToADesignatedPoint(1, generatedPoints, currentShipPos))[0];
    pointsPath.Add(pointPath);

    //removed it because it is now inside the path
    RemainingPoints.Remove(pointPath);

    List<Vector3D> localPointsToLastPointPath = new List<Vector3D>();

    //start doing the same to the rest of the points
    while (RemainingPoints.Count > 0)
    {
        //6 can be more if we want
        localPointsToLastPointPath = nClosestPointsToADesignatedPoint(6, RemainingPoints, pointPath);
        //1 because we want the closest to the starting position in order to do a peel pattern

        List<Vector3D> nextPointPathList = nClosestPointsToADesignatedPoint(1, RemainingPoints, currentShipPos);

        Vector3D nextPointPath = nextPointPathList[0];

        pointsPath.Add(nextPointPath);
        RemainingPoints.Remove(nextPointPath);
    }

    foreach (Vector3D PP_WPT in pointsPath)
    {
        Echo("PP_WPT:" + PP_WPT);
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
    // It's recommended to set RuntimeInfo.UpdateFrequency 
    // here, which will allow your script to run itself without a 
    // timer block.
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
    a = 4 * Math.PI * (r * r) / N;
    d = Math.Sqrt(a);


    M_v = Math.Round((Math.PI / d),3);
    d_v = Math.PI / M_v;
    d_phi = a / d_v;

    generateWaypoints();
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
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.
    Echo("N:" + N);
    Echo("a:" + a);
    Echo("d:" + d);
    Echo("M_v:" + M_v);
    Echo("d_v:" + d_v);
    Echo("d_phi:" + d_phi);

    //flushing the list for debugging purpose
    generatedPoints = new List<Vector3D>();

    generateWaypoints();

    List<Vector3D> lol = new List<Vector3D>();
    lol= nClosestPointsToADesignatedPoint(6, generatedPoints, new Vector3D(30000, 30000, 30000));

    Echo("lol.Count:" + lol.Count);

    //foreach (Vector3D nCP in lol)
    //{
    //    //Echo("nCP:" + nCP);
    //}

    Echo("N_count:" + N_count);
    Echo("generatedPoints:" + generatedPoints);
    Echo("generatedPoints.Count:" + generatedPoints.Count);

}
