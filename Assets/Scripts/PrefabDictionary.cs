﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Buttons;
using Items;

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

[System.Serializable]
public class BossPrefabTuple
{
    public string name;
    public BigMonster bossPrefab;
}

[System.Serializable]
public class BossPrefabDictionary
{
    [SerializeField] private List<BossPrefabTuple> bossPrefabTupleList;

    public BigMonster GetBoss(string name)
    {
        return bossPrefabTupleList.Find(x => x.name == name).bossPrefab;
    }
}

[System.Serializable]
public class ItemPrefabTuple
{
    public string name;
    public GameItem gameItemPrefab;
}

[System.Serializable]
public class ItemPrefabDictionary
{
    [SerializeField] private List<ItemPrefabTuple> gameItemTuples;

    public GameItem GetItem(string name)
    {
        return gameItemTuples.Find(x => x.name == name).gameItemPrefab;
    }
}

public class PrefabDictionary : Singleton<PrefabDictionary>
{
    public MonsterPrefabDictionary monsterPrefabDictionary;
    public ButtonPrefabDictionary buttonPrefabDictionary;
    public ItemPrefabDictionary itemPrefabDict;
    public Tile tilePrefab;
    public Orb orbPrefab;
    public GameObject escapeTeleporterPrefab;
    public GameObject keyUnlockerPrefab;

    public BossPrefabDictionary bossPrefabDict;
}