using UnityEngine;
using Utils;
using System.Collections;
using InputManagement;

namespace InputManagement
{
    public class SwipeMobileInputHandler : InputHandler
    {
        private bool pressed = false;
        private Vector2 mousePos = Vector2.zero;
        private Vector2 touchDelta = Vector2.zero;

        public float swipeThreshold;
        public float blinkTimeThreshold;

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
            if (Input.GetMouseButtonUp(0))
            {
                pressed = false;
                inputRegistered = true;
                touchDelta = ((Vector2) Input.mousePosition) - mousePos;
                if (blinkTimeCounter > blinkTimeThreshold)
                    canBlink = true;
            }
            if (pressed)
            {
                blinkTimeCounter += Time.deltaTime;
                if (blinkTimeCounter > blinkTimeThreshold) inputManager.CanBlink = true;
            }

            if (inputRegistered)
            {
                inputRegistered = false;
                if (Mathf.Abs(touchDelta.x - swipeThreshold) > Mathf.Abs(touchDelta.y - swipeThreshold))
                {
                    if (touchDelta.x > swipeThreshold)
                    {
                        if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Right));
                        else inputManager.Execute(new MoveCommand(Direction.Right));
                    }
                    else if (touchDelta.x < -swipeThreshold)
                    {
                        if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Left));
                        else inputManager.Execute(new MoveCommand(Direction.Left));
                    }
                }
                else
                {
                    if (touchDelta.y > swipeThreshold)
                    {
                        if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Up));
                        else inputManager.Execute(new MoveCommand(Direction.Up));
                    }
                    else if (touchDelta.y < -swipeThreshold)
                    {
                        if (canBlink) inputManager.Execute(new BlinkCommand(Direction.Down));
                        else inputManager.Execute(new MoveCommand(Direction.Down));
                    }
                }
                if (canBlink)
                {
                    blinkTimeCounter = 0f;
                    canBlink = false;
                    inputManager.CanBlink = false;
                }
            }

            if (!pressed) blinkTimeCounter = 0f;
        }
    }
}
