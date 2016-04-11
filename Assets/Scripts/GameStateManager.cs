using UnityEngine;
using System.Collections;
using FullInspector;

public class GameStateManager : Singleton<GameStateManager>
{
    public int TurnNumber { get; private set; }
    private TileManager tileManager;

    protected void Start()
    {
        ResetTurn();
        tileManager = TileManager.Instance;
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
