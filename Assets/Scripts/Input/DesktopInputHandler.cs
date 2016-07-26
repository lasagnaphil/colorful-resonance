using UnityEngine;

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
                if (Input.GetKey(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Left));
                else inputManager.Execute(new MoveCommand(Direction.Left));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Right));
                else inputManager.Execute(new MoveCommand(Direction.Right));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Up));
                else inputManager.Execute(new MoveCommand(Direction.Up));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Down));
                else inputManager.Execute(new MoveCommand(Direction.Down));
            }
        }
    }
}