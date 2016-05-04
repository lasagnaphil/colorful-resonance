public struct MapData
{
    public string name;
    public int width;
    public int height;
    public int[] tiles;
    public MonsterData[] monsters;
}

public struct MonsterData
{
    public string name;
    public Vector2i position;
}