﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using InputManagement;
using Items;
using SelectLevel;
using States;
using UnityEngine.UI;
using Utils;
using Direction = Utils.Direction;
using Debug = UnityEngine.Debug;

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }

    //private StateMachine<GameState> fsm;
    private IGameState state; 
    public IGameState CurrentState { get { return state; } }

    public bool IsLoading = false;

    // Various game objects
    public List<Monster> monsters = new List<Monster>();
    public List<Projectile> projectiles = new List<Projectile>();
    public List<Orb> orbs = new List<Orb>();
    public List<Buttons.Button> buttons = new List<Buttons.Button>();
    public List<BackgroundTile> bkgTiles = new List<BackgroundTile>();
    public List<GameItem> items = new List<GameItem>();

    // Initial num of monsters
    public int initialMonsterCount = 0;

    // Extra game objects (acting as static objects, not doing anything fancy)
    public List<GameObject> extraObjects = new List<GameObject>();

    // Reference to MapLoader (which loads the map)
    [NonSerialized]
    public MapLoader mapLoader;

    // GameStateManager stores the current map data
    public MapData mapData;

    // Store a player reference for other objects to use
    public Player player;

    // Current win condition mode
    public int numOfTurnsToSurvive;

    // Reference to "holder" gameobjects
    public GameObject monsterHolderObject;
    public GameObject projectileHolderObject;
    public GameObject orbHolderObject;
    public GameObject buttonHolderObject;
    public GameObject itemHolderObject;

    // We won't be using holder gameobjects for the buttons;
    // then we would have to add the object into every scene that we have made manually
    // public GameObject buttonHolderObject;

    public SoundManager soundManager;
    public InputManager inputManager;

    private TileManager tileManager;
    private ResultUIManager resultUIManager;
    public SaveLoadManager saveLoadManager;
    public new Camera camera;
    public Text turnNumberText;
    public Image[] playerHealthImages;
    
    // game object actions
    public event Action TileTurns;
    public event Func<Sequence> PlayerTurn;
    public event Func<Sequence> MonsterTurns;
    public event Action MonsterResets;
    public event Func<Sequence> ProjectileTurns;
    public event Action ButtonTurns;
    public event Action ItemTurns;

    public Action MonsterDied;

    // information related to turns
    public bool isTurnExecuting;

    public bool turnDelay;
    public float turnDelayAmount;

    // Editor related stuff
    [NonSerialized] public EditorUIManager editorUIManager;

    public override void Awake()
    {
        base.Awake();
        camera = FindObjectOfType<Camera>();
        tileManager = GetComponent<TileManager>();
        resultUIManager = GetComponent<ResultUIManager>();
        editorUIManager = FindObjectOfType<EditorUIManager>();
        mapLoader = GetComponent<MapLoader>();
        soundManager = FindObjectOfType<SoundManager>();
        saveLoadManager = FindObjectOfType<SaveLoadManager>();

//        editorUIManager.gameObject.SetActive(false);
    }

    protected void Start()
    {
        var levelInfoSender = FindObjectOfType<LevelInfoSender>();

        // if the user is coming from the select button...
        // then there will be a LevelInfoSender object in our scene
        // so load the current map name from it
        if (levelInfoSender != null)
        {
            mapLoader.MapToLoad = levelInfoSender.levelName;
            mapLoader.mapIndex = mapLoader.FindLevelIndexByName(mapLoader.MapToLoad);
        }

        ChangeState<GameStateLoad>();
    }

    protected void Update()
    {
        state.Update(this);
    }

    public void ChangeState<T>(params object[] args) where T : IGameState
    {
        if (state != null) state.Exit(this);
        state = (T) Activator.CreateInstance(typeof (T), args);
        if (state != null) state.Enter(this);
    }

    public void NextTurn()
    {
        int tempVar = 0;
        TurnNumber++;

        if (TileTurns != null) TileTurns();
        SequenceHelper.SimultaneousSequence(PlayerTurn).Play();
        isTurnExecuting = true;
        if (turnDelay)
            Invoke("AfterPlayerTurn", turnDelayAmount);
        else
            AfterPlayerTurn();
    }
    public void AfterPlayerTurn()
    {
        SequenceHelper.SimultaneousSequence(ProjectileTurns).Play();
        SequenceHelper.SimultaneousSequence(MonsterTurns).Play();
        if (ButtonTurns != null) ButtonTurns();
        //sequence.OnComplete(() =>
        //{
            if (player.Health <= 0)
            {
                player.GetComponent<Animator>().SetBool("IsDead", true);
                player.UpdateAuraColor();
                if (soundManager != null)
                {
                    soundManager.StopAll();
                    soundManager.Play(SoundManager.Sounds.Die);
                }
                else
                    Debug.Log("SoundManager is null.");
                resultUIManager.PopupLoseUI();
                ChangeState<GameStateLose>();
                isTurnExecuting = false;
                return;
            }
            if (mapData.winCondition is EliminationWinCondition)
            {
                if (monsters.Count == 0)
                {
                    GameWin();
                    return;
                }
            }
            else if (mapData.winCondition is SurvivalWinCondition)
            {
                if (TurnNumber >= ((SurvivalWinCondition) mapData.winCondition).numOfTurns)
                {
                    GameWin();
                    return;
                }
            }
            else if (mapData.winCondition is EscapeWinCondition)
            {
                if (player.pos.GetVector2i() == ((EscapeWinCondition) mapData.winCondition).escapePosition)
                {
                    GameWin();
                    return;
                }
            }
            else if (mapData.winCondition is KeyUnlockWinCondition)
            {
                if (player.Inventory.Any(item => item is KeyItem) &&
                    player.pos.GetVector2i() == ((KeyUnlockWinCondition) mapData.winCondition).keyUnlockPosition)
                {
                    GameWin();
                    return;
                }
            }
        isTurnExecuting = false;
        //});

        //sequence.Play();
    }

    public void GameWin()
    {

        var levelInfoSender = FindObjectOfType<LevelInfoSender>();

        if (soundManager != null)
        {
            soundManager.StopAll();
            soundManager.Play(SoundManager.Sounds.Clear);
        }
        else
        {
            Debug.Log("SoundManager is null.");
        }
        saveLoadManager.SaveData(mapLoader.PeekNextLevelName());
        resultUIManager.PopupWinUI();
        ChangeState<GameStateWin>();
        isTurnExecuting = false;
    }

    public void ResetTurn()
    {
        TurnNumber = 0;

        // In here : reset the monsters' and objects' position
        if (MonsterResets != null)
            MonsterResets();
    }

    public void AddMonster(Monster monster) { monsters.Add(monster); } 
    public void RemoveMonster(Monster monster) { monsters.Remove(monster); }
    public void ResetMonsters(Monster monster) { monsters.Clear(); }

    public Monster CheckMonsterPosition(int x, int y)
    {
        return monsters.Find(monster =>
        {
            if (monster is BigMonster)
                return ((BigMonster) monster).Contains(new Vector2i(x, y));
            else
                return monster.pos.X == x && monster.pos.Y == y;
        });
    }

    public Monster CheckMonsterPosition(Vector2i pos)
    {
        return CheckMonsterPosition(pos.x, pos.y); 
    }

    public Monster SpawnMonster(Monster monsterPrefab, int x, int y)
    {
        if (monsterPrefab != null)
        {
            Monster monster = Instantiate(monsterPrefab);
            monster.transform.parent = monsterHolderObject.transform;
            monster.pos.Set(x, y);
            return monster;
        }
        Debug.Log("Null reference : Monster prefab not found.");
        return null;
    }

    public Projectile SpawnProjectile(Projectile projectilePrefab, int x, int y, Direction direction)
    {
        if (projectilePrefab != null)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = projectileHolderObject.transform;
            projectile.MovingDirection = direction;
            projectile.pos.Set(x, y);
            return projectile;
        }
        Debug.Log("Null reference : Projectile prefab not found.");
        return null;
    }

    public Orb SpawnOrb(Orb orbPrefab, TileColor color, int x, int y)
    {
        if (orbPrefab != null)
        {
            Orb orb = Instantiate(orbPrefab);
            orb.transform.parent = orbHolderObject.transform;
            orb.pos.Set(x, y);
            orb.Color = color;
            return orb;
        }
        Debug.Log("Null reference : Orb prefab not found.");
        return null;
    }

    public Buttons.Button SpawnSwitch(Buttons.Button buttonPrefab, int x, int y)
    {
        if (buttonPrefab != null)
        {
            Buttons.Button button = Instantiate(buttonPrefab);
            button.transform.parent = buttonHolderObject.transform;
            button.pos.Set(x, y);
            return button;
        }
        Debug.Log("Null reference : Button prefab not found");
        return null;
    }

    public GameItem SpawnGameItem(GameItem itemPrefab, int x, int y)
    {
        if (itemPrefab != null)
        {
            GameItem item = Instantiate(itemPrefab);
            item.transform.parent = itemHolderObject.transform;
            item.pos.Set(x, y);
            return item;
        }
        Debug.Log("Null reference : GameItem prefab not found");
        return null;
    }

    public void AddProjectile(Projectile projectile) { projectiles.Add(projectile); }
    public void RemoveProjectile(Projectile projectile) { projectiles.Remove(projectile); }
    public void ResetProjectile() { projectiles.Clear(); }

    public Projectile CheckProjectilePosition(int x, int y)
    {
        return projectiles.Find(projectile => projectile.pos.X == x && projectile.pos.Y == y);
    }
    public Projectile CheckProjectilePosition(Vector2i pos)
    {
        return CheckProjectilePosition(pos.x, pos.y);
    }
    public Orb CheckOrbPosition(int x, int y)
    {
        return orbs.Find(orb => orb.pos.X == x && orb.pos.Y == y);
    }
    public Orb CheckOrbPosition(Vector2i pos)
    {
        return CheckOrbPosition(pos.x, pos.y);
    }
    public GameItem CheckItemPosition(int x, int y)
    {
        return items.Find(item => item.pos.X == x && item.pos.Y == y);
    }
    public GameItem CheckItemPosition(Vector2i pos)
    {
        return CheckItemPosition(pos.x, pos.y);
    }

    public void AddOrb(Orb orb) { orbs.Add(orb);}
    public void RemoveOrb(Orb orb) { orbs.Remove(orb);}
    public void ResetOrb() { orbs.Clear();}

    public void LoadGame()
    {
        IsLoading = true;

        // if there are gameobjects left from previous play
        // then we destroy it first
        for (int i = 0; i < tileManager.width; i++)
        {
            for (int j = 0; j < tileManager.height; j++)
            {
                Destroy(TileManager.Instance.tiles[i,j].gameObject);
            }
        }
        
        // One of the most weirdest behaviors of Unity I've ever seen
        // Need to loop backwards if we want to destroy all objects
        /* WRONG : 
        for (int i = 0; i < orbs.Count; i++)
        {
            DestroyImmediate(orbs[i].gameObject);
            Debug.Log("Orb destroyed!");
        }
        */
        // This is because when we destroy the orb gameObject, the Orb component is also destroyed,
        // therefore deleting the element itself, decreasing the list count by 1. (I thought that the orb element would just become a null reference...)
        for (int i = orbs.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(orbs[i].gameObject);
        }
        for (int i = monsters.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(monsters[i].gameObject);
        }
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(projectiles[i].gameObject);
        }
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(buttons[i].gameObject);
        }
        for (int i = items.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(items[i].gameObject);
        }
        for (int i = bkgTiles.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(bkgTiles[i].gameObject);
        }
        for (int i = extraObjects.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(extraObjects[i]);
        }

        monsters.Clear();
        projectiles.Clear();
        orbs.Clear();
        buttons.Clear();
        bkgTiles.Clear();
        items.Clear();
        extraObjects.Clear();

        // reset player and turn number
        player.Restart();
        TurnNumber = 0;

        // load the map
        mapData = mapLoader.LoadMap();
        initialMonsterCount = mapData.monsters.Length;

        resultUIManager.Initialize();

        //set currentStage of saveLoadManager into current level
        var levelInfoSender = FindObjectOfType<LevelInfoSender>();
        if (levelInfoSender != null)
            saveLoadManager.SetCurrentLevel(levelInfoSender.levelName);
    }
}
