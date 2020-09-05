double range_to_test_at = 6000;


//Constants to generate equidistributed points on the surface of a sphere
//sphere radius in meters
double r = 1;

//Numbers of wanted points
double N = 1500;

//numbers of generated points
double N_count = 0;

double a = 0;
double d = 0;

double M_v = 0;
double d_v = 0;
double d_phi = 0;

double planetRadius = 0;

List<Vector3D> pointsToScan = null;

List<MyDetectedEntityInfo> scanResults = new List<MyDetectedEntityInfo>();



public List<Vector3D> generateWaypoints(IMyRemoteControl remote)
{
    List<Vector3D> generatedPoints = new List<Vector3D>();

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

            Vector3D tmpCenterOfPlanet = new Vector3D(0, 0, 0);
            if (remote != null)
            {
                remote.TryGetPlanetPosition(out tmpCenterOfPlanet);
                generatedPoint += tmpCenterOfPlanet;
            }
            if(remote == null)
            {
                generatedPoint += Me.GetPosition();
            }
            //Echo("generatedPoint:"+ generatedPoint);
            generatedPoints.Add(generatedPoint);
            N_count = N_count + 1;
        }
    }

    return generatedPoints;
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
    Runtime.UpdateFrequency = UpdateFrequency.Update1;


    a = 4 * Math.PI * (r * r) / N;
    d = Math.Sqrt(a);


    M_v = Math.Round((Math.PI / d), 3);
    d_v = Math.PI / M_v;
    d_phi = a / d_v;

    planetRadius = range_to_test_at;
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

    //Best we have is Runtime.LastRunTime and Runtime.TimeSinceLastRun
    double dts = Runtime.TimeSinceLastRun.TotalSeconds;
    Echo("dts:" + dts);
    double dts2 = Runtime.LastRunTimeMs;
    Echo("dts2:" + dts2);

    double avg = 0;
    avg = avg * 0.99 + Runtime.LastRunTimeMs * 0.01;
    Echo(avg + "");


    List<IMyCameraBlock> cameraBlocksList = new List<IMyCameraBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyCameraBlock>(cameraBlocksList);
    IMyCameraBlock cameraBlock = cameraBlocksList[0];

    Echo("" + cameraBlock.DefinitionDisplayNameText);

    Echo("1");
    foreach(var cb in cameraBlocksList)
    {
        if (cb.EnableRaycast == false)
        {
            cb.EnableRaycast = true;
        }
    }

    if (pointsToScan == null)
    {
        pointsToScan = generateWaypoints(null);
    }

    //Echo("N:" + N);
    //Echo("a:" + a);
    //Echo("d:" + d);
    //Echo("M_v:" + M_v);
    //Echo("d_v:" + d_v);
    //Echo("d_phi:" + d_phi);

    //degree not radians ?
    Echo("RaycastConeLimit " + cameraBlock.RaycastConeLimit);

    Echo("Position " + cameraBlock.Position);

    //Echo("" + Me.Position);

    //Echo("" + Me.GetPosition());

    //Echo("RaycastDistanceLimit " + cameraBlock.RaycastDistanceLimit);

    

    Echo("pointsToScan.Count:"+pointsToScan.Count);

    foreach (var cb in cameraBlocksList)
    {
        //Echo("AvailableScanRange " + Math.Round((cb.AvailableScanRange), 2));
        if (cb.AvailableScanRange > range_to_test_at)
        {
            //Echo("AvailableScanRange> range_to_test_at");
            bool canScanOnePoint = false;
            Vector3D pointAboutToBeScanned = new Vector3D(0, 0, 0);
            foreach (Vector3D point in pointsToScan)
            {
                //CanScan(Vector3D)   Checks if the camera can scan to the given target
                if (cb.CanScan(point) == true)
                {
                    canScanOnePoint = true;
                    pointAboutToBeScanned = point;
                    break;
                }
            }
            if (canScanOnePoint == true)
            {
                var result = cb.Raycast(pointAboutToBeScanned);
                //Echo("result " + result);
                //Echo("result.HitPosition " + result.HitPosition);
                if (result.Type != MyDetectedEntityType.Planet)
                {
                    if (result.Type != MyDetectedEntityType.None)
                    {
                        scanResults.Add(result);
                    }
                }
                pointsToScan.Remove(pointAboutToBeScanned);
                break;
            }

        }
    }

    foreach (var result in scanResults)
    {
        //    Echo("=======================");
        //    Echo(scanResults.IndexOf(result) + "" + result.Name + "\n" + result.Type + "\n" + result.HitPosition);
        //    //example: "GPS:/// #4:53590.85:-26608.05:11979.08:
        //    Echo(scanResults.IndexOf(result) + "" + result.Name + "\n" + result.Type + "\n" + result.HitPosition);
        Vector3D tmpV3D = (Vector3D)result.HitPosition;
        MyWaypointInfo tmpWP = new MyWaypointInfo("scan " + scanResults.IndexOf(result), tmpV3D);
        Echo(tmpWP.ToString());
    }

}


