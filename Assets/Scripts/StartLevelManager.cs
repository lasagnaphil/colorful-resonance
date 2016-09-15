using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace DefaultNamespace
{
    public class StartLevelManager : MonoBehaviour
    {
		bool isCoroutinePlaying = false;

        void Awake()
        {
            SoundManager.Instance.StopAll();
            SoundManager.Instance.PlayBackground(SoundManager.Sounds.Opening);
        }

        public void GoToSelectLevel()
        {
			StartCoroutine(GoToScene ("Select_Final"));
        }

        public void GoToNoteLevel()
        {
			StartCoroutine(GoToScene ("Note"));
        }

		public void GoToSetUpLevel()
		{
			StartCoroutine(GoToScene ("SetUp"));
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