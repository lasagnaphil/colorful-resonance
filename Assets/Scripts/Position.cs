using FullInspector;
using UnityEngine;
using tk = FullInspector.tk<Position, FullInspector.tkDefaultContext>;

// 2d integer vector component
// Use this instead of Unity's Transform to move inside fixed 2D grid

public class Position : BaseBehavior//, tkCustomEditor
{
    /*
    tkControlEditor tkCustomEditor.GetEditor()
    {
        return new tkControlEditor( new tk.HorizontalGroup() {
            new tk.Label("Position"),
            tk.PropertyEditor.Create("X",
                (comp, context) => comp.X,
                (comp, context, edited) =>
                {
                    comp.X = edited;
                    transform.position = new Vector3(comp.X, transform.position.y);
                }),
            tk.PropertyEditor.Create("Y",
                (comp, context) => comp.Y,
                (comp, context, edited) =>
                {
                    comp.Y = edited;
                    transform.position = new Vector3(transform.position.x, comp.Y);
                })
        });
    }
    */

    [SerializeField] private int x;

    public int X
    {
        get { return x; }
        set
        {
            x = value;
            transform.position = new Vector2(x, y);
        }
    }

    [SerializeField] private int y;

    public int Y
    {
        get { return y; }
        set
        {
            y = value;
            transform.position = new Vector2(x, y);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        transform.position = new Vector2(x, y);
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
        return new Vector3((float) x, (float) y, 0f);
    }

    public void Set(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Add(Vector2i vec)
    {
        X += vec.x;
        Y += vec.y;
    }
} 