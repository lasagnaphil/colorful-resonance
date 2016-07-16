using UnityEngine;

namespace InputManagement
{
    public class DesktopInputHandler : InputHandler
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Left));
                else inputManager.Execute(new MoveCommand(Direction.Left));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Right));
                else inputManager.Execute(new MoveCommand(Direction.Right));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Up));
                else inputManager.Execute(new MoveCommand(Direction.Up));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    inputManager.Execute(new BlinkCommand(Direction.Down));
                else inputManager.Execute(new MoveCommand(Direction.Down));
            }
        }
    }
}