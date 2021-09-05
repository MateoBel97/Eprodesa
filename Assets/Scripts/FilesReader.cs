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
    
    FileInfo[] fileInfo;

    string[] pageSplit = new string[] { "*-*-*" };
    string[] rowSplit = new string[] { "***" };
    string[] columnSplit = new string[] { "---" };


    string chosenFilePath;
    string fileName = "";
    string info;

    bool deleteButtonPressed = false;

    void Start()
    {
        deleteTMP.enabled = false;
        //infoTMP.text = "";
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
                        string chosenFilePath = file.FullName;
                        GetFileInfo(chosenFilePath);

                    }
                    );
            }
        }
    }

    void GetFileInfo(string filePath)
    {
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

        fileName = company + "-" + workOrder;

        infoTMP.text = "Empresa: " + company + "\nFecha: " + day + "-" + month + "-" + year + "\nOrden de trabajo: " + workOrder;
    }

    public void ExportFile()
    {
#if UNITY_EDITOR_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + fileName + ".txt", info);
#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + fileName + ".txt", info);
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath + "/" + fileName + ".txt";
        StartCoroutine(TakeScreenshotAndShare());
#endif

    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare().AddFile(chosenFilePath)//.AddFile("/path/to/another/file.txt")
            .SetSubject("FORMATO DE CAMPO " + GlobalData.companyName).SetText("Formato de Campo generado mediante la app móvil.\n\n").SetUrl("https://www.eprodesaong.com/")
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
                Debug.Log("Seguro que desea eliminar?");

                deleteTMP.enabled = true;
                deleteButtonPressed = true;
            }
            else
            {
                Debug.Log("No File Selected");
            }

        }
        else
        {
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

    public void LoadInfo()
    {
        GlobalData.ResetValues();

        //
        string general = info.Split(pageSplit, System.StringSplitOptions.None)[0];
        LoadGeneralData(general);

        //
        string measurementPointsData = info.Split(pageSplit, System.StringSplitOptions.None)[1];
        LoadMeasurementPointsData(measurementPointsData);

        //
        string dayWeatherInfo = info.Split(pageSplit, System.StringSplitOptions.None)[2];
        LoadDayWeatherData(dayWeatherInfo);

        //
        string nigthWeatherInfo = info.Split(pageSplit, System.StringSplitOptions.None)[3];
        LoadNightWeatherData(nigthWeatherInfo);

        //
        string technicalInfo = info.Split(pageSplit, System.StringSplitOptions.None)[4];
        LoadTechnicalData(technicalInfo);

        //
        string serialNumbers = info.Split(pageSplit, System.StringSplitOptions.None)[5];
        LoadSerialNumbersData(serialNumbers);

        //
        string emissionInfo = info.Split(pageSplit, System.StringSplitOptions.None)[6];
        LoadEmissionData(emissionInfo);
        
        //
        string noiseInfo = info.Split(pageSplit, System.StringSplitOptions.None)[7];
        LoadNoiseData(noiseInfo);
        
        //
        string description = info.Split(pageSplit, System.StringSplitOptions.None)[8];
        LoadDescriptionData(description);

        //
        string eventsPointsData = info.Split(pageSplit, System.StringSplitOptions.None)[9];
        LoadEventsData(eventsPointsData);

        //
        string picturePath = info.Split(pageSplit, System.StringSplitOptions.None)[10];
        LoadPictureData(picturePath);

        //
        string source = info.Split(pageSplit, System.StringSplitOptions.None)[11];
        LoadSourceData(source);
        
        SceneManager.LoadScene("GeneralDataScene");
    }

    void LoadGeneralData(string general)
    {
        GlobalData.generalDataSaved = true;
        GlobalData.companyName = general.Split(columnSplit, System.StringSplitOptions.None)[0];
        GlobalData.day = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[1]);
        GlobalData.month = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[2]);
        GlobalData.year = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[3]);
        GlobalData.workOrder = general.Split(columnSplit, System.StringSplitOptions.None)[4];
    }

    void LoadMeasurementPointsData(string measurementPointsData)
    {
        GlobalData.measurementPointsDataSaved = true;
        string[] measurementPoints = measurementPointsData.Split(rowSplit, System.StringSplitOptions.None);
        for (int i = 0; i < measurementPoints.Length; i++)
        {
            string[] pointInfo = measurementPoints[i].Split(columnSplit, System.StringSplitOptions.None);
            if (pointInfo.Length != 1)
            {
                GlobalData.pointName.Add(pointInfo[0]);
                GlobalData.n.Add(pointInfo[1]);
                GlobalData.w.Add(pointInfo[2]);
            }
            else
            {
                GlobalData.pointName.Add("");
                GlobalData.n.Add("");
                GlobalData.w.Add("");
            }
        }
    }

    void LoadDayWeatherData(string dayWeatherInfo)
    {
        GlobalData.weatherDataSaved = true;
        if (!dayWeatherInfo.Equals("NO"))
        {
            GlobalData.dayMetConditions = true;
            string windSpeed = dayWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[0];
            GlobalData.initialWindSpeed_D = double.Parse(windSpeed.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalWindSpeed_D = double.Parse(windSpeed.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string temperature = dayWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[1];
            GlobalData.initialTemperature_D = double.Parse(temperature.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalTemperature_D = double.Parse(temperature.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string humidity = dayWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[2];
            GlobalData.initialHumidity_D = double.Parse(humidity.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalHumidity_D = double.Parse(humidity.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string atmPressure = dayWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[3];
            GlobalData.initialAtmPressure_D = double.Parse(atmPressure.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalAtmPressure_D = double.Parse(atmPressure.Split(columnSplit, System.StringSplitOptions.None)[1]);
        }
        else
        {

        }
    }

    void LoadNightWeatherData(string nigthWeatherInfo)
    {
        if (!nigthWeatherInfo.Equals("NO"))
        {
            GlobalData.nightMetConditions = true;
            string windSpeed = nigthWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[0];
            GlobalData.initialWindSpeed_N = double.Parse(windSpeed.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalWindSpeed_N = double.Parse(windSpeed.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string temperature = nigthWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[1];
            GlobalData.initialTemperature_N = double.Parse(temperature.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalTemperature_N = double.Parse(temperature.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string humidity = nigthWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[2];
            GlobalData.initialHumidity_N = double.Parse(humidity.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalHumidity_N = double.Parse(humidity.Split(columnSplit, System.StringSplitOptions.None)[1]);
            string atmPressure = nigthWeatherInfo.Split(rowSplit, System.StringSplitOptions.None)[3];
            GlobalData.initialAtmPressure_N = double.Parse(atmPressure.Split(columnSplit, System.StringSplitOptions.None)[0]);
            GlobalData.finalAtmPressure_N = double.Parse(atmPressure.Split(columnSplit, System.StringSplitOptions.None)[1]);
        }
        else
        {
            Debug.Log("NO NIGHT INFO");
        }
    }

    void LoadTechnicalData(string technicalInfo)
    {
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
    }

    void LoadSerialNumbersData(string serialNumbers)
    {
        GlobalData.soundMeterSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[0];
        GlobalData.calibratorSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[1];
        GlobalData.metStationSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[2];
    }

    void LoadEmissionData(string emissionInfo)
    {
        GlobalData.emissionResultsSaved = true;
        if (!emissionInfo.Equals("NO"))
        {
            foreach (string emissionPoint in emissionInfo.Split(rowSplit, System.StringSplitOptions.None))
            {
                if (emissionPoint.Split(columnSplit, System.StringSplitOptions.None).Length != 1)
                {
                    GlobalData.emissionDescription.Add(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[0]);
                    GlobalData.emissionLASEQ.Add(float.Parse(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[1]));
                    GlobalData.emissionL90.Add(float.Parse(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[2]));
                    GlobalData.emissionFileNumber.Add(int.Parse(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[3]));
                    GlobalData.emissionInitialTime.Add(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[4]);
                    GlobalData.emissionFinalTime.Add(emissionPoint.Split(columnSplit, System.StringSplitOptions.None)[5]);
                }
                else
                {
                    GlobalData.emissionDescription.Add("");
                    GlobalData.emissionLASEQ.Add(0.0f);
                    GlobalData.emissionL90.Add(0.0f);
                    GlobalData.emissionFileNumber.Add(0);
                    GlobalData.emissionInitialTime.Add("");
                    GlobalData.emissionFinalTime.Add("");
                }


            }
        }
        else
        {
            GlobalData.emissionDescription.Add("");
            GlobalData.emissionLASEQ.Add(0.0f);
            GlobalData.emissionL90.Add(0.0f);
            GlobalData.emissionFileNumber.Add(0);
            GlobalData.emissionInitialTime.Add("");
            GlobalData.emissionFinalTime.Add("");
        }
    }

    void LoadNoiseData(string noiseInfo)
    {
        GlobalData.noiseResultsSaved = true;
        if (!noiseInfo.Equals("NO"))
        {
            foreach (string noisePoint in noiseInfo.Split(rowSplit, System.StringSplitOptions.None))
            {
                if (noisePoint.Split(columnSplit, System.StringSplitOptions.None).Length != 1)
                {
                    GlobalData.noiseDescription.Add(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[0]);
                    GlobalData.noiseLevelN.Add(float.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[1]));
                    GlobalData.noisefileNumberN.Add(int.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[2]));
                    GlobalData.noiseLevelW.Add(float.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[3]));
                    GlobalData.noisefileNumberW.Add(int.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[4]));
                    GlobalData.noiseLevelE.Add(float.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[5]));
                    GlobalData.noisefileNumberE.Add(int.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[6]));
                    GlobalData.noiseLevelS.Add(float.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[7]));
                    GlobalData.noisefileNumberS.Add(int.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[8]));
                    GlobalData.noiseLevelV.Add(float.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[9]));
                    GlobalData.noisefileNumberV.Add(int.Parse(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[10]));
                    GlobalData.noiseInitialTime.Add(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[11]);
                    GlobalData.noiseFinalTime.Add(noisePoint.Split(columnSplit, System.StringSplitOptions.None)[12]);
                }
                else
                {
                    GlobalData.noiseDescription.Add("");
                    GlobalData.noiseLevelN.Add(0.0f);
                    GlobalData.noisefileNumberN.Add(0);
                    GlobalData.noiseLevelW.Add(0.0f);
                    GlobalData.noisefileNumberW.Add(0);
                    GlobalData.noiseLevelE.Add(0.0f);
                    GlobalData.noisefileNumberE.Add(0);
                    GlobalData.noiseLevelS.Add(0.0f);
                    GlobalData.noisefileNumberS.Add(0);
                    GlobalData.noiseLevelV.Add(0.0f);
                    GlobalData.noisefileNumberV.Add(0);
                    GlobalData.noiseInitialTime.Add("");
                    GlobalData.noiseFinalTime.Add("");
                }
            }
        }
        else
        {
            GlobalData.noiseDescription.Add("");
            GlobalData.noiseLevelN.Add(0.0f);
            GlobalData.noisefileNumberN.Add(0);
            GlobalData.noiseLevelW.Add(0.0f);
            GlobalData.noisefileNumberW.Add(0);
            GlobalData.noiseLevelE.Add(0.0f);
            GlobalData.noisefileNumberE.Add(0);
            GlobalData.noiseLevelS.Add(0.0f);
            GlobalData.noisefileNumberS.Add(0);
            GlobalData.noiseLevelV.Add(0.0f);
            GlobalData.noisefileNumberV.Add(0);
            GlobalData.noiseInitialTime.Add("");
            GlobalData.noiseFinalTime.Add("");
        }
    }

    void LoadDescriptionData(string description)
    {
        GlobalData.descriptionDataSaved = true;
        GlobalData.description = description;
    }

    void LoadEventsData(string eventsPointsData)
    {
        string[] eventPoints = eventsPointsData.Split(rowSplit, System.StringSplitOptions.None);
        GlobalData.eventsDataSaved = true;
        for (int i = 0; i < eventPoints.Length; i++)
        {
            string[] eventInfo = eventPoints[i].Split(columnSplit, System.StringSplitOptions.None);
            if (eventInfo.Length != 1)
            {
                GlobalData.eventName.Add(eventInfo[0]);
                GlobalData.eventLevel.Add(float.Parse(eventInfo[1]));
                GlobalData.eventTime.Add(eventInfo[2]);
                GlobalData.eventLength.Add(float.Parse(eventInfo[3]));
            }
            else
            {
                GlobalData.eventName.Add("");
                GlobalData.eventLevel.Add(0.0f);
                GlobalData.eventTime.Add("");
                GlobalData.eventLength.Add(0.0f);
            }
        }
    }

    void LoadPictureData(string picturePath)
    {

    }

    void LoadSourceData(string source)
    {
        GlobalData.sourceDataSaved = true;
        GlobalData.source = source;
    }
}
