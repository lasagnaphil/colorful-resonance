using UnityEngine;
using System.Collections;
using FullInspector;

public enum TileType
{
    None, Yellow, Blue, Green, Red, Purple, Black
}
public class Tile : BaseBehavior
{
    private TileManager tileManager;

    private SpriteRenderer spriteRenderer;

    public TileType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

	void Start ()
	{
	    tileManager = TileManager.Instance.GetComponent<TileManager>();
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    transform.position = new Vector3(X, Y);
	    spriteRenderer.sprite = tileManager.tileDict[Type];
	}
}
