using FullInspector;
using UnityEngine;

// 2d integer vector component
// Use this instead of Unity's Transform to move inside fixed 2D grid

public class Position : BaseBehavior
{
    [SerializeField]
    private int x;
    public int X
    {
        get { return x; }
        set
        {
            x = value;
            transform.position = new Vector2(x, y);
        }
    } 

    [SerializeField]
    private int y;
    public int Y
    {
        get { return y; }
        set
        {
            y = value;
            transform.position = new Vector2(x, y);
        }
    }

    public Vector2 GetVector()
    {
        return new Vector2((float)x, (float)y);
    }
}