using UnityEngine;

[RequireComponent(typeof(Position))]
public class Orb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TileManager tileManager;

    [SerializeField]
    private TileColor color;

    public TileColor Color
    {
        get { return color; }
        set
        {
            color = value;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = SpriteDictionary.Instance.orbSpriteDictionary.GetSprite(color);
            }
        }
    }

    public Position pos;

    protected void Awake()
    {
        pos = GetComponent<Position>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileManager = TileManager.Instance;;
    }

    protected void Start()
    {
        GameStateManager.Instance.AddOrb(this);
        tileManager.SetTileDataAndFill(pos.X, pos.Y, Color);
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.RemoveOrb(this);
    }
}