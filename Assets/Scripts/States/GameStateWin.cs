using UnityEngine;

namespace States
{
    public class GameStateWin : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            Debug.Log("Player Win : Press Enter to go to next level, Press R to restart");
        }

        public void Update(GameStateManager gsm)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gsm.mapLoader.SetLevelToNext();
                gsm.ChangeState<GameStateLoad>();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                gsm.ChangeState<GameStateLoad>();
            }
        }

        public void Exit(GameStateManager gsm)
        {
        }
    }
}