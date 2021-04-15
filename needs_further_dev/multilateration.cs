List<string> listOfGPSstring = null;
List<float> listOfGPSstringRangeFloat = null;
List<Vector3D> listOfGPSvector3D = null;

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

	var customDataRecovered = customDataRecoveredRaw.Split('\n');

	foreach (var str in customDataRecovered)
	{
		Echo("============= ");
		Echo("str " + str);

		MyWaypointInfo myWaypointInfoGPSbeacon = new MyWaypointInfo("lol", 0, 0, 0);
		MyWaypointInfo.TryParse(str, out myWaypointInfoGPSbeacon);

		Echo("myWaypointInfoGPSbeacon " + myWaypointInfoGPSbeacon.ToString());

		if (myWaypointInfoGPSbeacon.Name != "lol")
		{
			Echo("!= lol");
			Echo("parse ok");

			//extract the KMs
			float rangeGPSstring = 0.0f;
			string nameOfGps = myWaypointInfoGPSbeacon.Name;

			float tryingToConvertIntoFloat = (float)Convert.ToDouble("4.1");

			if (nameOfGps.Contains("km") == true)
            {
				Echo("Contains km");
				listOfGPSstring.Add(str);
				listOfGPSstringRangeFloat.Add(tryingToConvertIntoFloat);
				listOfGPSvector3D.Add(myWaypointInfoGPSbeacon.Coords);

			}
            else
			{
				Echo("!Contains km");
			}

		}
		else
		{
			Echo("== lol");
		}

	}

	if (listOfGPSstringRangeFloat.Count() >= 3)
    {

    }


}
