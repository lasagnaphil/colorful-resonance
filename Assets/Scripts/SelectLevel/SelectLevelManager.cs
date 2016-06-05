using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SelectLevel
{
    public class SelectLevelManager : MonoBehaviour
    {
        public UnityEngine.UI.Button buttonPrefab;
        public LevelInfoSender levelInfoSender;

        public Image buttonBoard;

        private List<UnityEngine.UI.Button> buttons;
    }
}
