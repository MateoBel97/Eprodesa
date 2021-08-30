using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class GlobalData
{
    // GeneralData
    public static bool generalDataSaved;
    public static string companyName;
    public static int day;
    public static int month;
    public static int year;
    public static string workOrder;

    // MeasurementPointsData
    public static bool measurementPointsDataSaved;
    public static List<string> pointName = new List<string> { };
    public static List<string> n = new List<string> { };
    public static List<string> w = new List<string> { };

    // WeatherData

    public static bool weatherDataSaved;
    public static bool dayTimeFrame;
    public static bool nightTimeFrame;
    public static double initialWindSpeed_D, finalWindSpeed_D;
    public static double initialTemperature_D, finalTemperature_D;
    public static double initialHumidity_D, finalHumidity_D;
    public static double initialAtmPressure_D, finalAtmPressure_D;

    public static double initialWindSpeed_N, finalWindSpeed_N;
    public static double initialTemperature_N, finalTemperature_N;
    public static double initialHumidity_N, finalHumidity_N;
    public static double initialAtmPressure_N, finalAtmPressure_N;


    // TechnicalInfoData

    public static bool technicalInfoDataSaved;

    public static bool isNoiseEmissionMeasurement, isEnvironmentalNoiseMeasurement;
    public static bool isEmissionParameter, isResidualParameter;
    public static bool isDayTimeFrame, isNightTimeFrame;
    public static string soundMeterSerialNumber;
    public static string calibratorSerialNumber;
    public static string metStationSerialNumber;


    // EmissionResults
    public static bool emissionResultsSaved;
    public static List<string> emissionDescription = new List<string> { };
    public static List<float> emissionLASEQ = new List<float> { };
    public static List<float> emissionL90 = new List<float> { };
    public static List<int> emissionFileNumber = new List<int> { };
    public static List<string> emissionInitialTime = new List<string> { };
    public static List<string> emissionFinalTime = new List<string> { };


    // NoiseResults
    public static bool noiseResultsSaved;
    public static List<string> noiseDescription = new List<string> { };
    public static List<float> noiseLevelN = new List<float> { };
    public static List<int> noisefileNumberN = new List<int> { };
    public static List<float> noiseLevelW = new List<float> { };
    public static List<int> noisefileNumberW = new List<int> { };
    public static List<float> noiseLevelE = new List<float> { };
    public static List<int> noisefileNumberE = new List<int> { };
    public static List<float> noiseLevelS = new List<float> { };
    public static List<int> noisefileNumberS = new List<int> { };
    public static List<float> noiseLevelV = new List<float> { };
    public static List<int> noisefileNumberV = new List<int> { };
    public static List<string> noiseInitialTime = new List<string> { };
    public static List<string> noiseFinalTime = new List<string> { };



    // DescriptionData
    public static bool descriptionDataSaved;
    public static string description;

    // EventsData
    public static bool eventsDataSaved;
    public static List<string> eventName = new List<string> { };
    public static List<float> eventLevel = new List<float> { };
    public static List<string> eventTime = new List<string> { };
    public static List<float> eventLength = new List<float> { };


    // sourceData
    public static bool sourceDataSaved;
    public static string source;


    public static void ResetValues()
    {
        generalDataSaved = false;
        companyName = null;
        day = 0;
        month = 0;
        year = 0;
        workOrder = null;

        measurementPointsDataSaved = false;
        pointName.Clear();
        n.Clear();
        w.Clear();
    

        weatherDataSaved = false;
        dayTimeFrame = false;
        nightTimeFrame = false;
        initialWindSpeed_D = 0.0f;
        finalWindSpeed_D = 0.0f;
        initialTemperature_D = 0.0f;
        finalTemperature_D = 0.0f;
        initialHumidity_D = 0.0f;
        finalHumidity_D = 0.0f;
        initialAtmPressure_D = 0.0f;
        finalAtmPressure_D = 0.0f;
        initialWindSpeed_N = 0.0f;
        finalWindSpeed_N = 0.0f;
        initialTemperature_N = 0.0f;
        finalTemperature_N = 0.0f;
        initialHumidity_N = 0.0f;
        finalHumidity_N = 0.0f;
        initialAtmPressure_N = 0.0f;
        finalAtmPressure_N = 0.0f;

        technicalInfoDataSaved = false;
        isNoiseEmissionMeasurement = false;
        isEnvironmentalNoiseMeasurement = false;
        isEmissionParameter = false;
        isResidualParameter = false;
        isDayTimeFrame = false;
        isNightTimeFrame = false;
        soundMeterSerialNumber = null;
        calibratorSerialNumber = null;
        metStationSerialNumber = null;

        emissionResultsSaved = false;
        emissionDescription.Clear();
        emissionLASEQ.Clear();
        emissionL90.Clear();
        emissionFileNumber.Clear();
        emissionInitialTime.Clear();
        emissionFinalTime.Clear();

        noiseResultsSaved = false;
        noiseDescription.Clear();
        noiseLevelN.Clear();
        noisefileNumberN.Clear();
        noiseLevelW.Clear();
        noisefileNumberW.Clear();
        noiseLevelE.Clear();
        noisefileNumberE.Clear();
        noiseLevelS.Clear();
        noisefileNumberS.Clear();
        noiseLevelV.Clear();
        noisefileNumberV.Clear();
        noiseInitialTime.Clear();
        noiseFinalTime.Clear();

        descriptionDataSaved = false;
        description = null;
        
        eventsDataSaved = false;
        eventName.Clear();
        eventLevel.Clear();
        eventTime.Clear();
        eventLength.Clear();

        sourceDataSaved = false;
        source = null;
    }

    public static void SaveTxt()
    {
        string text = CreateTxt();

        string chosenFilePath;
#if UNITY_EDITOR_WIN
        chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
        File.WriteAllText(chosenFilePath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt", text);
#elif UNITY_STANDALONE_WIN
        chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
        File.WriteAllText(chosenFilePath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt", text);
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath;
        File.WriteAllText(chosenFilePath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt", text);
#endif

        text = null;
    }

    public static string CreateTxt()
    {
        /*
         * 01 General
         * 02 Georreferencia
         * 03 Meteorología Día
         * 04 Meteorología Noche
         * 05 Informacion Tecnica Mediciones
         * 06 # Seriales
         * 07 PR-33 Emisión de Rudi
         * 08 PR-44 Ruido ambiental
         * 09 Descripción de condiciones
         * 10 Eventos
         * 11 Foto
         * 12 Fuente y receptor
         */
        string pageSplit = "*-*-*";
        string rowSplit = "***";
        string columnSplit = "---";

        string myText = "";

        myText += companyName;
        myText += columnSplit;
        myText += day.ToString() + columnSplit + month.ToString() + columnSplit + year.ToString();
        myText += columnSplit;
        myText += workOrder;
        myText += pageSplit;


        for (int i = 0; i < pointName.Count; i++)
        {
            myText += pointName[i] + columnSplit + n[i] + columnSplit + w[i];
            if( i < pointName.Count - 1)
            {
                myText += rowSplit;
            }
        }
        myText += pageSplit;

        if(dayTimeFrame)
        {
            myText += initialWindSpeed_D + columnSplit + finalWindSpeed_D;
            myText += rowSplit;
            myText += initialTemperature_D + columnSplit + finalTemperature_D;
            myText += rowSplit;
            myText += initialHumidity_D + columnSplit + finalHumidity_D;
            myText += rowSplit;
            myText += initialAtmPressure_D + columnSplit + finalAtmPressure_D;
        }
        else
        {
            myText += "NO";
        }

        myText += pageSplit;

        if (nightTimeFrame)
        {
            myText += initialWindSpeed_N + columnSplit + finalWindSpeed_N;
            myText += rowSplit;
            myText += initialTemperature_N + columnSplit + finalTemperature_N;
            myText += rowSplit;
            myText += initialHumidity_N + columnSplit + finalHumidity_N;
            myText += rowSplit;
            myText += initialAtmPressure_N + columnSplit + finalAtmPressure_N;
        }
        else
        {
            myText += "NO";
        }

        myText += pageSplit;

        myText += isNoiseEmissionMeasurement.ToString() + columnSplit + isEnvironmentalNoiseMeasurement.ToString();
        myText += rowSplit;
        myText += isEmissionParameter.ToString() + columnSplit + isResidualParameter.ToString();
        myText += rowSplit;
        myText += isDayTimeFrame.ToString() + columnSplit + isNightTimeFrame.ToString();
        myText += pageSplit;


        myText += soundMeterSerialNumber + columnSplit + calibratorSerialNumber + columnSplit + metStationSerialNumber;
        myText += pageSplit;


        if(isNoiseEmissionMeasurement)
        {
            for (int i = 0; i < emissionDescription.Count; i++)
            {
                //myText += pointName[i] + columnSplit + n[i] + columnSplit + w[i];
                myText += emissionDescription[i];
                myText += columnSplit;
                myText += emissionLASEQ[i];
                myText += columnSplit;
                myText += emissionL90[i];
                myText += columnSplit;
                myText += emissionFileNumber[i];
                myText += columnSplit;
                myText += emissionInitialTime[i];
                myText += columnSplit;
                myText += emissionFinalTime[i];

                if (i < emissionDescription.Count - 1)
                {
                    myText += rowSplit;
                }
            }
        }
        else
        {
            myText += "NO";
        }
        myText += pageSplit;

        if (isEnvironmentalNoiseMeasurement)
        {
            for (int i = 0; i < noiseDescription.Count; i++)
            {
                //myText += pointName[i] + columnSplit + n[i] + columnSplit + w[i];
                myText += noiseDescription[i];
                myText += columnSplit;
                myText += noiseLevelN[i];
                myText += columnSplit;
                myText += noisefileNumberN[i];
                myText += columnSplit;
                myText += noiseLevelW[i];
                myText += columnSplit;
                myText += noisefileNumberW[i];
                myText += columnSplit;
                myText += noiseLevelE[i];
                myText += columnSplit;
                myText += noisefileNumberE[i];
                myText += columnSplit;
                myText += noiseLevelS[i];
                myText += columnSplit;
                myText += noisefileNumberS[i];
                myText += columnSplit;
                myText += noiseLevelV[i];
                myText += columnSplit;
                myText += noisefileNumberV[i];
                myText += columnSplit;
                myText += noiseInitialTime[i];
                myText += columnSplit;
                myText += noiseFinalTime[i];

                if (i < emissionDescription.Count - 1)
                {
                    myText += rowSplit;
                }
            }
        }
        else
        {
            myText += "NO";
        }
        myText += pageSplit;



        myText += description;
        myText += pageSplit;

        for (int i = 0; i < eventName.Count; i++)
        {
            //myText += pointName[i] + columnSplit + n[i] + columnSplit + w[i];
            myText += eventName[i];
            myText += columnSplit;
            myText += eventLevel[i];
            myText += columnSplit;
            myText += eventTime[i];
            myText += columnSplit;
            myText += eventLength[i];

            if (i < eventName.Count - 1)
            {
                myText += rowSplit;
            }
        }
        myText += pageSplit;

        //Picture
        myText += "NO";
        myText += pageSplit;

        myText += source;

        return myText;
    }
}





