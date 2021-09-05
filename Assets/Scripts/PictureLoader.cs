using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class PictureLoader : MonoBehaviour
{
    [SerializeField] Image spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //LoadPicture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPicture()
    {
        //Texture2D tex = new Texture2D(2, 2);
#if UNITY_EDITOR_WIN
        string chosenFilePath = EditorUtility.OpenFilePanel("Load png Textures", "", "");

#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        
#elif UNITY_ANDROID
        chosenFilePath = Application.persistentDataPath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt";
        
#endif
        //NativeGallery.Permission NativeGallery.GetImageFromGallery(MediaPickCallback callback, string title = "", string mime = "image/*");
        spriteRenderer.sprite = GetSpritefromImage(chosenFilePath);
        spriteRenderer.preserveAspect = true;



    }

    private Sprite GetSpritefromImage(string imgPath)
    {

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(imgPath);
        Debug.Log(pngBytes[0].ToString() + " " + pngBytes[1].ToString());

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return fromTex;

    }
}
