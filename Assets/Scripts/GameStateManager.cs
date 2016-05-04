using System;
using UnityEngine;
using System.Collections.Generic;
using FullInspector;
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

    // Store a player reference for other objects to use
    public Player player;

    // Reference to "holder" gameobjects
    public GameObject monsterHolderObject;
    public GameObject projectileHolderObject;

    private TileManager tileManager;
    public Text turnNumberText;
    public Text playerHealthText;

    public event Action PlayerTurn;
    public event Action MonsterTurns;
    public event Action MonsterResets;
    public event Action ProjectileTurns; 

    protected void Start()
    {
        fsm = StateMachine<GameState>.Initialize(this);
        fsm.ChangeState(GameState.Play);

        tileManager = TileManager.Instance;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fsm.ChangeState(GameState.Play);
        }
    }

    public void NextTurn()
    {
        TurnNumber++;

        // In here : update all the players / monsters / objects
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
        foreach (var monster in monsters)
        {
            Position monsterPos = monster.GetComponent<Position>();
            if (monsterPos.X == x && monsterPos.Y == y)
            {
                return monster;
            }
        }
        return null;
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
    public void ResetProjectile(Projectile projectile) { projectiles.Clear(); }

    private void Load_Enter()
    {
        // load data from MapData
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
