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
    private Dictionary<char, TileData> tileDataDictionary;

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
        tileDataDictionary = new Dictionary<char, TileData>
        {
            {'.', new TileData(TileColor.None, TileType.None)},
            {'o', new TileData(TileColor.None, TileType.Normal)},
            {'l', new TileData(TileColor.Black, TileType.Normal)},
            {'w', new TileData(TileColor.White, TileType.Normal)},
            {'r', new TileData(TileColor.Red, TileType.Normal)},
            {'b', new TileData(TileColor.Blue, TileType.Normal)},
            {'y', new TileData(TileColor.Yellow, TileType.Normal)},
            {'g', new TileData(TileColor.Green, TileType.Normal)},
            {'O', new TileData(TileColor.None, TileType.Wall)},
            {'L', new TileData(TileColor.Black, TileType.Wall)},
            {'W', new TileData(TileColor.White, TileType.Wall)},
            {'R', new TileData(TileColor.Red, TileType.Wall)},
            {'B', new TileData(TileColor.Blue, TileType.Wall)},
            {'Y', new TileData(TileColor.Yellow, TileType.Wall)},
            {'G', new TileData(TileColor.Green, TileType.Wall)}
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

        // parse the tiles (represented in a string)
        string tileString = string.Join("", mapDataToLoad.tiles);

        char[] tileChars = tileString.ToCharArray().Where(c => c != ' ').ToArray();
        if (tileChars.Length != width*height)
        {
            Debug.LogError("Error parsing tile string: the number of tiles does not match");
        }

        // Instantiate the tile array first
        tiles = new Tile[width,height];

        // Load the tiles
        for (int i = 0; i < width*height; i++)
        {
            int x = i%width;
            int y = height - i/width - 1;
            tiles[x, y] = Instantiate(tilePrefab);
            tiles[x, y].pos.X = x;
            tiles[x, y].pos.Y = y;
            tiles[x, y].transform.parent = this.transform;
            TileData data = tileDataDictionary[tileChars[i]];
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
            monster.transform.parent = GameStateManager.Instance.monsterHolderObject.transform;
            monster.pos.Set(monsterData.position);
        }       

        // Load the orbs
        foreach (var orbData in mapDataToLoad.orbs)
        {
            var orb = Instantiate(PrefabDictionary.Instance.orbPrefab);
            orb.transform.parent = GameStateManager.Instance.orbHolderObject.transform;
            orb.Color = colorDictionary.GetValueOrDefault(orbData.color, () =>
            {
                Debug.LogError("Failed loading orb color. Defaulting to TileColor.None");
                return TileColor.None;
            });
            orb.pos.Set(orbData.position);
        }
    }
}