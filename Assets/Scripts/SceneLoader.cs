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

    public void LoadScene(string sceneToLoad)
    {
        if (chooseFromArray)
            SceneManager.LoadScene(scenesToLoad[dropdown.value]);
        else
        {
            Debug.Log(sceneToLoad.Equals("Results"));
            if (!sceneToLoad.Equals("Results"))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Loading Results");
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
