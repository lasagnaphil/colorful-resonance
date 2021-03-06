﻿using UnityEngine;
using Utils;
using Direction = Utils.Direction;

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

    public Vector2i Neighbor(Direction dir)
    {
        return this + DirectionHelper.ToVector2i(dir);
    }
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

    public Vector2 ToVector2()
    {
        return new Vector2((float)x, (float)y);
    }

    public Vector3 ToVector3(float z)
    {
        return new Vector3((float)x, (float)y, z);
    }

    public override int GetHashCode()
    {
        int hash = 23;
        hash = (hash*17) + x;
        hash = (hash*17) + y;
        return hash;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector2i vec = (Vector2i) obj;
        return this == vec;
    }
}