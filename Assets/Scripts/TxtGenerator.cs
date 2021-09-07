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
        text = Measurement.CreateTxt();
    }

    public void ExportFile()
    {

#if UNITY_EDITOR_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "C:/Users/Mateo/Desktop", "");
        File.WriteAllText(chosenFilePath + "/" + Measurement.companyName + "-" + Measurement.workOrder + ".txt", text);
#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "C:/Users/Mateo/Desktop", "texto.txt");
        File.WriteAllText(chosenFilePath + "/" + Measurement.companyName + "-" + Measurement.workOrder + ".txt", text);
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath + "/" + Measurement.companyName + "-" + Measurement.workOrder + ".txt";
        File.WriteAllText(chosenFilePath, text);
        //textTMP.text = chosenFilePath;
        StartCoroutine(ShareFiles());
#endif

    }

    private IEnumerator ShareFiles()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare().AddFile(chosenFilePath).AddFile(Measurement.picturePath)
            .SetSubject("FORMATO DE CAMPO " + Measurement.companyName).SetText("Formato de Campo generado mediante la app móvil.\n\n").SetUrl("https://www.eprodesaong.com/")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

}
