  //library used for the vectorHelper   https://steamcommunity.com/sharedfiles/filedetails/?id=1390966561

// PIDController pidAngleHipRight = new PIDController(0.06f, .00f, 0.01f);
// PIDController pidAngleHipLeft = new PIDController(0.06f, .00f, 0.01f);
// PIDController pidAngleKneeRight = new PIDController(0.06f, .00f, 0.01f);
// PIDController pidAngleKneeLeft = new PIDController(0.06f, .00f, 0.01f);
// PIDController pidAngleHipRight = new PIDController(0.1f, .00f, 0.00f);
// PIDController pidAngleHipLeft = new  PIDController(0.1f, .00f, 0.00f);
// PIDController pidAngleKneeRight = new  PIDController(0.1f, .00f, 0.00f);
// PIDController pidAngleKneeLeft = new  PIDController(0.1f, .00f, 0.00f);
PIDController pidAngleHipRight = new PIDController(1f, .00f, 0.00f);
PIDController pidAngleHipLeft = new  PIDController(1f, .00f, 0.00f);
PIDController pidAngleKneeRight = new  PIDController(1f, .00f, 0.00f);
PIDController pidAngleKneeLeft = new  PIDController(1f, .00f, 0.00f);
PIDController pidLeftWheelSpeed = new  PIDController(0.1f, .00f, 0.00f);
PIDController pidRightWheelSpeed = new  PIDController(0.1f, .00f, 0.00f);


Vector3D prevBarycenter = new Vector3D(0,0,0);
double prevAngleGravCOM = 0;

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
	
	
	List<IMyProgrammableBlock> programmableBlocks = new List<IMyProgrammableBlock>();
	GridTerminalSystem.GetBlocksOfType<IMyProgrammableBlock>(programmableBlocks);
	
	foreach(IMyShipController sc in shipControllers){
		var cubeGridShipController = sc.CubeGrid;
		var programmableBlocksGrid = programmableBlocks[0].CubeGrid;
		// Echo("cubeGridShipController:"+cubeGridShipController);
		// Echo("myMotorBaseGrid:"+myMotorBaseGrid);
		// Echo("==============");
		if(cubeGridShipController == programmableBlocksGrid){
			//shipController = shipControllers[0];
			shipController = sc;
		}
	}
	
	Echo("shipController:"+shipController);
	
	if(shipController == null){
		Echo("no ship controllers onto the PB grid!!!");
		return;
	}
	
	if(shipControllers.Count!=0){
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
		
		//names used :
		// leftHip
		// leftKnee
		// leftWheel
		
		// rightHip
		// rightKnee
		// rightWheel
		
		// leftUpperRc
		// leftLowerRc
		// rightUpperRc
		// rightLowerRc
		
		
		List<IMyMotorBase> hipsRotors = new List<IMyMotorBase>();
		List<IMyMotorBase> kneesRotors = new List<IMyMotorBase>();
		
		//guessing vectors for the two wheeled legs
		if(myMotorBases.Count != 0 ){
			foreach(IMyMotorBase mb in myMotorBases){
				Echo("mb:"+mb);
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
			Echo("hipsRotors.Count:"+hipsRotors.Count);
			Echo("kneesRotors.Count:"+kneesRotors.Count);
			
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
			IMyMotorStator leftHip = null;
			IMyMotorStator rightHip = null;
			
			foreach(IMyMotorStator mb2 in hipsRotors){
				Echo("mb2:"+mb2);
				if(shipLeftVector.Dot(new Vector3D(shipController.GetPosition() - mb2.GetPosition()))>0){
					 rightHip = mb2;
					 //Echo("rightHipDectected");
				 }
				 else{
					 leftHip = mb2;
					 //Echo("leftHipDectected");
				 }
			}
			
			//kneesRotors
			IMyMotorStator leftKnee = null;
			IMyMotorStator rightKnee = null;
			foreach(IMyMotorStator kr in kneesRotors){
				if(shipLeftVector.Dot(new Vector3D(shipController.GetPosition() - kr.GetPosition()))>0){
					rightKnee=kr;
					//Echo("rightKnee!!!!");
				}	 
				else{
					leftKnee=kr;
					//Echo("leftKnee!!!!");
				}
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
			
			

			IMyMotorSuspension rightWheel = null;
			IMyMotorSuspension leftWheel = null;
			List<IMyMotorSuspension> motorSuspensions = new List<IMyMotorSuspension>();
			GridTerminalSystem.GetBlocksOfType<IMyMotorSuspension>(motorSuspensions);
			//Echo("==========");
			foreach(var mS in motorSuspensions){
				Echo("mS:"+mS);
				if(shipLeftVector.Dot(new Vector3D(shipController.GetPosition() - mS.GetPosition()))>0){
					rightWheel  =mS;
					 // Echo("rightWheel");
				}
				else{
					leftWheel  =mS;
					 // Echo("leftWheel");
				}
			}
			
			
			Echo("lol_14");
			
			if(leftWheel == null){
				Echo("if(leftWheel == null){");
			}
			if(rightWheel == null){
				Echo("if(rightWheel == null){");
			} 
			if(leftKnee == null){
				Echo("if(leftKnee == null){");
			}
			if(rightKnee == null){
				Echo("if(rightKnee == null){");
			} 
			if(leftHip == null){
				Echo("if(leftHip == null){");
			} 
			if(rightHip == null){
				Echo("if(rightHip == null){");
			} 
			if(hipsRotors.Count == 0){
				Echo("if(hipsRotors.Count == 0){");
			} 
			if(kneesRotors.Count == 0){
				Echo("if(kneesRotors.Count == 0){");
			} 
			
			Echo("lol_15");
			
			leftWheel.CustomName = "leftWheel";
			rightWheel.CustomName = "rightWheel";
			
			Echo("lol_16");
			
				
				
			//figuring out what is the upper legs' angle:
			// Vector3D tmpUpperPosLeft = leftLeg[0].GetPosition() - leftLeg[1].GetPosition();
			// Vector3D tmpUpperPosRight = rightLeg[0].GetPosition() - rightLeg[1].GetPosition();
			Vector3D tmpUpperPosLeft = leftHip.GetPosition() - leftKnee.GetPosition();
			Vector3D tmpUpperPosRight = rightHip.GetPosition() - rightKnee.GetPosition();
			// Echo("tmpUpperPosLeft:"+Vector3D.Round(tmpUpperPosLeft,2));
			// Echo("tmpUpperPosRight:"+Vector3D.Round(tmpUpperPosRight,2));
			
			Echo("lol_11");
			
			Vector3D tmpUpperPosLeftNorm = Vector3D.Normalize(tmpUpperPosLeft);
			Vector3D tmpUpperPosRightNorm = Vector3D.Normalize(tmpUpperPosRight);
			
			Echo("lol_12");
			
			
			//figuring out what is the lower legs' angle:
			// Vector3D tmpLowerPosLeft = leftLeg[1].GetPosition() - leftLeg[2].GetPosition();
			// Vector3D tmpLowerPosRight = rightLeg[1].GetPosition() - rightLeg[2].GetPosition();
			Vector3D tmpLowerPosLeft = leftKnee.GetPosition() - leftWheel.GetPosition();
			Vector3D tmpLowerPosRight = rightKnee.GetPosition() - rightWheel.GetPosition();
			// Echo("tmpLowerPosLeft:"+Vector3D.Round(tmpLowerPosLeft,2));
			// Echo("tmpLowerPosRight:"+Vector3D.Round(tmpLowerPosRight,2));
			
			Echo("lol_13");
			
			
			Vector3D leftHipRotatingAxis = leftHip.WorldMatrix.Up;
			// Echo("leftHipRotatingAxis:"+Vector3D.Round(leftHipRotatingAxis,2));
			
			//angle for the left knee
			//generate vectors that belong to the plane normal to the hip vector
			Vector3D hipProjTmpUpperPosLeft = tmpUpperPosLeft - VectorHelper.VectorProjection(tmpUpperPosLeft,leftHipRotatingAxis);
			Vector3D hipProjTmpLowerPosLeft =  tmpLowerPosLeft - VectorHelper.VectorProjection(tmpLowerPosLeft,leftHipRotatingAxis);
			//DONE?: add the sign to the angle
			double angleSignLeftKnee = -leftHip.WorldMatrix.Up.Dot(hipProjTmpUpperPosLeft.Cross(hipProjTmpLowerPosLeft));//<- cross needed to guess the angle
			double angleLeftKnee = (180/Math.PI)* VectorHelper.VectorAngleBetween(hipProjTmpUpperPosLeft,hipProjTmpLowerPosLeft);
			if(angleSignLeftKnee<0){angleLeftKnee = -angleLeftKnee;}
			// Echo("hipProjTmpUpperPosLeft:"+Vector3D.Round(hipProjTmpUpperPosLeft,2));
			// Echo("hipProjTmpLowerPosLeft:"+Vector3D.Round(hipProjTmpLowerPosLeft,2));
			Echo("angleSignLeftKnee:"+Math.Round(angleSignLeftKnee,2));
			Echo("angleLeftKnee:"+Math.Round(angleLeftKnee,2));
			
			//angle for the right knee
			//generate vectors that belong to the plane normal to the hip vector
			Vector3D hipProjTmpUpperPosRight = tmpUpperPosRight - VectorHelper.VectorProjection(tmpUpperPosRight,leftHipRotatingAxis);
			Vector3D hipProjTmpLowerPosRight =  tmpLowerPosRight - VectorHelper.VectorProjection(tmpLowerPosRight,leftHipRotatingAxis);
			//DONE?: add the sign to the angle
			double angleSignRightKnee = -rightHip.WorldMatrix.Up.Dot(hipProjTmpUpperPosRight.Cross(hipProjTmpLowerPosRight));//<- cross needed to guess the angle
			double angleRightKnee =(180/Math.PI)* VectorHelper.VectorAngleBetween(hipProjTmpUpperPosRight,hipProjTmpLowerPosRight);
			if(angleSignRightKnee<0){angleRightKnee = -angleRightKnee;}
			// Echo("hipProjTmpUpperPosRight:"+Vector3D.Round(hipProjTmpUpperPosRight,2));
			// Echo("hipProjTmpLowerPosRight:"+Vector3D.Round(hipProjTmpLowerPosRight,2));
			Echo("angleSignRightKnee:"+Math.Round(angleSignRightKnee,2));
			Echo("angleRightKnee:"+Math.Round(angleRightKnee,2));
			
			//angle for the right hip
			//angle between the forward vector and the upper legs
			double angleSignRightHip = -shipController.WorldMatrix.Down.Dot(hipProjTmpUpperPosRight);
			double angleSignLeftHip = -shipController.WorldMatrix.Down.Dot(hipProjTmpUpperPosLeft);
			double angleBbodyAUpperLegRight = (180/Math.PI)* VectorHelper.VectorAngleBetween(shipController.WorldMatrix.Forward,hipProjTmpUpperPosRight);
			if(angleSignRightHip<0){
				angleBbodyAUpperLegRight = - angleBbodyAUpperLegRight;
			}
			double angleBbodyAUpperLegLeft = (180/Math.PI)* VectorHelper.VectorAngleBetween(shipController.WorldMatrix.Forward,hipProjTmpUpperPosLeft);
			if(angleSignLeftHip<0){
				angleBbodyAUpperLegLeft = - angleBbodyAUpperLegLeft;
			}
			Echo("angleBbodyAUpperLegRight:"+Math.Round(angleBbodyAUpperLegRight,2));
			Echo("angleBbodyAUpperLegLeft:"+Math.Round(angleBbodyAUpperLegLeft,2));
			
			
			//======================================
			//=============CONTROL===================
			//======================================
			
			//Best we have is Runtime.LastRunTime and Runtime.TimeSinceLastRun
			double dts = Runtime.TimeSinceLastRun.TotalSeconds;
			Echo("dts:" + dts);
			double dts2 = Runtime.LastRunTimeMs;
			Echo("dts2:" + dts2);
			
			if (dts == 0)
			{
				//listLight[0].Color = Color.DarkRed;
				return;
			}
			
			double wantedAngleHipRight = 45;
			double wantedAngleHipLeft = 45;
			double wantedAngleKneeLeft = 90;
			double wantedAngleKneeRight = -90;
			// double wantedAngleHipRight = 30;
			// double wantedAngleHipLeft = 30;
			// double wantedAngleKneeLeft = 150;
			// double wantedAngleKneeRight = -150;
			// wantedAngleHipRight = 90;
			// wantedAngleHipLeft = 90;
			// wantedAngleKneeLeft = 0;
			// wantedAngleKneeRight = 0;
			// wantedAngleHipRight = 45;
			// wantedAngleHipLeft = 45;
			// wantedAngleKneeLeft = 135;
			// wantedAngleKneeRight = -135;
			
			bool tryToStandUp = false;
			
			Echo("lol_17");
			
			
		// leftUpperRc
		// leftLowerRc
		// rightUpperRc
		// rightLowerRc
		
		
		
			
			IMyRemoteControl leftUpperRc = (IMyRemoteControl) GridTerminalSystem.GetBlockWithName("leftUpperRc");
			IMyRemoteControl leftLowerRc = (IMyRemoteControl) GridTerminalSystem.GetBlockWithName("leftLowerRc");
			IMyRemoteControl rightUpperRc = (IMyRemoteControl) GridTerminalSystem.GetBlockWithName("rightUpperRc");
			IMyRemoteControl rightLowerRc = (IMyRemoteControl) GridTerminalSystem.GetBlockWithName("rightLowerRc");
			
			
			if(leftUpperRc == null){
				Echo("if(leftUpperRc == null){");
			} 
			else{
				Echo(""+leftUpperRc);
			}
			if(leftLowerRc == null){
				Echo("if(leftLowerRc == null){");
			} 
			else{
				Echo(""+leftLowerRc);
			}
			if(rightUpperRc == null){
				Echo("if(rightUpperRc == null){");
			} 
			else{
				Echo(""+rightUpperRc);
			}
			if(rightLowerRc == null){
				Echo("if(rightLowerRc == null){");
			} 
			else{
				Echo(""+rightLowerRc);
			}
			
			Echo("if any are not detected with the same don't copy paste it, clear the name and type the name provided in the script on the ship");
			
			Echo("lol_18");
			
			
			// List<IMyRemoteControl> listRemoteControls = new List<IMyRemoteControl>();
			// GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(listRemoteControls);
			// foreach(IMyRemoteControl rc in listRemoteControls){
				// //Echo("rc:"+rc);
				// Echo("rc.CustomName:"+rc.CustomName);
				// if(rc.CustomName == "rightUpperRc")
				// {
					// if(rightUpperRc == null){
						// rightUpperRc = rc;
						// Echo("rightUpperRc_assigned");
					// }
				// }
				// if(rc.CustomName == "rightLowerRc")
				// {
					// if(rightLowerRc == null){
						// rightLowerRc = rc;
						// Echo("rightLowerRc_assigned");
					// }
				// }
			// }
			
			// Echo("listRemoteControls.Count:"+listRemoteControls.Count);
			
			
			//===================================
			//BARYCENTER computing start
			//===================================
			//use https://en.wikipedia.org/wiki/Barycenter
			
			//Multiply(Vector3D, double)	Multiplies a vector by a scalar value.
			Echo("lol_20");
			Vector3D WcomRightLowerLeg = new Vector3D(Vector3D.Multiply(rightLowerRc.CenterOfMass,rightLowerRc.Mass));
			//Vector3D WcomRightLowerLeg = Vector3D.Multiply(rightLowerRc.CenterOfMass,rightLowerRc.Mass);
			//Vector3D WcomRightLowerLeg = new Vector3D(0,0,0);
			Vector3D WcomLeftLowerLeg = new Vector3D(Vector3D.Multiply(leftLowerRc.CenterOfMass,leftLowerRc.Mass));
			Echo("lol_21");
			
			Vector3D WcomRightUpperLeg = new Vector3D(Vector3D.Multiply(rightUpperRc.CenterOfMass,rightUpperRc.Mass));
			Vector3D WcomLeftUpperLeg = new Vector3D(Vector3D.Multiply(leftUpperRc.CenterOfMass,leftUpperRc.Mass));
			
			Vector3D WcomShipPond = new Vector3D(Vector3D.Multiply(shipController.CenterOfMass,shipController.Mass));
			
			Echo("lol_19");
			
			Vector3D sumOfWcom = WcomRightLowerLeg+WcomLeftLowerLeg+
			WcomRightUpperLeg + WcomLeftUpperLeg
			+WcomShipPond;
			
			double sumOfMass = rightLowerRc.Mass + leftLowerRc.Mass+
			rightUpperRc.Mass + leftUpperRc.Mass+
			shipController.Mass;
			
			Vector3D barycenter = new Vector3D(Vector3D.Divide(sumOfWcom,sumOfMass));
			
			Echo("barycenter: "+Vector3D.Round(barycenter,2));
			
			//===================================
			//BARYCENTER computing end
			//===================================
			
			
			
			
			//TODO: consider all COM and figure out the global COM
			// Vector3D rightWheelToCOM = new Vector3D(shipController.CenterOfMass - rightWheel.GetPosition());
			// Vector3D leftWheelToCOM = new Vector3D(shipController.CenterOfMass - leftWheel.GetPosition());
			Vector3D rightWheelToCOM = new Vector3D(barycenter - rightWheel.GetPosition());
			Vector3D leftWheelToCOM = new Vector3D(barycenter - leftWheel.GetPosition());
			Vector3D wheelsCombToCOM = new Vector3D(rightWheelToCOM+leftWheelToCOM);
			Vector3D wheelsCombToCOMNorm = Vector3D.Normalize(wheelsCombToCOM);
			Echo("wheelsCombToCOMNorm:"+Vector3D.Round(wheelsCombToCOMNorm,2));
			
			Vector3D grav = shipController.GetTotalGravity();
			Vector3D gravNorm = Vector3D.Normalize(grav);
			
			//TODO change to something that change sign if the barycenter change side betzeen the 2 wheels and the gravity vector
			Vector3D crossRightLeftWheelsToCOM = leftWheelToCOM.Cross(rightWheelToCOM);
			

			//we can check is wheelsCombToCOMNorm is going above or below the plane normal to the gravity vector aka using Dot
			//double areWheelsCOMalignWithGravity = gravNorm.Dot(wheelsCombToCOMNorm);
			double areWheelsCOMalignWithGravity = crossRightLeftWheelsToCOM.Dot(gravNorm);
			Echo("areWheelsCOMalignWithGravity:"+Math.Round(areWheelsCOMalignWithGravity,2));
			
			double leftWheelPropControl =0;
			double rightWheelPropControl = 0;
			
			rightWheel.SetValueFloat("Propulsion override", 0.00f);
			leftWheel.SetValueFloat("Propulsion override", 0.00f);
			
			rightWheel.Friction = 100f;
			leftWheel.Friction = 100f;
			rightWheel.Height = 0f;
			leftWheel.Height = 0f;
			rightWheel.Height = 0f;
			leftWheel.Height = 0f;
			rightWheel.Strength = 100f;
			leftWheel.Strength = 100f;
			
			MyWaypointInfo toCustomData = new MyWaypointInfo("bary",barycenter);
			
			Me.CustomData = toCustomData.ToString();
			
			//barycenter absolute speed
			//if(barycenter)
			Vector3D speedBarycenter = new Vector3D((barycenter - prevBarycenter)/dts);
			Echo("speedBarycenter:"+Vector3D.Round(speedBarycenter,2));
			
			prevBarycenter = barycenter;
			
			//if(areWheelsCOMalignWithGravity<0){
				//is the pendulum align with gravity ?
				double isPendulumAlignWithGravity = shipLeftVector.Dot(wheelsCombToCOMNorm.Cross(gravNorm));
				//float ORWheels = 0.75f ;
				//float ORWheels = 0.2f* Convert.ToSingle( Math.Acos(areWheelsCOMalignWithGravity) / Math.PI );
				//float ORWheels = -2f * Convert.ToSingle( Math.Asin(areWheelsCOMalignWithGravity) / Math.PI );
				//float ORWheels = 2f * Convert.ToSingle( Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,3));
				//float ORWheels = 0.20f * Convert.ToSingle( Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,3));
				
				
				//TODO using something that change the sign if it flips on its back
				
				//float ORWheels = 2.0f * Convert.ToSingle( Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,1));
				//float ORWheels = 2.0f * Convert.ToSingle(speedBarycenter.Dot(shipForwardVector));
				//float ORWheels = 2.0f * Convert.ToSingle(speedBarycenter.Dot(shipForwardVector)+Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,1));
				//float ORWheels = 2.0f * Convert.ToSingle(speedBarycenter.Dot(shipForwardVector)+ Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,1));
				// float ORWheels = 2.0f * Convert.ToSingle( Math.Pow(Math.Cos(areWheelsCOMalignWithGravity) / Math.PI ,1));
				// //yes if = 0
				// // if(isPendulumAlignWithGravity<0){
					// //Echo("isPendulumAlignWithGravity<0");
					// rightWheel.SetValueFloat("Propulsion override", ORWheels);
					// leftWheel.SetValueFloat("Propulsion override", -ORWheels);
				// // }
				// // else{
					// // Echo("notisPendulumAlignWithGravity<0");
					// // rightWheel.SetValueFloat("Propulsion override", -ORWheels);
					// // leftWheel.SetValueFloat("Propulsion override", ORWheels);
				// // }
			// //}
			
			double wantedCOMangularSpeed = 0;
			double angleGravCOM = 0;
			
			double signlessAngleGravCOM = VectorHelper.VectorAngleBetween(crossRightLeftWheelsToCOM,gravNorm);
			
			double signOfCosAngleBetweenCOMwheelsAndGravNorm = shipLeftVector.Dot(gravNorm.Cross(wheelsCombToCOMNorm));
			
			Echo("signOfCosAngleBetweenCOMwheelsAndGravNorm:"+Math.Round(signOfCosAngleBetweenCOMwheelsAndGravNorm,2));
			
			if(signOfCosAngleBetweenCOMwheelsAndGravNorm>0){
				angleGravCOM = -signlessAngleGravCOM;
			}
			else{
				angleGravCOM = signlessAngleGravCOM;
			}
			
			double angularSpeedGravCOM = (prevAngleGravCOM - angleGravCOM) / dts;
			
			//E_p = MGH (1 - sin(theta))
			//with w = d theta / dts
			//E_c = 1/2*m*v*v
			//E_J = 1/2*J*w*w
			
			//to get E_p = mgh
			//we need theta = 0
			//delta_E = E_c + E_J
			//delta_E = .5 * (J*w*w + m*v*v)
			
			// MGH * sin( theta ) =  1/2 * (J*w*w + m*v*v)
			// MGH * sin( theta ) *2 - m*v*v  =   J*w*w 
			//1/J * (  MGH * sin( theta ) *2 - m*v*v   ) = w * w
			// w = sqrt (  1/J * (  MGH * sin( theta ) *2 - m*v*v   )    )
			
			double m = rightLowerRc.Mass  + leftLowerRc.Mass+rightUpperRc.Mass + leftUpperRc.Mass+shipController.Mass;
			
			double g = shipController.GetNaturalGravity().Length();
			//angle between grav and wheels/bary
			double theta = angleGravCOM;
			double J = 40000;
			//double h = 1;
			double h = (((2*barycenter)-(leftWheel.GetPosition()+rightWheel.GetPosition()))*.5).Length();
			
			//wantedCOMangularSpeed = Math.Sqrt((1/J)*Math.Abs((m * g * h)*Math.Sin(theta)*2));
			wantedCOMangularSpeed = Math.Sqrt((1/J)*Math.Abs((m * g * h)*Math.Sin(theta)*2-m*speedBarycenter.Length()*speedBarycenter.Length()));
			// if((m * g * h)*Math.Sin(theta)*2<m*speedBarycenter.Length()*speedBarycenter.Length()){
				// wantedCOMangularSpeed = Math.Sqrt((1/J)*Math.Abs((m * g * h)*Math.Sin(theta)*2-m*speedBarycenter.Length()*speedBarycenter.Length()));
			// }
			// else{
				// wantedCOMangularSpeed = Math.Sqrt((1/J)*Math.Abs((m * g * h)*Math.Sin(theta)*2+m*speedBarycenter.Length()*speedBarycenter.Length()));
			// }
			if(signOfCosAngleBetweenCOMwheelsAndGravNorm>0){
				wantedCOMangularSpeed *= 1;
			}
			else{
				wantedCOMangularSpeed *=  -1;
			}
			
			//==============
			
			//wantedCOMangularSpeed = -1 * angleGravCOM;
			
			Echo("wantedCOMangularSpeed:"+wantedCOMangularSpeed);
			
			double errorCOMangularSpeed = wantedCOMangularSpeed - angularSpeedGravCOM;
			Echo("errorCOMangularSpeed:"+errorCOMangularSpeed);
			
			float errorCOMangularSpeedFloat = Convert.ToSingle(errorCOMangularSpeed);
			
			rightWheel.SetValueFloat("Propulsion override", errorCOMangularSpeedFloat);
			leftWheel.SetValueFloat("Propulsion override", -errorCOMangularSpeedFloat);
			
			
			
			
			prevAngleGravCOM = angleGravCOM;
			
			
			
			// leftWheelPropControl = pidLeftWheelSpeed.Control(1 ,dts);*
			  // rightWheelPropControl = pidRightWheelSpeed.Control(lol,dts);

			// isItOnTheShipControllerBack
			
			// rightWheel.SetValueFloat("Propulsion override", 1.00f);
			// leftWheel.SetValueFloat("Propulsion override", -1.00f);
			
			//DEBUG
			tryToStandUp = true;
			if(tryToStandUp ==true){
				Echo("TODO: tryToStandUp ==true");
				// //legs extended behing the cockpit on the floor, cockpit is upright
				//pos 2
				// wantedAngleHipRight = 0;
				// wantedAngleHipLeft = 0;
				// wantedAngleKneeLeft = -20;
				// wantedAngleKneeRight = 20;
				
				// //offsets while standing up
				// //pos 1
				// wantedAngleHipRight = 45;
				// wantedAngleHipLeft = 45;
				// wantedAngleKneeLeft = 90;
				// wantedAngleKneeRight = -90;
				
				// //mirror symetrical relative to Up and Left of
				// //pos 2
				// //pos 3
				// wantedAngleHipRight = 178;
				// wantedAngleHipLeft = 178;
				// wantedAngleKneeLeft = -20;
				// wantedAngleKneeRight = 20;
				
				// //legs in the air
				// // pos 4
				// wantedAngleHipRight = 90;
				// wantedAngleHipLeft = 90;
				// wantedAngleKneeLeft = 0;
				// wantedAngleKneeRight = 0;
				
				// // get the legs in a duck position right before standing up
				// // pos 5
				// wantedAngleHipRight = 0;
				// wantedAngleHipLeft = 0;
				// wantedAngleKneeLeft = 160;
				// wantedAngleKneeRight = -160;
				
				
				// // // get the legs in a duck position right before standing up
				// // // pos 6
				// wantedAngleHipRight = 0;
				// wantedAngleHipLeft = 0;
				// wantedAngleKneeLeft = -135;
				// wantedAngleKneeRight = 135;
				
				// Vector3D grav = shipController.GetTotalGravity();
				// Vector3D gravNorm = Vector3D.Normalize(grav);
				
				// double uprightEnough = shipDownVector.Dot(gravNorm);
				// Echo("uprightEnough:"+uprightEnough);
				// //testing if the ship is upright enough
				// // if(Math.Abs(uprightEnough)<.5f){
				// if(uprightEnough<.5f){
					// Echo("ship is not upright");
					// //test if the back or the front is on the ground
					// if(shipForwardVector.Dot(gravNorm)>0){	
						// Echo("pos3");	
						// wantedAngleHipRight = 178;
						// wantedAngleHipLeft = 178;
						// wantedAngleKneeLeft = -20;
						// wantedAngleKneeRight = 20;			
					// }
					// else{	
						// Echo("pos2");
						// wantedAngleHipRight = 0;
						// wantedAngleHipLeft = 0;
						// wantedAngleKneeLeft = -20;
						// wantedAngleKneeRight = 20;
					// }
				// }
				// else{
					// // Echo("4545");
					// // wantedAngleHipRight = 45;
					// // wantedAngleHipLeft = 45;
					// // wantedAngleKneeLeft = 45;
					// // wantedAngleKneeRight = -45;
				// }
			}
			
			Echo("===============");
			Echo("wantedAngleHipRight:"+Math.Round(wantedAngleHipRight,2));
			Echo("wantedAngleHipLeft:"+Math.Round(wantedAngleHipLeft,2));
			Echo("wantedAngleKneeLeft:"+Math.Round(wantedAngleKneeLeft,2));
			Echo("wantedAngleKneeRight:"+Math.Round(wantedAngleKneeRight,2));
			Echo("===============");
			
			
			double errorAngleHipRight = wantedAngleHipRight-angleBbodyAUpperLegRight;
			if(errorAngleHipRight<-180){errorAngleHipRight += 180;}
			if(errorAngleHipRight>+180){errorAngleHipRight += -180;}
			if(wantedAngleHipRight*angleBbodyAUpperLegRight<0){errorAngleHipRight *= -1;}
			double angleControlHipRight = pidAngleHipRight.Control(errorAngleHipRight,dts);
			Echo("errorAngleHipRight:"+Math.Round(errorAngleHipRight,2));
			Echo("angleControlHipRight:"+Math.Round(angleControlHipRight,2));
			
			rightHip.TargetVelocityRPM=-Convert.ToSingle(angleControlHipRight);
			
			
			double errorAngleHipLeft = wantedAngleHipLeft-angleBbodyAUpperLegLeft;
			if(errorAngleHipLeft<-180){errorAngleHipLeft += 180;}
			if(errorAngleHipLeft>+180){errorAngleHipLeft += -180;}
			if(wantedAngleHipLeft*angleBbodyAUpperLegLeft<0){errorAngleHipLeft *= -1;}
			double angleControlHipLeft = pidAngleHipLeft.Control(errorAngleHipLeft,dts);
			Echo("errorAngleHipLeft:"+Math.Round(errorAngleHipLeft,2));
			Echo("angleControlHipLeft:"+Math.Round(angleControlHipLeft,2));
			
			leftHip.TargetVelocityRPM=Convert.ToSingle(angleControlHipLeft);
			
			
			double errorAngleKneeLeft = wantedAngleKneeLeft-angleLeftKnee;
			if(errorAngleKneeLeft<-180){errorAngleKneeLeft += 180;}
			if(errorAngleKneeLeft>+180){errorAngleKneeLeft += -180;}
			if(wantedAngleKneeLeft*angleLeftKnee<0){errorAngleKneeLeft *= -1;}
			double angleControlKneeLeft = pidAngleKneeLeft.Control(errorAngleKneeLeft,dts);
			Echo("errorAngleKneeLeft:"+Math.Round(errorAngleKneeLeft,2));
			Echo("angleControlKneeLeft:"+Math.Round(angleControlKneeLeft,2));
			
			leftKnee.TargetVelocityRPM=Convert.ToSingle(angleControlKneeLeft);
			
			
			
			double errorAngleKneeRight = wantedAngleKneeRight-angleRightKnee;
			if(errorAngleKneeRight<-180){errorAngleKneeRight += 180;}
			if(errorAngleKneeRight>+180){errorAngleKneeRight += -180;}
			if(wantedAngleKneeRight*angleRightKnee<0){errorAngleKneeRight *= -1;}
			double angleControlKneeRight = pidAngleKneeRight.Control(errorAngleKneeRight,dts);
			Echo("errorAngleKneeRight:"+Math.Round(errorAngleKneeRight,2));
			Echo("angleControlKneeRight:"+Math.Round(angleControlKneeRight,2));
			
			if(rightKnee==null){Echo("rightKnee is null");}
			rightKnee.TargetVelocityRPM=Convert.ToSingle(angleControlKneeRight);
			
			leftHip.CustomName = "leftHip";
			rightHip.CustomName = "rightHip";
			leftKnee.CustomName = "leftKnee";
			rightKnee.CustomName = "rightKnee";
			
			
			// pidAngleKneeRight.Control(Error);
			// pidAngleKneeLeft.Control(Error);

			
			
			
		}
		
		
		
		
	}
	
			
	
	Echo("lol2");
}


//=================================
//from the workshop link in the file's header

public class PIDController
{
    double p = 0;
    double i = 0;
    double d = 0;

    double errorIntegral = 0;
    double lastError = 0;

    bool firstRun = true;

    public PIDController(double p, double i, double d)
    {
        this.p = p;
        this.i = i;
        this.d = d;
    }

    public double Control(double error, double timeStep)
    {
        double errorDerivative;

        if (firstRun)
        {
            errorDerivative = 0;
            firstRun = false;
        }
        else
        {
            errorDerivative = (error - lastError) / timeStep;
        }

        lastError = error;

        errorIntegral += error * timeStep;
        return p * error + i * errorIntegral + d * errorDerivative;
    }

    public void Reset()
    {
        errorIntegral = 0;
        lastError = 0;
        firstRun = true;
    }
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
