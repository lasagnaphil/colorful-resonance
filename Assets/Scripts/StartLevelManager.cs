using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace DefaultNamespace
{
    public class StartLevelManager : MonoBehaviour
    {
		bool isCoroutinePlaying = false;

        public void GoToSelectLevel()
        {
			StartCoroutine(GoToScene ("Select_New"));
        }

        public void GoToCreditLevel()
        {
			StartCoroutine(GoToScene ("Credit"));
        }

        public void GoToNoteLevel()
        {
			StartCoroutine(GoToScene ("Note"));;
        }

		public void GoToSetUpLevel()
		{
			StartCoroutine(GoToScene ("SetUp"));
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