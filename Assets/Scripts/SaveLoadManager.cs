using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Utils;

public class SaveData
{
    public int Chapter { get; set; }
    public int Level { get; set; }

    public SaveData(int chapter, int level)
    {
        Chapter = chapter;
        Level = level;
    }
}

public class SaveLoadManager : MonoBehaviour {

    Stream stream_read, stream_write;
    StreamWriter streamWriter;
    StreamReader streamReader;

    string filePath = "/savedata/save.json";
    string json;
    SaveData dat;

	// Use this for initialization
	void Start () {
        // File Load
        if (File.Exists(filePath))
        {
            stream_read = File.Open(filePath, FileMode.Open);
            streamReader = new StreamReader(stream_read);
            json = streamReader.ReadToEnd();
            dat = JsonHelper.Deserialize<SaveData>(json);
            streamReader.Close();
            stream_read.Close();
        }
        else
        {
            stream_read = File.Create(filePath);
            stream_read.Close();
        }
    }

    int LevelCompare(string a, string b)
    {
        char[] delim = { '-' };
        string[] a_split = a.Split(delim);
        string[] b_split = b.Split(delim);

        if (System.Int32.Parse(a_split[0]) > System.Int32.Parse(b_split[0])) return 1;
        else if (System.Int32.Parse(a_split[0]) < System.Int32.Parse(b_split[0])) return -1;
        else
        {
            if (System.Int32.Parse(a_split[1]) > System.Int32.Parse(b_split[1])) return 1;
            else if (System.Int32.Parse(a_split[1]) < System.Int32.Parse(b_split[1])) return -1;
            else return 0;
        }
    }

    void SaveData(string currentLevel)
    {
        stream_write = File.Open(filePath, FileMode.Create);
        streamWriter = new StreamWriter(stream_write);

        char[] delim = { '-' };
        string[] levelInfo = currentLevel.Split(delim);

        dat.Chapter = System.Int32.Parse(levelInfo[0]);
        dat.Level = System.Int32.Parse(levelInfo[1]);

        json = JsonHelper.Serialize<SaveData>(dat);

        streamWriter.Write(json);
        streamWriter.Close();
        stream_write.Close();
    }

    void LoadData()
    {
        
    }
}
