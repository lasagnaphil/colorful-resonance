using UnityEngine;

namespace InputManagement
{
    public class InputHandler : MonoBehaviour
    {
        protected InputManager inputManager;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }
    }
}