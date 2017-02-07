using UnityEngine;
using Utils;

namespace InputManagement
{
    public class DesktopInputHandler : InputHandler
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                inputManager.CanBlink = true;
            if (Input.GetKeyUp(KeyCode.Space))
                inputManager.CanBlink = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (inputManager.CanBlink)
                    inputManager.Execute(new BlinkCommand(Direction.Left));
                else inputManager.Execute(new MoveCommand(Direction.Left));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (inputManager.CanBlink)
                    inputManager.Execute(new BlinkCommand(Direction.Right));
                else inputManager.Execute(new MoveCommand(Direction.Right));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (inputManager.CanBlink)
                    inputManager.Execute(new BlinkCommand(Direction.Up));
                else inputManager.Execute(new MoveCommand(Direction.Up));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (inputManager.CanBlink)
                    inputManager.Execute(new BlinkCommand(Direction.Down));
                else inputManager.Execute(new MoveCommand(Direction.Down));
            }
        }
    }
}