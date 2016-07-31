using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TileSpriteTuple
{
    public TileData tileData;
    public Sprite sprite;
}

[Serializable]
public class TileSpriteDictionary
{
    [SerializeField]
    private List<TileSpriteTuple> tupleList;

    public Sprite GetSprite(TileData tileData)
    {
        return tupleList.Find(x => x.tileData == tileData).sprite;
    }

    public Dictionary<TileData, Sprite> ToDictionary()
    {
        return tupleList.ToDictionary(t => t.tileData, t => t.sprite);
    }
}

[System.Serializable]
public class OrbSpriteTuple
{
    public TileColor color;
    public Sprite orbSprite;
}

[System.Serializable]
public class OrbSpriteDictionary
{
    [SerializeField] private List<OrbSpriteTuple> orbSpriteTupleList;

    public Sprite GetSprite(TileColor color)
    {
        return orbSpriteTupleList.Find(x => x.color == color).orbSprite;
    }
}

[System.Serializable]
public class BackgroundSpriteTuple
{
    public string name;
    public Sprite bkgSprite;
}

[System.Serializable]
public class BackgroundSpriteDictionary
{
    [SerializeField] private List<BackgroundSpriteTuple> bkgSpriteTupleList;

    public Sprite GetSprite(string name)
    {
        return bkgSpriteTupleList.Find(x => x.name == name).bkgSprite;
    }
}

public class SpriteDictionary : Singleton<SpriteDictionary>
{
    public TileSpriteDictionary tileSpriteDictionary;
    public OrbSpriteDictionary orbSpriteDictionary;
    public BackgroundSpriteDictionary bkgSpriteDictionary;
}