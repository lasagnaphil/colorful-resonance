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
			if (PlayerPrefs.GetString("GoToNextStage") == "yes")
            {
                gsm.mapLoader.SetLevelToNext();
                gsm.ChangeState<GameStateLoad>();
				PlayerPrefs.SetString ("GoToNextStage", "no");
            }
			if (PlayerPrefs.GetString ("Restart") == "yes")
            {
                gsm.ChangeState<GameStateLoad>();
				PlayerPrefs.SetString ("Restart", "no");
            }
        }

        public void Exit(GameStateManager gsm)
        {
        }
    }
}