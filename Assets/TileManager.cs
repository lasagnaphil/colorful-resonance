using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{
    private Tile[,] tiles;
    public int width;
    public int height;

    public Tile tilePrefab;

    protected void Start()
    {
        Tile[,] tiles = new Tile[width, height];
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                tiles[i,j] = Instantiate(tilePrefab) as Tile;
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
                    else tiles[i, j].Type = TileType.Blue;
                }
            }
        }
    }
}
