using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Utils;

public class SaveData
{
    public int Chapter { get; set; }
    public int Level { get; set; }

    public SaveData(string levelName)
    {
        char[] delim = { '-' };
        string[] levelInfo = levelName.Split(delim);
        

        Chapter = System.Int32.Parse(levelInfo[0]);
        Level = System.Int32.Parse(levelInfo[1]);
    }
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

    string filePath = "/save.json";
    string json;
    SaveData loadedStage, currentStage;

    void Awake()
    {
        loadedStage = new SaveData();
        currentStage = new SaveData();

        //File load
        if (File.Exists(Application.persistentDataPath + filePath))
        {
            stream_read = File.Open(Application.persistentDataPath + filePath, FileMode.Open);
            streamReader = new StreamReader(stream_read);
            json = streamReader.ReadToEnd();
            loadedStage = JsonHelper.Deserialize<SaveData>(json);
            streamReader.Close();
            stream_read.Close();
        }
        else
        {
            stream_write = File.Create(Application.persistentDataPath + filePath);
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
        

        currentStage.Chapter = System.Int32.Parse(levelInfo[0]);
        currentStage.Level = System.Int32.Parse(levelInfo[1]);
    }

    public bool LevelCompare(SaveData currentStage)
    {
        if (loadedStage == null)
        {
            Debug.Log("loadedstage is null");
            return true;
        }

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
        stream_write = File.Open(Application.persistentDataPath + filePath, FileMode.Create);
        streamWriter = new StreamWriter(stream_write);

        SetCurrentLevel(currentLevel);
 
        if (LevelCompare(currentStage))
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
