using UnityEngine;
using System.IO;
using System.Xml.Serialization;
public class GameUtility {

    public const float ResolutionDelayTime = 1;
    public const string SavePrefKey = "Game_Highscore_Value";

    public const string xmlFileName = "Questions_Data.xml";
    public static string xmlFilePath
    {
        get
        {
            return Application.dataPath + "/" + xmlFileName;
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
}