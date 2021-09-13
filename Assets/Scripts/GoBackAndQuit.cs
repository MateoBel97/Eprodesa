using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackAndQuit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GetComponent<SceneLoader>().LoadScene(sceneToLoad);
            }
        }
    }
}
