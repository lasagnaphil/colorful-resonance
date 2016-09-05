using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace SelectLevel
{
    public class LevelButton : MonoBehaviour
    {
        private UnityEngine.UI.Button buttonUI;
        private LevelInfoSender levelInfoSender;
		private bool isCoroutinePlaying;

        public string levelName;
		public SaveLoadManager saveLoadManager;

        public void Awake()
        {
            levelInfoSender = FindObjectOfType<LevelInfoSender>();
            buttonUI = GetComponent<UnityEngine.UI.Button>();
            saveLoadManager = FindObjectOfType<SaveLoadManager>();

            //if (saveLoadManager != null) buttonUI.interactable = !saveLoadManager.LevelCompare(new SaveData(levelName));

            buttonUI.onClick.AddListener(() =>
            {
                //if (!saveLoadManager.LevelCompare(new SaveData(levelName)))
                //{
                    levelInfoSender.levelName = levelName;
                    DontDestroyOnLoad(levelInfoSender);
					StartCoroutine(GoToGame());
                //}            
            });
        }

		IEnumerator GoToGame()
		{
			isCoroutinePlaying = true;
			FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
	    	IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
	    	yield return StartCoroutine(coroutine);
			isCoroutinePlaying = false;
			SceneManager.LoadScene("Game");
		}
    }
}