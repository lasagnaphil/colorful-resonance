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
			FindObjectOfType<FadeEffectCanvas>().FadeOut();
			yield return new WaitForSeconds (0.5f);
			isCoroutinePlaying = false;
			SceneManager.LoadScene(sceneName);
		}
    }
}
