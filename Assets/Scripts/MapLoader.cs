using System.Linq;
using System.Collections.Generic;
using Buttons;
using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public Player player;

    [SerializeField] private string mapToLoad;
    public string MapToLoad
    {
        get { return mapToLoad; }
        set
        {
            mapToLoad = value;
            mapIndex = FindLevelIndexByName(mapToLoad);
        }
    }

    public int mapIndex;
    public List<TextAsset> mapAssetList;
    private MapData mapDataToLoad;

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

        mapIndex = FindLevelIndexByName(MapToLoad);
        // mapDataToLoad = JsonHelper.Deserialize<MapData>(mapAssetList[mapIndex].text);
    }

    public int FindLevelIndexByName(string mapName)
    {
        int index;
        for (index = 0; index < mapAssetList.Count; index++)
        {
            if (mapAssetList[index].name == MapToLoad) return index;
        }
        Debug.Log("Cannot find level with name \"" + mapName + "\"");
        return -1;
    }

    public void SetLevelToNext()
    {
        if (mapIndex == mapAssetList.Count - 1)
        {
            SceneManager.LoadScene("Credit");
            Debug.Log("This is the last level! Reloading the same level again.");
        }
        else mapIndex++;
        MapToLoad = mapAssetList[mapIndex].name;
    }

    public void SetLevelToPrevious()
    {
        if (mapIndex == 0)
        {
            Debug.Log("This is the first level! Reloading the same level again.");
        }
        else mapIndex--;
        MapToLoad = mapAssetList[mapIndex].name;
    }

    public void InitializeTileArray(ref Tile[,] tiles, int width, int height)
    {
        tiles = new Tile[width, height];
    }

    public MapData LoadMap()
    {
        tileManager = TileManager.Instance;
        Tile tilePrefab = tileManager.tilePrefab;

        mapDataToLoad = JsonHelper.Deserialize<MapData>(mapAssetList[mapIndex].text);

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

        if (mapDataToLoad.winCondition is EscapeWinCondition)
        {
            // create a teleporter object..
            var escapePosition = (mapDataToLoad.winCondition as EscapeWinCondition).escapePosition;
            var teleporter = Instantiate(PrefabDictionary.Instance.escapeTeleporterPrefab);
            teleporter.pos.Set(escapePosition);
        }
        // Load the monsters
        foreach (var monsterData in mapDataToLoad.monsters)
        {
            if (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster(monsterData.name) == null)
            {
                Debug.Log("Cannot find monster!");
                continue;
            }
            var monster = Instantiate(PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster(monsterData.name));
            monster.Setup();
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
                var wallToggleButton = button as WallToggleButton;
                wallToggleButton.wallTogglePos = new Vector2i(buttonData.togglePosition);
                wallToggleButton.isWallOnButtonOff = buttonData.isWallOnButtonOff;
            }
            if (button is WallToggleLever)
            {
                var wallToggleLever = button as WallToggleLever;
                wallToggleLever.wallTogglePos = new Vector2i(buttonData.togglePosition);
                wallToggleLever.isWallOnButtonOff = buttonData.isWallOnButtonOff;
            }
        }

        // Load the boss monster (if there is one)
        if (mapDataToLoad.boss != null)
        {
            var bossMonster = Instantiate(PrefabDictionary.Instance.bossPrefabDict.GetBoss("TestBoss"));
            bossMonster.Setup();
            bossMonster.pos.Set(mapDataToLoad.boss.position);
        }
    }
}