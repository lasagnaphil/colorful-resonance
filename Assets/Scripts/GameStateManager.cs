using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MonsterLove.StateMachine;
using UnityEngine.UI;
using Utils;

public enum GameState
{
    Load,
    Play,
    Win,
    Lose
}

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }

    private StateMachine<GameState> fsm;
    private List<Monster> monsters = new List<Monster>();
    private List<Projectile> projectiles = new List<Projectile>();
    private List<Orb> orbs = new List<Orb>();

    // Reference to MapLoader (which loads the map)
    private MapLoader mapLoader;

    // Store a player reference for other objects to use
    public Player player;

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
        fsm = StateMachine<GameState>.Initialize(this);
        fsm.ChangeState(GameState.Load);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fsm.ChangeState(GameState.Load);
        }
    }

    public void NextTurn()
    {
        TurnNumber++;

        // In here : update all the players / monsters / objects
        if (TileTurns != null)
            TileTurns();
        if (PlayerTurn != null)
            PlayerTurn();
        if (ProjectileTurns != null)
            ProjectileTurns();
        if (MonsterTurns != null)
            MonsterTurns();
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

    private void Load_Enter()
    {
        Debug.Log("Loading Game");
        mapLoader.LoadMap(ref tileManager.tiles, tileManager.tilePrefab);
        fsm.ChangeState(GameState.Play);
    }

    private void Play_Enter()
    {
        ResetTurn();
        Debug.Log("Game Start");
    }

    private void Play_Update()
    {
        turnNumberText.text = "Turn number : " + TurnNumber;
        playerHealthText.text = "Player health : " + player.Health + " / " + player.MaxHealth;
    }

    private void Play_Exit()
    {
        Debug.Log("Game Over");
    }

    // show lose state text (with the option to restart / go back main menu)
    private void Lose_Enter()
    {
    }

    private void Lose_Update()
    {
        // if restart button is pressed then call ResetTurn() and go back to Play State
    }

    private void Win_Enter()
    {
    }

    private void Win_Update()
    {
    }
}
