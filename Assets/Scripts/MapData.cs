[System.Serializable]
public struct MapData
{
    public string name;
    public int width;
    public int height;
    public int[] tiles;
    public PlayerData playerData;
    public MonsterData[] monsters;
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