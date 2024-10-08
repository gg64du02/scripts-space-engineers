string[] lyrics = {"Lorem ipsum dolor sit amet",
"Duis eu quam porttitor",
"Cras varius justo ut sapien bibendum malesuada.",
"In tincidunt sem non eleifend feugiat.",
"Fusce non quam id est egestas congue.",
"Suspendisse tempor augue sit amet dui suscipit ultrices.",
"Duis mattis massa vitae tincidunt vestibulum.",
"Phasellus fermentum elit nec ligula porttitor feugiat.",
"Suspendisse elementum neque id nisl finibus congue.",
"Nullam at felis mollis, vehicula mi elementum, tristique libero.",
"Quisque a tellus vitae odio varius facilisis.",
"Phasellus vel turpis id lorem dignissim cursus at nec metus.",
"Duis nec augue a neque consequat ornare.",
"Duis at lorem consequat, interdum ex nec, lacinia nulla.",
"Quisque sit amet ante auctor, rutrum erat in, porta mi.",
"Quisque vestibulum augue ut nibh aliquet mollis.",
"Donec vitae augue eget elit maximus auctor."};




IMyRadioAntenna Antenna = null;

IMyBeacon Beacon = null;

int i = 0;

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
    Runtime.UpdateFrequency = UpdateFrequency.Update100;

	
	var Blocks = new List<IMyTerminalBlock>();
	GridTerminalSystem.GetBlocks(Blocks);
	Beacon = Blocks.Find(x => x.CubeGrid == Me.CubeGrid && x is IMyBeacon) as IMyBeacon;
	Antenna = Blocks.Find(x => x.CubeGrid == Me.CubeGrid && x is IMyRadioAntenna) as IMyRadioAntenna;
	
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
    // one of the programmable block s Run actions are invoked,
    // or the script updates itself. The updateSource argument
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.


    // Echo("" + lyrics.Length);
    Echo("i: " + i);
    i++;
	
	Antenna.HudText = lyrics[i%lyrics.Length];
	Antenna.CustomName = lyrics[i%lyrics.Length];
	
	Beacon.HudText = lyrics[i%lyrics.Length];
	Beacon.CustomName = lyrics[i%lyrics.Length];
	
}