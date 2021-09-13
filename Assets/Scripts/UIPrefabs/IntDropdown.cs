using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntDropdown : MonoBehaviour
{
    [SerializeField] string variable;
    [SerializeField] bool usingIntValues;
    [SerializeField] int initialValue;
    [SerializeField] int finalValue;
    public int offset;
    [SerializeField] BoolToggle[] togglesToUpdate;
    [SerializeField] FloatInputField[] floatInputFieldsToUpdate;
    [SerializeField] GameObject[] objectsToShow;
    [SerializeField] CurrentResult[] currentResults;

    private TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        if(usingIntValues)
        {
            FillDropdownOptions();
        }
        ShowParameter();
        dropdown.onValueChanged.AddListener(delegate {
            Measurement.UpdateParameter(variable, dropdown.value - offset);
            foreach(BoolToggle toggle in togglesToUpdate)
            {
                toggle.ShowParameter();
            }
            foreach (FloatInputField floatInputField in floatInputFieldsToUpdate)
            {
                floatInputField.ShowParameter();
            }
            for(int i = 0; i < objectsToShow.Length; i++)
            {
                objectsToShow[i].SetActive(i == dropdown.value);
            }
            foreach(CurrentResult currentResult in currentResults)
            {
                currentResult.Resize(dropdown.value);
            }

        });
    }

    void FillDropdownOptions()
    {
        List<string> options = new List<string> { };
        dropdown.ClearOptions();
        for (int i = initialValue; i <= finalValue; i++)
        {
            options.Add(i.ToString());
        }
        dropdown.AddOptions(options);
    }

    public void ShowParameter()
    {
        int type = 0;
        int value = Measurement.GetParameter(variable, type);
        dropdown.value = value + offset;
    }
}
