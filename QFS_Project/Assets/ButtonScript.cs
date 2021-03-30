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
        var uwr = new UnityWebRequest("http://188.194.230.87:443/Quizzes/" + filet.text + ".xml" , UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath +  "/StreamingAssets/" + filet.text + ".xml");
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
     
    }
    
    // Start is called before the first frame update
    
    public void updateQuizSelect()
    {

        //holder = filetext.text;
        GameUtility.xmlFileName = filet.text + ".xml" ;
        //GameUtility.xmlFileName = QuizInputField.Text;
       YOLO();
        print("warte alder");
        Debug.Log("Data is fetched and ready to use");
        
    }
    // Update is called once per frame
    
}

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
}


/*
public class ClickExample : MonoBehaviour
{
    public TMPButton SelectionButton;

    void Start()
    {
        Button btn = SelectionButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
    }
}*/