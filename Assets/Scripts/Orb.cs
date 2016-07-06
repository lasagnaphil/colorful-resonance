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
    
    public GameObject[] orbEffectObjects;

    protected void Awake()
    {
        pos = GetComponent<Position>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileManager = TileManager.Instance;;
    }

    protected void Start()
    {
        GameStateManager.Instance.AddOrb(this);
        tileManager.SetTileColorAndFill(pos.X, pos.Y, Color);
        
        SetEffect(Color);
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.RemoveOrb(this);
    }
    
    void SetEffect(TileColor color)
    {
        int colorIndex = (int)color;
        if (colorIndex < orbEffectObjects.Length)
        {
            GameObject effectObject =
                Instantiate(orbEffectObjects[colorIndex], transform.position, Quaternion.identity) as GameObject;
            effectObject.transform.parent = gameObject.transform;
        }
        else
        {
            Debug.LogError("Cannot find orb effect object : index " + colorIndex);
        }
    }
}