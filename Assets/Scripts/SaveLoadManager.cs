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
    public SaveData() : this(0, 0) { }
}

public class SaveLoadManager : MonoBehaviour {

    Stream stream_read, stream_write;
    StreamWriter streamWriter;
    StreamReader streamReader;

    string filePath = "savedata/save.json";
    string json;
    SaveData loadedStage, currentStage;

    void Awake()
    {
        loadedStage = new global::SaveData();
        currentStage = new global::SaveData();
    }

	// Use this for initialization
	void Start () {
        // File Load

        if (File.Exists(filePath))
        {
            stream_read = File.Open(filePath, FileMode.Open);
            streamReader = new StreamReader(stream_read);
            json = streamReader.ReadToEnd();
            loadedStage = JsonHelper.Deserialize<SaveData>(json);
            streamReader.Close();
            stream_read.Close();
        }
        else
        {
            stream_write = File.Create(filePath);
            streamWriter = new StreamWriter(stream_write);
            loadedStage = new SaveData(1, 1);
            json = JsonHelper.Serialize<SaveData>(loadedStage);
            streamWriter.Write(json);
            streamWriter.Close();
            stream_write.Close();
        }
    }


    public void SetCurrentLevel(string currentlevel)
    {
        char[] delim = { '-' };
        string[] levelInfo = currentlevel.Split(delim);

        Debug.Log(levelInfo[0].ToString());
        Debug.Log(levelInfo[1].ToString());

        currentStage.Chapter = System.Int32.Parse(levelInfo[0]);
        currentStage.Level = System.Int32.Parse(levelInfo[1]);
    }

    bool LevelCompare()
    {

        if (currentStage.Chapter > loadedStage.Chapter) return true;
        else if (currentStage.Chapter < loadedStage.Chapter) return false;
        else
        {
            if (currentStage.Level > loadedStage.Level) return true;
            else return false;
        }
    }
    

    public void SaveData(string currentLevel)
    {
        stream_write = File.Open(filePath, FileMode.Create);
        streamWriter = new StreamWriter(stream_write);

        char[] delim = { '-' };
        string[] levelInfo = currentLevel.Split(delim);

       
        if (LevelCompare())
        {
            json = JsonHelper.Serialize<SaveData>(currentStage);
        }         
        else
        {
            json = JsonHelper.Serialize<SaveData>(loadedStage);
        }

        streamWriter.Write(json);
        streamWriter.Close();
        stream_write.Close();
    }

    
}
