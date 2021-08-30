using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralData : MonoBehaviour
{
    [SerializeField] TMP_InputField companyNameInputField;
    [SerializeField] TMP_Dropdown dayDropdown;
    [SerializeField] TMP_Dropdown monthDropdown;
    [SerializeField] TMP_Dropdown yearDropdown;
    [SerializeField] TMP_InputField workOrderInputField;

    int initialYear = 2019;
    int finalYear = 2100;

    void Start()
    {

        FillDropdownObjects();
        if (!GlobalData.generalDataSaved)
        {
            SetCurrentDate();
        }
        else
        {
            LoadData();
        }
        
    }


    void Update()
    {
        
    }

    void FillDropdownObjects() // Llenar los Dropdown menus con días, meses y años
    {
        List<string> options = new List<string> { };

        dayDropdown.ClearOptions();
        for(int i = 1; i <= 31; i++)
        {
            options.Add(i.ToString());
        }
        options.Add(" ");
        dayDropdown.AddOptions(options);

        options.Clear();
        monthDropdown.ClearOptions();
        List<string> months = new List<string> {    "Enero",
                                                    "Febrero",
                                                    "Marzo",
                                                    "Abril",
                                                    "Mayo",
                                                    "Junio",
                                                    "Julio",
                                                    "Agosto",
                                                    "Septiembre",
                                                    "Octubre",
                                                    "Noviembre",
                                                    "Diciembre"};
        monthDropdown.AddOptions(months);

        yearDropdown.ClearOptions();
        for(int i = initialYear; i <= finalYear; i++)
        {
            options.Add(i.ToString());
        }
        options.Add(" ");
        yearDropdown.AddOptions(options);
    }

    void SetCurrentDate() // Asignar valores de Dropdown menus a fecha actual
    {
        int day = int.Parse(System.DateTime.Today.ToString().Split('/')[0]);
        dayDropdown.value = day - 1;

        int month = int.Parse(System.DateTime.Today.ToString().Split('/')[1]);
        monthDropdown.value = month - 1;

        int year = int.Parse(System.DateTime.Today.ToString().Split('/')[2].Split(' ')[0]);
        yearDropdown.value = year - initialYear;
    }

    public void UpdateCompanyName()
    {
        GlobalData.companyName = companyNameInputField.text;
    }

    public void UpdateDay()
    {
        GlobalData.day = dayDropdown.value + 1;
    }
    public void UpdateMonth()
    {
        GlobalData.month = monthDropdown.value + 1;
    }
    public void UpdateYear()
    {
        GlobalData.year = yearDropdown.value + initialYear;
    }
    public void UpdateWorkOrder()
    {
        GlobalData.workOrder = workOrderInputField.text;
    }
    public void SaveData()
    {

        GlobalData.generalDataSaved = true;
        GlobalData.SaveTxt();
    }

    void LoadData()
    {
        companyNameInputField.text = GlobalData.companyName;
        dayDropdown.value = GlobalData.day - 1;
        monthDropdown.value = GlobalData.month - 1;
        yearDropdown.value = GlobalData.year - initialYear;
        workOrderInputField.text = GlobalData.workOrder;
    }
}
