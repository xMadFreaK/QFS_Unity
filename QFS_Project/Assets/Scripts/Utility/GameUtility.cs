using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.Networking;
using System.Collections;
using System;

public class GameUtility {

    public const float ResolutionDelayTime = 1;
    public const string SavePrefKey = "Game_Highscore_Value";

    public static string xmlFileName; // = "Questions_Data.xml";

    public static string xmlFilePath
    {

        get
        {
            /* notwendiger Code zum öffnen einer .xml Datei in Android, benutzt UnityWebRequest, welches in UnityEngine.Networking; gespeichert ist. */
            var p = Path.Combine(Application.persistentDataPath, xmlFileName);
            var loadingRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, xmlFileName));
            loadingRequest.SendWebRequest();
            while (!loadingRequest.isDone)
            {
                if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
                {
                    break;
                }
            }
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {

            }
            else
            {
                File.WriteAllBytes(p, loadingRequest.downloadHandler.data);
            }
            return p;
            //return Path.Combine(Application.streamingAssetsPath, xmlFileName);
            //return Application.dataPath + "/" + "Resources" + "/" + xmlFileName;
        }
    }
}
[System.Serializable()]
public class Data
{

    public Question[] Questions = new Question[0];

    public Data () { }

    public static void Write(Data data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }
    public static Data Fetch()
    {
        return Fetch(out bool result);
    }
    public static Data Fetch(out bool result)
    {
        var quizz = Quizz.getQuiz(ButtonScript.vidSavePath);
        Data data = new Data();
        Array.Resize(ref data.Questions, quizz.Length);
        data.Questions = quizz;
        result = true;
        return data;
    }
}