using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatInputField : MonoBehaviour
{
    public string variable;
    public int indexToUpdate;
    [SerializeField] string textToShow;
    [SerializeField] bool textUp;

    private TMP_InputField inputField;
    private GameObject textUpGameObject;
    private GameObject textLeftGameObject;


    void Start()
    {
        
    //Debug.Log("Starting " + transform.name);
        inputField = transform.GetChild(0).GetComponent<TMP_InputField>();
        textUpGameObject = transform.GetChild(1).gameObject;
        textUpGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        textLeftGameObject = transform.GetChild(2).gameObject;
        textLeftGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        SetTextPosition();
        ShowParameter();
        inputField.onValueChanged.AddListener(delegate { Measurement.UpdateParameter(variable, float.Parse((inputField.text.Equals("") ? "0" : inputField.text)), index: indexToUpdate); });
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
        float type = 0.0f;
        float value = Measurement.GetParameter(variable, type, index: indexToUpdate);
        Debug.Log("if " + inputField);
        Debug.Log("v " + value);
        Debug.Log("vts " + value.ToString());

        inputField.text = (value != 0.0f ? value.ToString() : "");
    }

    void Update()
    {
        
    }
}
