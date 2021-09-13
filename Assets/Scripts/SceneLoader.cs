using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] bool chooseFromArray;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] string[] scenesToLoad;
    [SerializeField] bool createText;

    public void LoadScene(string sceneToLoad)
    {
        if (createText) Measurement.SaveTxt();
        if (chooseFromArray)
        {
            SceneManager.LoadScene(scenesToLoad[dropdown.value]);
        }
        else
        {
            if (!sceneToLoad.Equals("Results"))
            {
                if (sceneToLoad.Equals("Quit"))
                    Application.Quit();
                else
                    SceneManager.LoadScene(sceneToLoad);
            }
            
            else
            {
                switch (Measurement.typeOfMeasurement)
                {
                    case Measurement.TypeOfMeasurement.NoiseEmission:
                        SceneManager.LoadScene(scenesToLoad[0]);
                        break;
                    case Measurement.TypeOfMeasurement.EnvironmentalNoise:
                        SceneManager.LoadScene(scenesToLoad[1]);
                        break;
                    case Measurement.TypeOfMeasurement.LiteralG:
                        SceneManager.LoadScene(scenesToLoad[2]);
                        break;
                }
            }
        }
    }
}
