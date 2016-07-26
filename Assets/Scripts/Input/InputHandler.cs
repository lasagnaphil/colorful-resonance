using System;
using UnityEngine;

namespace InputManagement
{
    public class InputHandler : MonoBehaviour
    {
        [HideInInspector]
        public InputManager inputManager;

        protected void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }
    }
}