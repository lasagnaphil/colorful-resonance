using UnityEngine;
using System.Collections;
using FullInspector;

public enum TileType
{
    None, Yellow, Blue, Green, Red, Purple, Black
}

[RequireComponent(typeof(Position))]
public class Tile : BaseBehavior
{
    private TileManager tileManager;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private TileType type;
    public TileType Type
    {
        get { return type; }
        set
        {
            type = value;
            if (spriteRenderer != null && tileManager != null)
                spriteRenderer.sprite = tileManager.tileDict[type];
        }
    }

    [HideInInspector]
    public Position pos;

    [HideInInspector]
    public bool Marked { get; set; }

    // Always get the reference of Position on Awake() function
    protected override void Awake()
    {
        base.Awake();
        pos = GetComponent<Position>();
    }

	void Start()
	{
	    tileManager = TileManager.Instance;
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    transform.position = new Vector3(pos.X, pos.Y);
        spriteRenderer.sprite = tileManager.tileDict[type];
	}

    void Update()
    {
        // if (Marked) spriteRenderer.color = Color.blue;
    }
}
