	
IMyShipController myRemoteControl = null;


public Program()
{
    // The constructor, called only once every session and
    // always before any other method is called. Use it to
    // initialize your script. 
    //     
    // The constructor is optional and can be removed if not
    // needed.
    // 
    // It s recommended to set RuntimeInfo.UpdateFrequency 
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



public Vector3D generated_gps_point_on_cube_function(Point pointPixel, int faceNumber, int planet_radius){
		
	double intX = 0;
	double intY = 0;
	double intZ = 0;
		
	if(pointPixel==null){
		pointPixel =  new Point(0,0);
	}
	
	pointPixel = new Point((2*planet_radius/2048)*pointPixel.X,(2*planet_radius/2048)*pointPixel.Y);
	
	Vector3D generated_gps_point_on_cube = new Vector3D(0,0,0);
				
	if(faceNumber==0){
		intX = 1*(- planet_radius+pointPixel.Y*1);
		intY = -1*(- planet_radius+pointPixel.X*1);
		//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
		generated_gps_point_on_cube = new Vector3D(intX, intY,planet_radius);
	}
	if(faceNumber==1){
		intX = 1*(- planet_radius+pointPixel.Y*1);
		//intY = -1*(- planet_radius+pointPixel.X*1);
		intZ = -1*(- planet_radius+pointPixel.X*1);
		generated_gps_point_on_cube = new Vector3D(intX,-planet_radius, intZ);
	}
	if(faceNumber==2){
		intX = -1*(- planet_radius+pointPixel.Y*1);
		intY = -1*(- planet_radius+pointPixel.X*1);
		//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
		generated_gps_point_on_cube = new Vector3D(intX, intY,-planet_radius);	
	}
	if(faceNumber==3){
		// intX = 1*(- planet_radius+pointPixel.Y*1);
		intY = -1*(- planet_radius+pointPixel.X*1);
		intZ = -1*(- planet_radius+pointPixel.Y*1);
		generated_gps_point_on_cube = new Vector3D(planet_radius,intY, intZ);
	}
	if(faceNumber==4){
		//intX = 1*(- planet_radius+pointPixel.Y*1);
		intY = -1*(- planet_radius+pointPixel.X*1);
		intZ = 1*(- planet_radius+pointPixel.Y*1);
		generated_gps_point_on_cube = new Vector3D(-planet_radius,intY, intZ);
	}
	if(faceNumber==5){
		intX = -1*(- planet_radius+pointPixel.Y*1);
		// intY = -1*(- planet_radius+pointPixel.X*1);
		intZ = -1*(- planet_radius+pointPixel.X*1);
		//generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet);
		generated_gps_point_on_cube = new Vector3D(intX,planet_radius, intZ);
	}
	return generated_gps_point_on_cube;
}


public List<Point> fourNextPointsFunction(Point point, int distance, int max_range){
	
	List<Point> nextPoints=new List<Point>();
	
	Point pointUp = new Point(point.X,point.Y+distance);
	Point pointDown = new Point(point.X,point.Y-distance);
	Point pointRight = new Point(point.X+distance,point.Y);
	Point pointLeft = new Point(point.X-distance,point.Y);
	
	if(pointUp.Y<max_range){
		nextPoints.Add(pointUp);
	}
	if(pointDown.Y>0){
		nextPoints.Add(pointDown);
	}
	if(pointRight.X<max_range){
		nextPoints.Add(pointRight);
	}
	if(pointLeft.Y>0){
		nextPoints.Add(pointLeft);
	}
	
	return nextPoints;
}

public void echoFourNextPointsFunction(List<Point> points){
	
	foreach(Point point in points){
		Echo("pointInPoints:"+point);
	}
}



public void Main(string argument, UpdateType updateSource)
{
    // The main entry point of the script, invoked every time
    // one of the programmable block s Run actions are invoked,
    // or the script updates itself. The updateSource argument
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.
	
	//do a alogirthm in cs that can guess the face and pixel position on the face


	
	List<IMyRemoteControl> remoteControllers = new List<IMyRemoteControl>();
	GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(remoteControllers);

	if (remoteControllers.Count != 0)
	{
		foreach (var sc in remoteControllers)
		{
			if (sc.IsSameConstructAs(Me))
			{
				myRemoteControl = (IMyShipController)sc;
			}
		}
	}


    Echo("running...");
	
	// Echo(Me.GetPosition()+"");
	Vector3D myPos = Me.GetPosition();
	List<Point> tmpTestNextPoints = fourNextPointsFunction(new Point(1024,1024),512,2048);
	
	// GPS:///  1:53546.14:-26699.61:11974.64:#FF75C9F1:
	
	// foreach	(Point point in tmpTestNextPoints){
		// Echo("point"+point);
	// }
	
	
	List<int> intIndexFaces = new List<int>(6);
	intIndexFaces.Add(0);
	intIndexFaces.Add(1);
	intIndexFaces.Add(2);
	intIndexFaces.Add(3);
	intIndexFaces.Add(4);
	intIndexFaces.Add(5);
	
	Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
	int planet_radius = 60000;
	
	Vector3D planetCenter = new Vector3D(0,0,0);

	bool planetDetected = remoteControllers[0].TryGetPlanetPosition(out planetCenter);
	
	Echo("planetCenter:"+planetCenter);
	
	planet_radius = (int) (planetCenter-myPos).Length();
	
	Echo("planet_radius:"+planet_radius);
	
	foreach(int intTmp in intIndexFaces){
		//Echo("Checking planet face:"+intTmp);
		if(intTmp == 0)
		{
			continue;
			centerFacePositionOffset = new Vector3D(0, 0, planet_radius);
		}
		if(intTmp == 1)
		{
			continue;
			centerFacePositionOffset = new Vector3D(0, -planet_radius,0);
		}
		if(intTmp == 2)
		{
			continue;
			centerFacePositionOffset = new Vector3D(0, 0, -planet_radius);
		}
		if(intTmp == 3)
		{
			// continue;
			centerFacePositionOffset = new Vector3D(planet_radius,0,0);
		}
		if(intTmp == 4)
		{
			continue;
			centerFacePositionOffset = new Vector3D(-planet_radius,0,0);
		}
		if(intTmp == 5)
		{
			// continue;
			centerFacePositionOffset = new Vector3D(0, planet_radius,0);
		}
		//Vector3D centerFacePosition = detectedPlanet + centerFacePositionOffset;
		Vector3D centerFacePosition = centerFacePositionOffset;
		Vector3D difMyPosCFP = myPos - centerFacePosition;
		// // Echo("difMyPosCFP.Length():");
		// Echo(""+difMyPosCFP.Length());
		
		// GPS:///  1:53546.14:-26699.61:11974.64:#FF75C9F1:
		
		Point testPoint = new Point(0,0);
		
		
		Vector3D generated_gps_point_on_cube = generated_gps_point_on_cube_function(testPoint, intTmp, planet_radius);
		
		Echo("intTmp:"+intTmp);
		
		int currentDistanceGPS = 1000000;

		Point currentPoint = new Point(1024,1024);

		int currentDistancePoint = 512;

		List<Point> fourNextPoints = new List<Point>();
			
		Vector3D cubeFaceCenterFormulaResultPoint = new Vector3D(0,0,0);
		Vector3D cubeFaceCenterFormulaResultTmpClosestPoint = new Vector3D(0,0,0);

			
		Point tmpClosestPoint = new Point(1024,1024);
		
		while(currentDistanceGPS>5000){
			
			//Vector3D cubeFaceCenter = see formulas;
			Vector3D cubeFaceCenter = generated_gps_point_on_cube_function(currentPoint,intTmp,planet_radius);
			
			fourNextPoints = fourNextPointsFunction(currentPoint,currentDistancePoint,2048);
			
			// echoFourNextPointsFunction(fourNextPoints);
			
			double currentDistancePointLength = 1000000;
			double currentDistanceClosestPointLength = 1000000;
			
			foreach (Point point in fourNextPoints){
				// Echo("=================");
				// Echo("testing_tmpClosestPoint:");
				// Echo(""+tmpClosestPoint);
			
				cubeFaceCenterFormulaResultPoint = generated_gps_point_on_cube_function(point,intTmp,planet_radius);
				Vector3D cubeFaceCenterFormulaResultPointNorm = planet_radius*Vector3D.Normalize(cubeFaceCenterFormulaResultPoint);
				Vector3D difMyPosCFCPoint = myPos - cubeFaceCenterFormulaResultPointNorm;
				currentDistancePointLength = difMyPosCFCPoint.Length();
				
				cubeFaceCenterFormulaResultTmpClosestPoint = generated_gps_point_on_cube_function(tmpClosestPoint,intTmp,planet_radius);
				Vector3D cubeFaceCenterFormulaResultTmpClosestPointNorm = planet_radius*Vector3D.Normalize(cubeFaceCenterFormulaResultTmpClosestPoint);
				Vector3D difMyPosCFC = myPos - cubeFaceCenterFormulaResultTmpClosestPointNorm;
				currentDistanceClosestPointLength = difMyPosCFC.Length();
				// Echo("cubeFaceCenterFormulaResultPoint:");
				// Echo(""+cubeFaceCenterFormulaResultPoint);
				// Echo("cubeFaceCenterFormulaResultTmpClosestPoint:");
				// Echo(""+cubeFaceCenterFormulaResultTmpClosestPoint);
				Echo("currentD_PL:"+currentDistancePointLength);
				Echo("currentD_CPL:"+currentDistanceClosestPointLength);
				
				
				if(currentDistancePointLength<currentDistanceClosestPointLength){
					Echo("changing for point:"+point);
					tmpClosestPoint = point;
					currentPoint = tmpClosestPoint;
				}
			}
			
			currentDistancePoint = currentDistancePoint/2;
			Echo("=================");
			Echo("currentDistancePoint:"+currentDistancePoint);
			if(currentDistancePoint==0){
			// if(currentDistancePoint==128){
				break;
			}
			
		}

		Vector3D generated_gps_point_on_planet = new Vector3D(0,0,0);
		
		//Echo("generated_gps_point_on_cube"+generated_gps_point_on_cube);
		
		
		Vector3D generated_gps_point_on_cube_norm = Vector3D.Normalize(generated_gps_point_on_cube);
		
		
		// Vector3D cubeCenter = detectedPlanet;
		Vector3D cubeCenter = new Vector3D(0,0,0);;
		
		generated_gps_point_on_planet =  planet_radius * Vector3D.Normalize(generated_gps_point_on_cube_norm)+ cubeCenter;
		
		generated_gps_point_on_planet = Vector3D.Round(generated_gps_point_on_planet,1);
		
	}
	
				
	
	
}