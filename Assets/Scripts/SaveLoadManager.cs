using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Utils;

public class SaveData
{
    public string Level { get; set; }
}

public class SaveLoadManager : MonoBehaviour {

    StreamWriter w;
    StreamReader r;

    public int chapter { get; private set; }
    public int level { get; private set; }

    string filePath = "/savedata/save.dat";
    string json;

	// Use this for initialization
	void Start () {
        var saveData = new SaveData() { };
        json = JsonHelper.Serialize<SaveData>(saveData);
        var loadedSaveData = JsonHelper.Deserialize<SaveData>(json);
	}
	
	// Update is called once per frame
	void Update () {
	    
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
        Stream s = File.Open("/savedata/save.dat", FileMode.Create);
        w = new StreamWriter(s);
        w.Write(json);
        w.Close();
    }

    string LoadData()
    {
        return "";
    }
}
