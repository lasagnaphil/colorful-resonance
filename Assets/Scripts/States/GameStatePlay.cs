using UnityEngine;
using Utils;

namespace States
{
    public class GameStatePlay : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            gsm.IsLoading = false;
            gsm.ResetTurn();
            Debug.Log("Game Start");
        }

        public void Update(GameStateManager gsm)
        {
            gsm.player.GameUpdate();

            gsm.turnNumberText.text = "Turn number : " + gsm.TurnNumber;
            gsm.playerHealthText.text = "Player health : " + gsm.player.Health + " / " + gsm.player.MaxHealth;
            
            if (Input.GetKeyDown(KeyCode.F5))
            {
                gsm.ChangeState<GameStateEditor>();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                // TODO: save map data to disk
            }
        }

        public void Exit(GameStateManager gsm)
        {
            
        }
    }
}