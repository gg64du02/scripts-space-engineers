List<String> stringList = new List<String>();

List<Vector2D> oreCoords2DSubPattern = new List<Vector2D>();

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

    stringList.Add("5.285714285714286,50.857142857142854,SiNiMg");
    stringList.Add("7.266666666666667,17.666666666666668,SiUrMg");
    stringList.Add("8.666666666666666,88.125,UrMgSi");
    stringList.Add("10.785714285714286,112.14285714285714,AgAu");
    stringList.Add("23.434782608695652,49.08695652173913,UrAuFe");
    stringList.Add("26.785714285714285,76.14285714285714,AgAu");
    stringList.Add("34.22857142857143,16.685714285714287,MgSiNi");
    stringList.Add("36.666666666666664,109.125,UrMgSi");
    stringList.Add("42.833333333333336,42.333333333333336,FeNiCo");
    stringList.Add("51.22857142857143,70.68571428571428,MgSiNi");
    stringList.Add("60.56666666666667,7.033333333333333,FeNiCo");
    stringList.Add("62.88235294117647,104.32352941176471,FeAuUr");
    stringList.Add("69.88235294117646,29.323529411764707,FeAuUr");
    stringList.Add("70.78571428571429,55.142857142857146,AgAu");
    stringList.Add("78.26666666666667,79.66666666666667,SiUrMg");
    stringList.Add("88.78571428571429,108.14285714285714,AgAu");
    stringList.Add("90.66666666666667,13.125,UrMgSi");
    stringList.Add("92.28571428571429,53.857142857142854,SiNiMg");
    stringList.Add("96.66666666666667,82.125,UrMgSi");
    stringList.Add("112.83333333333333,18.333333333333332,FeNiCo");
    stringList.Add("114.70833333333333,83.66666666666667,UrAgCo");
    stringList.Add("116.41666666666667,52.083333333333336,UrAgCo");
    stringList.Add("115.26666666666667,112.66666666666667,SiUrMg");

    foreach(var str in stringList)
    {
        Echo("str" + str);

        // using the method 
        String[] strlist = str.Split(',');
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[1];
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo(oreCoords2DSubPattern.Count+"");

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



    //Get the PB Position:
    Vector3D myPos = Me.GetPosition();


    //Don't change unless you know what you are doing: 128 * 16 = 2048
    int constantNumbersOfSubPatternToGenerate = 16;

    //Get any control capable block to get the planet center

    //detect if it is a know planet

    //choose the appropriate settings to use for the detected planet

    //generate all coordinates to the cube planet

    //get the first three closest faces.
    
    //try to get closer with the first ores in the subpattern infos? (quicker to run ?)

    //then generated all ores nearby

    //sort and write those closest in the Custom Data



    //put the script output inside the customdata of the PB it is running onto
    Me.CustomData = "";
}
