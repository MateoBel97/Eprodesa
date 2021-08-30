using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventsData : MonoBehaviour
{

    [SerializeField] GameObject buttonTemplate;
    [SerializeField] GameObject scrollviewContent;

    [SerializeField] TMP_InputField eventNameInputField;
    [SerializeField] TMP_InputField eventLevelInputField;
    [SerializeField] TMP_InputField eventTimeInputField;
    [SerializeField] TMP_InputField eventLengthInputField;
    [SerializeField] TextMeshProUGUI eventTMP;

    int pointsCount = 0;
    int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalData.eventsDataSaved)
        {
            AddNewEvent();
        }
        else
        {
            LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewEvent()
    {
        pointsCount++;
        currentPoint = pointsCount;
        int copyOfPointsCount = pointsCount;
        InstantiateNewButton(copyOfPointsCount);
        /*
         GlobalData.pointName.Add("");
        GlobalData.n.Add("");
        GlobalData.w.Add("");
        */
        GlobalData.eventName.Add("");
        GlobalData.eventLevel.Add(0.0f);
        GlobalData.eventTime.Add("");
        GlobalData.eventLength.Add(0.0f);
        ShowEventInfo(pointsCount);
    }

    public void RemoveEvent()
    {
        if (pointsCount > 1)
        {
            GameObject buttonToDelete = GameObject.Find("Copy" + pointsCount);
            GameObject.Destroy(buttonToDelete);

            GlobalData.eventName.RemoveAt(currentPoint - 1);
            GlobalData.eventLevel.RemoveAt(currentPoint - 1);
            GlobalData.eventTime.RemoveAt(currentPoint - 1);
            GlobalData.eventLength.RemoveAt(currentPoint - 1);

            if (currentPoint == pointsCount)
            {
                currentPoint--;
            }
            pointsCount--;
            ShowEventInfo(currentPoint);
        }
    }

    public void UpdateEventName()
    {
        string text = eventNameInputField.text;
        GlobalData.eventName[currentPoint - 1] = text;
    }

    public void UpdateEventLevel()
    {
        string text = eventLevelInputField.text;
        GlobalData.eventLevel[currentPoint - 1] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }

    public void UpdateEventTime()
    {
        string text = eventTimeInputField.text;
        GlobalData.eventTime[currentPoint - 1] = text;
    }

    public void UpdateEventLength()
    {
        string text = eventLengthInputField.text;
        GlobalData.eventLength[currentPoint - 1] = (string.Equals(text, "") ? 0.0f : float.Parse(text));
    }


    void ShowEventInfo(int point)
    {
        currentPoint = point;
        eventTMP.text = "Evento " + currentPoint;
        string name = GlobalData.eventName[currentPoint - 1];
        eventNameInputField.text = (string.Equals("", name) ? "" : name);
        float level = GlobalData.eventLevel[currentPoint - 1];
        eventLevelInputField.text = (level == 0.0f ? "" : level.ToString());
        string time = GlobalData.eventTime[currentPoint - 1];
        eventTimeInputField.text = (string.Equals("", time) ? "" : time);
        float length = GlobalData.eventLength[currentPoint - 1];
        eventLengthInputField.text = (length == 0.0f ? "" : length.ToString());
    }

    public void SaveData()
    {
        GlobalData.eventsDataSaved = true;
        GlobalData.SaveTxt();
    }

    void InstantiateNewButton(int pointToShow)
    {
        var copy = Instantiate(buttonTemplate);
        copy.name = "Copy" + pointToShow;
        copy.transform.SetParent(scrollviewContent.transform);
        copy.transform.localScale = new Vector3(1f, 1f, 1f);
        copy.GetComponentInChildren<TextMeshProUGUI>().text = "Evento " + pointToShow;
        copy.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                ShowEventInfo(pointToShow);
            }
            );
    }

    void LoadData()
    {
        pointsCount = GlobalData.eventName.Count;

        for (int i = 1; i <= GlobalData.eventName.Count; i++)
        {
            int copyOfI = i;
            InstantiateNewButton(copyOfI);
        }
        ShowEventInfo(1);
    }


}
