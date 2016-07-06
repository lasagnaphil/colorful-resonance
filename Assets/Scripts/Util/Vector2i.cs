[System.Serializable]
public struct Vector2i
{
    public int x, y;
    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector2i(Vector2i vec) : this(vec.x, vec.y) { }

    public static Vector2i operator +(Vector2i v1, Vector2i v2)
    {
        return new Vector2i(v1.x + v2.x, v1.y + v2.y);
    }
    public static Vector2i operator -(Vector2i v1, Vector2i v2)
    {
        return new Vector2i(v1.x - v2.x, v1.y - v2.y);
    }
    public static int operator *(Vector2i v1, Vector2i v2)
    {
        return v1.x*v2.x + v1.y*v2.y;
    }

    public static bool operator ==(Vector2i v1, Vector2i v2)
    {
        return (v1.x == v2.x) && (v1.y == v2.y);
    }

    public static bool operator !=(Vector2i v1, Vector2i v2)
    {
        return !(v1 == v2);
    }
}