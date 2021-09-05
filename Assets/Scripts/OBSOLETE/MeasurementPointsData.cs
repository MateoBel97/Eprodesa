using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeasurementPointsData : MonoBehaviour
{

    [SerializeField] GameObject buttonTemplate;
    [SerializeField] GameObject scrollviewContent;

    [SerializeField] TextMeshProUGUI pointTMP;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField nInputField;
    [SerializeField] TMP_InputField wInputField;

    int pointsCount = 0;
    int currentPoint = 0;

    void Start()
    {
        if(!GlobalData.measurementPointsDataSaved)
        {
            AddNewPoint();
        }
        else
        {
            LoadData();
        }
        
    }


    public void AddNewPoint()
    {
        pointsCount++;
        currentPoint = pointsCount;
        int copyOfPointsCount = pointsCount;
        InstantiateNewButton(copyOfPointsCount);
        GlobalData.pointName.Add("");
        GlobalData.n.Add("");
        GlobalData.w.Add("");
        ShowPointInfo(pointsCount);
    }

    public void RemovePoint()
    {
        if (pointsCount > 1)
        {
            GameObject buttonToDelete = GameObject.Find("Copy" + pointsCount);
            GameObject.Destroy(buttonToDelete);

            GlobalData.pointName.RemoveAt(currentPoint - 1);
            GlobalData.n.RemoveAt(currentPoint - 1);
            GlobalData.w.RemoveAt(currentPoint - 1);

            if (currentPoint == pointsCount)
            {
                currentPoint--;
            }
            pointsCount--;
            ShowPointInfo(currentPoint);
        }
    }

    public void UpdatePointName()
    {
        GlobalData.pointName[currentPoint - 1] = nameInputField.text;
    }

    public void UpdateNValue()
    {
        GlobalData.n[currentPoint - 1] = nInputField.text;
    }

    public void UpdateWValue()
    {
        GlobalData.w[currentPoint - 1] = wInputField.text;
    }
    void ShowPointInfo(int point)
    {
        currentPoint = point;
        pointTMP.text = "Punto " + currentPoint;
        nameInputField.text = GlobalData.pointName[currentPoint - 1];
        nInputField.text = GlobalData.n[currentPoint - 1];
        wInputField.text = GlobalData.w[currentPoint - 1];
    }

    public void SaveData()
    {
        GlobalData.measurementPointsDataSaved = true;
        GlobalData.SaveTxt();

    }

    void InstantiateNewButton(int pointToShow)
    {
        var copy = Instantiate(buttonTemplate);
        copy.name = "Copy" + pointToShow;
        copy.transform.SetParent(scrollviewContent.transform);
        copy.transform.localScale = new Vector3(1f, 1f, 1f);
        copy.GetComponentInChildren<TextMeshProUGUI>().text = "Punto " + pointToShow;
        copy.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                ShowPointInfo(pointToShow);
            }
            );
    }
    void LoadData()
    {
        pointsCount = GlobalData.n.Count;

        for(int i = 1; i <= GlobalData.n.Count; i++)
        {
            int copyOfI = i;
            InstantiateNewButton(copyOfI);
        }
        ShowPointInfo(1);
    }
}
