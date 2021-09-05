using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoolToggle : MonoBehaviour
{
    [SerializeField] string variable;
    [SerializeField] string textToShow;
    [SerializeField] GameObject[] objectsToSetActive;

    private Toggle toggle;

    void Start()
    {
        Debug.Log("Starting " + transform.name);
        toggle = transform.GetChild(0).GetComponent<Toggle>();
        GameObject textGameObject = transform.GetChild(0).transform.GetChild(1).gameObject;
        textGameObject.GetComponent<Text>().text = textToShow;
        ShowParameter();
        toggle.onValueChanged.AddListener(delegate { 
            Measurement.UpdateParameter(variable, toggle.isOn);
            foreach(GameObject gameObject in objectsToSetActive)
            {
                gameObject.SetActive(toggle.isOn);
            }
        });
    }

    public void ShowParameter()
    {
        bool type = false;
        bool value = Measurement.GetParameter(variable, type);
        toggle.isOn = value;
        foreach (GameObject gameObject in objectsToSetActive)
        {
            gameObject.SetActive(toggle.isOn);
        }
    }

    void Update()
    {
        
    }
}
