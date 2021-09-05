using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeFrameDropdown : MonoBehaviour
{
    [SerializeField] GameObject dayMenu;
    [SerializeField] GameObject nightMenu;
    // Start is called before the first frame update
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        ShowTimeFrameOptions();
        Measurement.timeFrameBeingUpdated = (dropdown.options[0].text.Equals("Nocturno") ? 1 : 0);
    }

    void ShowTimeFrameOptions()
    {
        List<string> options = new List<string> { };
        dropdown.ClearOptions();
        if (Measurement.measuringDay)
            options.Add("Diurno");
            
        if (Measurement.measuringNight)
            options.Add("Nocturno");
        dropdown.AddOptions(options);
    }
    // Update is called once per frame
    void Update()
    {
        if (dropdown.options.Count > 0)
        {
            dayMenu.SetActive(dropdown.options[dropdown.value].text.Equals("Diurno") && !dropdown.options[dropdown.value].text.Equals(""));
            nightMenu.SetActive(dropdown.options[dropdown.value].text.Equals("Nocturno") && !dropdown.options[dropdown.value].text.Equals(""));
        }
        else
        {
            dayMenu.SetActive(false);
            nightMenu.SetActive(false);
        }
    }
}
