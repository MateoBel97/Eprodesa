using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour
{
    [SerializeField] TMP_Dropdown timeFrameDropDown;
    [SerializeField] Toggle dayToggle;
    [SerializeField] Toggle nightToggle;
    [SerializeField] GameObject infoGameObject;
    [SerializeField] TMP_InputField initialWindSpeedInputField, finalWindSpeedInputField;
    [SerializeField] TMP_InputField initialTemperatureInputField, finalTemperatureInputField;
    [SerializeField] TMP_InputField initialHumidityInputField, finalHumidityInputField;
    [SerializeField] TMP_InputField initialAtmPressureInputField, finalAtmPressureInputField;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!GlobalData.weatherDataSaved)
        {
            UpdateTimeFrameToShow();
            infoGameObject.SetActive(false);
        }
        else
        {
            
            LoadData();
        }

        dayToggle.onValueChanged.AddListener(delegate { ShowDayInfo(); });
        nightToggle.onValueChanged.AddListener(delegate { ShowNightInfo(); });
    }

    void Update()
    {

    }

    public void UpdateTimeFrameToShow()
    {
        //Debug.Log(timeFrameDropDown.value);
        switch(timeFrameDropDown.value)
        {
            case 0:
                ShowDayInfo();
                break;
            case 1:
                ShowNightInfo();
                break;

        }
    }

    public void ShowDayInfo()
    {
        //Debug.Log("Showing Day Info!");
        dayToggle.transform.gameObject.SetActive(true);
        nightToggle.transform.gameObject.SetActive(false);

        UpdateDayTimeFrame();
        if (dayToggle.isOn)
        {
            infoGameObject.SetActive(true);

            initialWindSpeedInputField.text = GlobalData.initialWindSpeed_D.ToString();
            finalWindSpeedInputField.text = GlobalData.finalWindSpeed_D.ToString();
            initialTemperatureInputField.text = GlobalData.initialTemperature_D.ToString();
            finalTemperatureInputField.text = GlobalData.finalTemperature_D.ToString();
            initialHumidityInputField.text = GlobalData.initialHumidity_D.ToString();
            finalHumidityInputField.text = GlobalData.finalHumidity_D.ToString();
            initialAtmPressureInputField.text = GlobalData.initialAtmPressure_D.ToString();
            finalAtmPressureInputField.text = GlobalData.finalAtmPressure_D.ToString();
        }
        else
        {
            infoGameObject.SetActive(false);
        }
    }

    public void ShowNightInfo()
    {
        //Debug.Log("Showing Night Info!");
        dayToggle.transform.gameObject.SetActive(false);
        nightToggle.transform.gameObject.SetActive(true);

        UpdateNightTimeFrame();
        if (nightToggle.isOn)
        {
            infoGameObject.SetActive(true);

            initialWindSpeedInputField.text = GlobalData.initialWindSpeed_N.ToString();
            finalWindSpeedInputField.text = GlobalData.finalWindSpeed_N.ToString();
            initialTemperatureInputField.text = GlobalData.initialTemperature_N.ToString();
            finalTemperatureInputField.text = GlobalData.finalTemperature_N.ToString();
            initialHumidityInputField.text = GlobalData.initialHumidity_N.ToString();
            finalHumidityInputField.text = GlobalData.finalHumidity_N.ToString();
            initialAtmPressureInputField.text = GlobalData.initialAtmPressure_N.ToString();
            finalAtmPressureInputField.text = GlobalData.finalAtmPressure_N.ToString();
        }
        else
        {
            infoGameObject.SetActive(false);
        }
    }

    public void UpdateDayTimeFrame()
    {
        GlobalData.dayMetConditions = dayToggle.isOn;
    }

    public void UpdateNightTimeFrame()
    {
        GlobalData.nightMetConditions = nightToggle.isOn;
    }

    public void UpdateInitialWindSpeed()
    {
        if(timeFrameDropDown.value == 0)
            GlobalData.initialWindSpeed_D = double.Parse(initialWindSpeedInputField.text);
        else
            GlobalData.initialWindSpeed_N = double.Parse(initialWindSpeedInputField.text);
    }

    public void UpdateFinalWindSpeed()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.finalWindSpeed_D = double.Parse(finalWindSpeedInputField.text);
        else
            GlobalData.finalWindSpeed_N = double.Parse(finalWindSpeedInputField.text);
    }
    public void UpdateInitialTemperature()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.initialTemperature_D = double.Parse(initialTemperatureInputField.text);
        else
            GlobalData.initialTemperature_N = double.Parse(initialTemperatureInputField.text);
    }
    public void UpdateFinalTemperature()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.finalTemperature_D = double.Parse(finalTemperatureInputField.text);
        else
            GlobalData.finalTemperature_N = double.Parse(finalTemperatureInputField.text);
    }
    public void UpdateInitialHumidity()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.initialHumidity_D = double.Parse(initialHumidityInputField.text);
        else 
            GlobalData.initialHumidity_N = double.Parse(initialHumidityInputField.text);
    }
    public void UpdateFinalHumidity()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.finalHumidity_D = double.Parse(finalHumidityInputField.text);
        else
            GlobalData.finalHumidity_N = double.Parse(finalHumidityInputField.text);
    }
    public void UpdateInitialAtmPressure()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.initialAtmPressure_D = double.Parse(initialAtmPressureInputField.text);
        else
            GlobalData.initialAtmPressure_N = double.Parse(initialAtmPressureInputField.text);
    }
    public void UpdateFinalAtmPressure()
    {
        if (timeFrameDropDown.value == 0)
            GlobalData.finalAtmPressure_D = double.Parse(finalAtmPressureInputField.text);
        else
            GlobalData.finalAtmPressure_N = double.Parse(finalAtmPressureInputField.text);
    }
    public void SaveData()
    {
        GlobalData.weatherDataSaved = true;
        GlobalData.SaveTxt();
    }

    void LoadData()
    {
        //Debug.Log("Loading Data :D");
        dayToggle.isOn = GlobalData.dayMetConditions;
        nightToggle.isOn = GlobalData.nightMetConditions;

        if(GlobalData.dayMetConditions)
        {
            //Debug.Log("Loading day info...");
            timeFrameDropDown.value = 0;
            ShowDayInfo();
        }
        else if(GlobalData.nightMetConditions)
        {
            //Debug.Log("Loading night info...");
            timeFrameDropDown.value = 1;
            ShowNightInfo();
        }
        else
        {
            timeFrameDropDown.value = 0;
            infoGameObject.SetActive(false);
            dayToggle.transform.gameObject.SetActive(true);
            nightToggle.transform.gameObject.SetActive(false);
        }
        
    }

}
