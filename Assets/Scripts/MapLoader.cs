using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public string mapToLoad;
    public List<TextAsset> mapAssetList;
    
    private List<MapData> mapDataList;
    private MapData mapDataToLoad;

    private Dictionary<int, TileData> tileDataDictionary;

    protected void Awake()
    {
        tileDataDictionary = new Dictionary<int, TileData>
        {
            // Because of serialization issues with dictionary containing tiledata type, 
            // we hard-code this dictionary as for now.
            {0, new TileData(TileColor.None, TileType.Normal)},
            {1, new TileData(TileColor.Black, TileType.Normal)},
            {2, new TileData(TileColor.White, TileType.Normal)},
            {3, new TileData(TileColor.Red, TileType.Normal)},
            {4, new TileData(TileColor.Blue, TileType.Normal)},
            {5, new TileData(TileColor.Green, TileType.Normal)},
            {6, new TileData(TileColor.Yellow, TileType.Normal)},
            {10, new TileData(TileColor.None, TileType.Wall)},
            {11, new TileData(TileColor.Black, TileType.Wall)},
            {12, new TileData(TileColor.White, TileType.Wall)},
            {13, new TileData(TileColor.Red, TileType.Wall)},
            {14, new TileData(TileColor.Blue, TileType.Wall)},
            {15, new TileData(TileColor.Yellow, TileType.Wall)},
            {16, new TileData(TileColor.Green, TileType.Wall)}
        };

        mapDataList = mapAssetList.Select(
            asset => JsonUtility.FromJson<MapData>(asset.text))
            .ToList();
    }

    public void LoadMap(Tile[,] tiles, Tile tilePrefab)
    {
        mapDataToLoad = mapDataList.Find(x => x.name == mapToLoad);
        int width = mapDataToLoad.width;
        int height = mapDataToLoad.height;
        for (int i = 0; i < width*height; i++)
        {
            int x = i%width;
            int y = i/width;
            tiles[x, y] = Instantiate(tilePrefab);
            tiles[x, y].pos.X = x;
            tiles[x, y].pos.Y = y;
            tiles[x, y].transform.parent = this.transform;
            TileData data = tileDataDictionary[mapDataToLoad.tiles[i]];
            tiles[x, y].Data = new TileData(data.color, data.type);
        }
        /*
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                tiles[i, j] = Instantiate(tilePrefab);
                tiles[i, j].pos.X = i;
                tiles[i, j].pos.Y = j;
                tiles[i, j].transform.parent = this.transform;
                tiles[i, j].Data.type = TileType.Normal;
                tiles[i, j].Data.color = TileColor.White;
            }
        }
        // Hardcode the map (just for testing)
        for (int j = 0; j < height; j++)
        {
            if (j == 0 || j == height - 1)
            {
                for (int i = 0; i < width; i++)
                {
                    tiles[i, j].Data.type = TileType.Wall;
                    tiles[i, j].Data.color = TileColor.Black;
                }
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    if (i == 0 || i == width - 1)
                    {
                        tiles[i, j].Data.type = TileType.Wall;
                        tiles[i, j].Data.color = TileColor.Black;
                    }
                }
            }
        }
        tiles[4, 4].Data.color = TileColor.Black;
        tiles[8, 8].Data.color = TileColor.Black;
        */
    }
}