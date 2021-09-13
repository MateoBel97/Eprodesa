using System.Collections.Generic;
using UnityEngine;

public class NoiseEmissionMeasurement
{
    public enum Parameters
    {
        emissionOnly = 0,
        emissionAndResidual = 1
    }
    public int emissionResultBeingUpdated { get; set; }
    public int residualResultBeingUpdated { get; set; }
    public Parameters parameters { get; set; }
    public string descripcion { get; set; }
    public List<NoiseEmissionResult> emissionResults { get; set; }
    public List<NoiseEmissionResult> residualResults { get; set; }

    public NoiseEmissionMeasurement()
    {
        this.parameters = Parameters.emissionOnly;
        this.emissionResultBeingUpdated = 0;
        this.residualResultBeingUpdated = 0;
        this.emissionResults = new List<NoiseEmissionResult> { new NoiseEmissionResult(0.0f, 0.0f, 0, "", "")};
        //this.emissionResults = new List<NoiseEmissionResult> { };
        this.residualResults = new List<NoiseEmissionResult> { };
    }
}

public class NoiseEmissionResult
{
    public float laeq { get; set; }
    public float l90 { get; set; }
    public int fileNumber { get; set; }
    public string initialTime { get; set; }
    public string finalTime { get; set; }

    public NoiseEmissionResult(float laeq, float l90, int fileNumber, string initialTime, string finalTime)
    {
        this.laeq = laeq;
        this.l90 = l90;
        this.fileNumber = fileNumber;
        this.initialTime = initialTime;
        this.finalTime = finalTime;
    }

    public NoiseEmissionResult(string[] results)
    {
        char oldC = ' ', newC = ' ';
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            oldC = '.';
            newC = ',';
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            oldC = ',';
            newC = '.';
        }
        this.laeq = float.Parse(results[1].Replace(oldC, newC));
        this.l90 = float.Parse(results[2].Replace(oldC, newC));
        this.fileNumber = int.Parse(results[3]);
        this.initialTime = results[4];
        this.finalTime = results[5];
    }

    public NoiseEmissionResult()
    {
        this.laeq = 0.0f;
        this.l90 = 0.0f;
        this.fileNumber = 0;
        this.initialTime = null;
        this.finalTime = null;
    }



}

