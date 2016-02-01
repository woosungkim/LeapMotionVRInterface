using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System.IO;
public class PlanController: MonoBehaviour
{

    WebCamTexture wtex;
    Texture2D boxTex;

    // Use this for initialization
    void Start()
    {

        print("start");
        print(this.GetComponent<Renderer>().material);
        print(WebCamTexture.devices.Length);


        //캡처가 저장될 cube Object
        GameObject box = GameObject.Find("Plane");
        //캡처를 할 실제 텍스쳐 생성과 적용
        boxTex = new Texture2D(640, 480, TextureFormat.ARGB32, false);
        box.GetComponent<Renderer>().material.mainTexture = boxTex;

        if (WebCamTexture.devices.Length < 1)
            return;

        wtex = new WebCamTexture(WebCamTexture.devices[0].name, 640, 480, 30);
        this.GetComponent<Renderer>().material.mainTexture = wtex;
        wtex.Play();
    }

    void OnGUI()
    {
        /*
        if (GUI.Button(new Rect(100, 100, 200, 200), "촬영"))
        {
            saveTex();
        }
         */ 
    }

    void saveTex()
    {
        print(wtex.width + " " + wtex.height + " " + wtex.GetPixels().Length);
        boxTex.SetPixels(wtex.GetPixels());
        //중요
        boxTex.Apply();

        //이미지의 저장
        byte[] savedata = boxTex.EncodeToPNG();
        FileStream fs = System.IO.File.Create(@"C:\Apache24\htdocs\uploads\unitytest.png");
        fs.Write(savedata, 0, savedata.Length);
        fs.Close();

    }

    // Update is called once per frame
    Vector3 rotVec = new Vector3(0, 1, 0);
    void Update()
    {
        //this.transform.Rotate(rotVec);
    }
}
