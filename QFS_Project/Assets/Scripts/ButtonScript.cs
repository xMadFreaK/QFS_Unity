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
    public bool doesitwork = false;

    IEnumerator DownloadFile()
    {
        string url = "http://188.193.204.54:443/Quizzes/" + filet.text + ".csv";
        //Der Pfad, indem die heruntergeladenen Quizze gespeichert werden
        vidSavePath = Path.Combine(Application.persistentDataPath, filet.text + ".csv");

        //Erstellt das Verzeichnis, falls es nicht existiert
        if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
        }
        //Unity Webrequest
        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(vidSavePath);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            print("Server nicht erreichbar");
        }
        else
        {
            Debug.Log("Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
            print("Download erfolgreich");
            doesitwork = true;
        }
        if (doesitwork)
        {
            print(vidSavePath);
            SelectionButton.SetActive(true);
        }
    }

    public void updateQuizSelect()
    {

        //holder = filetext.text;
        GameUtility.xmlFileName = filet.text + ".xml";
        //GameUtility.xmlFileName = QuizInputField.Text;

        print("warte alder");

        StartCoroutine(DownloadFile());
        Debug.Log("Data is fetched and ready to use");
    }
    public void Quiz()
    {
        Quizz.Ausprobieren();
    }
}