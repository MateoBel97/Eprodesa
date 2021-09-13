using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class FilesReader : MonoBehaviour
{
    [SerializeField] GameObject buttonTemplate;
    [SerializeField] GameObject scrollviewContent;
    [SerializeField] TextMeshProUGUI infoTMP;
    [SerializeField] TextMeshProUGUI deleteTMP;
    [SerializeField] GameObject[] gameObjects;
     
    FileInfo[] fileInfo;

    string[] pageSplit = new string[] { "+*page*+" };
    string[] rowSplit = new string[] { "+*row*+" };
    string[] columnSplit = new string[] { "+*column*+" };
    string[] pointSplit = new string[] { "+*point*+" };
    string[] timeFrameSplit = new string[] { "+*timeFrame*+" };
    string[] paramSplit = new string[] { "+*param*+" };


    string chosenFilePath, picturePath;
    string fileName = "";
    string info;

    char oldCharacter, newCharacter;

    bool deleteButtonPressed = false;

    void Start()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            oldCharacter = '.';
            newCharacter = ',';
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            oldCharacter = ',';
            newCharacter = '.';
        }
        Debug.Log("Will replace " + oldCharacter + " with " + newCharacter);
        deleteTMP.enabled = false;
        //infoTMP.text = "";
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
        GetFilesInDirectory();
        ShowFoldersInScrollView();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void GetFilesInDirectory()
    {
#if UNITY_EDITOR_WIN
        chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
#elif UNITY_STANDALONE_WIN
        chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
#elif UNITY_ANDROID
        chosenFilePath =  Application.persistentDataPath;
#endif

        DirectoryInfo directoryInfo = new DirectoryInfo(chosenFilePath);
        fileInfo = directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories);

    }

    void ShowFoldersInScrollView()
    {
        foreach (FileInfo file in fileInfo)
        {
            if ((!file.Name.Equals("-.txt"))&&(!file.Name.Equals(".txt")))
            {
                var copy = Instantiate(buttonTemplate);
                copy.transform.SetParent(scrollviewContent.transform);
                copy.transform.localScale = new Vector3(1f, 1f, 1f);
                copy.GetComponentInChildren<TextMeshProUGUI>().text = file.Name;
                copy.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        string path = file.FullName;
                        GetFileInfo(path);

                    }
                    );
            }
        }
    }

    void GetFileInfo(string filePath)
    {
        chosenFilePath = filePath;
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
        deleteButtonPressed = false;
        deleteTMP.enabled = false;
        info = File.ReadAllText(filePath);
        
        if (!info.Equals(""))
        {
            string general = info.Split(pageSplit, System.StringSplitOptions.None)[0];
            ShowGeneralInfo(general);
        }
        else
        {
            Debug.Log("Nada guardado");
        }
    }

    void ShowGeneralInfo(string general)
    {
        string company = general.Split(columnSplit, System.StringSplitOptions.None)[0];
        string day = general.Split(columnSplit, System.StringSplitOptions.None)[1];
        string month = general.Split(columnSplit, System.StringSplitOptions.None)[2];
        string year = general.Split(columnSplit, System.StringSplitOptions.None)[3];
        string workOrder = general.Split(columnSplit, System.StringSplitOptions.None)[4];
        picturePath = PageSplit(info)[10];

        fileName = company + "-" + workOrder;

        infoTMP.text = "Empresa: " + company + "\nFecha: " + day + "-" + month + "-" + year + "\nOrden de trabajo: " + workOrder;
    }

    public void ExportFile()
    {
#if UNITY_EDITOR_WIN
        string newPath = EditorUtility.OpenFolderPanel("Seleccionar carpeta", "C:/Users/Public/Documents/Formatos de Campo", "");
        File.WriteAllText(chosenFilePath + "/" + fileName + ".txt", info);
#elif UNITY_STANDALONE_WIN
        string newPath = EditorUtility.OpenFolderPanel("Seleccionar carpeta", "C:/Users/Public/Documents/Formatos de Campo", "");
        File.WriteAllText(chosenFilePath + "/" + fileName + ".txt", info);
#elif UNITY_ANDROID
        //chosenFilePath = Application.persistentDataPath + "/" + Measurement.companyName + "-" + Measurement.workOrder + ".txt";
        //File.WriteAllText(chosenFilePath, info);
        //textTMP.text = chosenFilePath;
        StartCoroutine(ShareFiles());
#endif

    }

    private IEnumerator ShareFiles()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare().AddFile(chosenFilePath).AddFile(picturePath)
            .SetSubject("FORMATO DE CAMPO " + fileName).SetText("Formato de Campo generado mediante la app móvil.\n\n").SetUrl("https://www.eprodesaong.com/")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public void DeleteFile()
    {
        if (!deleteButtonPressed)
        {
            if (!fileName.Equals(""))
            {
                //Debug.Log("Seguro que desea eliminar?");

                deleteTMP.enabled = true;
                deleteButtonPressed = true;
            }
            else
            {
                //Debug.Log("No File Selected");
            }

        }
        else
        {
            chosenFilePath = "no path";
#if UNITY_EDITOR_WIN
            chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
#elif UNITY_STANDALONE_WIN
        chosenFilePath = "C:/Users/Public/Documents/Formatos de Campo";
#elif UNITY_ANDROID
        chosenFilePath =  Application.persistentDataPath;
#endif

            File.Delete(chosenFilePath + "/" + fileName + ".txt");
            SceneManager.LoadScene("FilesScene");
        }
    }

    string[] PageSplit(string textToSplit)
    {
        return textToSplit.Split(pageSplit, System.StringSplitOptions.None);
    }
    string[] RowSplit(string textToSplit)
    {
        return textToSplit.Split(rowSplit, System.StringSplitOptions.None);
    }
    string[] ColumnSplit(string textToSplit)
    {
        return textToSplit.Split(columnSplit, System.StringSplitOptions.None);
    }
    string[] PointSplit(string textToSplit)
    {
        return textToSplit.Split(pointSplit, System.StringSplitOptions.None);
    }
    string[] TimeFrameSplit(string textToSplit)
    {
        return textToSplit.Split(timeFrameSplit, System.StringSplitOptions.None);
    }
    string[] ParamSplit(string textToSplit)
    {
        return textToSplit.Split(paramSplit, System.StringSplitOptions.None);
    }

    public void LoadInfo()
    {
        //
        string general = PageSplit(info)[0];
        LoadGeneralData(general);

        //
        string measurementPointsData = PageSplit(info)[1];
        LoadMeasurementPointsData(measurementPointsData);

        //
        string dayWeatherInfo = PageSplit(info)[2];
        LoadDayWeatherData(dayWeatherInfo);

        //
        string nigthWeatherInfo = PageSplit(info)[3];
        LoadNightWeatherData(nigthWeatherInfo);

        
        //
        string technicalInfo = PageSplit(info)[4];
        LoadTechnicalData(technicalInfo);

        
        
        string emissionInfo = PageSplit(info)[5];
        LoadEmissionData(emissionInfo);
        
        
        string noiseInfo = PageSplit(info)[6];
        LoadNoiseData(noiseInfo);
        
        
        string description = PageSplit(info)[8];
        LoadDescriptionData(description);

        
        string eventsPointsData = PageSplit(info)[9];
        LoadEventsData(eventsPointsData);

        
        string picturePath = info.Split(pageSplit, System.StringSplitOptions.None)[10];
        LoadPictureData(picturePath);

        
        string source = info.Split(pageSplit, System.StringSplitOptions.None)[11];
        LoadSourceData(source);
        
        SceneManager.LoadScene("GeneralDataScene");
    }
    void LoadGeneralData(string general)
    {
        Measurement.companyName = general.Split(columnSplit, System.StringSplitOptions.None)[0];
        Measurement.day = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[1]);
        Measurement.month = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[2]);
        Measurement.year = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[3]);
        Measurement.workOrder = general.Split(columnSplit, System.StringSplitOptions.None)[4];
        Measurement.measurementPointBeingUpdated = 0;
        Measurement.timeFrameBeingUpdated = 0;
        Measurement.measurementPointBeingUpdated = 0;
    }
    void LoadMeasurementPointsData(string measurementPointsData)
    {
        string[] measurementPoints = measurementPointsData.Split(rowSplit, System.StringSplitOptions.None);
        foreach(string point in measurementPoints)
        {
            string[] pointInfo = point.Split(columnSplit, System.StringSplitOptions.None);
            if(point.Length != 1)
            {
                Measurement.measurementPoints.Add(new MeasurementPoint(
                    pointInfo[0], pointInfo[1], pointInfo[2]));
            }
            else
            {
                Measurement.measurementPoints.Add(new MeasurementPoint("", "", ""));
            }
        }
    }
    void LoadDayWeatherData(string dayWeatherInfo)
    {
        string[] metConditions = dayWeatherInfo.Split(rowSplit, System.StringSplitOptions.None);
        string[] windSpeed = metConditions[0].Split(columnSplit, System.StringSplitOptions.None);
        
        Measurement.usingDayMetConditions = (!windSpeed[0].Equals("-"));
        if(Measurement.usingDayMetConditions)
        {
            Measurement.dayMetConditions.initialWindSpeed = float.Parse(windSpeed[0]);
            Measurement.dayMetConditions.finalWindSpeed = float.Parse(windSpeed[1]);
            string[] temperature = metConditions[1].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.dayMetConditions.initialTemperature = float.Parse(temperature[0]);
            Measurement.dayMetConditions.finalTemperature = float.Parse(temperature[1]);
            string[] humidity = metConditions[2].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.dayMetConditions.initialHumidity = float.Parse(humidity[0]);
            Measurement.dayMetConditions.finalHumidity = float.Parse(humidity[1]);
            string[] atmPressure = metConditions[3].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.dayMetConditions.initialAtmPressure = float.Parse(atmPressure[0]);
            Measurement.dayMetConditions.finalAtmPressure = float.Parse(atmPressure[1]);
        }
        else
        {
            Measurement.dayMetConditions = new MetConditions();
        }
    }
    void LoadNightWeatherData(string nigthWeatherInfo)
    {
        string[] metConditions = nigthWeatherInfo.Split(rowSplit, System.StringSplitOptions.None);
        foreach (string value in metConditions) value.Replace(oldCharacter, newCharacter);
        string[] windSpeed = metConditions[0].Split(columnSplit, System.StringSplitOptions.None);
        Measurement.usingNightMetConditions = (!windSpeed[0].Equals("-"));
        if (Measurement.usingNightMetConditions)
        {
            Measurement.nightMetConditions.initialWindSpeed = float.Parse(windSpeed[0]);
            Measurement.nightMetConditions.finalWindSpeed = float.Parse(windSpeed[1]);
            string[] temperature = metConditions[1].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.nightMetConditions.initialTemperature = float.Parse(temperature[0]);
            Measurement.nightMetConditions.finalTemperature = float.Parse(temperature[1]);
            string[] humidity = metConditions[2].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.nightMetConditions.initialHumidity = float.Parse(humidity[0]);
            Measurement.nightMetConditions.finalHumidity = float.Parse(humidity[1]);
            string[] atmPressure = metConditions[3].Split(columnSplit, System.StringSplitOptions.None);
            Measurement.nightMetConditions.initialAtmPressure = float.Parse(atmPressure[0]);
            Measurement.nightMetConditions.finalAtmPressure = float.Parse(atmPressure[1]);
        }
        else
        {
            Measurement.nightMetConditions = new MetConditions();
        }
    }
    void LoadTechnicalData(string technicalInfo)
    {
        /*
        GlobalData.technicalInfoDataSaved = true;
        string typeOfMeasurement = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[0];
        GlobalData.isNoiseEmissionMeasurement = bool.Parse(typeOfMeasurement.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isEnvironmentalNoiseMeasurement = bool.Parse(typeOfMeasurement.Split(columnSplit, System.StringSplitOptions.None)[1]);
        string parameters = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[1];
        GlobalData.isEmissionParameter = bool.Parse(parameters.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isResidualParameter = bool.Parse(parameters.Split(columnSplit, System.StringSplitOptions.None)[1]);
        string timeFrame = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[2];
        GlobalData.isDayTimeFrame = bool.Parse(timeFrame.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isNightTimeFrame = bool.Parse(timeFrame.Split(columnSplit, System.StringSplitOptions.None)[1]);
        */
        string[] tech = technicalInfo.Split(rowSplit, System.StringSplitOptions.None);
        if (tech[0].Equals("X"))
            Measurement.typeOfMeasurement = Measurement.TypeOfMeasurement.NoiseEmission;
        else if (tech[1].Equals("X"))
            Measurement.typeOfMeasurement = Measurement.TypeOfMeasurement.EnvironmentalNoise;
        else
            Measurement.typeOfMeasurement = Measurement.TypeOfMeasurement.LiteralG;
        //5 6
        Measurement.measuringDay = (tech[5].Equals("X"));
        Measurement.measuringNight = (tech[6].Equals("X"));
        Measurement.soundMeterSerialNumber = tech[7];
        Measurement.calibartorSerialNumber = tech[8];
        Measurement.metStationSerialNumber = tech[9];

    }
    void LoadEmissionData(string noiseEmissionInfo)
    {
        if (noiseEmissionInfo.Length != 0)
        {
            string[] pointsInfo = PointSplit(noiseEmissionInfo);
            for (int point = 0; point < pointsInfo.Length; point++)
            {
                string[] timeFrameInfo = TimeFrameSplit(pointsInfo[point]);
                if(timeFrameInfo[0].Length != 0)
                {
                    string[] dayNoiseEmissionInfo = ParamSplit(timeFrameInfo[0]);
                    string[] emissionInfo = RowSplit(dayNoiseEmissionInfo[0]);
                    Measurement.measurementPoints[point].dayNoiseEmissionMeasurement.emissionResults.Clear();
                    foreach (string emissionResult in emissionInfo)
                    {
                        string[] values = ColumnSplit(emissionResult);
                        Measurement.measurementPoints[point].dayNoiseEmissionMeasurement.emissionResults.Add(
                            new NoiseEmissionResult(values));
                    }
                    if (dayNoiseEmissionInfo.Length == 2)
                    {
                        string[] residualInfo = RowSplit(dayNoiseEmissionInfo[1]);
                        foreach (string residualResult in residualInfo)
                        {
                            string[] values = ColumnSplit(residualResult);
                            Measurement.measurementPoints[point].dayNoiseEmissionMeasurement.residualResults.Add(
                                new NoiseEmissionResult(values));
                        }
                    }
                }
                if (timeFrameInfo[1].Length != 0)
                {
                    string[] nightNoiseEmissionInfo = ParamSplit(timeFrameInfo[1]);
                    string[] emissionInfo = RowSplit(nightNoiseEmissionInfo[0]);
                    Measurement.measurementPoints[point].nightNoiseEmissionMeasurement.emissionResults.Clear();
                    foreach (string emissionResult in emissionInfo)
                    {
                        string[] values = ColumnSplit(emissionResult);
                        Measurement.measurementPoints[point].nightNoiseEmissionMeasurement.emissionResults.Add(
                            new NoiseEmissionResult(values));
                    }
                    if (nightNoiseEmissionInfo.Length == 2)
                    {
                        string[] residualInfo = RowSplit(nightNoiseEmissionInfo[1]);
                        foreach (string residualResult in residualInfo)
                        {
                            string[] values = ColumnSplit(residualResult);
                            Measurement.measurementPoints[point].nightNoiseEmissionMeasurement.residualResults.Add(
                                new NoiseEmissionResult(values));
                        }
                    }
                }
            }
        }
    }
    void LoadNoiseData(string noiseInfo)
    {
        
        if(noiseInfo.Length != 0)
        {
            string[] pointsInfo = PointSplit(noiseInfo);
            foreach (string value in pointsInfo) value.Replace(oldCharacter, newCharacter);
            for (int point = 0; point < pointsInfo.Length; point++)
            {
                string[] timeFrameInfo = TimeFrameSplit(pointsInfo[point]);
                if (timeFrameInfo[0].Length != 0)
                {
                    string[] values = ColumnSplit(timeFrameInfo[0]);
                    Measurement.measurementPoints[point].dayEnvironmentalNoiseMeasurement.environmentalNoiseResult =
                        new EnvironmentalNoiseResult(values);
                }
                if (timeFrameInfo[1].Length != 0)
                {
                    string[] values = ColumnSplit(timeFrameInfo[1]);
                    Measurement.measurementPoints[point].nightEnvironmentalNoiseMeasurement.environmentalNoiseResult =
                        new EnvironmentalNoiseResult(values);
                }
            }
        }
    }
    void LoadDescriptionData(string description)
    {
        Measurement.description = description;
    }
    void LoadEventsData(string eventsData)
    {
        string[] externalEvents = RowSplit(eventsData);//measurementPointsData.Split(rowSplit, System.StringSplitOptions.None);
        foreach (string externalEvent in externalEvents)
        {
            string[] eventInfo = ColumnSplit(externalEvent);//point.Split(columnSplit, System.StringSplitOptions.None);
             
            if (eventInfo.Length != 1)
            {
                Measurement.externalEvents.Add(new ExternalEvent(eventInfo));
            }
        }
    }
    void LoadPictureData(string picturePath)
    {
        Measurement.picturePath = picturePath;
    }
    void LoadSourceData(string source)
    {
        Measurement.sourceInformation = source;
    }
}
