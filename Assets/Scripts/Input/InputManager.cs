using System;
using States;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public Player player;

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                desktopInputEnabled = value;
                simpleMobileInputEnabled = value;
                swipeMobileInputEnabled = value;
            }
        }

        private bool canBlink;
        public bool CanBlink
        {
            get { return canBlink; }
            set
            {
                canBlink = value;
                if (canBlink) player.ArrowActive();
                else player.ArrowInactive();
            }
        }

        private DesktopInputHandler desktopInput;
        private SimpleMobileInputHandler simpleMobileInput;
        private SwipeMobileInputHandler swipeMobileInput;

        private bool desktopInputEnabled;
        public bool DesktopInputEnabled
        {
            get { return desktopInputEnabled; }
            set
            {
                desktopInputEnabled = value;
                desktopInput.enabled = value;
            }
        }

        private bool simpleMobileInputEnabled;
        public bool SimpleMobileInputEnabled
        {
            get { return simpleMobileInputEnabled; }
            set
            {
                simpleMobileInputEnabled = value;
                simpleMobileInput.enabled = value;
            }
        }

        private bool swipeMobileInputEnabled;
        public bool SwipeMobileInputEnabled
        {
            get { return swipeMobileInputEnabled; }
            set
            {
                swipeMobileInputEnabled = value;
                swipeMobileInput.enabled = value;
            }
        }

        public void Execute(Command command)
        {
            if (GameStateManager.Instance.CurrentState is GameStatePlay)
            {
                command.Execute(player);
                if (command is BlinkCommand)
                {
                    CanBlink = false;
                }
            }
        }

        void Awake()
        {
            desktopInput = GetComponent<DesktopInputHandler>();
            simpleMobileInput = GetComponent<SimpleMobileInputHandler>();
            swipeMobileInput = GetComponent<SwipeMobileInputHandler>();
        }

    }
}