using DG.Tweening;
using UnityEngine;

// 2d integer vector component
// Use this instead of Unity's Transform to move inside fixed 2D grid

public enum SpriteLayer
{
    Tile, Button, Orb, Aura, Monster, Player, WallTile, Projectile, Effect
}

public class Position : MonoBehaviour
{
    [SerializeField] private int x;

    public int X
    {
        get { return x; }
        set
        {
            x = value;
            transform.position = new Vector3(x, y, z);
        }
    }

    [SerializeField] private int y;

    public int Y
    {
        get { return y; }
        set
        {
            y = value;
            SetDepth(y);
            transform.position = new Vector3(x, y, z);
        }
    }

    [SerializeField] private SpriteLayer spriteLayer;

    public SpriteLayer SpriteLayer
    {
        get { return spriteLayer; }
        set
        {
            spriteLayer = value;
            // supported up to 100 different layers
            z = Mathf.Floor(z) + (99 - (float)spriteLayer)*0.01f;
            transform.position = new Vector3(x, y, z);
        }
    }

    [SerializeField] private float z;

    public void SetDepth(int depth)
    {
        z = (depth) + (z - Mathf.Floor(z));
    }

    protected void Awake()
    {
        z = Mathf.Floor(z) + (99 - (float) spriteLayer)*0.01f;
        SetDepth(y);
        transform.position = new Vector3(x, y, z);
    }

    public Vector2 GetVector2()
    {
        return new Vector2((float) x, (float) y);
    }

    public Vector2i GetVector2i()
    {
        return new Vector2i(x, y);
    }

    public Vector3 GetVector3()
    {
        return new Vector3((float) x, (float) y, z);
    }

    public void Set(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Tween AnimatedMove(int x, int y, float duration)
    {
        SetDepth(y);
        this.x = x;
        this.y = y;
        return transform.DOMove(GetVector3(), duration);
    }

    public Tween AnimatedMove(Vector2i vec, float duration)
    {
        return AnimatedMove(vec.x, vec.y, duration);
    }

    public void Set(Vector2i vec)
    {
        X = vec.x;
        Y = vec.y;
    }

    public void Add(Vector2i vec)
    {
        X += vec.x;
        Y += vec.y;
    }
} 