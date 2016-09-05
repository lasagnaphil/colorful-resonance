using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SelectLevel
{
    public class SelectLevelManager : MonoBehaviour
    {
		bool isCoroutinePlaying = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
				StartCoroutine(GoToScene ("Main"));
            }
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
