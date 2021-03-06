﻿using UnityEngine;
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
            gsm.inputManager.Enabled = true;
			if(PlayerPrefs.GetString("Control") == "Simple" || PlayerPrefs.GetString("Control") == "TouchPad")
				gsm.inputManager.SwipeMobileInputEnabled = false;
        }

        public void Update(GameStateManager gsm)
        {
            gsm.player.CameraUpdate();
			gsm.player.MobileManagerUpdate();
            if (!gsm.isTurnExecuting)
                gsm.player.GameUpdate();

            gsm.turnNumberText.text = gsm.TurnNumber.ToString();
            UpdatePlayerHealth(gsm);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                gsm.ChangeState<GameStateEditor>();
            }
            if (Input.GetKeyDown(KeyCode.S))
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
                if (GameObject.Find("TipPanel") != null)
                    GameObject.Find("TipPanel").SetActive(false);
                else
                {
                    Object.Destroy(gsm.gameObject);
                    SceneManager.LoadScene("Select_Final");
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (GameObject.Find("TipPanel") != null)
                    GameObject.Find("TipPanel").SetActive(false);
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
            gsm.inputManager.Enabled = false;
        }
    }
}