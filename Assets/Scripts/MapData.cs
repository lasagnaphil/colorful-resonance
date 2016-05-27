using FullSerializer;

[System.Serializable, fsObject(Converter = typeof(MapDataConverter))]
public struct MapData
{
    public string name;
    public WinCondition winCondition;
    public int width;
    public int height;
    public char[] tiles;
    public PlayerData playerData;
    public MonsterData[] monsters;
    public OrbData[] orbs;
}

[System.Serializable]
public struct PlayerData
{
    public Vector2i position;
}

[System.Serializable]
public struct MonsterData
{
    public string name;
    public Vector2i position;
}

[System.Serializable]
public struct OrbData
{
    public string color;
    public Vector2i position;
}