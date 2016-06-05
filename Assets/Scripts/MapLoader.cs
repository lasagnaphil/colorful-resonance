using System.Linq;
using System.Collections.Generic;
using Buttons;
using UnityEngine;
using Utils;

public class MapLoader : MonoBehaviour
{
    public Player player;

    public string mapToLoad;
    public List<TextAsset> mapAssetList;

    private List<MapData> mapDataList;

    private Dictionary<string, TileColor> colorDictionary;
    private Dictionary<char, TileData> tileDataDictionary;

    private TileManager tileManager;

    protected void Awake()
    {
        tileManager = GetComponent<TileManager>();

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
            asset => JsonHelper.Deserialize<MapData>(asset.text))
            .ToList();

    }

    public void Start()
    {
        // LoadGameObjects();
    }

    public void SetLevelToNext()
    {
        int index;
        for (index = 0; index < mapAssetList.Count; index++)
        {
            if (mapDataList[index].name == mapToLoad)
            {
                if (index != mapAssetList.Count - 1)
                    mapToLoad = mapDataList[index + 1].name;
                else
                    Debug.Log("This is the last level! Reloading the same level again.");
                return;
            }
        }
    }

    public void InitializeTileArray(ref Tile[,] tiles, int width, int height)
    {
        tiles = new Tile[width, height];
    }

    public MapData LoadMap()
    {
        tileManager = TileManager.Instance;
        Tile tilePrefab = tileManager.tilePrefab;

        if (!mapDataList.Exists(x => x.name == mapToLoad))
        {
            Debug.LogError("Error loading map: map name " + mapToLoad + " not found");
            return null;
        }
        MapData mapDataToLoad = mapDataList.Find(x => x.name == mapToLoad);

        InitializeTileArray(ref tileManager.tiles, mapDataToLoad.width, mapDataToLoad.height);

        int width = mapDataToLoad.width;
        int height = mapDataToLoad.height;
        tileManager.width = width;
        tileManager.height = height;

        Tile[,] tiles = tileManager.tiles;

        // Load the tiles
        for (int i = 0; i < width * height; i++)
        {
            int x = i % width;
            int y = height - i / width - 1;
            tiles[x, y] = Instantiate(tilePrefab);
            tiles[x, y].pos.X = x;
            tiles[x, y].pos.Y = y;
            tiles[x, y].transform.parent = this.transform;
            TileData data = tileDataDictionary[mapDataToLoad.tiles[i]];
            tiles[x, y].Data = new TileData(data.color, data.type);
            if (data.type == TileType.Wall) tiles[x, y].pos.SpriteLayer = SpriteLayer.WallTile;
        }

        LoadGameObjects(mapDataToLoad);

        // Load background tiles
        gameObject.GetComponent<BackgroundLoader>().LoadBackgroundTiles(mapDataToLoad.background, width, height);

        return mapDataToLoad;
    }

    public void LoadGameObjects(MapData mapDataToLoad)
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

        // Load the buttons
        foreach (var buttonData in mapDataToLoad.buttons)
        {
            var button = Instantiate(PrefabDictionary.Instance.buttonPrefabDictionary.GetButton(buttonData.name));
            //button.transform.parent = GameStateManager.Instance.buttonHolderObject.transform;
            button.pos.Set(buttonData.position);
            if (button is WallToggleButton)
            {
                (button as WallToggleButton).wallTogglePos = new Vector2i(buttonData.togglePosition);
                (button as WallToggleButton).isWallOnButtonOff = buttonData.isWallOnButtonOff;
            }
        }
    }
}