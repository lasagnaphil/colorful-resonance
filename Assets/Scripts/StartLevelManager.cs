using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class StartLevelManager : MonoBehaviour
    {
        public void GoToSelectLevel()
        {
            SceneManager.LoadScene("Select");
        }

        public void GoToCreditLevel()
        {
            SceneManager.LoadScene("Credit");
        }

        public void GoToNoteLevel()
        {
            SceneManager.LoadScene("Note");
        }

		public void GoToSetUpLevel()
		{
			SceneManager.LoadScene ("SetUp");
		}
    }
}