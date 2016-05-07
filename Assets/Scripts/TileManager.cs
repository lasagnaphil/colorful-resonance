using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileManager : Singleton<TileManager>
{
    [NonSerialized]
    public Tile[,] tiles;
    [NonSerialized]
    public int width;
    [NonSerialized]
    public int height;

    public Tile tilePrefab;

    private MapLoader mapLoader;

    protected void Awake()
    {
        mapLoader = GetComponent<MapLoader>();
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public void SetTileData(int x, int y, TileData data)
    {
        tiles[x, y].Data = data;
    }

    public void SetTileColor(int x, int y, TileColor color)
    {
        tiles[x, y].Data = new TileData(color, tiles[x, y].Data.type);
    }

    public void SetTileType(int x, int y, TileType type)
    {
        tiles[x, y].Data = new TileData(tiles[x, y].Data.color, type);
    }

    public void SetTileDataAndFill(int x, int y, TileData data)
    {
        SetTileData(x, y, data);
        FillTilesUsingContours(x, y, data.color);
    }

    public void SetTileColorAndFill(int x, int y, TileColor color)
    {
        SetTileColor(x, y, color);
        FillTilesUsingContours(x, y, color);
    }

    public TileData GetTileData(int x, int y)
    {
        return tiles[x, y].Data;
    }

    public TileColor GetTileColor(int x, int y)
    {
        return tiles[x, y].Data.color;
    }

    public TileType GetTileType(int x, int y)
    {
        return tiles[x, y].Data.type;
    }

    public void FindContours(int x, int y, TileColor playerTileColor, List<Vector2i> positions)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (!GetTile(x, y).Marked && GetTileColor(x, y) == playerTileColor)
            {
                positions.Add(new Vector2i(x, y));
                GetTile(x, y).Marked = true;

                FindContours(x - 1, y, playerTileColor, positions);
                FindContours(x + 1, y, playerTileColor, positions);
                FindContours(x, y - 1, playerTileColor, positions);
                FindContours(x, y + 1, playerTileColor, positions);
            }

        }
    }

    private void ResetMarkers()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                GetTile(i, j).Marked = false;
            }
        }
    }

    public void FillTilesUsingContours(int x, int y, TileColor playerTileColor)
    {
        ResetMarkers();

        List<Vector2i> positions = new List<Vector2i>();
        FindContours(x, y, playerTileColor, positions);

        IntRect pRect = new IntRect(
            positions.Min(pos => pos.x),
            positions.Min(pos => pos.y),
            positions.Max(pos => pos.x),
            positions.Max(pos => pos.y));

        bool[,] tileMatrix = new bool[pRect.GetWidth(), pRect.GetHeight()];
        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            for (int i = 0; i < pRect.GetWidth(); i++)
            {
                tileMatrix[i, j] = (GetTileColor(pRect.x1 + i, pRect.y1 + j) == playerTileColor);
            }
        }
        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            if (!tileMatrix[0, j])
                FillTileMatrix(tileMatrix, 0, j, pRect);
            if (!tileMatrix[pRect.GetWidth() - 1, j])
                FillTileMatrix(tileMatrix, pRect.GetWidth() - 1, j, pRect);
        }
        for (int i = 1; i < pRect.GetWidth() - 1; i++)
        {
            if (!tileMatrix[i, 0])
                FillTileMatrix(tileMatrix, i, 0, pRect);
            if (!tileMatrix[i, pRect.GetHeight() - 1])
                FillTileMatrix(tileMatrix, i, pRect.GetHeight() - 1, pRect);
        }

        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            for (int i = 0; i < pRect.GetWidth(); i++)
            {
                if (!tileMatrix[i, j] && GetTileType(pRect.x1 + i, pRect.y1 + j) != TileType.Wall)
                    SetTileColor(pRect.x1 + i, pRect.y1 + j, playerTileColor);
            }
        }

    }

    public void FillTileMatrix(bool[,] tileMatrix, int x, int y, IntRect pRect)
    {
        // get reference to pRect
        if (x < 0 || x >= pRect.GetWidth() || y < 0 || y >= pRect.GetHeight()) return;

        if (!tileMatrix[x, y])
        {
            tileMatrix[x, y] = true;

            FillTileMatrix(tileMatrix, x - 1, y - 1, pRect);
            FillTileMatrix(tileMatrix, x - 1, y, pRect);
            FillTileMatrix(tileMatrix, x - 1, y + 1, pRect);
            FillTileMatrix(tileMatrix, x, y - 1, pRect);
            FillTileMatrix(tileMatrix, x, y + 1, pRect);
            FillTileMatrix(tileMatrix, x + 1, y - 1, pRect);
            FillTileMatrix(tileMatrix, x + 1, y, pRect);
            FillTileMatrix(tileMatrix, x + 1, y + 1, pRect);
        }
    }
}
