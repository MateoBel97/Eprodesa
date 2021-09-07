using System.Collections.Generic;

public class EnvironmentalNoiseMeasurement
{
    public EnvironmentalNoiseResult environmentalNoiseResult = new EnvironmentalNoiseResult();
}

public class EnvironmentalNoiseResult
{
    public string description { get; set; }
    public float levelN { get; set; }
    public int fileNumberN { get; set; }
    public float levelW { get; set; }
    public int fileNumberW { get; set; }
    public float levelE { get; set; }
    public int fileNumberE { get; set; }
    public float levelS { get; set; }
    public int fileNumberS { get; set; }
    public float levelV { get; set; }
    public int fileNumberV { get; set; }
    public string initialTime { get; set; }
    public string finalTime { get; set; }

    public EnvironmentalNoiseResult(
        float levelN, int fileNumberN,
        float levelW, int fileNumberW,
        float levelE, int fileNumberE,
        float levelS, int fileNumberS,
        float levelV, int fileNumberV,
        string initialTime, string finalTime)
    {
        this.levelN = levelN;
        this.fileNumberN = fileNumberN;
        this.levelW = levelW;
        this.fileNumberW = fileNumberW;
        this.levelE = levelE;
        this.fileNumberE = fileNumberE;
        this.levelS = levelS;
        this.fileNumberS = fileNumberS;
        this.levelV = levelV;
        this.fileNumberV = fileNumberV;
        this.initialTime = initialTime;
        this.finalTime = finalTime;
    }

    public EnvironmentalNoiseResult ()
    {
        this.levelN = 0.0f;
        this.fileNumberN = 0;
        this.levelW = 0.0f;
        this.fileNumberW = 0;
        this.levelE = 0.0f;
        this.fileNumberE = 0;
        this.levelS = 0.0f;
        this.fileNumberS = 0;
        this.levelV = 0.0f;
        this.fileNumberV = 0;
        this.initialTime = null;
        this.finalTime = null;
    }
}
