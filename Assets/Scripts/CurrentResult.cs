using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrentResult : MonoBehaviour
{
    [SerializeField] string array;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] TextMeshProUGUI currentResultText;
    [SerializeField] IntInputField[] intInputFieldsToUpdate;
    [SerializeField] FloatInputField[] floatInputFieldsToUpdate;
    [SerializeField] StringInputField[] stringInputFieldsToUpdate;


    int size, newSize;
    int current;


    void Start()
    {
        //SetCurrentSize();

        dropdown.onValueChanged.AddListener(delegate { });
        previousButton.GetComponent<Button>().onClick.AddListener(delegate {
            PreviousResult();
        });
        nextButton.GetComponent<Button>().onClick.AddListener(delegate {
            NextResult();
        });
        SetCurrentSize();
        //ShowResult();
    }
    void NextResult()
    {
        switch (array)
        {
            case "dayEmission":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated++;
                break;
            case "dayResidual":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated++;
                break;
            case "nightEmission":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated++;
                break;
            case "nightResidual":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated++;
                break;
        }

        ShowResult();
    }

    void PreviousResult()
    {
        switch (array)
        {
            case "dayEmission":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated--;
                break;
            case "dayResidual":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated--;
                break;
            case "nightEmission":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated--;
                break;
            case "nightResidual":
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated--;
                break;
        }

        ShowResult();
    }


    void SetCurrentSize()
    {
        switch (array)
        {
            case "dayEmission":
                size = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Count;
                break;
            case "dayResidual":
                size = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Count;
                break;
            case "nightEmission":
                size = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Count;
                break;
            case "nightResidual":
                size = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Count;
                break;
        }

    }

    void ShowResult()
    {
        foreach(IntInputField intInputField in intInputFieldsToUpdate)
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
    }


    public void Resize(int dropdownValue)
    {
        SetCurrentSize();
        Debug.Log("Resizing");
        switch (array)
        {
            case "dayEmission":
                switch(dropdownValue)
                {
                    case 0:
                        newSize = 1;
                        break;
                    case 1:
                        newSize = 3;
                        break;
                    case 2:
                        newSize = 5;
                        break;
                }
                Debug.Log("Size: " + size + "    New Size: " + newSize);
                if(newSize > size)
                {

                    Debug.Log("Need to Add");
                    // size = 1;
                    // newSize = 5;
                    for (int i = size; i < newSize; i++)
                    { 
                        Debug.Log("Adding...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.Add(
                            new NoiseEmissionResult());
                    }
                }
                else
                {
                    Debug.Log("Need to Remove");
                    for (int i = size; i > newSize; i--)
                    { 
                        Debug.Log("Removing...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResults.RemoveAt(i - 1);
                    }
                }
                size = newSize;
                Debug.Log("New Size: " + size);
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated = 0;
                ShowResult();
                break;
            case "dayResidual":
                switch (dropdownValue)
                {
                    case 0:
                        newSize = 0;
                        break;
                    case 1:
                        newSize = 1;
                        break;
                    case 2:
                        newSize = 3;
                        break;
                    case 3:
                        newSize = 5;
                        break;
                }
                Debug.Log("Size: " + size + "    New Size: " + newSize);
                if (newSize > size)
                {

                    Debug.Log("Need to Add");
                    // size = 1;
                    // newSize = 5;
                    for (int i = size; i < newSize; i++)
                    {
                        Debug.Log("Adding...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.Add(
                            new NoiseEmissionResult());
                    }
                }
                else
                {
                    Debug.Log("Need to Remove");
                    for (int i = size; i > newSize; i--)
                    {
                        Debug.Log("Removing...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResults.RemoveAt(i - 1);
                    }
                }
                size = newSize;
                Debug.Log("New Size: " + size);
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated = 0;
                ShowResult();
                break;
            case "nightEmission":
                switch (dropdownValue)
                {
                    case 0:
                        newSize = 1;
                        break;
                    case 1:
                        newSize = 3;
                        break;
                    case 2:
                        newSize = 5;
                        break;
                }
                Debug.Log("Size: " + size + "    New Size: " + newSize);
                if (newSize > size)
                {

                    Debug.Log("Need to Add");
                    // size = 1;
                    // newSize = 5;
                    for (int i = size; i < newSize; i++)
                    {
                        Debug.Log("Adding...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.Add(
                            new NoiseEmissionResult());
                    }
                }
                else
                {
                    Debug.Log("Need to Remove");
                    for (int i = size; i > newSize; i--)
                    {
                        Debug.Log("Removing...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResults.RemoveAt(i - 1);
                    }
                }
                size = newSize;
                Debug.Log("New Size: " + size);
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated = 0;
                ShowResult();
                break;
            case "nightResidual":
                switch (dropdownValue)
                {
                    case 0:
                        newSize = 0;
                        break;
                    case 1:
                        newSize = 1;
                        break;
                    case 2:
                        newSize = 3;
                        break;
                    case 3:
                        newSize = 5;
                        break;
                }
                Debug.Log("Size: " + size + "    New Size: " + newSize);
                if (newSize > size)
                {

                    Debug.Log("Need to Add");
                    // size = 1;
                    // newSize = 5;
                    for (int i = size; i < newSize; i++)
                    {
                        Debug.Log("Adding...");
                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.Add(
                            new NoiseEmissionResult());
                    }
                }
                else
                {
                    Debug.Log("Need to Remove");
                    for (int i = size; i > newSize; i--)
                    {
                        Debug.Log("mpbu: " + Measurement.measurementPointBeingUpdated);

                        Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResults.RemoveAt(i - 1);
                    }
                }
                size = newSize;
                Debug.Log("New Size: " + size);
                Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated = 0;
                ShowResult();
                break;
        }
        SetCurrentSize();

    }
    // Update is called once per frame
    void Update()
    {
        
        switch (array)
        {
            case "dayEmission":
                current = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.emissionResultBeingUpdated;
                break;
            case "dayResidual":
                current = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].dayNoiseEmissionMeasurement.residualResultBeingUpdated;
                foreach (IntInputField intInputField in intInputFieldsToUpdate)
                {
                    intInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                foreach (FloatInputField floatInputField in floatInputFieldsToUpdate)
                {
                    floatInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                foreach (StringInputField stringInputField in stringInputFieldsToUpdate)
                {
                    stringInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                currentResultText.transform.gameObject.SetActive(dropdown.value != 0);
                break;
            case "nightEmission":
                current = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.emissionResultBeingUpdated;
                
                break;
            case "nightResidual":
                current = Measurement.measurementPoints[Measurement.measurementPointBeingUpdated].nightNoiseEmissionMeasurement.residualResultBeingUpdated;
                foreach (IntInputField intInputField in intInputFieldsToUpdate)
                {
                    intInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                foreach (FloatInputField floatInputField in floatInputFieldsToUpdate)
                {
                    floatInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                foreach (StringInputField stringInputField in stringInputFieldsToUpdate)
                {
                    stringInputField.transform.gameObject.SetActive(dropdown.value != 0);
                }
                currentResultText.transform.gameObject.SetActive(dropdown.value != 0);
                break;
        }
        previousButton.SetActive(current > 0);
        nextButton.SetActive(current < (size - 1));
        currentResultText.text = (current + 1).ToString() + "/" + size.ToString();

    }
}
