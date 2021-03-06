using System.Collections.Generic;
public class MeasurementPoint
{
    public string name { get; set; }
    public string n { get; set; }
    public string w { get; set; }


   
    public NoiseEmissionMeasurement dayNoiseEmissionMeasurement { get; set; }
    public NoiseEmissionMeasurement nightNoiseEmissionMeasurement { get; set; }

    public EnvironmentalNoiseMeasurement dayEnvironmentalNoiseMeasurement { get; set; }
    public EnvironmentalNoiseMeasurement nightEnvironmentalNoiseMeasurement { get; set; }

    public MeasurementPoint(string name, string n, string w)
    {
        this.name = name;
        this.n = n;
        this.w = w;

        dayNoiseEmissionMeasurement = new NoiseEmissionMeasurement();
        nightNoiseEmissionMeasurement = new NoiseEmissionMeasurement();
        dayEnvironmentalNoiseMeasurement = new EnvironmentalNoiseMeasurement();
        nightEnvironmentalNoiseMeasurement = new EnvironmentalNoiseMeasurement();
    }


}

