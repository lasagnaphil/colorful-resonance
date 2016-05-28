using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening;
using States;
using UnityEngine.UI;
using Utils;

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }

    //private StateMachine<GameState> fsm;
    private IGameState state; 

    public bool IsLoading = false;

    private List<Monster> monsters = new List<Monster>();
    private List<Projectile> projectiles = new List<Projectile>();
    private List<Orb> orbs = new List<Orb>();

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

    private TileManager tileManager;
    public Text turnNumberText;
    public Text playerHealthText;

    public event Action TileTurns;
    public event Func<Sequence> PlayerTurn;
    public event Func<Sequence> MonsterTurns;
    public event Action MonsterResets;
    public event Func<Sequence> ProjectileTurns;

    protected void Awake()
    {
        tileManager = GetComponent<TileManager>();
        mapLoader = GetComponent<MapLoader>();
    }

    protected void Start()
    {
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
        TurnNumber++;

        Sequence sequence = DOTween.Sequence();

        if (TileTurns != null) TileTurns();
        sequence.AddSequence(SequenceHelper.SimultaneousSequence(PlayerTurn));
        sequence.AddSequence(SequenceHelper.SimultaneousSequence(ProjectileTurns));
        sequence.AddSequence(SequenceHelper.SimultaneousSequence(MonsterTurns));
        sequence.OnComplete(() =>
        {
            if (player.Health <= 0)
            {
                ChangeState<GameStateLose>();
            }
            if (mapData.winCondition is EliminationWinCondition)
            {
                if (monsters.Count == 0)
                {
                    ChangeState<GameStateWin>();
                }
            }
            else if (mapData.winCondition is SurvivalWinCondition)
            {
                if (TurnNumber >= (mapData.winCondition as SurvivalWinCondition).numOfTurns)
                {
                    ChangeState<GameStateWin>();
                }
            }
        });

        sequence.Play();
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
        return monsters.Find(monster => monster.pos.X == x && monster.pos.Y == y);
    }

    public Monster CheckMonsterPosition(Vector2i pos)
    {
        return CheckMonsterPosition(pos.x, pos.y); 
    }

    public void SpawnProjectile(Projectile projectilePrefab, int x, int y, Direction direction)
    {
        if (projectilePrefab != null)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = projectileHolderObject.transform;
            projectile.MovingDirection = direction;
            projectile.GetComponent<Position>().Set(x, y);
        }
        else
        {
            Debug.Log("Null reference : Projectile prefab not found.");
        }
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

    public void AddOrb(Orb orb) { orbs.Add(orb);}
    public void RemoveOrb(Orb orb) { orbs.Remove(orb);}
    public void ResetOrb() { orbs.Clear();}

    public void LoadGame()
    {
        Debug.Log("Loading Game");
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

        monsters.Clear();
        projectiles.Clear();
        orbs.Clear();

        // reset player and turn number
        player.Restart();
        TurnNumber = 0;

        // load the map
        mapData = mapLoader.LoadMap();
    }
}
