using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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

    // Function to check if there is any border color on the 4 direections of the current location
    // Need to come up with a more correct algorithm though...
    public bool CheckIfFillable(int x, int y, TileType fillType)
    {
        bool detectedBorder = false;
        for (int i = x-1; i >= 0; i--)
        {
            if (GetTileType(i, y) == fillType)
            {
                detectedBorder = true;
                break;
            }
        }
        if (!detectedBorder) return false;
        detectedBorder = false;
        for (int i = x+1; i < width; i++)
        {
            if (GetTileType(i, y) == fillType)
            {
                detectedBorder = true;
                break;
            }
        }
        if (!detectedBorder) return false;
        detectedBorder = false;
        for (int j = y-1; j >= 0; j--)
        {
            if (GetTileType(x, j) == fillType)
            {
                detectedBorder = true;
                break;
            }
        }
        if (!detectedBorder) return false;
        detectedBorder = false;
        for (int j = y+1; j < height; j--)
        {
            if (GetTileType(x, j) == fillType)
            {
                detectedBorder = true;
                break;
            }
        }
        if (!detectedBorder) return false;
        return true;
    }

    public bool FillEnclosedArea(int x, int y, TileType fillType, TileType borderType)
    {
        TileType currentType = GetTileType(x, y);
        if (currentType == fillType) return true;
        if (currentType == borderType) return false;

        if (!FillEnclosedArea(x - 1, y - 1, fillType, borderType)) return false;
        if (!FillEnclosedArea(x - 1, y, fillType, borderType)) return false;
        if (!FillEnclosedArea(x - 1, y + 1, fillType, borderType)) return false;
        if (!FillEnclosedArea(x, y - 1, fillType, borderType)) return false;
        if (!FillEnclosedArea(x, y + 1, fillType, borderType)) return false;
        if (!FillEnclosedArea(x + 1, y - 1, fillType, borderType)) return false;
        if (!FillEnclosedArea(x + 1, y, fillType, borderType)) return false;
        if (!FillEnclosedArea(x + 1, y + 1, fillType, borderType)) return false;

        SetTileType(x, y, fillType);

        return true;
    }
}
