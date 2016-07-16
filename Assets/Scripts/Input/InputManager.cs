using System;
using States;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public InputData Data { get; set; }

        public Player player;

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                foreach (var inputHandler in inputHandlers)
                {
                    inputHandler.gameObject.SetActive(enabled);
                }
            }
        }

        private InputHandler[] inputHandlers;

        public void Execute(Command command)
        {
            command.Execute(player);
        }

        void Awake()
        {
            Data = new InputData() { CanBlink = true, MoveDirection = Direction.None };

            inputHandlers = GetComponents<InputHandler>();
        }
    }
}