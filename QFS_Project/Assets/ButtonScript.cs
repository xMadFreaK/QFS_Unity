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
    public GameObject SelectionButton;
    public static string vidSavePath;
    public TMP_Text filetext;
    public Text filet;
    public bool sheeeesh = false;

    public void yolo()
    {
        StartCoroutine(DownloadFile());

        StopCoroutine(DownloadFile());
        int c = counting();
        if ( c != 0)

        {
            print(vidSavePath);
            SelectionButton.SetActive(true);
        }
    }

    IEnumerator DownloadFile()
    {
        /* var uwr = new UnityWebRequest("http://188.194.230.87:443/Quizzes/" + filet.text + ".xml" , UnityWebRequest.kHttpVerbGET);
         string path = Path.Combine(Application.persistentDataPath +  "/StreamingAssets/" + filet.text + ".xml");
         uwr.downloadHandler = new DownloadHandlerFile(path);
         yield return uwr.SendWebRequest(); */

        string url = "http://188.193.204.54:443/Quizzes/" + filet.text + ".csv";

        vidSavePath = Application.streamingAssetsPath;
        vidSavePath = Path.Combine(vidSavePath, filet.text + ".csv");

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))

        {
            Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));

        }


        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(vidSavePath);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            print("hallo");

        }
        else
        {
            Debug.Log("Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
            print("läuft");
            sheeeesh = true;
        }
    }
        public int counting()
    { 
        int counter = 0;
string line;

    // Read the file and display it line by line.  
    System.IO.StreamReader file =
        new System.IO.StreamReader(vidSavePath);  
while((line = file.ReadLine()) != null)  {

            System.Console.WriteLine(line);
    counter++;  
}
file.Close();
return counter;
    }

    

 

    public void updateQuizSelect()
    {

        //holder = filetext.text;
        GameUtility.xmlFileName = filet.text + ".xml";
        //GameUtility.xmlFileName = QuizInputField.Text;

        print("warte alder");
        
        yolo();
        Debug.Log("Data is fetched and ready to use");
        

    }
    // Update is called once per frame

    public void Quiz()
    {
        Quizz.Ausprobieren();
    }
}

/* public class FileDownloader : MonoBehaviour
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
*/

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