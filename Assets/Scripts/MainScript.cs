using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTMP;
    public bool defaultValues;
    
    void Start()
    {
        if(defaultValues)
        {
            CreateDefaultValues();
        }
        else
        {
            GlobalData.ResetValues();
        }

    }

    void CreateDefaultValues()
    {
        GlobalData.generalDataSaved = true;
        GlobalData.measurementPointsDataSaved = true;
        GlobalData.weatherDataSaved = true;
        GlobalData.technicalInfoDataSaved = true;
        GlobalData.emissionResultsSaved = true;
        GlobalData.noiseResultsSaved = true;
        GlobalData.descriptionDataSaved = true;
        GlobalData.eventsDataSaved = true;
        GlobalData.sourceDataSaved = true;


        GlobalData.companyName = "Empresa X";
        GlobalData.day = 10;
        GlobalData.month = 8;
        GlobalData.year = 2021;
        GlobalData.workOrder = "OT123456";

        GlobalData.pointName = new List<string> { "Punto 1", "Punto 2", "Punto 3" };
        GlobalData.n = new List<string> { "11111.11", "22222.22", "33333.33" };
        GlobalData.w = new List<string> { "111111.11", "222222.22", "333333.33" };

        //GlobalData.timeFrame = GlobalData.TimeFrame.night;

        GlobalData.dayTimeFrame = true;
        GlobalData.nightTimeFrame = true;

        GlobalData.initialWindSpeed_D = 5;
        GlobalData.finalWindSpeed_D = 6;
        GlobalData.initialTemperature_D = 26;
        GlobalData.finalTemperature_D = 31;
        GlobalData.initialHumidity_D = 55;
        GlobalData.finalHumidity_D = 60;
        GlobalData.initialAtmPressure_D = 1100;
        GlobalData.finalAtmPressure_D = 1100;

        GlobalData.initialWindSpeed_N = 7;
        GlobalData.finalWindSpeed_N = 7;
        GlobalData.initialTemperature_N = 77;
        GlobalData.finalTemperature_N = 77;
        GlobalData.initialHumidity_N = 77;
        GlobalData.finalHumidity_N = 77;
        GlobalData.initialAtmPressure_N = 7700;
        GlobalData.finalAtmPressure_N = 7770;


        GlobalData.isNoiseEmissionMeasurement = true;
        GlobalData.isEnvironmentalNoiseMeasurement = true;
        GlobalData.isEmissionParameter = true;
        GlobalData.isResidualParameter = true;
        GlobalData.isDayTimeFrame = true;
        GlobalData.isNightTimeFrame = false;
        GlobalData.soundMeterSerialNumber = "SN123123";
        GlobalData.calibratorSerialNumber = "SN456456";
        GlobalData.metStationSerialNumber = "SN789789";


        GlobalData.emissionDescription = new List<string> { "Emision1", "Emision2", "Emision3", "Emision4", "Emision5" };
        GlobalData.emissionLASEQ = new List<float> { 1f, 11f, 111f, 1111f, 11111f };
        GlobalData.emissionL90 = new List<float> { 10f, 110f, 1110f, 11110f, 111110f };
        GlobalData.emissionFileNumber = new List<int> { 101, 102, 103, 104, 105};
        GlobalData.emissionInitialTime = new List<string> { "01:01", "02:02", "03:03", "04:04", "05:05" };
        GlobalData.emissionFinalTime = new List<string> { "01:11", "02:22", "03:33", "04:44", "05:55" };

    
        GlobalData.noiseDescription = new List<string> { "Residual1", "Residual2", "Residual2", "Residual4", "Residual5" };
        GlobalData.noiseLevelN = new List<float> { 50.1f, 50.2f, 50.3f, 50.4f, 50.5f };
        GlobalData.noisefileNumberN = new List<int> { 01, 02, 103, 04, 05 };
        GlobalData.noiseLevelW = new List<float> { 51.1f, 51.2f, 51.3f, 51.4f, 51.5f };
        GlobalData.noisefileNumberW = new List<int> { 101, 102, 103, 104, 105 };
        GlobalData.noiseLevelE = new List<float> { 52.1f, 52.2f, 52.3f, 52.4f, 52.5f };
        GlobalData.noisefileNumberE = new List<int> { 201, 202, 203, 204, 205 };
        GlobalData.noiseLevelS = new List<float> { 53.1f, 53.2f, 53.3f, 53.4f, 53.5f };
        GlobalData.noisefileNumberS = new List<int> { 301, 302, 303, 304, 305 };
        GlobalData.noiseLevelV = new List<float> { 54.1f, 54.2f, 54.3f, 54.4f, 54.5f };
        GlobalData.noisefileNumberV = new List<int> { 401, 402, 403, 404, 405 };
        GlobalData.noiseInitialTime = new List<string> { "11:01", "22:02", "33:03", "44:04", "55:05" };
        GlobalData.noiseFinalTime = new List<string> { "11:11", "22:22", "33:33", "44:44", "55:55" };

        GlobalData.description = "Texto con descripción de las condiciones y procesos de medición";

        GlobalData.eventName = new List<string> { "Evento 1", "Evento 2", "Evento 3" };
        GlobalData.eventLevel = new List<float> { 80.0f, 81.0f, 82.0f};
        GlobalData.eventTime = new List<string> { "00:01", "00:02", "00:03" };
        GlobalData.eventLength = new List<float> { 10.0f, 20.0f, 30.0f};
        
        GlobalData.source = "Texto con descripción de la relación entre la fuente de ruido y el receptor, superficies, geometrías y métodos de control existentes";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
