using UnityEngine;

namespace States
{
    public class GameStateWin : IGameState
    {
        private GameObject GameManager;

        public void Enter(GameStateManager gsm)
        {
            Debug.Log(GameObject.Find("GameManager").GetComponent<MapLoader>().MapToLoad);
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