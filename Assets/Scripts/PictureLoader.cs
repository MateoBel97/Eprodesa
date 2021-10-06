using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PictureLoader : MonoBehaviour
{
    [SerializeField] Image spriteRenderer;
    [SerializeField] TextMeshProUGUI textTMP;

    int maxSize = 1024;

    // Start is called before the first frame update
    void Start()
    {
        if(Measurement.picturePath != null)
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(Measurement.picturePath, maxSize);
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
            spriteRenderer.preserveAspect = true;
            //textTMP.text = Measurement.picturePixels.Length.ToString();
        }
        //LoadPicture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPicture()
    {
        //Texture2D tex = new Texture2D(2, 2);
        string chosenFilePath;
#if UNITY_EDITOR_WIN
        chosenFilePath = EditorUtility.OpenFilePanel("Load png Textures", "", "");
        spriteRenderer.sprite = GetSpritefromImage(chosenFilePath);
        spriteRenderer.preserveAspect = true;
        Measurement.picturePath = chosenFilePath;
        Measurement.picturePixels = File.ReadAllBytes(chosenFilePath);

#elif UNITY_STANDALONE_WIN
        chosenFilePath = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        spriteRenderer.sprite = GetSpritefromImage(chosenFilePath);
        spriteRenderer.preserveAspect = true;
        Measurement.picturePath = chosenFilePath;
        Measurement.picturePixels = File.ReadAllBytes(chosenFilePath);
        
#elif UNITY_ANDROID
        //chosenFilePath = Application.persistentDataPath + "/" + GlobalData.companyName + "-" + GlobalData.workOrder + ".txt";
        
        PickImage(1024);
        
#endif
        //NativeGallery.Permission NativeGallery.GetImageFromGallery(MediaPickCallback callback, string title = "", string mime = "image/*");




    }

    private Sprite GetSpritefromImage(string imgPath)
    {

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(imgPath);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return fromTex;

    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                spriteRenderer.preserveAspect = true;
                //textTMP.text = path;
                Measurement.picturePath = path;
                Measurement.picturePixels = File.ReadAllBytes(path);

                //textTMP.text = Measurement.picturePixels.Length.ToString();
                //textTMP.text = path;
                /*
                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
                */
            }
        });

        Debug.Log("Permission result: " + permission);
    }

}
