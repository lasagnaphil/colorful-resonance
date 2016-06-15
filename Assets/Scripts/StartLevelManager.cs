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
    }
}