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
            //GlobalData.ResetValues();
            Measurement.ResetValues();
        }

    }

    void CreateDefaultValues()
    {
        /*
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

        GlobalData.dayMetConditions = true;
        GlobalData.nightMetConditions = true;

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
        */
        Measurement.companyName = "Empresa X";
        Measurement.day = 10;
        Measurement.month = 8;
        Measurement.year = 2021;
        Measurement.workOrder = "OT123456";

        Measurement.measurementPoints = new List<MeasurementPoint>
        {
            new MeasurementPoint("Mi Punto 1", "11111.11", "111111.11"),
            new MeasurementPoint("Mi Punto 2", "22222.22", "222222.22"),
            new MeasurementPoint("Mi Punto 3", "33333.33", "333333.33"),
            new MeasurementPoint("Mi Punto 4", "44444.44", "444444.44")
        };
        Measurement.metConditionsBeingUpdated = 0;
        Measurement.usingDayMetConditions = true;
        Measurement.dayMetConditions = new MetConditions(
            5.0f, 6.0f,
            21.5f, 23.1f,
            50f, 55f,
            990f, 985f
            );
        Measurement.usingNightMetConditions = false;
        Measurement.nightMetConditions = new MetConditions();
        
        Measurement.usingNightMetConditions = true;
        Measurement.nightMetConditions = new MetConditions(
            8.0f, 7.0f,
            14.5f, 13.1f,
            60f, 54f,
            1000f, 1005f
            );
        
        Measurement.typeOfMeasurement = Measurement.TypeOfMeasurement.EnvironmentalNoise;
        Measurement.measuringDay = true;
        Measurement.measuringNight = true;
        Measurement.soundMeterSerialNumber = "SN1231230";
        Measurement.calibartorSerialNumber = "SN456456";
        Measurement.metStationSerialNumber = "SN78978";

        
        int j = 0;
        foreach(MeasurementPoint measurementPoint in Measurement.measurementPoints)
        {
            ++j;
            measurementPoint.dayNoiseEmissionMeasurement = new NoiseEmissionMeasurement();
            measurementPoint.dayNoiseEmissionMeasurement.parameters = NoiseEmissionMeasurement.Parameters.emissionAndResidual;
            measurementPoint.dayNoiseEmissionMeasurement.descripcion = "Descripción para día";
            measurementPoint.dayNoiseEmissionMeasurement.emissionResults = new List<NoiseEmissionResult> { };
            measurementPoint.nightNoiseEmissionMeasurement = new NoiseEmissionMeasurement();
            measurementPoint.nightNoiseEmissionMeasurement.parameters = NoiseEmissionMeasurement.Parameters.emissionAndResidual;
            measurementPoint.nightNoiseEmissionMeasurement.descripcion = "Descripción para noche";
            measurementPoint.nightNoiseEmissionMeasurement.emissionResults = new List<NoiseEmissionResult> { };

            for (int i = 1; i <= 3; i++)
            { 
                measurementPoint.dayNoiseEmissionMeasurement.emissionResults.Add(
                    new NoiseEmissionResult(80.0f + j + (i * 0.1f), 50.0f + j + (i * 0.1f), 20 + j + (i * 10), "12:0" + j.ToString(), "12:1" + j.ToString()));
                measurementPoint.dayNoiseEmissionMeasurement.residualResults.Add(
                    new NoiseEmissionResult(75.0f + j + (i * 0.1f), 45.0f + j + (i * 0.1f), 30 + j + (i * 10), "12:2" + j.ToString(), "12:2" + j.ToString()));
                measurementPoint.nightNoiseEmissionMeasurement.emissionResults.Add(
                    new NoiseEmissionResult(70.0f + j + (i * 0.1f), 40f + j + (i * 0.1f), 40 + j + (i * 10), "22:0" + j.ToString(), "22:1" + j.ToString()));
                measurementPoint.nightNoiseEmissionMeasurement.residualResults.Add(
                    new NoiseEmissionResult(65.0f + j + (i * 0.1f), 35f + j + (i * 0.1f), 50 + j + (i * 10), "22:2" + j.ToString(), "22:3" + j.ToString()));
            }

            measurementPoint.dayEnvironmentalNoiseMeasurement.environmentalNoiseResult = new EnvironmentalNoiseResult(
                60f + j + 0.1f, 20 + j, 60f + j + 0.2f, 30 + j, 60f + j + 0.3f, 40 + j, 60f + j + 0.4f, 50 + j, 60f + j + 0.5f, 60 + j, "08:00", "09:00" );
            measurementPoint.nightEnvironmentalNoiseMeasurement.environmentalNoiseResult = new EnvironmentalNoiseResult(
                40f + j + 0.1f, 20 + j, 40f + j + 0.2f, 30 + j, 40f + j + 0.3f, 40 + j, 40f + j + 0.4f, 50 + j, 40f + j + 0.5f, 60 + j, "22:00", "23:00");



        }

        Measurement.description = "Texto con descripción de las condiciones y procesos de medición";

        Measurement.externalEvents = new List<ExternalEvent>
        {
            new ExternalEvent("Avión", 75.6f, "21:45", 5f),
            new ExternalEvent("Automóvil", 81.4f, "21:55", 1f),
            new ExternalEvent("Perro", 71.7f, "22:07", 0.5f)
        };

        Measurement.picturePath = "/storage/emulated/0/DCIM/Camera/IMG_20210808_120222.JPG";

        Measurement.sourceInformation = "Texto con descripción de la relación entre la fuente de ruido y el receptor, superficies, geometrías y métodos de control existentes";
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
