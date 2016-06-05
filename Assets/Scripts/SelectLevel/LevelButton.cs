﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectLevel
{
    public class LevelButton : MonoBehaviour
    {
        private UnityEngine.UI.Button buttonUI;
        private LevelInfoSender levelInfoSender;
        public string levelName;

        public void Awake()
        {
            levelInfoSender = FindObjectOfType<LevelInfoSender>();
            buttonUI = GetComponent<UnityEngine.UI.Button>();

            buttonUI.onClick.AddListener(() =>
            {
                levelInfoSender.levelName = levelName;
                DontDestroyOnLoad(levelInfoSender);
                SceneManager.LoadScene("Game");
            });
        }

    }
}