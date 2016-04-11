using UnityEngine;
using System.Collections.Generic;
using FullInspector;
using MonsterLove.StateMachine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Win,
    Lose
}

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }

    private StateMachine<GameState> fsm;
    private List<Monster> monsters = new List<Monster>();

    private TileManager tileManager;
    public Text turnNumberText;

    public delegate void MonsterTurnHandler();
    public event MonsterTurnHandler MonsterTurns;

    public delegate void MonsterResetHandler();
    public event MonsterResetHandler MonsterResets;

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

        // In here : update all the monsters and objects
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

    private void Play_Enter()
    {
        ResetTurn();
        Debug.Log("Game Start");
    }

    private void Play_Update()
    {
        turnNumberText.text = "Turn number : " + TurnNumber;
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
