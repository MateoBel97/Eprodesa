using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;


public class TxtGenerator : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textTMP;

    string chosenFilePath = "";
    string text;

    // Start is called before the first frame update
    void Start()
    {
        text = GlobalData.CreateTxt();
    }
    
    public void ExportFile()
    {

#if UNITY_EDITOR_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt", text);
#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        File.WriteAllText(chosenFilePath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt", text);
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt";
        StartCoroutine(TakeScreenshotAndShare());
#endif

    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare().AddFile(chosenFilePath)//.AddFile("/path/to/another/file.txt")
            .SetSubject("FORMATO DE CAMPO " + GlobalData.companyName).SetText("Formato de Campo generado mediante la app móvil.\n\n").SetUrl("https://www.eprodesaong.com/")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

}
