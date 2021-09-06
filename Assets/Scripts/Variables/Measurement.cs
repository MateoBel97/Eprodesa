using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Measurement
{
    // General Data
    public static string companyName { get; set; }
    public static int day { get; set; }
    public static int month { get; set; }
    public static int year { get; set; }
    public static string workOrder { get; set; }


    // Measurement Points Data

    public static int measurementPointBeingUpdated { get; set; }
    public static int timeFrameBeingUpdated { get; set; }
    public static List<MeasurementPoint> measurementPoints { get; set; }

    //MetConditions

    public static int metConditionsBeingUpdated { get; set; } // 0 = Day; 1 = Night;
    public static bool usingDayMetConditions { get; set; }
    public static MetConditions dayMetConditions { get; set; }
    public static bool usingNightMetConditions { get; set; }
    public static MetConditions nightMetConditions { get; set; }

    // TechnicalInfo
    public enum TypeOfMeasurement
    {
        NoiseEmission = 0,
        EnvironmentalNoise = 1,
        LiteralG = 2
    };
    public static TypeOfMeasurement typeOfMeasurement { get; set; }
    public static bool measuringDay { get; set; }
    public static bool measuringNight { get; set; }
    public static string soundMeterSerialNumber { get; set; }
    public static string calibartorSerialNumber { get; set; }
    public static string metStationSerialNumber { get; set; }


    //
    public static string description { get; set; }

    //
    public static List<ExternalEvent> externalEvents { get; set; }

    //
    public static string picturePath { get; set; }
    public static byte[] picturePixels { get; set; }

    //
    public static string sourceInformation { get; set; }

    public static void ResetValues()
    {
        companyName = null;
        day = 1;
        month = 1;
        year = 2019;
        workOrder = null;

        measurementPoints = new List<MeasurementPoint> { };

        usingDayMetConditions = false;
        dayMetConditions = new MetConditions();
        usingNightMetConditions = false;
        nightMetConditions = new MetConditions();

        typeOfMeasurement = TypeOfMeasurement.NoiseEmission;
        measuringDay = false;
        measuringNight = false;

        soundMeterSerialNumber = null;
        calibartorSerialNumber = null;
        metStationSerialNumber = null;

        description = null;

        externalEvents = new List<ExternalEvent> { };

        picturePath = null;
        picturePixels = null;

        sourceInformation = null;

    }

    public static void UpdateParameter(string variable, bool newValue, int point = 0, int measurement = 0)
    {
        switch (variable)
        {
            case "usingMetConditions":
                Debug.Log("Updating Met Conditions");
                if (metConditionsBeingUpdated == 0)
                {
                    usingDayMetConditions = newValue;
                }
                else
                {
                    usingNightMetConditions = newValue;
                }
                break;
                
            case "measuringDay":
                measuringDay = newValue;
                break;
            case "measuringNight":
                measuringNight = newValue;
                break;
            case "emissionMeasured":
                break;
            default:
                Debug.Log("NO VARIABLE UPDATED");
                break;
        }
    }
    public static bool GetParameter(string variable, bool type, int index = 0)
    {
        bool value = false;
        switch (variable)
        {
            case "usingMetConditions":
                value = (metConditionsBeingUpdated == 0 ? usingDayMetConditions : usingNightMetConditions);
                break;
            case "measuringDay":
                value = measuringDay;
                break;
            case "measuringNight":
                value = measuringNight;
                break;
            default:
                value = false;
                break;
        }
        return value;
    }

    private static int i;
    // Int Values
    public static void UpdateParameter(string variable, int newValue, int index = 0)
    {
        Debug.Log("About to update " + variable);
        switch (variable)
        {
            case "day":
                day = newValue;
                break;
            case "month":
                month = newValue;
                break;
            case "year":
                year = newValue;
                break;
            case "metConditionsBeingUpdated":
                metConditionsBeingUpdated = newValue;
                break;
            case "typeOfMeasurement":
                switch(newValue)
                {
                    case 0: typeOfMeasurement = TypeOfMeasurement.NoiseEmission;
                        break;
                    case 1: typeOfMeasurement = TypeOfMeasurement.EnvironmentalNoise;
                        break;
                    case 2: typeOfMeasurement = TypeOfMeasurement.LiteralG;
                        break;
                }
                break;
            case "measurementPoints":
                measurementPointBeingUpdated = newValue;
                break;
            case "timeFrameBeingUpdated":
                timeFrameBeingUpdated = newValue;
                Debug.Log("MP " + measurementPointBeingUpdated + ": " + newValue);
                break;
            case "dayEmission.fileNumber":
                if(measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].fileNumber = newValue;

                }
                break;
            case "dayResidual.fileNumber":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].fileNumber = newValue;
                }
                break;
            case "nightEmission.fileNumber":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].fileNumber = newValue;
                }

                break;
            case "nightResidual.fileNumber":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].fileNumber = newValue;
                }
                break;
            default:
                Debug.Log("NO VARIABLE UPDATED");
                break;
        }
    }
    public static int GetParameter(string variable, int type, int index = 0)
    {
        int value = 0;
        switch (variable)
        {
            case "day":
                value = day;
                break;
            case "month":
                value = month;
                break;
            case "year":
                value = year;
                break;
            case "metConditionsBeingUpdated":
                value = metConditionsBeingUpdated;
                break;
            case "typeOfMeasurement":
                switch(typeOfMeasurement)
                {
                    case TypeOfMeasurement.NoiseEmission:
                        value = 0;
                            break;
                    case TypeOfMeasurement.EnvironmentalNoise:
                        value = 1;
                        break;
                    case TypeOfMeasurement.LiteralG:
                        value = 2;
                        break;
                }
                break;
            case "measurementPoints":
                value = measurementPoints.Count;
                break;
            case "timeFrameBeingUpdated":
                value = timeFrameBeingUpdated;
                break;
            case "dayEmission.fileNumber":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0 :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].fileNumber);
                break;
            case "dayResidual.fileNumber":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count == 0 ? 0 :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].fileNumber);
                break;
            case "nightEmission.fileNumber":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0 :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].fileNumber);
                break;
            case "nightResidual.fileNumber":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count == 0 ? 0 :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].fileNumber);
                break;
            case "dayEmission.Num":

                value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count;
                switch (value)
                {
                    case 1:
                        value = 0;
                        break;
                    case 3:
                        value = 1;
                        break;
                    case 5:
                        value = 2;
                        break;
                }
                break;
            case "dayResidual.Num":

                value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count;
                switch (value)
                {
                    case 0:
                        value = 0;
                        break;
                    case 1:
                        value = 1;
                        break;
                    case 3:
                        value = 2;
                        break;
                    case 5:
                        value = 3;
                        break;
                }
                break;
            case "nightEmission.Num":
                
                value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count;
                switch (value)
                {
                    case 1:
                        value = 0;
                        break;
                    case 3:
                        value = 1;
                        break;
                    case 5:
                        value = 2;
                        break;
                }
                Debug.Log("Returning " + value + " as numResults");
                break;
            case "nightResidual.Num":
                value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count;
                switch (value)
                {
                    case 0:
                        value = 0;
                        break;
                    case 1:
                        value = 1;
                        break;
                    case 3:
                        value = 2;
                        break;
                    case 5:
                        value = 3;
                        break;
                }
                break;
            default:
                value = 0;
                break;
        }
        return value;
    }

    // Float Values
    public static void UpdateParameter(string variable, float newValue, int index = 0)
    {
        switch (variable)
        {
            case "initialWindSpeed":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.initialWindSpeed = newValue;
                else
                    nightMetConditions.initialWindSpeed = newValue;
                break;
            case "finalWindSpeed":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.finalWindSpeed = newValue;
                else
                    nightMetConditions.finalWindSpeed = newValue;
                break;
            case "initialTemperature":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.initialTemperature = newValue;
                else
                    nightMetConditions.initialTemperature = newValue;
                break;
            case "finalTemperature":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.finalTemperature = newValue;
                else
                    nightMetConditions.finalTemperature = newValue;
                break;
            case "initialHumidity":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.initialHumidity = newValue;
                else
                    nightMetConditions.initialHumidity = newValue;
                break;
            case "finalHumidity":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.finalHumidity = newValue;
                else
                    nightMetConditions.finalHumidity = newValue;
                break;
            case "initialAtmPressure":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.initialAtmPressure = newValue;
                else
                    nightMetConditions.initialAtmPressure = newValue;
                break;
            case "finalAtmPressure":
                if (metConditionsBeingUpdated == 0)
                    dayMetConditions.finalAtmPressure = newValue;
                else
                    nightMetConditions.finalAtmPressure = newValue;
                break;
            case "dayEmission.LAEQ":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].laeq = newValue;
                }
                break;
            case "dayEmission.L90":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].l90 = newValue;
                }
                break;
            case "dayResidual.LAEQ":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].laeq = newValue;
                }
                break;
            case "dayResidual.L90":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].l90 = newValue;
                }
                break;
            case "nightEmission.LAEQ":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].laeq = newValue;
                }
                break;
            case "nightEmission.L90":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].l90 = newValue;
                }
                break;
            case "nightResidual.LAEQ":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].laeq = newValue;
                }
                break;
            case "nightResidual.L90":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].l90 = newValue;
                }
                break;
            case "externalEvents.level":
                if (index != -1)
                    externalEvents[index].level = newValue;
                break;
            case "externalEvents.length":
                if (index != -1)
                    externalEvents[index].length = newValue;
                break;
            default:
                Debug.Log("NO VARIABLE UPDATED");
                break;
        }
    }
    public static float GetParameter(string variable, float type, int index = 0)
    {
        float value = 0.0f;
        switch (variable)
        {
            case "initialWindSpeed":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.initialWindSpeed : nightMetConditions.initialWindSpeed);
                break;
            case "finalWindSpeed":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.finalWindSpeed : nightMetConditions.finalWindSpeed);
                break;
            case "initialTemperature":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.initialTemperature : nightMetConditions.initialTemperature);
                break;
            case "finalTemperature":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.finalTemperature : nightMetConditions.finalTemperature);
                break;
            case "initialHumidity":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.initialHumidity : nightMetConditions.initialHumidity);
                break;
            case "finalHumidity":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.finalHumidity : nightMetConditions.finalHumidity);
                break;
            case "initialAtmPressure":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.initialAtmPressure : nightMetConditions.initialAtmPressure);
                break;
            case "finalAtmPressure":
                value = (metConditionsBeingUpdated == 0 ? dayMetConditions.finalAtmPressure : nightMetConditions.finalAtmPressure);
                break;
            case "dayEmission.LAEQ":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].laeq);
                break;
            case "dayEmission.L90":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].l90);
                break;
            case "dayResidual.LAEQ":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].laeq);
                break;
            case "dayResidual.L90":
                i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].l90);
                break;
            case "nightEmission.LAEQ":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].laeq);
                break;
            case "nightEmission.L90":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].l90);
                break;
            case "nightResidual.LAEQ":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].laeq);
                Debug.Log("Returning LAEQ: " + value);
                break;
            case "nightResidual.L90":
                i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                value = (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count == 0 ? 0.0f :
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].l90);
                Debug.Log("Returning L90: " + value);
                break;
            case "externalEvents.level":

                value = (index == -1 ? 0f : externalEvents[index].level);
                Debug.Log("Showing Level at " + index.ToString() + ": " + externalEvents[index].level);
                break;
            case "externalEvents.length":
                value = (index == -1 ? 0f : externalEvents[index].length);
                Debug.Log("Showing Length at " + index.ToString() + ": " + externalEvents[index].length);
                break;
            default:
                value = 0;
                break;
        }
        return value;
    }

    // String Values
    public static void UpdateParameter(string variable, string newValue, int index = 0)
    {
        switch (variable)
        {
            case "companyName":
                companyName = newValue;
                break;
            case "workOrder":
                workOrder = newValue;
                break;
            case "measurementPoints.name":
                if (index != -1)
                    measurementPoints[index].name = newValue;
                break;
            case "measurementPoints.n":
                if (index != -1)
                    measurementPoints[index].n = newValue;
                break;
            case "measurementPoints.w":
                if (index != -1)
                    measurementPoints[index].w = newValue;
                break;
            case "soundMeterSerialNumber":
                soundMeterSerialNumber = newValue;
                break;
            case "calibartorSerialNumber":
                calibartorSerialNumber = newValue;
                break;
            case "metStationSerialNumber":
                metStationSerialNumber = newValue;
                break;
            case "dayEmission.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].initialTime = newValue;
                }
                break;
            case "dayEmission.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].finalTime = newValue;
                }
                break;
            case "dayResidual.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].initialTime = newValue;
                }
                break;
            case "dayResidual.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].finalTime = newValue;
                }
                break;
            case "nightEmission.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].initialTime = newValue;
                }
                break;
            case "nightEmission.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].finalTime = newValue;
                }
                break;
            case "nightResidual.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].initialTime = newValue;
                }
                break;
            case "nightResidual.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].finalTime = newValue;
                }
                break;
            case "description":
                description = newValue;
                break;
            case "externalEvents.name":
                if (index != -1)
                    externalEvents[index].name = newValue;
                break;
            case "externalEvents.time":
                if (index != -1)
                    externalEvents[index].time = newValue;
                break;
            default:
                Debug.Log("NO VARIABLE UPDATED");
                break;
        }
    }
    public static string GetParameter(string variable, string type, int index = 0) 
    {
        string value = null;
        switch (variable)
        {
            case "companyName":
                value = companyName;
                break;
            case "workOrder":
                value = workOrder;
                break;
            case "measurementPoints.name":
                value = (index == -1 ? "" : measurementPoints[index].name);
                break;
            case "measurementPoints.n":
                value = (index == -1 ? "" : measurementPoints[index].n);
                break;
            case "measurementPoints.w":
                value = (index == -1 ? "" : measurementPoints[index].w);
                break;
            case "soundMeterSerialNumber":
                value = soundMeterSerialNumber;
                break;
            case "calibartorSerialNumber":
                value = calibartorSerialNumber;
                break;
            case "metStationSerialNumber":
                value = metStationSerialNumber;
                break;
            case "measurementPoints":
                value = measurementPoints[index].name;
                break;
            case "dayEmission.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].initialTime;
                }
                break;
            case "dayEmission.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults[i].finalTime;
                }
                break;
            case "dayResidual.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].initialTime;
                }
                break;
            case "dayResidual.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults[i].finalTime;
                }
                break;
            case "nightEmission.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].initialTime;
                }
                break;
            case "nightEmission.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults[i].finalTime;
                }
                break;
            case "nightResidual.initialTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].initialTime;
                }
                break;
            case "nightResidual.finalTime":
                if (measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count != 0)
                {
                    i = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                    value = measurementPoints[measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults[i].finalTime;
                }
                break;
            case "description":
                value = description;
                break;
            case "externalEvents.name":
                value = (index == -1 ? "" : externalEvents[index].name);
                break;
            case "externalEvents.time":
                value = (index == -1 ? "" : externalEvents[index].time);
                break;
            default:
                value = "";
                break;
        }
        return value;
    }
}

