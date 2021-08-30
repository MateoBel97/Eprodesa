using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DescriptionData : MonoBehaviour
{
    [SerializeField] TMP_InputField descriptionInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalData.descriptionDataSaved)
        {

        }
        else
        {
            LoadData();
        }
    }

    void Update()
    {

    }

    public void UpdateDescription()
    {
        GlobalData.description = descriptionInputField.text;
    }

    public void SaveData()
    {
        GlobalData.descriptionDataSaved = true;
        GlobalData.SaveTxt();
    }

    public void LoadPreviousResultsScene()
    {
        if (GlobalData.isEnvironmentalNoiseMeasurement)
            SceneManager.LoadScene("NoiseResultsScene");
        else if (GlobalData.isNoiseEmissionMeasurement)
            SceneManager.LoadScene("EmissionResultsScene");
        else
            SceneManager.LoadScene("TechnicalDataScene");
    }

    void LoadData()
    {
        descriptionInputField.text = GlobalData.description;
    }
}
