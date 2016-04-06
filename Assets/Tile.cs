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

    public TileType Type { get; set; }

    [HideInInspector]
    public Position pos;

    // Always get the reference of Position on Awake() function
    void Awake()
    {
        base.Awake();
        pos = GetComponent<Position>();
    }

	void Start()
	{
	    tileManager = TileManager.Instance.GetComponent<TileManager>();
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    transform.position = new Vector3(pos.X, pos.Y);
	    spriteRenderer.sprite = tileManager.tileDict[Type];
	}
}
