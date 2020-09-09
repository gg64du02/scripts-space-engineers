double range_to_test_at = 6000;


//Constants to generate equidistributed points on the surface of a sphere
//sphere radius in meters
double r = 1;

//Numbers of wanted points
double N = 5000;

//numbers of generated points
double N_count = 0;

double a = 0;
double d = 0;

double M_v = 0;
double d_v = 0;
double d_phi = 0;

double planetRadius = 0;

List<Vector3D> pointsToScan = new List<Vector3D>();

List<MyDetectedEntityInfo> scanResults = new List<MyDetectedEntityInfo>();

List<IMyCameraBlock> cameraBlocksList = new List<IMyCameraBlock>();

int m_in_main = 0;
int n_in_main = 0;

public List<Vector3D> generateWaypoints(IMyRemoteControl remote)
{
    List<Vector3D> generatedPoints = new List<Vector3D>();

    for (int m = m_in_main; m < M_v; m++)
    {
        double v = Math.PI * (m + .5) / M_v;
        double M_phi = Math.Round((2 * Math.PI * Math.Sin(v) / d_v), 3);
		//Echo("M_v:"+M_v+"|M_phi:"+M_phi);
        for (int n = n_in_main; n < M_phi; n++)
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
			if(N_count%10==0){
				m_in_main = m;
				n++;
				if(!(n < M_phi)){
					m_in_main++;
				}
				n_in_main = n;
				return generatedPoints;
			}
        }
    }

	m_in_main = -1;
	n_in_main = -1;
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
    Runtime.UpdateFrequency = UpdateFrequency.Update100;


    a = 4 * Math.PI * (r * r) / N;
    d = Math.Sqrt(a);


    M_v = Math.Round((Math.PI / d), 3);
    d_v = Math.PI / M_v;
    d_phi = a / d_v;

    planetRadius = range_to_test_at;


    GridTerminalSystem.GetBlocksOfType<IMyCameraBlock>(cameraBlocksList);
    
    foreach (var cb in cameraBlocksList)
    {
        if (cb.EnableRaycast == false)
        {
            cb.EnableRaycast = true;
        }
    }
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
    //avg = avg * 0.99 + Runtime.LastRunTimeMs * 0.01;
    avg = avg * 0.9 + Runtime.LastRunTimeMs * 0.1;
    Echo(avg + "");
	

    IMyCameraBlock cameraBlock = cameraBlocksList[0];
    //degree not radians ?
    Echo("RaycastConeLimit " + cameraBlock.RaycastConeLimit);
    Echo("Position " + cameraBlock.Position);
	
	Echo("N " + N);
	Echo("N_count " + N_count);
    Echo("pointsToScan.Count:"+pointsToScan.Count);
	
	Echo("scanResults.Count "+scanResults.Count );


    if (avg > .02)
    {
        return;
    }

    if (pointsToScan.Count <5)
    {
		//Echo(m_in_main + " "+ n_in_main);
		if(N_count<N*1.02){
			List<Vector3D> tmpListV3D = generateWaypoints(null);

			foreach(Vector3D point in tmpListV3D){
				pointsToScan.Add(point);
			}
		}
		else{
			Echo("!if(N_count<N*1.02)");
			if(pointsToScan.Count  == 0){
				Echo("scan finnished");
			}
		}
    }

    //Echo("N:" + N);
    //Echo("a:" + a);
    //Echo("d:" + d);
    //Echo("M_v:" + M_v);
    //Echo("d_v:" + d_v);
    //Echo("d_phi:" + d_phi);

    //Echo("" + Me.Position);
    //Echo("" + Me.GetPosition());
    //Echo("RaycastDistanceLimit " + cameraBlock.RaycastDistanceLimit);
	
    foreach (var result in scanResults)
    {
        //    //example: "GPS:/// #4:53590.85:-26608.05:11979.08:
        //    Echo(scanResults.IndexOf(result) + "" + result.Name + "\n" + result.Type + "\n" + result.HitPosition);
        Vector3D tmpV3D = (Vector3D)result.HitPosition;
        MyWaypointInfo tmpWP = new MyWaypointInfo("scan " + scanResults.IndexOf(result), Vector3D.Round((tmpV3D),0));
        //Echo(tmpWP.ToString());
    }

    if (pointsToScan.Count == 0)
    {
        return;
    }
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


    List<IMyTextPanel> textPanelList = new List<IMyTextPanel>();

    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(textPanelList);

	//font size 0.78
    if (textPanelList.Count != 0)
    {
        foreach (IMyTextPanel tp in textPanelList)
        {
			 //Echo(""+tp.CustomName);
			 string tmpCmdTest = tp.CustomName;
			if(tmpCmdTest.Contains("RR ")==true){
				//RR -p<page_number> [-all] <options>
				Echo("writting in the LCD");
				StringBuilder sb = new StringBuilder(tmpCmdTest+":", 500);
				if(tmpCmdTest.Contains("-all")==true){
					sb.AppendFormat("total:"+scanResults.Count + " " +"\n");
					foreach (var result in scanResults)
					{
						//    List<MyDetectedEntityInfo> scanResults 
						//Echo(""+result.Type	);
						Vector3D tmpV3D = (Vector3D)result.HitPosition;
						MyWaypointInfo tmpWP = new MyWaypointInfo("scan " + scanResults.IndexOf(result), Vector3D.Round((tmpV3D),0));
						sb.AppendFormat(tmpWP.ToString() +"\n");
					}
				}
				else{
					//    RR -SG -LG -CH -FO -As -Me -Mi -p2
					//
					//bool No = tmpCmdTest.Contains("-No");
					bool SG = tmpCmdTest.Contains("-SG");
					bool LG = tmpCmdTest.Contains("-LG");
					bool CH = tmpCmdTest.Contains("-CH");
					bool CO = tmpCmdTest.Contains("-CO");
					bool FO = tmpCmdTest.Contains("-FO");
					bool As = tmpCmdTest.Contains("-As");
					//bool Pl = tmpCmdTest.Contains("-Pl");
					bool Me = tmpCmdTest.Contains("-Me");
					bool Mi = tmpCmdTest.Contains("-Mi");
					var pageNumber = tmpCmdTest.Split(' ');
					sb.AppendFormat("total:"+scanResults.Count + " Pages:"+ (scanResults.Count/5+1) +"\n");
					sb.AppendFormat("total:"+scanResults.Count + " Pages:"+ (scanResults.Count/5+1) +"Page:"+pageNumber+"\n");
					//tmpCmdTest.Contains("-p")
					foreach (var result in scanResults){
						Vector3D tmpV3D = (Vector3D)result.HitPosition;
						MyWaypointInfo tmpWP = new MyWaypointInfo("scan " + scanResults.IndexOf(result), Vector3D.Round((tmpV3D),0));
						//Echo(""+result.Type);
						if(SG){
							if(result.Type==MyDetectedEntityType.SmallGrid){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(LG){
							if(result.Type==MyDetectedEntityType.LargeGrid){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(CH){
							if(result.Type==MyDetectedEntityType.CharacterHuman){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(CO){
							if(result.Type==MyDetectedEntityType.CharacterOther){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(FO){
							if(result.Type==MyDetectedEntityType.FloatingObject){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(As){
							if(result.Type==MyDetectedEntityType.Asteroid){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(Me){
							if(result.Type==MyDetectedEntityType.Meteor){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
						if(Mi){
							if(result.Type==MyDetectedEntityType.Missile){
								sb.AppendFormat(tmpWP.ToString() +"\n");
							}
						}
					}
				}
				
				tp.WriteText(sb,false);
				
				//header 
				
				//content of the page
				
			}
        }
    }
    else
    {
        Echo("No LCD to display on");
    }
}
