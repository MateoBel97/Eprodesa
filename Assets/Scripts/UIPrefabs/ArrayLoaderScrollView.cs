using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrayLoaderScrollView : MonoBehaviour
{
    [SerializeField] string array;
    [SerializeField] string title;
    [SerializeField] GameObject[] stringInputFieldObjects;

    private TextMeshProUGUI pointText;
    private GameObject buttonTemplate;
    private GameObject scrollViewContent;
    private Button removeButton;
    private Button addButton;
    private int currentIndex;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting " + transform.name);
        buttonTemplate = transform.GetChild(0).gameObject;
        buttonTemplate.SetActive(false);
        scrollViewContent = transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
        pointText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        pointText.text = title;
        ShowArrayInfo();
        removeButton = transform.GetChild(3).GetComponent<Button>();
        addButton = transform.GetChild(4).GetComponent<Button>();
        removeButton.onClick.AddListener(delegate { RemoveFromArray(); });
        addButton.onClick.AddListener(delegate { AddToArray(); });

        foreach (GameObject stringIputFieldObject in stringInputFieldObjects)
        {
            stringIputFieldObject.SetActive(true);
        }
    }

    void ShowArrayInfo()
    {
        switch(array)
        {
            case "measurementPoints":
                int numPoints = Measurement.measurementPoints.Count;
                if (numPoints != 0)
                {
                    int pointCount = 0;
                    foreach(MeasurementPoint point in Measurement.measurementPoints)
                    {
                        int num = ++pointCount;
                        InstantiateNewButton(num);
                    }
                }
                else 
                {
                    foreach(GameObject stringIputFieldObject in stringInputFieldObjects)
                    {
                        stringIputFieldObject.GetComponent<StringInputField>().indexToUpdate = -1;
                    }
                }
                
                break;
            case "externalEvents":
                break;
            default:
                Debug.Log("Arreglo no encontrado");
                break;
        }
    }

    void InstantiateNewButton(int pointToShow)
    {
        var copy = Instantiate(buttonTemplate);
        copy.name = "Copy" + pointToShow.ToString();
        copy.transform.SetParent(scrollViewContent.transform);
        copy.transform.localScale = new Vector3(1f, 1f, 1f);
        copy.transform.gameObject.SetActive(true);
        copy.GetComponentInChildren<TextMeshProUGUI>().text = "Punto " + pointToShow;
        copy.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                currentIndex = pointToShow - 1;
                foreach(GameObject gameObject in stringInputFieldObjects)
                {
                    gameObject.GetComponent<StringInputField>().indexToUpdate = currentIndex;
                }
                ShowInfoAtCurrentIndex();
            }
            );
    }

    void ShowInfoAtCurrentIndex()
    {
        pointText.text = title + " " + (currentIndex == -1 ? "" : (currentIndex + 1).ToString());   
        switch(array)
        {
            case "measurementPoints":
                string type = "";
                foreach(GameObject gameObject in stringInputFieldObjects)
                {
                    gameObject.GetComponent<StringInputField>().ShowParameter();
                }
                break;
            case "externalEvents":
                break;
            default:
                break;
        }
    }

    void RemoveFromArray()
    {
        int count;
        switch (array)
        {
            case "measurementPoints":
                count = Measurement.measurementPoints.Count;
                if (count > 0)
                {
                    GameObject buttonToDelete = GameObject.Find("Copy" + count);
                    GameObject.Destroy(buttonToDelete);
                    Measurement.measurementPoints.RemoveAt(currentIndex);

                    if ((currentIndex + 1) == count)
                    {
                        currentIndex--;
                        
                    }
                    foreach (GameObject stringIputFieldObject in stringInputFieldObjects)
                    {
                        stringIputFieldObject.GetComponent<StringInputField>().indexToUpdate = currentIndex;
                    }
                    ShowInfoAtCurrentIndex();
                }
                break;
        }
    }

    public void AddToArray()
    {
        switch(array)
        {
            case "measurementPoints":
                int count = Measurement.measurementPoints.Count;
                currentIndex = count;
                Measurement.measurementPoints.Add(new MeasurementPoint("", "", ""));
                foreach (GameObject stringIputFieldObject in stringInputFieldObjects)
                {
                    stringIputFieldObject.GetComponent<StringInputField>().indexToUpdate = currentIndex;
                }
                InstantiateNewButton(++count);
                ShowInfoAtCurrentIndex();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}