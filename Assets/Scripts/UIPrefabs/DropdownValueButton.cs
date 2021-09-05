using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownValueButton : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] bool increment;

    private int numOptions;

    void Start()
    {
        Debug.Log("Starting " + transform.name);
        GetNumOptions();
    }

    void GetNumOptions()
    {
        numOptions = dropdown.options.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
