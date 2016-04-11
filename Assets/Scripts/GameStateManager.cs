using UnityEngine;
using System.Collections;
using FullInspector;
using UnityEngine.UI;

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }
    private TileManager tileManager;

    public Text turnNumberText;

    protected void Start()
    {
        ResetTurn();
        tileManager = TileManager.Instance;
    }

    protected void Update()
    {
        turnNumberText.text = "Turn number : " + TurnNumber;
    }

    public void NextTurn()
    {
        TurnNumber++;

        // In here : update all the monsters and objects
    }

    public void ResetTurn()
    {
        TurnNumber = 0;

        // In here : reset the monsters' and objects' position
    }
}
