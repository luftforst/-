using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;


public class DataParsing : Singleton<DataParsing>
{
    public void WriteStringToFile(Dictionary<string, JSONNode> dict, string filename)
    {
        string path = PathForDocumentsFile("Assets/Resources/data/" + filename + ".json");

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter streamWriter = new StreamWriter(file);

        // serialize
        JSONNode serialize = new JSONClass();
        foreach (KeyValuePair<string, JSONNode> data in dict)
        {
            serialize.Add(data.Key, data.Value);
        }

        string str = null;
        str = serialize.ToString();

        streamWriter.WriteLine(str);

        streamWriter.Close();
        file.Close();
    }

    // 게임 내에서 저장된 파일 읽기
    // 정보가 변하는 파일을 읽음
    public JSONNode ReadStringFromFileOnStorage(string filename)
    {
        string path = PathForDocumentsFile("Assets/Resources/data/" + filename + ".json");
        if (path == null)
        {
            Debug.Log("path null");
            return null;
        }

        FileStream file = new FileStream(path, FileMode.Open);
        if (file == null)
        {
            Debug.Log("file null");
            return null;
        }
        StreamReader streamReader = new StreamReader(file);

        if (streamReader != null)
        {
            string str = null;
            str = streamReader.ReadToEnd();

            JSONNode node = JSON.Parse(str);

            streamReader.Close();
            file.Close();

            return node;
        }

        Debug.Log("Stream Reader Error");
        return null;
    }

    // 정보가 변하지 않는 파일을 읽음
    public JSONNode ReadStringFromFile(string filename)
    {
        TextAsset data = (TextAsset)Resources.Load("data/" + filename, typeof(TextAsset));

        if (data.text != null)
        {
            string str = data.text;
            JSONNode node = JSON.Parse(str);

            return node;
        }

        Debug.Log("Read Error");
        return null;
    }


    /* 
     * Application.persistentDataPath : /mnt/sdcard/Android/data/번들이름/files
     * Application.persistentDataPath : /data/data/번들이름/files/
     * Substring(int1, int2) : int1에서부터 int2까지의 문자열
     * LastIndexOf(string) : 마지막 string문자의 인덱스를 리턴
     */

    public string PathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }

        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }

}
