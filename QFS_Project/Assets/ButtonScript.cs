using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.Networking;


public class ButtonScript : MonoBehaviour
{
    
    public TMP_Text filetext;
    public Text filet;

    public void YOLO()
    {
        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile()
    {
        string xmlName = GameUtility.xmlFileName;
        var uwr = new UnityWebRequest("http://188.194.230.87:443/StreamingAssets/", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, xmlName);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();

    }

    // Start is called before the first frame update

    public void updateQuizSelect()
    {

        //holder = filetext.text;
        GameUtility.xmlFileName = filet.text;
        //GameUtility.xmlFileName = QuizInputField.Text;
        print(filet.text);
        YOLO();
        print("warte alder");
       
        
    }
    // Update is called once per frame

}
/*
public class FileDownloader : MonoBehaviour
{

    public void YOLO()
    {
        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile()
    {
        string xmlName = GameUtility.xmlFileName;
        var uwr = new UnityWebRequest("http://188.194.230.87:443/StreamingAssets/", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, xmlName);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();

    }
}*/

/*IEnumerator DownloadFile()
{
    var uwr = new UnityWebRequest("https://unity3d.com/", UnityWebRequest.kHttpVerbGET);
    string path = Path.Combine(Application.persistentDataPath, "unity3d.html");
    uwr.downloadHandler = new DownloadHandlerFile(path);
    yield return uwr.SendWebRequest();
    if (uwr.result != UnityWebRequest.Result.Success)
        Debug.LogError(uwr.error);
    else
        Debug.Log("File successfully downloaded and saved to " + path);
}*/