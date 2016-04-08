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

    // Data related to the algorithms
    private int algorithmStartPosX;
    private int algorithmStartPosY;

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

    public void FillEnclosedArea(int x, int y, TileType fillType, TileType borderType)
    {
        TileType currentType = GetTileType(x, y);

        if (x >= 0 && x < width && y >= 0 && y < height && currentType != fillType)
        {
            SetTileType(x, y, fillType);

            FillEnclosedArea(x - 1, y - 1, fillType, borderType);
            FillEnclosedArea(x - 1, y, fillType, borderType);
            FillEnclosedArea(x - 1, y + 1, fillType, borderType);
            FillEnclosedArea(x, y - 1, fillType, borderType);
            FillEnclosedArea(x, y + 1, fillType, borderType);
            FillEnclosedArea(x + 1, y - 1, fillType, borderType);
            FillEnclosedArea(x + 1, y, fillType, borderType);
            FillEnclosedArea(x + 1, y + 1, fillType, borderType);
        }
    }

    // not finished yet
    public TileNode CreateTileNode(int x, int y, TileType playerTileType)
    {
        TileNode tileNode = new TileNode();
        tileNode.tile = GetTile(x, y);
        tileNode.marked = true;

        var adjacentTiles = new Tile[4]
        {
            GetTile(x - 1, y), GetTile(x + 1, y), GetTile(x, y - 1), GetTile(x, y + 1)
        };
        foreach (var node in adjacentTiles)
        {
            if (node.Type == playerTileType && !(node.pos.X == algorithmStartPosX && node.pos.Y == algorithmStartPosY))
            {
                TileNode tileNodeToAdd = CreateTileNode(node.pos.X, node.pos.Y, playerTileType);
                tileNode.nodes.Add(tileNodeToAdd);
            }
        }
        return tileNode;
    }

    public void FillTilesCloseToPosition(int x, int y, TileType playerTileType)
    {
        algorithmStartPosX = x;
        algorithmStartPosY = y;
        TileNode tileNode = CreateTileNode(x, y, playerTileType);
    }
}
