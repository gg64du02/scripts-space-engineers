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

	Me.CustomData  = customDataRecoveredRaw + "\n" + resultMWPI.ToString();
	
}
