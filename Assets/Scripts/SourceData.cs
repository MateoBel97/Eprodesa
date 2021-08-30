using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SourceData : MonoBehaviour
{
    [SerializeField] TMP_InputField sourceInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalData.sourceDataSaved)
        {

        }
        else
        {
            LoadData();
        }
    }

    void Update()
    {

    }

    public void UpdateSource()
    {
        GlobalData.source = sourceInputField.text;
    }

    public void SaveData()
    {
        GlobalData.sourceDataSaved = true;
        GlobalData.SaveTxt();
    }

    void LoadData()
    {
        sourceInputField.text = GlobalData.source;
    }
}
