using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.Networking;
public class GameUtility {

    public const float ResolutionDelayTime = 1;
    public const string SavePrefKey = "Game_Highscore_Value";

    public const string xmlFileName = "Q1.xml" { get; set; };
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

        if (!File.Exists(GameUtility.xmlFilePath)) { result = false; return new Data(); }
        XmlSerializer deserializer = new XmlSerializer(typeof(Data));
        using(Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Open))
        {
            var data = (Data)deserializer.Deserialize(stream);

            result = true;
            return data;
        }
    }

    //public static Data Load(/*Übergabe des Files*/)
    // {
    /* wenn Zielfile (xml Dokument) noch nicht existiert, dann soll es erzeugt werden.
     *  if (!File.Exists(GameUtility.xmlFilePath)) { result = false; return new Data(); }
    XmlSerializer deserializer = new XmlSerializer(typeof(Data));
    using(Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Open))
    {
        var data = (Data)deserializer.Deserialize(stream);

        result = true;
        return data;
    }
     * 
     * 
     * wenn in dem xml Dokument etwas existiert, wird es rausgelöscht
     * 
     * Fetchen der Daten des Übergabedokuments
     * 
     * Übergabe an das Zielfile (write)
     * 
     */

    // }


    /*public void UploadData() ---> lädt Files in eine Datenbank (und übersetzt csv in xml?)
     * 
     * 
     */
}