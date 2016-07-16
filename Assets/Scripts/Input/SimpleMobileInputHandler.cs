using UnityEngine;

namespace InputManagement
{
    public class SimpleMobileInputHandler : InputHandler
    {
        [SerializeField]
        private bool canBlink = false;
        
        public void AtLeftButtonPressed()
        {
            inputManager.Execute(new MoveCommand(Direction.Left));
        }

        public void AtRightButtonPressed()
        {
            inputManager.Execute(new MoveCommand(Direction.Right));
        }

        public void AtUpButtonPressed()
        {
            inputManager.Execute(new MoveCommand(Direction.Up));
        }

        public void AtDownButtonPressed()
        {
            inputManager.Execute(new MoveCommand(Direction.Down));
        }

        public void AtBlinkButtonPressed()
        {
            canBlink = true;
        }
    }
}