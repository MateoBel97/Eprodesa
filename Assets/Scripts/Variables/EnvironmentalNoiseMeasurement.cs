using System.Collections.Generic;
using UnityEngine;

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

    public EnvironmentalNoiseResult(string[] values)
    {
        char oldC = ' ', newC = ' ';
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            oldC = '.';
            newC = ',';
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            oldC = ',';
            newC = '.';
        }
        this.levelN = float.Parse(values[1].Replace(oldC, newC));
        this.fileNumberN = int.Parse(values[2]);
        this.levelW = float.Parse(values[3].Replace(oldC, newC));
        this.fileNumberW = int.Parse(values[4]);
        this.levelE = float.Parse(values[5].Replace(oldC, newC));
        this.fileNumberE = int.Parse(values[6]);
        this.levelS = float.Parse(values[7].Replace(oldC, newC));
        this.fileNumberS = int.Parse(values[8]);
        this.levelV = float.Parse(values[9].Replace(oldC, newC));
        this.fileNumberV = int.Parse(values[10]);
        this.initialTime = values[11];
        this.finalTime = values[12];
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
