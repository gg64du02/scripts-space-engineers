
	
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

public List<Point> fourNextPoints(Point point, int distance, int max_range){
	
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

    Echo("running...");
	
	Echo(myRemoteControl.Position+"");
	List<Point> tmpTestNextPoints = fourNextPoints(new Point(1024,1024),512,2048);
	
	foreach	(Point point in tmpTestNextPoints){
		Echo("point"+point);
	}
		
}