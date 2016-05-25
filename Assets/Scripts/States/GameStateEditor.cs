using UnityEngine;

namespace States
{
    public class GameStateEditor : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            Debug.Log("Entering Editor...");
            // LoadGame();
            Debug.Log("Editor functionality not implemented yet!!!");
        }

        public void Update(GameStateManager gsm)
        {
        }

        public void Exit(GameStateManager gsm)
        {
        }
    }
}