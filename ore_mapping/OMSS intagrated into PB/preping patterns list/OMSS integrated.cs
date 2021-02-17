List<String> stringList = new List<String>();
List<String> stringListOres = new List<String>();

List<Vector2D> oreCoords2DSubPattern = new List<Vector2D>();

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

    stringList.Add("5.285714285714286,50.857142857142854,SiNiMg");
    stringList.Add("7.266666666666667,17.666666666666668,SiUrMg");
    stringList.Add("8.666666666666666,88.125,UrMgSi");
    stringList.Add("10.785714285714286,112.14285714285714,AgAu");
    stringList.Add("23.434782608695652,49.08695652173913,UrAuFe");
    stringList.Add("26.785714285714285,76.14285714285714,AgAu");
    stringList.Add("34.22857142857143,16.685714285714287,MgSiNi");
    stringList.Add("36.666666666666664,109.125,UrMgSi");
    stringList.Add("42.833333333333336,42.333333333333336,FeNiCo");
    stringList.Add("51.22857142857143,70.68571428571428,MgSiNi");
    stringList.Add("60.56666666666667,7.033333333333333,FeNiCo");
    stringList.Add("62.88235294117647,104.32352941176471,FeAuUr");
    stringList.Add("69.88235294117646,29.323529411764707,FeAuUr");
    stringList.Add("70.78571428571429,55.142857142857146,AgAu");
    stringList.Add("78.26666666666667,79.66666666666667,SiUrMg");
    stringList.Add("88.78571428571429,108.14285714285714,AgAu");
    stringList.Add("90.66666666666667,13.125,UrMgSi");
    stringList.Add("92.28571428571429,53.857142857142854,SiNiMg");
    stringList.Add("96.66666666666667,82.125,UrMgSi");
    stringList.Add("112.83333333333333,18.333333333333332,FeNiCo");
    stringList.Add("114.70833333333333,83.66666666666667,UrAgCo");
    stringList.Add("116.41666666666667,52.083333333333336,UrAgCo");
    stringList.Add("115.26666666666667,112.66666666666667,SiUrMg");

    foreach(var str in stringList)
    {

        // using the method 
        String[] strlist = str.Split(',');
        /*Echo("str" + str);
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);*/
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[2];
		stringListOres.Add(ores);
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo(oreCoords2DSubPattern.Count+"");

	Echo("lol4");

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


	Echo("lol3");

    //Get the PB Position:
    Vector3D myPos = Me.GetPosition();


    //Don't change unless you know what you are doing: 128 * 16 = 2048
    int constantNumbersOfSubPatternToGenerate = 16;

	Vector3D vec3Dtarget = new Vector3D(0,0,0);
	List<IMyShipController> shipsControllers = new List<IMyShipController>();
	GridTerminalSystem.GetBlocksOfType<IMyShipController>(shipsControllers);
	if(shipsControllers.Count==0){
			Echo("This script need any kind of ship controller\n\na remote control, cockpit, ....");
			return;
	}
	IMyShipController firstController = shipsControllers[0];
    //Get any control capable block to get the planet center
	//using the expected remote control to give us the center of the current planet
	bool planetCenterDetected = firstController.TryGetPlanetPosition(out vec3Dtarget);
	
	Echo("vec3Dtarget"+vec3Dtarget);
	
	Echo("lol2");
	
	if(planetCenterDetected){
		
		Echo("planet detected");
		
		
		Echo("checking detected planet");
		
		
		//Every stock planets center
		List<String> listOfPlanetsGPSString = new List<String>();

		listOfPlanetsGPSString.Add("GPS:EarthLike:0.50:0.50:0.50:");
		listOfPlanetsGPSString.Add("GPS:Moon:16384.50:136384.50:-113615.50:");

		listOfPlanetsGPSString.Add("GPS:Mars:1031072.50:131072.50:1631072.50:");
		listOfPlanetsGPSString.Add("GPS:Europa:916384.50:16384.50:1616384.50:");

		listOfPlanetsGPSString.Add("GPS:Triton:-284463.50:-2434463.50:365536.50:");

		listOfPlanetsGPSString.Add("GPS:Pertam:-3967231.50:-32231.50:-767231.50:");

		listOfPlanetsGPSString.Add("GPS:Alien:131072.50:131072.50:5731072.50:");
		listOfPlanetsGPSString.Add("GPS:Titan:36384.50:226384.50:5796384.50:");
		
		// if you want to use the ship controller for some reason
		// myPos = firstController.GetPosition();
		
		Vector3D detectedPlanet = new Vector3D(0,0,0);
		MyWaypointInfo tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
		
		Echo("lol1");
		
		foreach(string str in listOfPlanetsGPSString){
			
			//MyWaypointInfo tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
			tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
			MyWaypointInfo.TryParse(str, out tmpTestPlanetCenter);
			//Echo("tmpTestPlanetCenter"+tmpTestPlanetCenter);
			
			Vector3D tmpVector3DplanetCenter = tmpTestPlanetCenter.Coords;
			//Echo("tmpVector3DplanetCenter"+tmpVector3DplanetCenter);
			
			 Vector3D vector3DToPlanetCenter = tmpVector3DplanetCenter - myPos;
			 
			 double distanceToPlanetCenter = vector3DToPlanetCenter.Length();
			Echo("distanceToPlanetCenter"+distanceToPlanetCenter);
			
			if(distanceToPlanetCenter < 100000){
				detectedPlanet = tmpVector3DplanetCenter;
				break;
			}
		}
		
		
		//detect if it is a known planet
		if(detectedPlanet != new Vector3D(0,0,0)){
			Echo("tmpTestPlanetCenter"+tmpTestPlanetCenter);
			
			string planetsName = tmpTestPlanetCenter.Name;
			
			Echo("planetsName: "+planetsName);
			
			Echo("detectedPlanet "+detectedPlanet);
			Vector3D cubeCenter = detectedPlanet;
			
			//in meters
			double planet_radius = 0;
			//width constant of sub pattern
			int subPatternSize = 128;
			
			//choose the appropriate settings to use for the detected planet
			//or adapt to local elevation of the firstController?
			if(planetsName == "Alien"){
				planet_radius = 60000;
			}
			if(planetsName == "EarthLike"){
				planet_radius = 58200;
			}
			if(planetsName == "Europa"){
				planet_radius = 9650;
			}
			if(planetsName == "Mars"){
				planet_radius = 58000;
			}
			if(planetsName == "Moon"){
				planet_radius = 8500;
			}
			if(planetsName == "Pertam"){
				planet_radius = 30000;
			}
			if(planetsName == "Titan"){
				planet_radius = 9300;
			}
			if(planetsName == "Triton"){
				planet_radius = 38000;
			}
			
			List<int> intIndexFaces = new List<int>(6);
			intIndexFaces.Add(0);
			intIndexFaces.Add(1);
			intIndexFaces.Add(2);
			intIndexFaces.Add(3);
			intIndexFaces.Add(4);
			intIndexFaces.Add(5);
			
			Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
			foreach(int intTmp in intIndexFaces){
				Echo("intTmp "+intTmp);
				if(intTmp == 0)
				{
					centerFacePositionOffset = new Vector3D(0, 0, planet_radius);
				}
				if(intTmp == 1)
				{
					centerFacePositionOffset = new Vector3D(0, -planet_radius,0);
				}
				if(intTmp == 2)
				{
					centerFacePositionOffset = new Vector3D(0, 0, -planet_radius);
				}
				if(intTmp == 3)
				{
					centerFacePositionOffset = new Vector3D(planet_radius,0,0);
				}
				if(intTmp == 4)
				{
					centerFacePositionOffset = new Vector3D(-planet_radius,0,0);
				}
				if(intTmp == 5)
				{
					centerFacePositionOffset = new Vector3D(0, planet_radius,0);
				}
				Vector3D centerFacePosition = detectedPlanet + centerFacePositionOffset;
				
				Echo("centerFacePosition "+centerFacePosition);
				
				Vector3D vectorToFaceCenter = myPos - centerFacePosition;
				
				Echo("vectorToFaceCenter "+vectorToFaceCenter);
				
				double distanceToFaceCenter = vectorToFaceCenter.Length();
				
				Echo("distanceToFaceCenter "+distanceToFaceCenter);
				
				if(distanceToFaceCenter < 0.707 * planet_radius){
					Echo("face close enough to try to generate");
					
					int intXsubPattern = 0;
					int intYsubPattern = 0;
					
					int[] subIntSubPattern = Enumerable.Range(0, 15).ToArray();
					
					double intX = 0;
					double intY = 0;
					double intZ = 0;
					
					Vector3D generated_gps_point_on_cube = new Vector3D(0,0,0);
					
					
					List<float> centroid_surface_lack_planetSized = new List<float>();
					centroid_surface_lack_planetSized.Add(0);
					centroid_surface_lack_planetSized.Add(0);
					
					
                    //centroid_surface_lack_planetSized = arr.array('d', [2*planet_radius* (centroid_surface_lack_array[0]/2048),2*planet_radius* (centroid_surface_lack_array[1]/2048)])
                    // print("centroid_surface_lack_planetSized:",centroid_surface_lack_planetSized)
					
					foreach(int subXindex in subIntSubPattern){
						Echo("subXindex"+subXindex);
						foreach(int subYindex in subIntSubPattern){
							Echo("subYindex"+subYindex);	
							int oreCoordSubPatternIndex = 0;
							foreach(Vector2D oreCoordSubPattern in oreCoords2DSubPattern){
								Echo("oreCoordSubPattern"+oreCoordSubPattern);
								/*centroid_surface_lack_planetSized[0] = (128 * subXindex+oreCoordSubPattern.X) * (2*planet_radius/2048);
								centroid_surface_lack_planetSized[1] = (128 * subYindex+oreCoordSubPattern.Y) * (2*planet_radius/2048);*/
								centroid_surface_lack_planetSized[0] = Convert.ToSingle((128 * subXindex+oreCoordSubPattern.X) * (2*planet_radius/2048));
								centroid_surface_lack_planetSized[1] = Convert.ToSingle((128 * subYindex+oreCoordSubPattern.Y) * (2*planet_radius/2048));
								
								
					
								if(intTmp==0){
									intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
									generated_gps_point_on_cube = new Vector3D(intX, intY,planet_radius);
								}
								if(intTmp==1){
									intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									//intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									generated_gps_point_on_cube = new Vector3D(intX,-planet_radius, intZ);
								}
								if(intTmp==2){
									intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
									generated_gps_point_on_cube = new Vector3D(intX, intY,-planet_radius);	
								}
								if(intTmp==3){
									// intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									generated_gps_point_on_cube = new Vector3D(planet_radius,intY, intZ);
								}
								if(intTmp==4){
									//intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									generated_gps_point_on_cube = new Vector3D(-planet_radius,intY, intZ);
								}
								if(intTmp==5){
									intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									// intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet);
									generated_gps_point_on_cube = new Vector3D(intX,planet_radius, intZ);
								}
					
								Vector3D generated_gps_point_on_planet = new Vector3D(0,0,0);
								
								Echo("generated_gps_point_on_cube"+generated_gps_point_on_cube);
								//Echo(typeof(generated_gps_point_on_cube));
								
								Vector3D generated_gps_point_on_cube_norm = Vector3D.Normalize(generated_gps_point_on_cube);
								
								
								generated_gps_point_on_planet =  planet_radius * Vector3D.Normalize(generated_gps_point_on_cube_norm)+ cubeCenter;
								
								Echo("generated_gps_point_on_planet"+generated_gps_point_on_planet);
								
								string oreNames = stringListOres[oreCoordSubPatternIndex];
								
								Echo("oreNames"+oreNames);
								
								//MyWaypointInfo tmpWPI  = new MyWaypointInfo("test", generated_gps_point_on_planet);
								MyWaypointInfo tmpWPI  = new MyWaypointInfo(oreNames, generated_gps_point_on_planet);
								
								//Me.CustomData = "lol6";
								Me.CustomData = tmpWPI.ToString();
								
								//enerated_gps_point_on_planet = (planet_radius+alt_adj) * (
                                //generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))+center_of_planet
								
								oreCoordSubPatternIndex +=1 ;
								//return;
							}
							//oreCoords2DSubPattern
						}
					}
				}
				
				
			}
		}
		else {
			Echo("unidentified planet");
			return;
		}
	}
	


    //generate all coordinates to the cube planet

    //get the first three closest faces.
    
    //try to get closer with the first ores in the subpattern infos? (quicker to run ?)

    //then generated all ores nearby

    //sort and write those closest in the Custom Data



    //put the script output inside the customdata of the PB it is running onto
    //Me.CustomData = "";
}
