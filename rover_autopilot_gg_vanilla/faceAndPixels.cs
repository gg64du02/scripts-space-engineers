	
IMyRemoteControl myRemoteControl = null;


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

public void faceAndPointOnPlanetsCalculated(IMyRemoteControl sc,out int facenumber,out Point pixelPos){
	
	

    //Echo("running...");
	
	// Echo(Me.GetPosition()+"");
	Vector3D myPos = sc.GetPosition();
	
	// GPS:///  1:53546.14:-26699.61:11974.64:#FF75C9F1:
	
	// foreach	(Point point in tmpTestNextPoints){
		// Echo("point"+point);
	// }
	
	Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
	int planet_radius = 60000;
	
	Vector3D planetCenter = new Vector3D(0,0,0);

	bool planetDetected = sc.TryGetPlanetPosition(out planetCenter);
	
	Echo("planetCenter:"+planetCenter);
	
	// planet_radius = (int) (planetCenter-myPos).Length();
	planet_radius = (int) (myPos-planetCenter).Length();
	
	Echo("planet_radius:"+planet_radius);
	
	
	double myPosXAbs = Math.Abs(myPos.X);
	double myPosYAbs = Math.Abs(myPos.Y);
	double myPosZAbs = Math.Abs(myPos.Z);
	
	// Echo("myPosXAbs:"+myPosXAbs);
	// Echo("myPosYAbs:"+myPosYAbs);
	// Echo("myPosZAbs:"+myPosZAbs);
	
	
	
	Vector3D sphereLocalVector = (myPos-planetCenter);
	
	Vector3D projectedSphereVector  = new Vector3D(0,0,0);
	
	int faceNumber = -1;
	
	double pixelScalingToIGW = (2*planet_radius/2048);
	
	//shorter names formulas
	double intX = 0;
	double intY = 0;
	double intZ = 0;
	
	Point extractedPoint = new Point(0,0);
	double extractionX_pointRL = 0;
	double extractionY_pointRL = 0;
	
	if(myPosXAbs>myPosYAbs){
		if(myPosXAbs>myPosZAbs){
			projectedSphereVector = (planet_radius/myPosXAbs)*sphereLocalVector;
			intY = projectedSphereVector.Y;
			intZ = projectedSphereVector.Z;
			if(myPos.X>0){
				faceNumber = 3;
				extractionX_pointRL = planet_radius - intY;
				extractionY_pointRL = planet_radius - intZ;
			}
			else{
				faceNumber = 4;
				extractionX_pointRL = planet_radius - intY;
				extractionY_pointRL = planet_radius + intZ;
			}
		}
	}
	
	if(myPosYAbs>myPosXAbs){
		if(myPosYAbs>myPosZAbs){
			projectedSphereVector = (planet_radius/myPosYAbs)*sphereLocalVector;
			intX = projectedSphereVector.X;
			intZ = projectedSphereVector.Z;
			if(myPos.Y>0){
				faceNumber = 5;
				extractionY_pointRL = planet_radius - intX;
				extractionX_pointRL = planet_radius - intZ;
			}
			else{
				faceNumber = 1;
				extractionY_pointRL = planet_radius + intX ;
				extractionX_pointRL = planet_radius - intZ ;
			}
		}
	}
	
	if(myPosZAbs>myPosXAbs){
		if(myPosZAbs>myPosYAbs){
			projectedSphereVector = (planet_radius/myPosZAbs)*sphereLocalVector;
			intX = projectedSphereVector.X;
			intY = projectedSphereVector.Y;
			if(myPos.Z>0){
				faceNumber = 0;
				extractionY_pointRL = planet_radius + intX;
				extractionX_pointRL = planet_radius - intY;
			}
			else{
				faceNumber = 2;
				extractionY_pointRL = planet_radius - intX;
				extractionX_pointRL =  planet_radius - intY;
			}
		}
	}
	
	if(extractionX_pointRL==0){
		//out-ing
		facenumber =faceNumber;
		pixelPos = new Point(0,0);
		
		return;}
	
	if(extractionY_pointRL==0){
		
		//out-ing
		facenumber =faceNumber;
		pixelPos = new Point(0,0);
		return;}
	
	double tmpCalcX = extractionX_pointRL / pixelScalingToIGW;
	double tmpCalcY = extractionY_pointRL / pixelScalingToIGW;
	
	extractedPoint = new Point((int)tmpCalcX,(int)tmpCalcY);
	
	//Echo("extractedPoint:"+extractedPoint);
	//Echo("faceNumber:"+faceNumber);
	//Echo("projectedSphereVector:"+projectedSphereVector);
	
	Point calculatedPoint = new Point(-1,-1);
	
	
	//out-ing
	facenumber =faceNumber;
	pixelPos =extractedPoint;
	
}


public void faceAndPointOnPlanetsConverging(IMyRemoteControl sc,out int facenumber,out Point pixelPos){
	
	

    //Echo("running...");
	
	// Echo(Me.GetPosition()+"");
	Vector3D myPos = Me.GetPosition();
	List<Point> tmpTestNextPoints = fourNextPointsFunction(new Point(1024,1024),512,2048);
	
	// GPS:///  1:53546.14:-26699.61:11974.64:#FF75C9F1:
	
	// foreach	(Point point in tmpTestNextPoints){
		// Echo("point"+point);
	// }
	
	//outputting tmp
	int facenumberTmp = -1;
	Point pixelPosTmp = new Point(-1,-1);
	
	
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

	bool planetDetected = sc.TryGetPlanetPosition(out planetCenter);
	
	Echo("planetCenter:"+planetCenter);
	
	planet_radius = (int) (planetCenter-myPos).Length();
	
	Echo("planet_radius:"+planet_radius);
	
	double min_range = 1*(2*planet_radius/2048)*(1.414/2);
	
	Echo("min_range:"+min_range);
	
	foreach(int intTmp in intIndexFaces){
		//Echo("Checking planet face:"+intTmp);
		if(intTmp == 0)
		{
			//continue;
			centerFacePositionOffset = new Vector3D(0, 0, planet_radius);
		}
		if(intTmp == 1)
		{
			//continue;
			centerFacePositionOffset = new Vector3D(0, -planet_radius,0);
		}
		if(intTmp == 2)
		{
			//continue;
			centerFacePositionOffset = new Vector3D(0, 0, -planet_radius);
		}
		if(intTmp == 3)
		{
			// continue;
			centerFacePositionOffset = new Vector3D(planet_radius,0,0);
		}
		if(intTmp == 4)
		{
			//continue;
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
		
		// Echo("intTmp:"+intTmp);
		
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
				
				// Echo("currentD_PL:"+currentDistancePointLength);
				// Echo("currentD_CPL:"+currentDistanceClosestPointLength);
				
				
				if(currentDistancePointLength<currentDistanceClosestPointLength){
					//Echo("changing for point:"+point);
					tmpClosestPoint = point;
					currentPoint = tmpClosestPoint;
					
					if(currentDistancePointLength<min_range){
						min_range=currentDistancePointLength;
						//Echo("min_range:"+min_range);
						//outputting tmp
						facenumberTmp = intTmp;
						pixelPosTmp = tmpClosestPoint;
					}
				}
				
			}
			
			currentDistancePoint = currentDistancePoint/2;
			// Echo("=================");
			// Echo("currentDistancePoint:"+currentDistancePoint);
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
	
	//compiling complaince
	// facenumber = -1;
	// pixelPos = new Point(0,0);
	
	
	
	// //out-ing
	// facenumber = intTmp;
	// pixelPos = tmpClosestPoint;
	
	
	if(facenumberTmp == -1){
		facenumber = -1;
		pixelPos = new Point(0,0);
	}
	else{
		facenumber = facenumberTmp;
		pixelPos = pixelPosTmp;
	}
	
}

public void whichFileShouldIlook(int facenumber){
	
	string tmpStr = ""+facenumber+" is ";
	
	if(facenumber==0){
		tmpStr += "back";
	}
	if(facenumber==1){
		tmpStr += "down";
	}
	
	if(facenumber==2){
		tmpStr += "front";
	}
	if(facenumber==3){
		tmpStr += "left";
	}
	
	if(facenumber==4){
		tmpStr += "right";
	}
	if(facenumber==5){
		tmpStr += "up";
	}
	
	Echo(tmpStr);
	
	// 0 is back
	// 1 is down

	// 2 is front
	// 3 is left

	// 4 is right
	// 5 is up
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
				myRemoteControl = (IMyRemoteControl)sc;
			}
		}
	}
	
	int facenumberCalculated = -1;
	Point pixelPosCalculated = new Point(0,0);
		
	faceAndPointOnPlanetsCalculated( myRemoteControl,out facenumberCalculated,out pixelPosCalculated);
	
	Echo("facenumberMain1:"+facenumberCalculated);
	Echo("pixelPosMain1:"+pixelPosCalculated);

	
	int facenumberConverging = -1;
	Point pixelPosConverging = new Point(0,0);
	
	faceAndPointOnPlanetsConverging(myRemoteControl,out facenumberConverging,out pixelPosConverging);
	
	Echo("facenumberMain2:"+facenumberConverging);
	Echo("pixelPosMain2:"+pixelPosConverging);
	
	whichFileShouldIlook(facenumberCalculated);
	
}