


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
    //   IsSameConstructAs(IMyTerminalBlock)

    List<IMyTerminalBlock> listTerminalBlock = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(listTerminalBlock);


    foreach (var tb in listTerminalBlock)
    {
        Echo("tb:" + tb);
        if (tb.IsSameConstructAs(Me))
        {
            Echo("if (tb.IsSameConstructAs(Me))");
        }
        else
        {
            Echo("NOT if (tb.IsSameConstructAs(Me))");
        }
        Echo("=========================");
    }
}
