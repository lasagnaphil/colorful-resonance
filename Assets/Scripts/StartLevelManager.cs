using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace DefaultNamespace
{
    public class StartLevelManager : MonoBehaviour
    {
        public GameObject NoControlSettingPanel;

		bool isCoroutinePlaying = false;

        void Awake()
        {
            SoundManager.Instance.StopAll();
            SoundManager.Instance.PlayBackground(SoundManager.Sounds.Opening);
        }

        void Start()
        {
            NoControlSettingPanel.SetActive(false);
        }

        public void GoToSelectLevel()
        {
            PlayerPrefs.SetString ("Control", "Simple");
    		Debug.Log ("Simple Apply");

            if (PlayerPrefs.HasKey ("Control"))
                StartCoroutine (GoToScene ("Select_Final"));
            else
            {
                NoControlSettingPanel.SetActive(true);
            }
        }

        public void GoToNoteLevel()
        {
			StartCoroutine(GoToScene ("Note"));
        }

		public void GoToSetUpLevel()
		{
			StartCoroutine(GoToScene ("SetUp"));
		}

        public void PerfectResetButton()
        {
            PlayerPrefs.DeleteAll();
        }

		IEnumerator GoToScene(string sceneName)
		{
			isCoroutinePlaying = true;
			FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
			IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
			yield return StartCoroutine(coroutine);
			isCoroutinePlaying = false;
			SceneManager.LoadScene(sceneName);
		}
    }
}