using UnityEngine;

[RequireComponent(typeof(Position))]
public class Orb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

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
    }

    protected void Start()
    {
        GameStateManager.Instance.AddOrb(this);
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.RemoveOrb(this);
    }
}