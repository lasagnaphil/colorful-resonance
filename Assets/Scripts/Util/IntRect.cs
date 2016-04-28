public struct IntRect
{
    public int x1;
    public int y1;
    public int x2;
    public int y2;

    public IntRect(int x1, int y1, int x2, int y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }

    public int GetWidth()
    {
        return x2 - x1 + 1;
    }

    public int GetHeight()
    {
        return y2 - y1 + 1;
    }

    public Vector2i GetSize()
    {
        return new Vector2i(GetWidth(), GetHeight());
    }
}