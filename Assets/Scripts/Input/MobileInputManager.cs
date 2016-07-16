using UnityEngine;
using System.Collections;
using InputManagement;

namespace InputManagement
{
    public class MobileInputManager : MonoBehaviour
    {
        private InputManager inputManager;

        private bool pressed = false;
        private Vector2 mousePos = Vector2.zero;
        private Vector2 touchDelta = Vector2.zero;

        public float swipeThreshold = 20f;
        public float blinkTimeThreshold = 1f;

        private bool acceptInput = true;
        private bool inputRegistered = false;
        private float blinkTimeCounter = 0.0f;

        private bool canBlink = false;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }

        void Update()
        {
            if (!acceptInput) return;
            if (Input.GetMouseButtonDown(0))
            {
                pressed = true;
                mousePos = Input.mousePosition;
            }
            if (Input.GetMouseButtonDown(1))
            {
                pressed = false;
                inputRegistered = true;
                touchDelta = (Vector2) Input.mousePosition - mousePos;
            }
            if (blinkTimeCounter < blinkTimeThreshold)
                blinkTimeCounter += Time.deltaTime;
            else
                canBlink = true;

            if (inputRegistered)
            {
                inputRegistered = false;
                if (touchDelta.x > swipeThreshold)
                {
                    if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Left));
                    else inputManager.Execute(new MoveCommand(Direction.Left));
                }
                if (touchDelta.x < -swipeThreshold)
                {
                    if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Right));
                    else inputManager.Execute(new MoveCommand(Direction.Right));
                }
                if (touchDelta.y > swipeThreshold)
                {
                    if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Up));
                    else inputManager.Execute(new MoveCommand(Direction.Up));
                }
                if (touchDelta.y < -swipeThreshold)
                {
                    if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Down));
                    else inputManager.Execute(new MoveCommand(Direction.Down));
                }
                if (canBlink)
                {
                    blinkTimeCounter = 0f;
                    canBlink = false;
                }
            }
        }
    }
}
