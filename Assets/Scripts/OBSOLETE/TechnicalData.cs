using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TechnicalData : MonoBehaviour
{
    [SerializeField] Toggle isNoiseEmissionMeasurementToggle, isEnvironmentalNoiseMeasurementToggle;
    [SerializeField] Toggle isEmissionToggle, isResidualToggle;
    [SerializeField] Toggle isDayTimeFrameToggle, isNightTimeFrameToggle;
    [SerializeField] TMP_InputField soundMeterSerialNumberInputField;
    [SerializeField] TMP_InputField calibratorSerialNumberInputField;
    [SerializeField] TMP_InputField metStationSerialNumberInputField;

    // Start is called before the first frame update
    void Start()
    {
        if(!GlobalData.technicalInfoDataSaved)
        {

        }
        else
        {
            LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNoiseEmissionMeasurement()
    {
        GlobalData.isNoiseEmissionMeasurement = isNoiseEmissionMeasurementToggle.isOn;
    }

    public void UpdateEnvironmentalNoiseMeasurement()
    { 
        GlobalData.isEnvironmentalNoiseMeasurement = isEnvironmentalNoiseMeasurementToggle.isOn;
    }
    public void UpdateEmissionParameter()
    {
        GlobalData.isEmissionParameter = isEmissionToggle.isOn;
    }

    public void UpdateResidualParameter()
    {
        GlobalData.isResidualParameter = isResidualToggle.isOn;
    }

    public void UpdateDayTimeFrame()
    {
        GlobalData.isDayTimeFrame = isDayTimeFrameToggle.isOn;
    }

    public void UpdateNightTimeFrame()
    {
        GlobalData.isNightTimeFrame = isNightTimeFrameToggle.isOn;
    }

    public void UpdateSoundMeterSerialNumber()
    {
        GlobalData.soundMeterSerialNumber = soundMeterSerialNumberInputField.text;
    }

    public void UpdateCalibratorSerialNumber()
    {
        GlobalData.calibratorSerialNumber = calibratorSerialNumberInputField.text;
    }

    public void UpdateMetStationSerialNumber()
    {
        GlobalData.metStationSerialNumber = metStationSerialNumberInputField.text;
    }


    public void SaveData()
    {
        GlobalData.technicalInfoDataSaved = true;
        GlobalData.SaveTxt();
    }

    public void LoadNextResultsScene()
    {
        if (GlobalData.isNoiseEmissionMeasurement)
            SceneManager.LoadScene("EmissionResultsScene");
        else if (GlobalData.isEnvironmentalNoiseMeasurement)
            SceneManager.LoadScene("NoiseResultsScene");
        else
            SceneManager.LoadScene("DescriptionDataScene");
    }

    void LoadData()
    {
        
        isNoiseEmissionMeasurementToggle.isOn = GlobalData.isNoiseEmissionMeasurement;
        isEnvironmentalNoiseMeasurementToggle.isOn = GlobalData.isEnvironmentalNoiseMeasurement;
        isEmissionToggle.isOn = GlobalData.isEmissionParameter;
        isResidualToggle.isOn = GlobalData.isResidualParameter;
        isDayTimeFrameToggle.isOn = GlobalData.isDayTimeFrame;
        isNightTimeFrameToggle.isOn = GlobalData.isNightTimeFrame;

        soundMeterSerialNumberInputField.text = GlobalData.soundMeterSerialNumber;
        calibratorSerialNumberInputField.text = GlobalData.calibratorSerialNumber;
        metStationSerialNumberInputField.text = GlobalData.metStationSerialNumber;
    }
}
