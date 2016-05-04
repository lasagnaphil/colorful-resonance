using UnityEngine;
using System.Collections;

public enum TileColor
{
    Black, White, Red, Blue, Yellow, Green, None
}

public enum TileType
{
    Normal, Wall
}

[System.Serializable]
public class TileData
{
    public TileColor color;
    public TileType type;

    public TileData() : this(TileColor.None, TileType.Normal) { }
    public TileData(TileColor color, TileType type)
    {
        this.color = color;
        this.type = type;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        TileData tileData = obj as TileData;
        return Equals(tileData);
    }

    public bool Equals(TileData tileData)
    {
        if ((object) tileData == null)
        {
            return false;
        }

        return (color == tileData.color && type == tileData.type);
    }

    public override int GetHashCode()
    {
        return (int)color ^ (int)type;
    }

    public static bool operator ==(TileData t1, TileData t2)
    {
        if (ReferenceEquals(t1, t2)) return true;
        if (((object) t1 == null) || ((object) t2 == null)) return false;
        return (t1.color == t2.color && t1.type == t2.type);
    }

    public static bool operator !=(TileData t1, TileData t2)
    {
        return !(t1 == t2);
    }
}

[RequireComponent(typeof(Position))]
public class Tile : MonoBehaviour
{
    private TileManager tileManager;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private TileData _data;
    public TileData Data
    {
        get { return _data; }
        set
        {
            _data = value;
            if (spriteRenderer != null && tileManager != null)
                spriteRenderer.sprite = tileManager.tileSpriteDict.GetSprite(_data);
        }
    }

    [HideInInspector]
    public Position pos;

    [HideInInspector]
    public bool Marked { get; set; }

    // Always get the reference of Position on Awake() function
    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

	void Start()
	{
	    tileManager = TileManager.Instance;
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    transform.position = new Vector3(pos.X, pos.Y);
	    spriteRenderer.sprite = tileManager.tileSpriteDict.GetSprite(_data);
	}

    void Update()
    {
    }
}
