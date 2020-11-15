  //library used for the vectorHelper   https://steamcommunity.com/sharedfiles/filedetails/?id=1390966561

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
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
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
	
    List<IMyMotorBase> myMotorBases = new List<IMyMotorBase>();
    GridTerminalSystem.GetBlocksOfType<IMyMotorBase>(myMotorBases);
	
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
		
		List<IMyMotorBase> hipsRotors = new List<IMyMotorBase>();
		List<IMyMotorBase> kneesRotors = new List<IMyMotorBase>();
		
		//guessing vectors for the two wheeled legs
		if(myMotorBases.Count != 0 ){
			foreach(IMyMotorBase mb in myMotorBases){
				//Echo("mb:"+mb);
				if(mb.IsSameConstructAs(shipController)){
					if(mb is IMyMotorStator){
						var cubeGridShipController = shipController.CubeGrid;
						var myMotorBaseGrid = mb.CubeGrid;
						// Echo("cubeGridShipController:"+cubeGridShipController);
						// Echo("myMotorBaseGrid:"+myMotorBaseGrid);
						// Echo("==============");
						if(cubeGridShipController == myMotorBaseGrid){
							//Echo("sameGrid!!!");
							hipsRotors.Add(mb);
						}
						else{
							//Echo("notSameGrid!!!");
							kneesRotors.Add(mb);
						}
					}
				}
			}
			// foreach(IMyMotorBase mb2 in hipsRotors){
				 // Echo("hipsRotor:"+mb2);
			// }
			// foreach(IMyMotorBase mb2 in kneesRotors){
				 // Echo("kneesRotor:"+mb2);
			// }
			//figure out what components belong to the right leg and the left leg
			//getting vectors to help with angles proposals
			Vector3D shipForwardVector = shipController.WorldMatrix.Forward;
			Vector3D shipLeftVector = shipController.WorldMatrix.Left;
			Vector3D shipDownVector = shipController.WorldMatrix.Down;
			// Echo("shipForwardVector:" + shipForwardVector);
			// Echo("shipLeftVector:" + shipLeftVector);
			// Echo("shipDownVector:" + shipDownVector);
			
			//?: if you were an humanoid, knew your hips and knees' position, how do you figure out which one belongs to which leg ?
			double tmpScalar = 0;
			IMyMotorBase leftHip = null;
			IMyMotorBase rightHip = null;
			foreach(IMyMotorBase mb2 in hipsRotors){
				//Echo("==========");
				 //Echo("hipsRotor:"+mb2);
				 Vector3D tmpVector = new Vector3D(mb2.Position -shipController.Position);
				 //Echo("tmpVector:"+tmpVector);
				 //tmpScalar = shipLeftVector.Dot(tmpVector);
				 tmpScalar = tmpVector.X;
				 //Echo("tmpScalar:"+tmpScalar);
				 if(tmpScalar>.5){
					 leftHip = mb2;
					 //Echo("leftHipDectected");
				 }
				 else{
					 rightHip = mb2;
					 //Echo("rightHipDectected");
				 }
				 
				//Echo("==========");
			}
			
			
			
			//now going through all components to sort them into a leg
			List<IMyMotorBase> rightLeg= new List<IMyMotorBase>();
			List<IMyMotorBase> leftLeg= new List<IMyMotorBase>();
			
			
			//Echo("==========");
			foreach(IMyMotorBase mb3 in myMotorBases){
				 // Echo("mb3.CustomName:"+mb3.CustomName);
				 // Echo("mb3.Position"+mb3.Position);
				 // Echo("mb3.GetPosition"+mb3.GetPosition());
				 // Echo("shipController.GetPosition"+shipController.GetPosition());
				 Vector3D tmpVectorCompToShip = new Vector3D(shipController.GetPosition() - mb3.GetPosition());
				 // Echo("tmpVectorCompToShip"+tmpVectorCompToShip);
				 Vector3D tmpCheckThisSign =  shipDownVector.Cross(tmpVectorCompToShip);
				 // Echo("tmpCheckThisSign"+tmpCheckThisSign);
				 double signScalar = tmpCheckThisSign.Dot(shipForwardVector);
				 // Echo("signScalar"+signScalar);
				 if(signScalar<0){
					 leftLeg.Add(mb3);
					 // Echo("leftLeg.Add");
				 }
				 else{
					 rightLeg.Add(mb3);
					 // Echo("rightLeg.Add");
				 }
				 
				// Echo("==========");
			}
			
			Echo("leftLeg.Count:"+leftLeg.Count);
			Echo("rightLeg.Count:"+rightLeg.Count);
			
			// foreach(var mb4 in leftLeg){
				// Echo("leftLeg:mb4.CustomName:"+mb4.CustomName);
			// }
			// foreach(var mb5 in rightLeg){
				// Echo("rightLeg:mb5.CustomName:"+mb5.CustomName);
			// }
				
				
			//figuring out what is the upper legs' angle:
			Vector3D tmpUpperPosLeft = leftLeg[0].GetPosition() - leftLeg[1].GetPosition();
			Vector3D tmpUpperPosRight = rightLeg[0].GetPosition() - rightLeg[1].GetPosition();
			// Echo("tmpUpperPosLeft:"+Vector3D.Round(tmpUpperPosLeft,2));
			// Echo("tmpUpperPosRight:"+Vector3D.Round(tmpUpperPosRight,2));
			
			Vector3D tmpUpperPosLeftNorm = Vector3D.Normalize(tmpUpperPosLeft);
			Vector3D tmpUpperPosRightNorm = Vector3D.Normalize(tmpUpperPosRight);
			
			
			//figuring out what is the lower legs' angle:
			Vector3D tmpLowerPosLeft = leftLeg[1].GetPosition() - leftLeg[2].GetPosition();
			Vector3D tmpLowerPosRight = rightLeg[1].GetPosition() - rightLeg[2].GetPosition();
			// Echo("tmpLowerPosLeft:"+Vector3D.Round(tmpLowerPosLeft,2));
			// Echo("tmpLowerPosRight:"+Vector3D.Round(tmpLowerPosRight,2));
			
			
			Vector3D leftHipRotatingAxis = leftHip.WorldMatrix.Up;
			// Echo("leftHipRotatingAxis:"+Vector3D.Round(leftHipRotatingAxis,2));
			
			//angle for the left knee
			//generate vectors that belong to the plane normal to the hip vector
			Vector3D hipProjTmpUpperPosLeft = tmpUpperPosLeft - VectorHelper.VectorProjection(tmpUpperPosLeft,leftHipRotatingAxis);
			Vector3D hipProjTmpLowerPosLeft =  tmpLowerPosLeft - VectorHelper.VectorProjection(tmpLowerPosLeft,leftHipRotatingAxis);
			//TODO: add the sign to the angle
			double angleSignLeftKnee = -hipProjTmpLowerPosLeft.Dot(hipProjTmpUpperPosLeft);
			double angleLeftKnee = (180/Math.PI)* VectorHelper.VectorAngleBetween(hipProjTmpUpperPosLeft,hipProjTmpLowerPosLeft);
			if(angleSignLeftKnee<0){angleLeftKnee = -angleLeftKnee;}
			// Echo("hipProjTmpUpperPosLeft:"+Vector3D.Round(hipProjTmpUpperPosLeft,2));
			// Echo("hipProjTmpLowerPosLeft:"+Vector3D.Round(hipProjTmpLowerPosLeft,2));
			Echo("angleLeftKnee:"+Math.Round(angleLeftKnee,2));
			
			//angle for the right knee
			//generate vectors that belong to the plane normal to the hip vector
			Vector3D hipProjTmpUpperPosRight = tmpUpperPosRight - VectorHelper.VectorProjection(tmpUpperPosRight,leftHipRotatingAxis);
			Vector3D hipProjTmpLowerPosRight =  tmpLowerPosRight - VectorHelper.VectorProjection(tmpLowerPosRight,leftHipRotatingAxis);
			//TODO: add the sign to the angle
			double angleSignRightKnee = -hipProjTmpLowerPosRight.Dot(hipProjTmpUpperPosRight);
			double angleRighKnee =(180/Math.PI)* VectorHelper.VectorAngleBetween(hipProjTmpUpperPosRight,hipProjTmpLowerPosRight);
			if(angleSignRightKnee<0){angleRighKnee = -angleRighKnee;}
			// Echo("hipProjTmpUpperPosRight:"+Vector3D.Round(hipProjTmpUpperPosRight,2));
			// Echo("hipProjTmpLowerPosRight:"+Vector3D.Round(hipProjTmpLowerPosRight,2));
			Echo("angleRighKnee:"+Math.Round(angleRighKnee,2));
			
			//angle for the right hip
			//angle between the forward vector and the upper legs
			double angleSignRightHip = -shipController.WorldMatrix.Down.Dot(hipProjTmpUpperPosRight);
			double angleSignLeftHip = -shipController.WorldMatrix.Down.Dot(hipProjTmpUpperPosLeft);
			double angleBbodyAUpperLegRight = (180/Math.PI)* VectorHelper.VectorAngleBetween(shipController.WorldMatrix.Forward,hipProjTmpUpperPosRight);
			if(angleSignRightHip<0){
				angleBbodyAUpperLegRight = - angleBbodyAUpperLegRight;
			}
			double angleBbodyAUpperLegLeft = (180/Math.PI)* VectorHelper.VectorAngleBetween(shipController.WorldMatrix.Forward,hipProjTmpUpperPosLeft);
			if(angleSignRightHip<0){
				angleBbodyAUpperLegLeft = - angleBbodyAUpperLegLeft;
			}
			Echo("angleBbodyAUpperLegRight:"+Math.Round(angleBbodyAUpperLegRight,2));
			Echo("angleBbodyAUpperLegLeft:"+Math.Round(angleBbodyAUpperLegLeft,2));
			
			
			
			
		}
		
		
		
		
	}
	
			
	
	Echo("lol2");
}

public static class VectorHelper
{
    // in radians
    public static double VectorAngleBetween(Vector3D a, Vector3D b)
    {
        if (Vector3D.IsZero(a) || Vector3D.IsZero(b))
            return 0;
        else
            return Math.Acos(MathHelper.Clamp(a.Dot(b) / Math.Sqrt(a.LengthSquared() * b.LengthSquared()), -1, 1));
    }

    public static Vector3D VectorProjection(Vector3D vectorToProject, Vector3D projectsToVector)
    {
        if (Vector3D.IsZero(projectsToVector))
            return Vector3D.Zero;

        return vectorToProject.Dot(projectsToVector) / projectsToVector.LengthSquared() * projectsToVector;
    }

}
