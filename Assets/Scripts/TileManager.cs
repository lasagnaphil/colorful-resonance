using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Algorithm;
using FullInspector;

public class TileManager : Singleton<TileManager>
{
    private Tile[,] tiles;
    public int width;
    public int height;

    public Tile tilePrefab;

    public Dictionary<TileType, Sprite> tileDict;

    protected void Start()
    {
        tiles = new Tile[width, height];
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                tiles[i, j] = Instantiate(tilePrefab);
                tiles[i, j].pos.X = i;
                tiles[i, j].pos.Y = j;
                tiles[i, j].transform.parent = this.transform;
            }
        }
        // Hardcode the map (just for testing)
        for (int j = 0; j < height; j++)
        {
            if (j == 0 || j == height - 1)
            {
                for (int i = 0; i < width; i++) tiles[i, j].Type = TileType.Black;
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    if (i == 0 || i == width - 1) tiles[i, j].Type = TileType.Black;
                }
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public void SetTileType(int x, int y, TileType type)
    {
        tiles[x, y].Type = type;
    }

    public TileType GetTileType(int x, int y)
    {
        return tiles[x, y].Type;
    }

    public void FindContours(int x, int y, TileType playerTileType, List<Vector2i> positions)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (!GetTile(x, y).Marked && GetTileType(x, y) == playerTileType)
            {
                positions.Add(new Vector2i(x, y));
                GetTile(x, y).Marked = true;

                FindContours(x - 1, y, playerTileType, positions);
                FindContours(x + 1, y, playerTileType, positions);
                FindContours(x, y - 1, playerTileType, positions);
                FindContours(x, y + 1, playerTileType, positions);
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

    public void FillTilesUsingContours(int x, int y, TileType playerTileType)
    {
        ResetMarkers();

        List<Vector2i> positions = new List<Vector2i>();
        FindContours(x, y, playerTileType, positions);

        IntRect pRect = new IntRect(
            positions.Min(pos => pos.x),
            positions.Min(pos => pos.y),
            positions.Max(pos => pos.x),
            positions.Max(pos => pos.y));

        Debug.Log("Calculated rect: " + pRect.x1 + " " + pRect.y1 + " " + pRect.x2 + " " + pRect.y2);

        bool[,] tileMatrix = new bool[pRect.GetWidth(), pRect.GetHeight()];
        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            for (int i = 0; i < pRect.GetWidth(); i++)
            {
                tileMatrix[i, j] = (GetTileType(pRect.x1 + i, pRect.y1 + j) == playerTileType);
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
                if (!tileMatrix[i, j])
                    SetTileType(pRect.x1 + i, pRect.y1 + j, playerTileType);
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
