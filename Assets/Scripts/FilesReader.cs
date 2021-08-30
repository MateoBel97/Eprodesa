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
    
    FileInfo[] fileInfo;

    string[] pageSplit = new string[] { "*-*-*" };
    string[] rowSplit = new string[] { "***" };
    string[] columnSplit = new string[] { "---" };


    string chosenFilePath;
    string info;

    void Start()
    {
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
            if (!file.Name.Equals("-.txt"))
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
        info= File.ReadAllText(filePath);


        
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

        infoTMP.text = "Empresa: " + company + "\nFecha: " + day + "-" + month + "-" + year + "\nOrden de trabajo: " + workOrder;
    }

    public void ExportFile()
    {
        string general = info.Split(pageSplit, System.StringSplitOptions.None)[0];
        string company = general.Split(columnSplit, System.StringSplitOptions.None)[0];
        string workOrder = general.Split(columnSplit, System.StringSplitOptions.None)[4];
#if UNITY_EDITOR_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + company + "-" + workOrder + ".txt", info);
#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + company + "-" + workOrder + ".txt", info);
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath + "/" + company + "-" + workOrder + ".txt";
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

    public void LoadInfo()
    {
        GlobalData.ResetValues();
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

        //
        string general = info.Split(pageSplit, System.StringSplitOptions.None)[0];
        GlobalData.generalDataSaved = true;
        GlobalData.companyName = general.Split(columnSplit, System.StringSplitOptions.None)[0];
        GlobalData.day = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[1]);
        GlobalData.month = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[2]);
        GlobalData.year = int.Parse(general.Split(columnSplit, System.StringSplitOptions.None)[3]);
        GlobalData.workOrder = general.Split(columnSplit, System.StringSplitOptions.None)[4];

        //
        GlobalData.measurementPointsDataSaved = true;
        string[] measurementPoints = info.Split(pageSplit, System.StringSplitOptions.None)[1].Split(rowSplit, System.StringSplitOptions.None);
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

        //
        GlobalData.weatherDataSaved = true;
        string dayWeatherInfo = info.Split(pageSplit, System.StringSplitOptions.None)[2];
        if(!dayWeatherInfo.Equals("NO"))
        {
            GlobalData.dayTimeFrame = true;
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
            Debug.Log("NO DAY INFO");
        }

        //
        string nigthWeatherInfo = info.Split(pageSplit, System.StringSplitOptions.None)[3];
        if (!nigthWeatherInfo.Equals("NO"))
        {
            GlobalData.nightTimeFrame = true;
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

        //
        GlobalData.technicalInfoDataSaved = true;
        string technicalInfo = info.Split(pageSplit, System.StringSplitOptions.None)[4];
        string typeOfMeasurement = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[0];
        GlobalData.isNoiseEmissionMeasurement = bool.Parse(typeOfMeasurement.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isEnvironmentalNoiseMeasurement = bool.Parse(typeOfMeasurement.Split(columnSplit, System.StringSplitOptions.None)[1]);
        string parameters = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[1];
        GlobalData.isEmissionParameter = bool.Parse(parameters.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isResidualParameter = bool.Parse(parameters.Split(columnSplit, System.StringSplitOptions.None)[1]);
        string timeFrame = technicalInfo.Split(rowSplit, System.StringSplitOptions.None)[2];
        GlobalData.isDayTimeFrame = bool.Parse(timeFrame.Split(columnSplit, System.StringSplitOptions.None)[0]);
        GlobalData.isNightTimeFrame = bool.Parse(timeFrame.Split(columnSplit, System.StringSplitOptions.None)[1]);

        //
        string serialNumbers = info.Split(pageSplit, System.StringSplitOptions.None)[5];
        GlobalData.soundMeterSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[0];
        GlobalData.calibratorSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[1];
        GlobalData.metStationSerialNumber = serialNumbers.Split(columnSplit, System.StringSplitOptions.None)[2];

        //
        GlobalData.emissionResultsSaved = true;
        string emissionInfo = info.Split(pageSplit, System.StringSplitOptions.None)[6];
        if(!emissionInfo.Equals("NO"))
        {
            foreach(string emissionPoint in emissionInfo.Split(rowSplit,System.StringSplitOptions.None))
            {
                if(emissionPoint.Split(columnSplit,System.StringSplitOptions.None).Length != 1)
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
        
        //
        GlobalData.noiseResultsSaved = true;
        string noiseInfo = info.Split(pageSplit, System.StringSplitOptions.None)[7];
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

        //
        GlobalData.descriptionDataSaved = true;
        GlobalData.description = info.Split(pageSplit, System.StringSplitOptions.None)[8];

        //
        GlobalData.eventsDataSaved = true;
        string[] eventsPoints = info.Split(pageSplit, System.StringSplitOptions.None)[9].Split(rowSplit, System.StringSplitOptions.None);
        for (int i = 0; i < eventsPoints.Length; i++)
        {
            string[] eventInfo = eventsPoints[i].Split(columnSplit, System.StringSplitOptions.None);
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

        GlobalData.sourceDataSaved = true;
        GlobalData.source = info.Split(pageSplit, System.StringSplitOptions.None)[11];


        SceneManager.LoadScene("GeneralDataScene");
    }




}
