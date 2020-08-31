//script would be based on
//https://www.cmu.edu/biolphys/deserno/pdf/sphere_equi.pdf

//sphere radius in meters
double r = 60;

//Numbers of wanted points
double N = 50;

//numbers of generated points
double N_count = 0;

double a = 0;
double d = 0;


double M_v = 0;
double d_v = 0;
double d_phi = 0;







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
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
    a = 4 * Math.PI * (r * r) / N;
    d = Math.Sqrt(a);


    M_v = Math.Round((Math.PI / d),3);
    d_v = Math.PI / M_v;
    d_phi = a / d_v;

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
    Echo("a:" + a);
    Echo("d:" + d);
    Echo("M_v:" + M_v);
    Echo("d_v:" + d_v);
    Echo("d_phi:" + d_phi);

    for (int m = 0; m < M_v; m++)
    {
        double v = Math.PI * (m + .5) / M_v;
        double M_phi = Math.Round((2 * Math.PI * Math.Sin(v) / d_v), 3);
        for (int n = 0; n < M_phi; n++)
        {
            double phi = 2 * Math.PI * n / M_phi;
            //Create point using Eqn. (1).
            N_count = N_count + 1;
        }
    }
    Echo("N_count:" + N_count);
}
