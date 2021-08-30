using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmissionResults : MonoBehaviour
{

    [SerializeField] TMP_Dropdown measurementDropdown;
    [SerializeField] Button deleteButton, addButton;
    [SerializeField] TMP_InputField descriptionInputField;
    [SerializeField] TMP_InputField laseqInputField;
    [SerializeField] TMP_InputField l90InputField;
    [SerializeField] TMP_InputField fileNumberInputField;
    [SerializeField] TMP_InputField initalTimeInputField;
    [SerializeField] TMP_InputField finalTimeInputField;

    int measurementCount;

    List<string> dropdownOptions = new List<string> { };


    void Start()
    {

        if (!GlobalData.emissionResultsSaved)
        {
            measurementCount = 0;
            AddResult();
        }
        else
        {
            LoadResults();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDescription()
    {
        string text = descriptionInputField.text;
        GlobalData.emissionDescription[measurementDropdown.value] = text;
    }

    public void UpdateLASEQ()
    {
        string text = laseqInputField.text;
        GlobalData.emissionLASEQ[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateL90()
    {
        string text = l90InputField.text;
        GlobalData.emissionL90[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }
    public void UpdateFileNumber()
    {
        string text = fileNumberInputField.text;
        GlobalData.emissionFileNumber[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }

    public void UpdateInitialTime()
    {
        GlobalData.emissionInitialTime[measurementDropdown.value] = initalTimeInputField.text;
    }

    public void UpdateFinalTime()
    {
        GlobalData.emissionFinalTime[measurementDropdown.value] = finalTimeInputField.text;
    }

    public void AddResult()
    {

        measurementCount++;
        dropdownOptions.Add("Medición " + measurementCount.ToString());
        GlobalData.emissionDescription.Add("");
        GlobalData.emissionLASEQ.Add(0.0f);
        GlobalData.emissionL90.Add(0.0f);
        GlobalData.emissionFileNumber.Add(0);
        GlobalData.emissionInitialTime.Add("");
        GlobalData.emissionFinalTime.Add("");
        measurementDropdown.ClearOptions();
        measurementDropdown.AddOptions(dropdownOptions);
        measurementDropdown.value = measurementCount - 1;
        //Debug.Log("Añadido");

    }

    public void RemoveResult()
    {
        if (measurementCount > 1)
        { 
            measurementCount--;
            int currentValue = measurementDropdown.value;
           
            measurementDropdown.ClearOptions();
            dropdownOptions.RemoveAt(measurementCount);
            measurementDropdown.AddOptions(dropdownOptions);

            GlobalData.emissionDescription.RemoveAt(currentValue);
            GlobalData.emissionLASEQ.RemoveAt(currentValue);
            GlobalData.emissionL90.RemoveAt(currentValue);
            GlobalData.emissionFileNumber.RemoveAt(currentValue);
            GlobalData.emissionInitialTime.RemoveAt(currentValue);
            GlobalData.emissionFinalTime.RemoveAt(currentValue);
            if (currentValue > (measurementCount))
            {
                measurementDropdown.value = measurementDropdown.options.Count;
            }
            else
            {
                measurementDropdown.value = currentValue;
            }
            ShowResult();
        }
       
    }

    public void ShowResult()
    {
        string descpription = GlobalData.emissionDescription[measurementDropdown.value];
        descriptionInputField.text = (string.Equals("", descpription) ? "" : descpription);
        float laseq = GlobalData.emissionLASEQ[measurementDropdown.value];
        laseqInputField.text = (laseq == 0.0f ? "" : laseq.ToString());
        float l90 = GlobalData.emissionL90[measurementDropdown.value];
        l90InputField.text = (l90 == 0.0f ? "" : l90.ToString());
        int fileNumber = GlobalData.emissionFileNumber[measurementDropdown.value];
        fileNumberInputField.text = (fileNumber == 0 ? "" : fileNumber.ToString());
        string initialTime = GlobalData.emissionInitialTime[measurementDropdown.value];
        initalTimeInputField.text = (string.Equals("", initialTime) ? "" : initialTime);
        string finalTime = GlobalData.emissionFinalTime[measurementDropdown.value];
        finalTimeInputField.text = (string.Equals("", finalTime) ? "" : finalTime);
    }

    public void SaveResults()
    {
        GlobalData.emissionResultsSaved = true;
        GlobalData.SaveTxt();
    }

    public void LoadNextResultsScene()
    {
        if (GlobalData.isEnvironmentalNoiseMeasurement)
            SceneManager.LoadScene("NoiseResultsScene");
        else
            SceneManager.LoadScene("DescriptionDataScene");    
    }

    void LoadResults()
    {
        measurementCount = GlobalData.emissionDescription.Count;

        dropdownOptions.Clear();
        for(int i = 1; i <= measurementCount; i++)
        {
            dropdownOptions.Add("Medición " + i.ToString());
        }

        measurementDropdown.ClearOptions();
        measurementDropdown.AddOptions(dropdownOptions);
        measurementDropdown.value = 0;

        ShowResult();
    }


}
