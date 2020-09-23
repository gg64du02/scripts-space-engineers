//from https://steamcommunity.com/sharedfiles/filedetails/?id=1134945036
/*   
/// Gravity Drive Manager v5 - 10/9/2018 ///   
/________________________________________________   
Description:   
   
    This code control your Gravity Drive automatically.   
________________________________________________   
How do I use this?   
   
    1) Create a group with the name "GDrive" which contain:  
        - At least one Seat, Cockpit or Remote.  
        - At least one Gravity Generator (3 in different axis for full motion).  
        - At least one Artificial Mass (In the range of the Gravity Generator).  
   
    2) Place a program block with this code   

    3) Buckle up!
   
________________________________________________      
Author's Notes      
      
    Special thanks to Whiplash141 and it's VTOL script for laying down the    
    basement (also most of the walls) of this script.    
    See: https://steamcommunity.com/sharedfiles/filedetails/?id=757123653&searchtext=vtol   
   
    Make sure to setup the Artificial Masses evently arround the center of mass   
    of the ship or you will get torque.   
   
    You dont need any thrusters to make your ship move but you will need them to   
    make a smooth stop. In emergency situation, kill the 'dampenerAntiJerk' in order    
    to achive a jerky kind of stop. It can be usefull after a hard fight!   
   
    Spreading of Masses and Gravity Generator through all the ship will give   
    you a amazing redundancy in combat.   
   
    Multiply Masses and Gravity Generator for increase thrust.   
   
    In order to reduce power consumption, reduce the range of the Gravity Generators    
    to the minimum amount needed to have your Artificial Masses inside the gravity feild.   
   
    If the drive is too powerfull for the ship, you will experence oscillations at low    
    speed. Increase the dampenerAntiJerk to a value higher than the speed of your    
    oscillations will correct the issue.   
   
    Fly safe!   
      
- Mohawk   
*/   
   
    const string nameTag = "GDrive";   
    // Suffix requested on any Remote, Control Seat, Artificial Mass & Gravity generator.   
    
    const double dampenerScalingFactor = 50;   
    // This controls how responsive the dampeners are. Higher numbers mean quicker response but   
    // can also lead to oscillations.   
   
    const double dampenerAntiJerk = 5.00;    
    // This act as a kill switch that kill oscillations at low speed.   
    
    const double fullBurnToleranceAngle = 30;     
    // Max angle (in degrees) that a thruster can be off axis of input direction and still    
    // receive maximum thrust output.   
    
    const double maxThrustAngle = 65;   
    // Max angle (in degrees) that a thruster can deviate from the desired travel direction     
    // and still be controlled with movement keys.   
    
    const double maxDampeningAngle = 65;   
    // Max angle (in degrees) between a thruster's dampening direction and desired move direction   
    // that is allowed for dampener function.   
   
//-----------------------------------------------   
//         No touching below this line.   
//-----------------------------------------------   
   
const double updatesPerSecond = 10; // Number of updates per second.   
const double timeMaxCycle = 1 / updatesPerSecond;   
double timeCurrentCycle = 0;     
const double refreshInterval = 5;    
double refreshTime = 141;    
bool isSetup = false;   
bool isSoftShutdown = false;   
bool isHardShutdown = false;   
bool isAntiJerkOn = true;   
   
double maxThrustDotProduct = Math.Cos(maxThrustAngle * Math.PI / 180);    
double minDampeningDotProduct = Math.Cos(maxDampeningAngle * Math.PI / 180);    
double fullBurnDotProduct = Math.Cos(fullBurnToleranceAngle * Math.PI / 180);    
   
List<IMyShipController> referenceList = new List<IMyShipController>();   
List<IMyGravityGenerator> gravityGenerators = new List<IMyGravityGenerator>();   
List<IMyVirtualMass> virtualMasses = new List<IMyVirtualMass>();  
  
bool gravityGeneratorsExcludeStandby = false;  
List<IMyGravityGenerator> gravityGeneratorsExclude = new List<IMyGravityGenerator>(); 
List<bool> gravityGeneratorsExcludeEnable = new List<bool>();  
 
List<IMyGravityGeneratorSphere> gravityGeneratorsSphereExclude = new List<IMyGravityGeneratorSphere>(); 
List<bool> gravityGeneratorsSphereExcludeEnable = new List<bool>();  
   
IMyShipController thisReferenceBlock = null;

//spherical GDRIVE mod
List<IMyGravityGeneratorSphere> gravityGeneratorsSphere= new List<IMyGravityGeneratorSphere>(); 

public Program()
{
  // Configure this program to run the Main method every 1 update ticks
  Runtime.UpdateFrequency = UpdateFrequency.Update1;
  
}
   
void Main(string argument)   
{   
	//Echo("lol1");
    ProcessArgument(argument);   
   
    timeCurrentCycle += Runtime.TimeSinceLastRun.TotalSeconds;   
    refreshTime += Runtime.TimeSinceLastRun.TotalSeconds;   
   
	//Echo("lol2");
	
	if (Runtime.TimeSinceLastRun.TotalSeconds > 0.1)   
	{   
		Echo("WARNING: Slow refresh detected \nUse a 'Trigger Now' timer loop\n");   
	}   
   
    if (!isSetup || (refreshTime >= refreshInterval))   
    {   
		//Echo("lol3");
        isSetup = GrabBlocks();   
        refreshTime = 0;   
		//Echo("lol4");
    }   
	
	if(gravityGenerators!=null){
		Echo("gravityGenerators.Count: "+gravityGenerators.Count);
	}
	if(gravityGeneratorsSphere == null){
	Echo("gravityGeneratorsSphere == null");
	}
	else{
		Echo("gravityGeneratorsSphere.Count: "+gravityGeneratorsSphere.Count);
	}

	//Echo("lol5");
	
	
    if (isSetup && (timeCurrentCycle >= timeMaxCycle))   
    {   
        try   
        {   
            Echo("GDrive - Gravity Drive Manager.\n");   
   
            //Gets reference block that is under control   
            thisReferenceBlock = GetControlledShipController(referenceList);   
   
            Echo($"Non-GDrive Gravity Generators: {gravityGeneratorsExclude.Count + gravityGeneratorsSphereExclude.Count}"); 
            Echo($"GDrive Generators: {gravityGenerators.Count}");   
            Echo($"GDrive Masses: {virtualMasses.Count}");   
   
            Echo("\n GDrive status: " + DriveState());   
            Echo("\n Anti-Jerk status: " + (isAntiJerkOn ? "ON" : "OFF"));   
   
            // Travel Vector.   
            var travelVec = thisReferenceBlock.GetShipVelocities().LinearVelocity;   
            if (travelVec.LengthSquared() > 0)   
            {   
                travelVec = Vector3D.Normalize(travelVec);   
            }   
   
			 // Ship Speed.   
            var shipSpeed = thisReferenceBlock.GetShipSpeed();   
   
            // Desired Direction Vector.   
            var referenceMatrix = thisReferenceBlock.WorldMatrix;   
            var inputVec = thisReferenceBlock.MoveIndicator; // Raw input vector.   
            var desiredDirection = referenceMatrix.Backward * inputVec.Z + referenceMatrix.Right * inputVec.X + referenceMatrix.Up * inputVec.Y; // World relative input vector.   
            if (desiredDirection.LengthSquared() > 0)   
            {   
                desiredDirection = Vector3D.Normalize(desiredDirection);   
            }   
   
            // Dampeners State.   
            bool dampenersOn = thisReferenceBlock.DampenersOverride;   
   
            if (isHardShutdown)   
			{   
				ShutdownDrive();   
			}   
			else if (isSoftShutdown && shipSpeed < dampenerAntiJerk)   
			{   
				isHardShutdown = true;   
			}   
			else   
			{   
		
				ApplyThrust( travelVec, shipSpeed, desiredDirection, dampenersOn);   
			}   
               
            timeCurrentCycle = 0;   
        }   
        catch   
        {   
            isSetup = false;   
        }   
    }   
}   
   
void ProcessArgument(string arg)   
{   
    switch( arg.ToLower() )   
    {   
    	// StartUp the drive.   
    	case "startup":    
			isSoftShutdown = false;   
			isHardShutdown = false;   
			break;   
   
		// Shutdown the drive when ship velocity is low enought.   
        case "soft_shutdown":    
        	isSoftShutdown = true;   
        	break;   
   
        // Shutdown immediatly the drive.   
        case "hard_shutdown":    
        	isHardShutdown = true;   
        	break;   
   
        // Use the GDrive for make a complete stop (Emergency only).   
        case "toggle_antijerk":   
        	isAntiJerkOn = !isAntiJerkOn;   
        	break;   
    }   
}   
   
bool GrabBlocks()   
{ 
    //IMyBlockGroup theGroup = GridTerminalSystem.GetBlockGroupWithName(nameTag);  
 
    List<IMyBlockGroup> blockGroups = new List<IMyBlockGroup>(); 
    IMyBlockGroup theGroup = null; 
 
    GridTerminalSystem.GetBlockGroups(blockGroups); 
    foreach (IMyBlockGroup blockGroup in blockGroups)   
    {  
        if (blockGroup.Name.Contains(nameTag)) 
        { 
             theGroup = blockGroup; 
        } 
    }  
 
    if (theGroup == null)   
    {  
        Echo($"[ERROR]: No group with name tag '{nameTag}' was found");   
        return false;   
    }  
  
    theGroup.GetBlocksOfType(referenceList, block => block.CubeGrid == Me.CubeGrid);   
    if (referenceList.Count == 0)   
    {   
        Echo($"[ERROR]: No remote or control seat inside group name tag '{nameTag}' was found");   
        return false;   
    }  
   
	GridTerminalSystem.GetBlocksOfType<IMyGravityGeneratorSphere>(gravityGeneratorsSphere);
	
	theGroup.GetBlocksOfType(gravityGeneratorsSphere, block => block.CubeGrid == referenceList[0].CubeGrid);   
	if(gravityGeneratorsSphere.Count == 0){
        Echo($"[ERROR]: No SPHERICAL Gravity Generators inside group name tag '{nameTag}' was found");   
        return false;   
	}
	/*
    if (gravityGenerators.Count == 0)   
    {   
        Echo($"[ERROR]: No Gravity Generators inside group name tag '{nameTag}' was found");   
        return false;   
    }   
	*/
   
	Echo("virtualMasses.Count: "+virtualMasses.Count);
	//Echo("block: "+block);
	Echo("referenceList: "+referenceList);
   
    theGroup.GetBlocksOfType(virtualMasses, block => block.CubeGrid == referenceList[0].CubeGrid); 
	
	//Echo("theGroup.Count: "+theGroup.Count);
    if (gravityGeneratorsSphere.Count == 0)   
    {   
        Echo($"[ERROR]: No Artificial Mass inside group name tag '{nameTag}' was found");   
        return false;   
    }   
   
    GridTerminalSystem.GetBlocksOfType(gravityGeneratorsExclude, block => block.CubeGrid == referenceList[0].CubeGrid);  
    foreach (IMyGravityGenerator thisgravityGenerator in gravityGenerators)   
    {  
        gravityGeneratorsExclude.Remove(thisgravityGenerator);  
    }  
    foreach (IMyGravityGenerator thisgravityGenerator in gravityGeneratorsExclude)   
    {  
        gravityGeneratorsExcludeEnable.Add(thisgravityGenerator.Enabled);  
    } 
 
    GridTerminalSystem.GetBlocksOfType(gravityGeneratorsSphereExclude, block => block.CubeGrid == referenceList[0].CubeGrid);  
    foreach (IMyGravityGeneratorSphere thisgravityGenerator in gravityGeneratorsSphereExclude)   
    {  
        gravityGeneratorsSphereExcludeEnable.Add(thisgravityGenerator.Enabled);  
    } 
 
    return true;   
} 
   
IMyShipController GetControlledShipController(List<IMyShipController> SCs)   
{   
    foreach (IMyShipController thisController in SCs)   
    {   
        if (thisController.IsUnderControl && thisController.CanControlShip)   
            return thisController;   
    }   
   
    return SCs[0];   
}   
   
void ShutdownDrive()      
{     
    foreach (IMyGravityGenerator thisGravityGenerator in gravityGenerators)   
    {   
    	thisGravityGenerator.ApplyAction("OnOff_Off");   
    }   
   
	foreach (IMyVirtualMass thisVirtualMass in virtualMasses)   
    {   
    	thisVirtualMass.ApplyAction("OnOff_Off");   
    }   
  
    foreach (IMyGravityGenerator thisgravityGenerator in gravityGeneratorsExclude)   
    {   
    	thisgravityGenerator.ApplyAction("OnOff_On");   
    }   
}   
   
void ApplyThrust(Vector3D travelVec, double speed, Vector3D desiredDirectionVec, bool dampenersOn)   
{  
	bool isActive = false;  
	
	Echo("ApplyThrust:begin");
	
	Echo("desiredDirectionVec: "+desiredDirectionVec);
	
	//disable the artificial mass
	foreach (IMyVirtualMass thisVirtualMass in virtualMasses)   
    {   
    	thisVirtualMass.ApplyAction("OnOff_Off");   
		Echo("OnOff_On");
    }   
	
	var mdr_str = "";
	
    foreach (IMyVirtualMass virtualMass in virtualMasses)   
    {
		//Echo("virtualMass.CustomName:"+virtualMass.CustomName);
		//make out the side of center of gravity the artificial mass is on
		//Echo("virtualMass.CubeGrid.GetPosition()"+virtualMass.CubeGrid.GetPosition());
		//Echo("virtualMass.GetPosition()"+virtualMass.GetPosition());
		//Echo("thisReferenceBlock.CenterOfMass"+thisReferenceBlock.CenterOfMass);
		Vector3D COGtoAMass = virtualMass.GetPosition() - thisReferenceBlock.CenterOfMass;
		//Echo("COGtoAMass: "+COGtoAMass);
		
		double offsetNeededInVelocity = COGtoAMass.Dot(desiredDirectionVec);
		Echo("offsetNeededInVelocity:"+offsetNeededInVelocity);
		
		mdr_str = mdr_str + "|" + Math.Round((offsetNeededInVelocity), 0);
		
		//if(Math.Abs(offsetNeededInVelocity)>4){
		if(offsetNeededInVelocity>4){
			//choose to enable on not a specific artificial mass
			virtualMass.ApplyAction("OnOff_On");   
		}
		
		if(dampenersOn == true){
			//Echo("damp:on");
			double offsetDampeningVelocity = COGtoAMass.Dot(travelVec);
			if(offsetDampeningVelocity>-4){
				virtualMass.ApplyAction("OnOff_Off");   
			}
			else{
				virtualMass.ApplyAction("OnOff_On");   
			}
			
            //SetGravityGeneratorOverride(thisGravityGenerator, (float)Math.Max(scale * 100f, targetOverride));   
		}
		else{
			//Echo("damp:off");
		}
	}
	
    List<IMyRadioAntenna> listAntenna = new List<IMyRadioAntenna>();
    GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(listAntenna);
    //debug roll
    var str_to_display = mdr_str;
    if (listAntenna.Count != 0)
    {
        listAntenna[0].HudText = str_to_display;
    }
   
    /// GDrive Gravity Generator ///   
    foreach (IMyGravityGenerator thisGravityGenerator in gravityGenerators)   
    {   
    	var thrustDirection = thisGravityGenerator.WorldMatrix.Up; //gets the direction that the thruster flame fires   
        float scale = -(float)thrustDirection.Dot(desiredDirectionVec); //projection of the thruster's direction onto the desired direction    
   
        if (scale >= maxThrustDotProduct)     
        {     
            scale /= (float)fullBurnDotProduct; //scales it so that the thruster output ramps down after the fullBurnToleranceAngle is exceeded    
   
            var velocityInThrustDirection = thrustDirection.Dot(travelVec) * speed;     
            double targetOverride = 0;      
     
            if (velocityInThrustDirection < 1)      
                targetOverride = velocityInThrustDirection * dampenerScalingFactor;      
            else      
                targetOverride = velocityInThrustDirection * Math.Abs(velocityInThrustDirection) * dampenerScalingFactor;     
                  
            SetGravityGeneratorOverride(thisGravityGenerator, (float)Math.Max(scale * 100f, targetOverride));   
            isActive = true;   
            continue;     
        }   
   
    	thrustDirection = thisGravityGenerator.WorldMatrix.Down; //gets the direction that the thruster flame fires    
        scale = -(float)thrustDirection.Dot(desiredDirectionVec); //projection of the thrust's direction onto the desired direction    
   
        if (scale >= maxThrustDotProduct)     
        {     
            scale /= (float)fullBurnDotProduct; //scales it so that the thruster output ramps down after the fullBurnToleranceAngle is exceeded    
   
            var velocityInThrustDirection = thrustDirection.Dot(travelVec) * speed;     
            double targetOverride = 0;      
     
            if (velocityInThrustDirection < 1)      
                targetOverride = velocityInThrustDirection * dampenerScalingFactor;      
            else      
                targetOverride = velocityInThrustDirection * Math.Abs(velocityInThrustDirection) * dampenerScalingFactor;     
                  
            SetGravityGeneratorOverride(thisGravityGenerator, -(float)Math.Max(scale * 100f, targetOverride));   
            isActive = true;   
            continue;     
        }   
   
        /// Dampeners ///   
        if (dampenersOn && (!isAntiJerkOn || (isAntiJerkOn && (speed > dampenerAntiJerk))))   
        {   
	        thrustDirection = thisGravityGenerator.WorldMatrix.Up; //gets the direction that the thruster flame fires    
	        scale = -(float)thrustDirection.Dot(travelVec); //projection of the thrust's direction onto the desired direction    
   
	        if ((scale >= maxThrustDotProduct) && (thrustDirection.Dot(desiredDirectionVec) <= minDampeningDotProduct))     
	        {     
				var velocityInThrustDirection = thrustDirection.Dot(travelVec) * speed;     
	            double targetOverride = 0;      
	     
	            if (velocityInThrustDirection < 1)      
	                targetOverride = velocityInThrustDirection * dampenerScalingFactor;      
	            else      
	                targetOverride = velocityInThrustDirection * velocityInThrustDirection * dampenerScalingFactor;     
	                  
	            SetGravityGeneratorOverride(thisGravityGenerator, (float)targetOverride);   
	            isActive = true;   
	            continue;    
	        }   
   
	        thrustDirection = thisGravityGenerator.WorldMatrix.Down; //gets the direction that the thruster flame fires    
	        scale = -(float)thrustDirection.Dot(travelVec); //projection of the thrust's direction onto the desired direction    
   
	        if ((scale >= maxThrustDotProduct) && (thrustDirection.Dot(desiredDirectionVec) <= minDampeningDotProduct))     
	        {     
				var velocityInThrustDirection = thrustDirection.Dot(travelVec) * speed;     
	            double targetOverride = 0;      
	     
	            if (velocityInThrustDirection < 1)      
	                targetOverride = velocityInThrustDirection * dampenerScalingFactor;      
	            else      
	                targetOverride = velocityInThrustDirection * velocityInThrustDirection * dampenerScalingFactor;     
	                  
	            SetGravityGeneratorOverride(thisGravityGenerator, -(float)targetOverride);   
	            isActive = true;   
	            continue;    
	        }   
	    }   
   
        SetGravityGeneratorOverride(thisGravityGenerator, 0);   
    }   
  
    /// Non-GDrive Gravity Generator ///   
    if (isActive)   
    { 
        // Normal Non-GDrive Gravity Generator 
        for(int i = 0; i < gravityGeneratorsExclude.Count && !gravityGeneratorsExcludeStandby; ++i)  
        {  
            gravityGeneratorsExcludeEnable[i] = gravityGeneratorsExclude[i].Enabled;  
        }  
        foreach (IMyGravityGenerator thisGravityGenerator in gravityGeneratorsExclude)   
        {   
            thisGravityGenerator.ApplyAction("OnOff_Off");  
        } 
 
        // Sphere Non-GDrive Gravity Generator 
        for(int i = 0; i < gravityGeneratorsSphereExclude.Count && !gravityGeneratorsExcludeStandby; ++i)  
        {  
            gravityGeneratorsSphereExcludeEnable[i] = gravityGeneratorsSphereExclude[i].Enabled;  
        }  
        foreach (IMyGravityGeneratorSphere thisGravityGenerator in gravityGeneratorsSphereExclude)   
        {   
            thisGravityGenerator.ApplyAction("OnOff_Off");  
        }   
  
        gravityGeneratorsExcludeStandby = true;  
    }   
    else   
    {   
        // Normal Non-GDrive Gravity Generator 
        for(int i = 0; i < gravityGeneratorsExclude.Count && gravityGeneratorsExcludeStandby; ++i)  
        {  
            gravityGeneratorsExclude[i].ApplyAction((gravityGeneratorsExcludeEnable[i])? "OnOff_On" : "OnOff_Off");   
        } 
 
        // Sphere Non-GDrive Gravity Generator 
        for(int i = 0; i < gravityGeneratorsSphereExclude.Count && gravityGeneratorsExcludeStandby; ++i)  
        {  
            gravityGeneratorsSphereExclude[i].ApplyAction((gravityGeneratorsSphereExcludeEnable[i])? "OnOff_On" : "OnOff_Off");   
        }  
  
        gravityGeneratorsExcludeStandby = false;  
    }   
  
  /*
    /// Artificial Masses ///  
    if (isActive)   
    {   
        foreach (IMyVirtualMass thisVirtualMass in virtualMasses)   
        {   
            thisVirtualMass.ApplyAction("OnOff_On");   
        }   
    }   
    else   
    // {   
        foreach (IMyVirtualMass thisVirtualMass in virtualMasses)   
        {   
            thisVirtualMass.ApplyAction("OnOff_Off");   
        }   
    }   */
}   
   
void SetGravityGeneratorOverride(IMyGravityGenerator gravityGenerator, float overrideValue)      
{      
    if (overrideValue > 0 || overrideValue < 0)   
    {   
    	gravityGenerator.SetValue<Single>("Gravity", overrideValue);   
		gravityGenerator.ApplyAction("OnOff_On");    
    }   
    else   
    {   
    	gravityGenerator.ApplyAction("OnOff_Off");   
    }   
}   
   
string DriveState()   
{   
	string strDriveState = "";   
   
	if (isHardShutdown)   
        strDriveState = "Shutdown. ";   
    else if (isSoftShutdown)   
        strDriveState = "Shuting Down... ";   
    else   
        strDriveState = "Running... ";   
   
    strDriveState += RunningSymbol();   
   
	return strDriveState;   
}   
   
int runningSymbolVariant = 0;   
string RunningSymbol()   
{   
    runningSymbolVariant++;   
    string strRunningSymbol = "";   
    
    if (runningSymbolVariant == 0)   
        strRunningSymbol = "|";   
    else if (runningSymbolVariant == 1)   
        strRunningSymbol = "/";   
    else if (runningSymbolVariant == 2)   
        strRunningSymbol = "--";   
    else if (runningSymbolVariant == 3)   
    {   
        strRunningSymbol = "\\";   
        runningSymbolVariant = 0;   
    }   
   
    return strRunningSymbol;   
}   
   
// v.2 Manage Arguments, Add Auhtor Notes, Add GDrive status in console, Add arguments to toggle anti-jerk.  
// v.3 Using Group name, Support other Gravity Generator in the grid. 
// v.4 Using Group name less sensitive, Support other Gravity Generator Sphere in the grid.
// v.5 No timer needed.