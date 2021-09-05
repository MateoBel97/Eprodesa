using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntInputField : MonoBehaviour
{
    [SerializeField] string variable;
    [SerializeField] string textToShow;
    [SerializeField] bool textUp;

    private TMP_InputField inputField;
    private GameObject textUpGameObject;
    private GameObject textLeftGameObject;


    void Start()
    {
        inputField = transform.GetChild(0).GetComponent<TMP_InputField>();
        textUpGameObject = transform.GetChild(1).gameObject;
        textUpGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        textLeftGameObject = transform.GetChild(2).gameObject;
        textLeftGameObject.GetComponent<TextMeshProUGUI>().text = textToShow;
        SetTextPosition();
        ShowParameter();
        inputField.onValueChanged.AddListener(delegate { Measurement.UpdateParameter(variable, int.Parse((inputField.text.Equals("") ? "0" : inputField.text))); });
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
        int type = 0;
        int value = Measurement.GetParameter(variable, type);
        inputField.text = (value != 0 ? value.ToString(): "");
    }


    void Update()
    {
        
    }
}
