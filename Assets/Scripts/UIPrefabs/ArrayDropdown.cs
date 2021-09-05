using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrayDropdown : MonoBehaviour
{
    [SerializeField] string array;
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] IntDropdown[] intDropdownsToUpdate;
    [SerializeField] IntInputField[] intInputFieldsToUpdate;
    [SerializeField] FloatInputField[] floatInputFieldsToUpdate;
    [SerializeField] StringInputField[] stringInputFieldsToUpdate;
    [SerializeField] CurrentResult[] currentResults;



    private TMP_Dropdown dropdown;
    int numValues;

    void Start()
    {
        Debug.Log("Starting " + transform.name);
        dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        GetArrayValues();
        previousButton.GetComponent<Button>().onClick.AddListener(delegate { dropdown.value--; });
        nextButton.GetComponent<Button>().onClick.AddListener(delegate { dropdown.value++; });
        dropdown.onValueChanged.AddListener(delegate {
            Measurement.UpdateParameter(array, dropdown.value);
            foreach (IntDropdown intDropdown in intDropdownsToUpdate)
            {
                intDropdown.ShowParameter();
            }
            foreach (IntInputField intInputField in intInputFieldsToUpdate)
            {
                intInputField.ShowParameter();
            }
            foreach (FloatInputField floatInputField in floatInputFieldsToUpdate)
            {
                floatInputField.ShowParameter();
            }
            foreach (StringInputField stringInputField in stringInputFieldsToUpdate)
            {
                stringInputField.ShowParameter();
            }
            foreach (CurrentResult currentResult in currentResults)
            {

            }


        });


    }

    void GetArrayValues()
    {

        dropdown.ClearOptions();
        int type = 0;
        numValues = Measurement.GetParameter(array, type);
        Debug.Log("Num Values: " + numValues);
        List<string> options = new List<string> { };
        for(int i = 0; i < numValues; i++)
        {
            string typeString = "";
            options.Add(Measurement.GetParameter(array, typeString, index: i));
        }
        dropdown.AddOptions(options);
        switch(array)
        {
            case "measurementPoints":
                dropdown.value = Measurement.measurementPointBeingUpdated;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (dropdown.value > 0)
            previousButton.SetActive(true);
        else
        {
            previousButton.SetActive(false);
        }

        if (dropdown.value < (numValues - 1))
            nextButton.SetActive(true);
        else
        {
            nextButton.SetActive(false);
        }
    }
}
