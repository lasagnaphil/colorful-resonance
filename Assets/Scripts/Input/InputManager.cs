using System;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public InputData Data { get; set; }

        public Player player;

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