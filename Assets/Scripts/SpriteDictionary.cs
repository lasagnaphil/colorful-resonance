using System;
using System.Collections.Generic;
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

public class SpriteDictionary : Singleton<SpriteDictionary>
{
    public TileSpriteDictionary tileSpriteDictionary;
    public OrbSpriteDictionary orbSpriteDictionary;
}