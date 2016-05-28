using UnityEngine;
using System.Collections;

public enum TileColor
{
    Black, White, Red, Blue, Yellow, Green, None
}

public enum TileType
{
    Normal, Wall, None
}

[System.Serializable]
public class TileData
{
    public TileColor color;
    public TileType type;
   
    public TileData() : this(TileColor.None, TileType.Normal) { }
    public TileData(TileColor color, TileType type)
    {
        this.type = type;
        this.color = color;
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
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = SpriteDictionary.Instance.tileSpriteDictionary.GetSprite(_data);
                if (_data.type == TileType.Wall) spriteRenderer.sortingLayerName = "WallTile";
                else spriteRenderer.sortingLayerName = "Tile";
            }
        }
    }

    [HideInInspector]
    public Position pos;

    public bool Marked { get; set; }

    private bool activated = false;
    public bool Activated
    {
        get { return activated; }
        set
        {
            activated = value;
            if (spriteRenderer != null) UpdateSprite();
        }
    }

    // Always get the reference of Position on Awake() function
    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

    private void UpdateSprite()
    {
	    spriteRenderer.sprite = SpriteDictionary.Instance.tileSpriteDictionary.GetSprite(_data);
        if (_data.type == TileType.Wall) spriteRenderer.sortingLayerName = "WallTile";
        else spriteRenderer.sortingLayerName = "Tile";
    }

	void Start()
	{
	    tileManager = TileManager.Instance;
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    transform.position = new Vector3(pos.X, pos.Y);

        UpdateSprite();
        UpdateColorIndex();
        
	    GameStateManager.Instance.TileTurns += OnTurn;
	}
    
    public void UpdateColorIndex()
    {
        int newColorIndex = (int)_data.color;
        if (_data.type == TileType.Wall)
            newColorIndex += 10;            
        if (_data.type == TileType.None && _data.color == TileColor.None)
            newColorIndex = -1;
        gameObject.GetComponent<Animator>().SetInteger("colorIndex", newColorIndex);    
    }

    protected void OnTurn()
    {
        if (Activated) Activated = false;
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.TileTurns -= OnTurn;
        }
    }
    
}
