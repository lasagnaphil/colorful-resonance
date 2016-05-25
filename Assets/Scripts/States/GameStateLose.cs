using UnityEngine;

namespace States
{
    public class GameStateLose : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            Debug.Log("Player Lose : Press R to restart");
        }

        public void Update(GameStateManager gsm)
        {
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