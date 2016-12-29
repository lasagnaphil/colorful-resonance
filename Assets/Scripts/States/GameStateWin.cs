using UnityEngine;

namespace States
{
    public class GameStateWin : IGameState
    {
        private GameObject GameManager;
        private string MapName;

        public void Enter(GameStateManager gsm)
        {
            MapName = GameObject.Find("GameManager").GetComponent<MapLoader>().MapToLoad;

            switch (MapName)
            {
                case "1-1" :
                    PlayerPrefs.SetString("1-1 Clear", "Yes");
                    break;
                case "1-2":
                    PlayerPrefs.SetString("1-2 Clear", "Yes");
                    break;
                case "1-3":
                    PlayerPrefs.SetString("1-3 Clear", "Yes");
                    break;
                case "1-04":
                    PlayerPrefs.SetString("1-4 Clear", "Yes");
                    break;
                case "1-05":
                    PlayerPrefs.SetString("1-5 Clear", "Yes");
                    break;
                case "1-06":
                    PlayerPrefs.SetString("1-6 Clear", "Yes");
                    break;
                case "1-07":
                    PlayerPrefs.SetString("1-7 Clear", "Yes");
                    break;
                case "1-08":
                    PlayerPrefs.SetString("1-8 Clear", "Yes");
                    break;
                case "1-09":
                    PlayerPrefs.SetString("1-9 Clear", "Yes");
                    break;
                case "1-10":
                    PlayerPrefs.SetString("1-10 Clear", "Yes");
                    break;
                case "2-01":
                    PlayerPrefs.SetString("2-1 Clear", "Yes");
                    break;
                case "2-02":
                    PlayerPrefs.SetString("2-2 Clear", "Yes");
                    break;
                case "2-03":
                    PlayerPrefs.SetString("2-3 Clear", "Yes");
                    break;
                case "2-04":
                    PlayerPrefs.SetString("2-4 Clear", "Yes");
                    break;
                case "2-05":
                    PlayerPrefs.SetString("2-5 Clear", "Yes");
                    break;
                case "2-06":
                    PlayerPrefs.SetString("2-6 Clear", "Yes");
                    break;
                case "2-07":
                    PlayerPrefs.SetString("2-7 Clear", "Yes");
                    break;
                case "3-1":
                    PlayerPrefs.SetString("3-1 Clear", "Yes");
                    break;
                case "3-2":
                    PlayerPrefs.SetString("3-2 Clear", "Yes");
                    break;
                case "3-3":
                    PlayerPrefs.SetString("3-3 Clear", "Yes");
                    break;
                case "3-4":
                    PlayerPrefs.SetString("3-4 Clear", "Yes");
                    break;
                case "3-5":
                    PlayerPrefs.SetString("3-5 Clear", "Yes");
                    break;
                case "4-1":
                    PlayerPrefs.SetString("4-1 Clear", "Yes");
                    break;
                case "4-2":
                    PlayerPrefs.SetString("4-2 Clear", "Yes");
                    break;
                case "4-3":
                    PlayerPrefs.SetString("4-3 Clear", "Yes");
                    break;
                case "4-4":
                    PlayerPrefs.SetString("4-4 Clear", "Yes");
                    break;
                case "4-5":
                    PlayerPrefs.SetString("4-5 Clear", "Yes");
                    break;
                case "5-1":
                    PlayerPrefs.SetString("5-1 Clear", "Yes");
                    break;
                case "5-2":
                    PlayerPrefs.SetString("5-2 Clear", "Yes");
                    break;
                case "5-3":
                    PlayerPrefs.SetString("5-3 Clear", "Yes");
                    break;
                case "5-4":
                    PlayerPrefs.SetString("5-4 Clear", "Yes");
                    break;
                case "5-5":
                    PlayerPrefs.SetString("5-5 Clear", "Yes");
                    break;
                default:
                    Debug.Log("Game State Win is Something Error");
                    break;
            }
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