
Vector3D vec3Dtarget = new Vector3D(0, 0, 0);
IMyShipController flightIndicatorsShipController = null;
public Program()
{
    // The constructor, called only once every session and
    // always before any other method is called. Use it to
    // initialize your script. 
        
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
	
	
	List<IMyRemoteControl> remoteControllers = new List<IMyRemoteControl>();
	GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(remoteControllers);

	flightIndicatorsShipController = remoteControllers[0];
            
	
	flightIndicatorsShipController.TryGetPlanetPosition(out vec3Dtarget);
	Echo("vec3Dtarget:\n"+vec3Dtarget);
}
