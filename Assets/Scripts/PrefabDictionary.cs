using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Buttons;

[System.Serializable]
public class ButtonPrefabTuple
{
    public string name;
    public Button buttonPrefab;
}

[System.Serializable]
public class ButtonPrefabDictionary
{
    [SerializeField] private List<ButtonPrefabTuple> buttonPrefabTupleList;

    public Button GetButton(string name)
    {
        return buttonPrefabTupleList.Find(x => x.name == name).buttonPrefab;
    }

    public Dictionary<string, Buttons.Button> ToDictionary()
    {
        return buttonPrefabTupleList.ToDictionary(t => t.name, t => t.buttonPrefab);
    }
}

[System.Serializable]
public class MonsterPrefabTuple
{
    public string name;
    public Monster monsterPrefab;
}

[System.Serializable]
public class MonsterPrefabDictionary
{
    [SerializeField]
    private List<MonsterPrefabTuple> monsterPrefabTupleList;

    public Monster GetMonster(string name)
    {
        return monsterPrefabTupleList.Find(x => x.name == name).monsterPrefab;
    }

    public Dictionary<string, Monster> ToDictionary()
    {
        return monsterPrefabTupleList.ToDictionary(t => t.name, t => t.monsterPrefab);
    }
}

public class PrefabDictionary : Singleton<PrefabDictionary>
{
    public MonsterPrefabDictionary monsterPrefabDictionary;
    public ButtonPrefabDictionary buttonPrefabDictionary;
    public Tile tilePrefab;
    public Orb orbPrefab;
    public EscapeTeleporter escapeTeleporterPrefab;
}