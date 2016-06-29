using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace States
{
    public class GameStatePlay : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            gsm.IsLoading = false;
            gsm.ResetTurn();
            gsm.soundManager.StopAll();
            gsm.soundManager.PlayBackground(SoundManager.Sounds.Main);
            Debug.Log("Game Start");
        }

        public void Update(GameStateManager gsm)
        {
            gsm.player.CameraUpdate();
            if (!gsm.isTurnExecuting)
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
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                && Input.GetKeyDown(KeyCode.RightBracket))
            {
                gsm.mapLoader.SetLevelToNext();
                gsm.ChangeState<GameStateLoad>();
            }
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                && Input.GetKeyDown(KeyCode.LeftBracket))
            {
                gsm.mapLoader.SetLevelToPrevious();
                gsm.ChangeState<GameStateLoad>();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Select");
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