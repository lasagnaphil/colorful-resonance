using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectLevel
{
    public class LevelButton : MonoBehaviour
    {
        private UnityEngine.UI.Button buttonUI;
        private LevelInfoSender levelInfoSender;
        public string levelName;

        public SaveLoadManager saveLoadManager;

        public void Awake()
        {
            levelInfoSender = FindObjectOfType<LevelInfoSender>();
            buttonUI = GetComponent<UnityEngine.UI.Button>();
            saveLoadManager = GetComponent<SaveLoadManager>();

            buttonUI.onClick.AddListener(() =>
            {
                levelInfoSender.levelName = levelName;
                DontDestroyOnLoad(levelInfoSender);
                SceneManager.LoadScene("Game");
            });
        }

    }
}