using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StringInputField : MonoBehaviour
{
    public string variable;
    public int indexToUpdate;
    [SerializeField] string textToShow;
    [SerializeField] bool textUp;
    

    private TMP_InputField inputField;
    private GameObject textUpGameObject;
    private GameObject textLeftGameObject;


    void Awake()
    {
        Debug.Log("Starting " + transform.name);
        inputField = transform.GetChild(0).GetComponent<TMP_InputField>();
        textUpGameObject = transform.GetChild(1).gameObject;
        textUpGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        textLeftGameObject = transform.GetChild(2).gameObject;
        textLeftGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        SetTextPosition();
        ShowParameter();
        inputField.onValueChanged.AddListener(delegate { Measurement.UpdateParameter(variable, inputField.text, index: indexToUpdate); });
    }

    void SetTextPosition()
    {
        if (textUp)
        {
            textUpGameObject.SetActive(true);
            textLeftGameObject.SetActive(false);
        }
        else
        {
            textUpGameObject.SetActive(false);
            textLeftGameObject.SetActive(true);
        }
    }

    public void ShowParameter()
    {
        inputField = transform.GetChild(0).GetComponent<TMP_InputField>();
        string type = "";
        string value = Measurement.GetParameter(variable, type, index: indexToUpdate);
        inputField.text = value;
    }

    void Update()
    {
        
    }
}
