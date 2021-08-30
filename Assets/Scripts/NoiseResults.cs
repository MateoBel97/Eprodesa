using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoiseResults : MonoBehaviour
{

    [SerializeField] TMP_Dropdown measurementDropdown;
    [SerializeField] Button deleteButton, addButton;
    [SerializeField] TMP_InputField descriptionInputField;
    [SerializeField] TMP_InputField N_LevelInputField;
    [SerializeField] TMP_InputField N_FileNumberInputField;
    [SerializeField] TMP_InputField W_LevelInputField;
    [SerializeField] TMP_InputField W_FileNumberInputField;
    [SerializeField] TMP_InputField E_LevelInputField;
    [SerializeField] TMP_InputField E_FileNumberInputField;
    [SerializeField] TMP_InputField S_LevelInputField;
    [SerializeField] TMP_InputField S_FileNumberInputField;
    [SerializeField] TMP_InputField V_LevelInputField;
    [SerializeField] TMP_InputField V_FileNumberInputField;
    [SerializeField] TMP_InputField initalTimeInputField;
    [SerializeField] TMP_InputField finalTimeInputField;


    int measurementCount;

    List<string> dropdownOptions = new List<string> { };

    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalData.noiseResultsSaved)
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
        GlobalData.noiseDescription[measurementDropdown.value] = text;
    }

    public void UpdateLevelN()
    {
        string text = N_LevelInputField.text;
        GlobalData.noiseLevelN[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateFileNumberN()
    {
        string text = N_FileNumberInputField.text;
        GlobalData.noisefileNumberN[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }

    public void UpdateLevelW()
    {
        string text = W_LevelInputField.text;
        GlobalData.noiseLevelW[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateFileNumberW()
    {
        string text = W_FileNumberInputField.text;
        GlobalData.noisefileNumberW[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }
    public void UpdateLevelE()
    {
        string text = E_LevelInputField.text;
        GlobalData.noiseLevelE[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateFileNumberE()
    {
        string text = E_FileNumberInputField.text;
        GlobalData.noisefileNumberE[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }
    public void UpdateLevelS()
    {
        string text = S_LevelInputField.text;
        GlobalData.noiseLevelS[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateFileNumberS()
    {
        string text = S_FileNumberInputField.text;
        GlobalData.noisefileNumberS[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }
    public void UpdateLevelV()
    {
        string text = V_LevelInputField.text;
        GlobalData.noiseLevelV[measurementDropdown.value] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateFileNumberV()
    {
        string text = V_FileNumberInputField.text;
        GlobalData.noisefileNumberV[measurementDropdown.value] = (string.Equals(text, "") ? 0 : int.Parse(text));
    }

    public void UpdateInitialTime()
    {
        GlobalData.noiseInitialTime[measurementDropdown.value] = initalTimeInputField.text;
    }

    public void UpdateFinalTime()
    {
        GlobalData.noiseFinalTime[measurementDropdown.value] = finalTimeInputField.text;
    }

    public void AddResult()
    {
        measurementCount++;
        dropdownOptions.Add("Medición " + measurementCount.ToString());

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

            GlobalData.noiseDescription.RemoveAt(currentValue);
            GlobalData.noiseLevelN.RemoveAt(currentValue);
            GlobalData.noisefileNumberN.RemoveAt(currentValue);
            GlobalData.noiseLevelW.RemoveAt(currentValue);
            GlobalData.noisefileNumberW.RemoveAt(currentValue);
            GlobalData.noiseLevelE.RemoveAt(currentValue);
            GlobalData.noisefileNumberE.RemoveAt(currentValue);
            GlobalData.noiseLevelS.RemoveAt(currentValue);
            GlobalData.noisefileNumberS.RemoveAt(currentValue);
            GlobalData.noiseLevelV.RemoveAt(currentValue);
            GlobalData.noisefileNumberV.RemoveAt(currentValue);
            GlobalData.noiseInitialTime.RemoveAt(currentValue);
            GlobalData.noiseFinalTime.RemoveAt(currentValue);

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
        string descpription = GlobalData.noiseDescription[measurementDropdown.value];
        descriptionInputField.text = (string.Equals("", descpription) ? "" : descpription);

        float nLevel = GlobalData.noiseLevelN[measurementDropdown.value];
        N_LevelInputField.text = (nLevel == 0.0f ? "" : nLevel.ToString());
        int nFileNumber = GlobalData.noisefileNumberN[measurementDropdown.value];
        N_FileNumberInputField.text = (nFileNumber == 0 ? "" : nFileNumber.ToString());
        float wLevel = GlobalData.noiseLevelW[measurementDropdown.value];
        W_LevelInputField.text = (wLevel == 0.0f ? "" : wLevel.ToString());
        int wFileNumber = GlobalData.noisefileNumberW[measurementDropdown.value];
        W_FileNumberInputField.text = (wFileNumber == 0 ? "" : wFileNumber.ToString());
        float eLevel = GlobalData.noiseLevelE[measurementDropdown.value];
        E_LevelInputField.text = (eLevel == 0.0f ? "" : eLevel.ToString());
        int eFileNumber = GlobalData.noisefileNumberE[measurementDropdown.value];
        E_FileNumberInputField.text = (eFileNumber == 0 ? "" : eFileNumber.ToString());
        float sLevel = GlobalData.noiseLevelS[measurementDropdown.value];
        S_LevelInputField.text = (sLevel == 0.0f ? "" : sLevel.ToString());
        int sFileNumber = GlobalData.noisefileNumberS[measurementDropdown.value];
        S_FileNumberInputField.text = (sFileNumber == 0 ? "" : sFileNumber.ToString());
        float vLevel = GlobalData.noiseLevelV[measurementDropdown.value];
        V_LevelInputField.text = (vLevel == 0.0f ? "" : vLevel.ToString());
        int vFileNumber = GlobalData.noisefileNumberV[measurementDropdown.value];
        V_FileNumberInputField.text = (vFileNumber == 0 ? "" : vFileNumber.ToString());
        string initialTime = GlobalData.noiseInitialTime[measurementDropdown.value];
        initalTimeInputField.text = (string.Equals("", initialTime) ? "" : initialTime);
        string finalTime = GlobalData.noiseFinalTime[measurementDropdown.value];
        finalTimeInputField.text = (string.Equals("", finalTime) ? "" : finalTime);
    }

    public void SaveResults()
    {
        GlobalData.noiseResultsSaved = true;
        GlobalData.SaveTxt();
    }

    public void LoadPreviousResultsScene()
    {
        if (GlobalData.isNoiseEmissionMeasurement)
            SceneManager.LoadScene("EmissionResultsScene");
        else
            SceneManager.LoadScene("TechnicalDataScene");
    }

    void LoadResults()
    {
        measurementCount = GlobalData.noiseDescription.Count;

        dropdownOptions.Clear();
        for (int i = 1; i <= measurementCount; i++)
        {
            dropdownOptions.Add("Medición " + i.ToString());
        }

        measurementDropdown.ClearOptions();
        measurementDropdown.AddOptions(dropdownOptions);
        measurementDropdown.value = 0;

        ShowResult();
    }
}
