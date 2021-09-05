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
            SceneManager.LoadScene(sceneToLoad);
    }
}
