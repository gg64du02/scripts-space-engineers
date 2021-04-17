List<string> listOfGPSstring = null;
List<float> listOfGPSstringRangeFloat = null;
List<Vector3D> listOfGPSvector3D = null;
List<float> listOfWeightCorrespondingToGPSs = null;
float furthestAwayGPSrangeKm = 0f;

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

	string customDataRecoveredRaw = Me.CustomData;

	Echo("customDataRecoveredRaw " + customDataRecoveredRaw);

	listOfGPSstring = new List<string>();

	List<Vector3D> listOfGPSvector3D = new List<Vector3D>();
	
	listOfWeightCorrespondingToGPSs = new List<float>();

	listOfGPSstringRangeFloat = new List<float>();

	int numberOfGPSs = 0;

	var customDataRecovered = customDataRecoveredRaw.Split('\n');

	foreach (var str in customDataRecovered)
	{
		Echo("============= ");
		Echo("str " + str);

		//Echo("str.Length:"+str.Length);
		if(str.Length<5){
			Echo("Line is too short, stopping to look for new GPSs");
			break;
		}

		MyWaypointInfo myWaypointInfoGPSbeacon = new MyWaypointInfo("lol", 0, 0, 0);
		
		if (str.Contains(":#") == true)
		{
			Echo("if (str.Contains(:#) == true)");
			MyWaypointInfo.TryParse(str.Substring(0, str.Length - 10), out myWaypointInfoGPSbeacon);
		}
		else
		{
			Echo("not if (str.Contains(:#) == true)");
			MyWaypointInfo.TryParse(str, out myWaypointInfoGPSbeacon);
		}
		if (myWaypointInfoGPSbeacon.Coords != new Vector3D(0, 0, 0))
		{
			//x,y,z coords is global to remember between each loop
			//vec3Dtarget = myWaypointInfoTarget.Coords;
		}
		/*
		MyWaypointInfo.TryParse(str, out myWaypointInfoGPSbeacon);

		Echo("myWaypointInfoGPSbeacon " + myWaypointInfoGPSbeacon.ToString());
		*/
		

		Echo(myWaypointInfoGPSbeacon.Name);
			
		if (myWaypointInfoGPSbeacon.Name != "lol")
		{
			Echo("!= lol");
			Echo("parseOk");

			//extract the KMs
			float rangeGPSstring = 0.0f;
			string nameOfGps = myWaypointInfoGPSbeacon.Name;

			//Echo("lol1");
			Echo(nameOfGps);
			Echo("trying to parse name:"+nameOfGps);
			//float tryingToConvertIntoFloat = (float)Convert.ToDouble("4.1");
			float tryingToConvertIntoFloat = -1f;
			//tryingToConvertIntoFloat = (float)Convert.ToDouble(nameOfGps);
			tryingToConvertIntoFloat = float.Parse(nameOfGps);

			Echo("lol11");
			Echo("tryingToConvertIntoFloat:"+tryingToConvertIntoFloat);
			if(tryingToConvertIntoFloat == null){
				Echo("tryingToConvertIntoFloat=null");
				//Echo("lol12");
			}
			Echo("lol13");
			listOfGPSstringRangeFloat.Add(tryingToConvertIntoFloat);
			
			//Echo("lol2");
			
			 
			listOfGPSvector3D.Add(myWaypointInfoGPSbeacon.Coords);

			tryingToConvertIntoFloat = 1000f * tryingToConvertIntoFloat;
			listOfWeightCorrespondingToGPSs.Add(tryingToConvertIntoFloat);

			numberOfGPSs += 1;
				
			//Echo("lol3");
			if(tryingToConvertIntoFloat>furthestAwayGPSrangeKm){
				furthestAwayGPSrangeKm = tryingToConvertIntoFloat;
			}

		}

	}

	Echo("numberOfGPSs:"+numberOfGPSs);
	Echo("furthestAwayGPSrangeKm:"+furthestAwayGPSrangeKm);

	Vector3D weightedVector3Dsum = new Vector3D(0,0,0);
	float weightsSum = 0f;
	//==============================
	//TODO: check : true range multilateration on the english wikipedia
	//==============================
	for (int i = 0; i < numberOfGPSs; i++)
	{

		float weightOfIndexedGPS = listOfWeightCorrespondingToGPSs[i];
		weightsSum += weightOfIndexedGPS;
		weightedVector3Dsum += (weightOfIndexedGPS)*listOfGPSvector3D[i];

/*
		float weightOfIndexedGPS = listOfWeightCorrespondingToGPSs[i];
		weightsSum += furthestAwayGPSrangeKm-weightOfIndexedGPS;
		weightedVector3Dsum += (furthestAwayGPSrangeKm-weightOfIndexedGPS)
*listOfGPSvector3D[i];
		*/
	}

	Vector3D result = weightedVector3Dsum / weightsSum;
	Echo("result:"+result);

	MyWaypointInfo resultMWPI = new MyWaypointInfo("result", result);
	
	//Me.CustomData  = customDataRecoveredRaw + "\n" + resultMWPI.ToString();


	if(numberOfGPSs<3){
			Echo("Not enough GPSs as inputs");
	}
	//===========
	//working on  true range multilateration
	float x = 0f;
	float y = 0f;
	float z = 0f;
	
	Vector3D c_1 = listOfGPSvector3D[0];
	Vector3D c_2 = listOfGPSvector3D[1];
	Vector3D c_3 = listOfGPSvector3D[2];
	
	
	float r_1 = listOfWeightCorrespondingToGPSs[0];
	float r_2 = listOfWeightCorrespondingToGPSs[1];
	float r_3 = listOfWeightCorrespondingToGPSs[2];
	
	Echo("r_1:"+r_1);
	Echo("r_2:"+r_2);
	Echo("r_3:"+r_3);
	
	float U = (float)(c_1-c_2).Length();
	Echo("U:"+U);

	//TODO V_x V_y
	float V_x = 0f;
	float V_y = 0f;
	
	V_x = (float) (Vector3D.Normalize((c_2-c_1))).Dot(c_3-c_1);
	float V_y_abs =  (float) ((Vector3D.Normalize((c_2-c_1))).Cross(c_3-c_1)).Length();
	
	float V = (float) (c_3-c_1).Length();
	float V_y_pyth =  (float) Math.Sqrt( (V*V - V_x*V_x) );
	
	V_y = V_y_pyth;
	
	//float V_y_sign = (float) (((Vector3D.Normalize((c_2-c_1))).Cross(c_3-c_1))).Cross();
	//V_y = (float) ((Vector3D.Normalize((c_2-c_1))).Cross(c_3-c_1));
	
	Echo("V_x:"+V_x);
	Echo("V_y:"+V_y);
	Echo("V_y_abs:"+V_y_abs);
	Echo("V_y_pyth:"+V_y_pyth);
	
	//float V = (float) Math.Sqrt(V_x*V_x + V_y*V_y);
	float V_squarred = V * V ;
	
	Echo("V:"+V);
	Echo("V_squarred:"+V_squarred);
	
	x = ( (r_1*r_1) - (r_2*r_2) + (U*U) ) 
	/
	(2*U);
	
	y = ( (r_1*r_1) - (r_3*r_3) + (V*V) - 2 * V_x * x ) 
	/
	(2*V_y);
	
	
	float z_minus = 1f * (float) Math.Sqrt( (r_1*r_1) - (x*x) + (y*y) );
	float z_plus = -1f * (float) Math.Sqrt( (r_1*r_1) - (x*x) + (y*y) );
	
	Echo("x:"+x);
	Echo("y:"+y);
	Echo("z:"+z);
	Echo("z_minus:"+z_minus);
	Echo("z_plus:"+z_plus);
	
	x = (float) Math.Round(x,0);
	y = (float) Math.Round(y,0);
	z_minus = (float) Math.Round(z_minus,0);
	z_plus =(float)  Math.Round(z_plus,0);
	
	//generating local (triedre) offset at c1 with x and y being included within the A and D vector
	Vector3D A = Vector3D.Normalize(c_2-c_1);
	Vector3D B = Vector3D.Normalize(c_3-c_1);
	Vector3D D = Vector3D.Normalize(A.Cross(B));
	Vector3D E = Vector3D.Normalize(D.Cross(A));
	
	//(c_1,Ax,Ey,+-zD)
	
	Vector3D result_minus = x*A+y*E+z_minus*D;
	Vector3D result_plus = x*A+y*E+z_plus*D;
	
	/*
	Vector3D result_minus = new Vector3D(x,y,z_minus);
	Vector3D result_plus = new Vector3D(x,y,z_plus);
	*/
	
	Echo("result_minus:"+result_minus);
	Echo("result_plus:"+result_plus);
	
	Vector3D checkThis_minus = c_1 + result_minus;
	Vector3D checkThis_plus = c_1 + result_plus;
	

	MyWaypointInfo resultMWPI_minus = 
	new MyWaypointInfo("checkThis_minus", checkThis_minus);
	MyWaypointInfo resultMWPI_plus = 
	new MyWaypointInfo("checkThis_plus", checkThis_plus);
	
	Me.CustomData  = customDataRecoveredRaw 
	+ "\n" + resultMWPI_minus.ToString()
	+ "\n" + resultMWPI_plus.ToString()
	;
	
	
}
