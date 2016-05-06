using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Player player;

    public string mapToLoad;
    public List<TextAsset> mapAssetList;
    
    private List<MapData> mapDataList;
    private MapData mapDataToLoad;

    private Dictionary<string, TileColor> colorDictionary;
    private Dictionary<int, TileData> tileDataDictionary;

    protected void Awake()
    {
        colorDictionary = new Dictionary<string, TileColor>
        {
            {"none", TileColor.None},
            {"black", TileColor.Black},
            {"white", TileColor.White},
            {"red", TileColor.Red},
            {"blue", TileColor.Blue},
            {"yellow", TileColor.Yellow},
            {"green", TileColor.Green}
        };
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

    public void Start()
    {
        // LoadGameObjects();
    }

    public void LoadMap(ref Tile[,] tiles, Tile tilePrefab)
    {
        mapDataToLoad = mapDataList.Find(x => x.name == mapToLoad);
        
        // Get the width and height information of the map
        TileManager.Instance.width = mapDataToLoad.width;
        TileManager.Instance.height = mapDataToLoad.height;
        int width = mapDataToLoad.width;
        int height = mapDataToLoad.height;

        // Instantiate the tile array first
        tiles = new Tile[width,height];

        // Load the tiles
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

        LoadGameObjects();
    }

    public void LoadGameObjects()
    {
        // Load player data
        player.pos.Set(mapDataToLoad.playerData.position);

        // Load the monsters
        foreach (var monsterData in mapDataToLoad.monsters)
        {
            var monster = Instantiate(PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster(monsterData.name));
            monster.pos.Set(monsterData.position);
        }       

        // Load the orbs
        foreach (var orbData in mapDataToLoad.orbs)
        {
            var orb = Instantiate(PrefabDictionary.Instance.orbPrefab);
            orb.Color = colorDictionary.GetValueOrDefault(orbData.color, () =>
            {
                Debug.LogError("Failed loading orb color. Defaulting to TileColor.None");
                return TileColor.None;
            });
            orb.pos.Set(orbData.position);
        }
    }
}