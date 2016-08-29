using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class StartLevelManager : MonoBehaviour
    {
        public void GoToSelectLevel()
        {
            FindObjectOfType<FadeEffectCanvas>().FadeOut();
            SceneManager.LoadScene("Select_New");
        }

        public void GoToCreditLevel()
        {
            FindObjectOfType<FadeEffectCanvas>().FadeOut();
            SceneManager.LoadScene("Credit");
        }

        public void GoToNoteLevel()
        {
            FindObjectOfType<FadeEffectCanvas>().FadeOut();
            SceneManager.LoadScene("Note");
        }

		public void GoToSetUpLevel()
		{
            FindObjectOfType<FadeEffectCanvas>().FadeOut();
			SceneManager.LoadScene ("SetUp");
		}
    }
}