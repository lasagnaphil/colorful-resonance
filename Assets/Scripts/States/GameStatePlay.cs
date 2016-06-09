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

            gsm.turnNumberText.text = gsm.TurnNumber.ToString();
            UpdatePlayerHealth(gsm);
            
            if (Input.GetKeyDown(KeyCode.F5))
            {
                gsm.ChangeState<GameStateEditor>();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                gsm.ChangeState<GameStateSave>();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                gsm.ChangeState<GameStateLoad>();
            }
        }

        // MaxHealth = 3;
        void UpdatePlayerHealth(GameStateManager gsm)
        {
            int remainHealth = gsm.player.Health;
            
            foreach (var playerHealthImage in gsm.playerHealthImages)
                playerHealthImage.gameObject.SetActive(false);
            
            if (remainHealth > 0)
                gsm.playerHealthImages[0].gameObject.SetActive(true);
            if (remainHealth > 1)
                gsm.playerHealthImages[1].gameObject.SetActive(true);
            if (remainHealth > 2)
                gsm.playerHealthImages[2].gameObject.SetActive(true);               
        }

        public void Exit(GameStateManager gsm)
        {
            
        }
    }
}