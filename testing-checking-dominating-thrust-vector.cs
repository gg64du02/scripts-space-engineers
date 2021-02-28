

double MaxEffectiveThrustFoward = 0;
double MaxEffectiveThrustBackward = 0;
double MaxEffectiveThrustLeft = 0;
double MaxEffectiveThrustRight = 0;
double MaxEffectiveThrustDown = 0;
double MaxEffectiveThrustUp = 0;

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
	
	List<IMyRemoteControl> myRemoteControlList = new List<IMyRemoteControl>();
	GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(myRemoteControlList);
	
	var myRemoteControl = myRemoteControlList[0];
	
    Vector3D shipForwardVector = myRemoteControl.WorldMatrix.Forward;
    Vector3D shipLeftVector = myRemoteControl.WorldMatrix.Left;
    Vector3D shipDownVector = myRemoteControl.WorldMatrix.Down;
    Vector3D shipBackwardVector = myRemoteControl.WorldMatrix.Backward;
    Vector3D shipRightVector = myRemoteControl.WorldMatrix.Right;
    Vector3D shipUpVector = myRemoteControl.WorldMatrix.Up;
	
	/*
	List<IMyRadioAntenna> listAntenna = new List<IMyRadioAntenna>();
	GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);
	*/
	
	float constantNinetyNine = 0.98f;
	
    var cs = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType(cs);
    foreach (var c in cs)
    {
        if (c.IsFunctional == true)
        {
			Vector3D fowardThrust = c.WorldMatrix.Forward;
			
			Echo("fowardThrust"+fowardThrust);
			
			Echo("shipForwardVector"+shipForwardVector);
			Echo("shipLeftVector"+shipLeftVector);
			Echo("shipDownVector"+shipDownVector);
			Echo("shipBackwardVector"+shipBackwardVector);
			Echo("shipRightVector"+shipRightVector);
			Echo("shipUpVector"+shipUpVector);
			
			Echo(""+shipForwardVector.Dot(fowardThrust));
			if(shipForwardVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustFoward += c.MaxEffectiveThrust;
			}
			
			//Echo(""+shipLeftVector.Dot(fowardThrust));
			if(shipLeftVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustLeft += c.MaxEffectiveThrust;
			}
			
			if(shipDownVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustDown += c.MaxEffectiveThrust;
			}
			
			if(shipBackwardVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustBackward += c.MaxEffectiveThrust;
			}
			
			Echo(""+shipRightVector.Dot(fowardThrust));
			if(shipRightVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustRight += c.MaxEffectiveThrust;
			}
			
			if(shipUpVector.Dot(fowardThrust)>constantNinetyNine){
				MaxEffectiveThrustUp += c.MaxEffectiveThrust;
			}
			
		}
	}
	
	Echo("MaxEffectiveThrustFoward"+MaxEffectiveThrustFoward);
	Echo("MaxEffectiveThrustBackward"+MaxEffectiveThrustBackward);
	Echo("MaxEffectiveThrustLeft"+MaxEffectiveThrustLeft);
	Echo("MaxEffectiveThrustRight"+MaxEffectiveThrustRight);
	Echo("MaxEffectiveThrustDown"+MaxEffectiveThrustDown);
	Echo("MaxEffectiveThrustUp"+MaxEffectiveThrustUp );
}

/*
double MaxEffectiveThrustFoward = 0;
double MaxEffectiveThrustBackward = 0;
double MaxEffectiveThrustLeft = 0;
double MaxEffectiveThrustRight = 0;
double MaxEffectiveThrustDown = 0;
double MaxEffectiveThrustUp = 0;
*/

