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
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
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
	
	
    List<IMyRadioAntenna> listAntenna = new List<IMyRadioAntenna>();
    GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);
	
	bool tryToStandStill = false;
	bool tryToGoFoward = false;
	bool tryToGoBackward = false;
	bool tryToGoRight = false;
	bool tryToGoLeft = false;
	
	Echo("lol1");
	
	IMyShipController shipController = null;
	

	List<IMyShipController> shipControllers = new List<IMyShipController>();
	GridTerminalSystem.GetBlocksOfType<IMyShipController>(shipControllers);
	
	if(shipControllers.Count!=0){
		shipController = shipControllers[0];
		if(shipController.IsUnderControl == true){
			Echo("isManned");
			//TODO
			Vector3D moveIndicator = shipController.MoveIndicator;
			Echo("moveIndicator:"+moveIndicator);
			
			
			var str_to_display = ""+moveIndicator;
			if (listAntenna.Count != 0)
			{
				//var str_to_display = "133";
				listAntenna[0].HudText = str_to_display;
			}
			shipController.CubeGrid.CustomName = str_to_display;
			
			
			if(moveIndicator.Z!=0){
				if(moveIndicator.Z ==1){
					tryToGoBackward = true;
				}
				else{
					tryToGoFoward = true;
				}
			}
			if(moveIndicator.X !=0){
				if(moveIndicator.X ==1){
					tryToGoRight = true;
				}
				else{
					tryToGoLeft = true;
				}
			}
			
		}
		else{
			Echo("isNotManned");
			tryToStandStill = true;
		}
		int V_max = 10;
		
		int V_wanted = 0;
		int V_right = 0;
		int V_left = 0;
		if(tryToStandStill ==true){
			V_wanted = 0;
		    V_right = 0;
			V_left = 0;
		}
		else{
			if(tryToGoLeft ==true){
				V_left = V_max * 2;
			}
			if(tryToGoRight==true){
				V_right = V_max * 2;
			}
			if(tryToGoFoward ==true){
				V_wanted = V_max;
				V_right = V_max * 1;
				V_left = V_max * 1;
			}
			if(tryToGoBackward ==true){
				V_wanted = -V_max;
				V_right = V_max * -1;
				V_left = V_max * -1;
			}
		}
		
	}
	
			
	
	Echo("lol2");
}
