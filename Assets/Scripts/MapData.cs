[System.Serializable]
public struct MapData
{
    public string name;
    //public string winCondition;
    public int width;
    public int height;
    public string[] tiles;
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