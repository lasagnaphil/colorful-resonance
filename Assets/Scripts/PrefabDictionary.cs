using UnityEngine;
using System.Collections.Generic;

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
}

public class PrefabDictionary : Singleton<PrefabDictionary>
{
    public MonsterPrefabDictionary monsterPrefabDictionary;
    public Tile tilePrefab;
    public Orb orbPrefab;
}