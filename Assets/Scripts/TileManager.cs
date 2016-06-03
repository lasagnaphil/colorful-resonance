using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

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
        tiles[x, y].UpdateColorIndex(); 
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
        if (x < 0 || x >= width || y < 0 || y >= height)
            return new TileData(TileColor.None, TileType.None);
        return tiles[x, y].Data;
    }

    public TileColor GetTileColor(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return TileColor.None;
        return tiles[x, y].Data.color;
    }

    public TileType GetTileType(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return TileType.None;
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

    public void FindContoursForBlankTile(int x, int y, List<Vector2i> positions, Func<int, int, bool> predicate)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (!GetTile(x, y).Marked && predicate(x, y))
            {
                positions.Add(new Vector2i(x, y));
                GetTile(x, y).Marked = true;

                FindContoursForBlankTile(x - 1, y, positions, predicate);
                FindContoursForBlankTile(x + 1, y, positions, predicate);
                FindContoursForBlankTile(x, y - 1, positions, predicate);
                FindContoursForBlankTile(x, y + 1, positions, predicate);
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

        ResetMarkers(); // reset the markers again, we are going to use the markers later

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

        // Now if there is a blank tile (TileType.None) inside the area we want to fill,
        // then mark the area and make sure it is not filled
        List<Vector2i> fillPositionList = new List<Vector2i>();

        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            for (int i = 0; i < pRect.GetWidth(); i++)
            {
                fillPositionList = new List<Vector2i>();
                if (!tileMatrix[i, j] && !GetTile(pRect.x1 + i, pRect.y1 + j).Marked)
                {
                    FindContoursForBlankTile(pRect.x1 + i, pRect.y1 + j, fillPositionList, (posX, posY) => !tileMatrix[posX - pRect.x1, posY - pRect.y1]);
                }
                if (fillPositionList.Exists(pos => GetTileType(pos.x, pos.y) == TileType.None))
                {
                    fillPositionList.ForEach(pos => tileMatrix[pos.x - pRect.x1, pos.y - pRect.y1] = true);
                    fillPositionList.Clear();
                }
            }
        }
                
        HashSet<Vector2i> borderPositions = new HashSet<Vector2i>();
        List<Vector2i> adjacentPositions = new List<Vector2i>()
        {
            new Vector2i(-1, 1), new Vector2i(0, 1), new Vector2i(1, 1),
            new Vector2i(-1, 0), new Vector2i(1, 0),
            new Vector2i(1, -1), new Vector2i(0, -1), new Vector2i(-1, -1)
        };
        for (int j = 0; j < pRect.GetHeight(); j++)
        {
            for (int i = 0; i < pRect.GetWidth(); i++)
            {
                if (!tileMatrix[i, j] &&
                   (GetTileType(pRect.x1 + i, pRect.y1 + j) != TileType.Wall &&
                    GetTileData(pRect.x1 + i, pRect.y1 + j) != new TileData(TileColor.None, TileType.Normal)))
                {
                    adjacentPositions.Select(pos => new Vector2i(pRect.x1 + i + pos.x, pRect.y1 + j + pos.y)).ToList().ForEach(
                        pos => borderPositions.Add(new Vector2i(pos.x, pos.y)));

                    SetTileColor(pRect.x1 + i, pRect.y1 + j, playerTileColor);
                    if (GetTileType(pRect.x1 + i, pRect.y1 + j) == TileType.Normal)
                    {
                        GetTile(pRect.x1 + i, pRect.y1 + j).Activated = true;
                        GetTile(pRect.x1 + i, pRect.y1 + j).PlayEffect();
                    }
                }
            }
        }

        // Activate the borders of the filled tiles too!
        foreach (var pos in borderPositions)
        {
            if (GetTileType(pos.x, pos.y) != TileType.Wall &&
                GetTileData(pos.x, pos.y) != new TileData(TileColor.None, TileType.Normal))
            {
                SetTileColor(pos.x, pos.y, playerTileColor);
                if (GetTileType(pos.x, pos.y) == TileType.Normal)
                {
                    GetTile(pos.x, pos.y).Activated = true;
                    GetTile(pos.x, pos.y).PlayEffect();
                
                    GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Jump");
                }
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
