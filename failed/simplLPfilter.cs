
public class simplLPfilter
{
	float output = 0f;
	 float expected_dts =0f;
	 float frequency = 100f;
	 float tau=1f;
	public simplLPfilter(float expected_dts, float frequency){
		this.expected_dts = expected_dts;
		this.frequency = frequency;
		this.tau = 1 / (1/(2*Convert.ToSingle(Math.PI) *this.frequency));
	}
	
	public float compute(float new_sample){
		float tmp = 0f;
		tmp = this.output + (1/this.tau)*((new_sample-this.output))*this.expected_dts;
		
		return tmp;
	}
}