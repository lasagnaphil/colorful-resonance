using FullSerializer;
// using UnityEditor;

[System.Serializable, fsObject(Converter = typeof(MapDataConverter))]
public class MapData
{
    public string name;
    public string comment;
    public WinCondition winCondition;
    public int width;
    public int height;
    public char[] tiles;
    public string background;
    public PlayerData playerData;
    public MonsterData[] monsters;
    public OrbData[] orbs;
    public ButtonData[] buttons;
}

[System.Serializable]
public class PlayerData
{
    public Vector2i position;
}

[System.Serializable]
public class MonsterData
{
    public string name;
    public Vector2i position;
}

[System.Serializable]
public class OrbData
{
    public string color;
    public Vector2i position;
}

[System.Serializable]
public class ButtonData
{
    public string name;
    public Vector2i position;
    public Vector2i togglePosition;
    public bool isWallOnButtonOff;
}