using UnityEngine;
using Utils;

namespace InputManagement
{
	public class TouchPadMobileInputHandler : InputHandler
	{
		public void AtLeftButtonPressed()
		{
			if (inputManager.CanBlink)
				inputManager.Execute(new BlinkCommand(Direction.Left));
			else inputManager.Execute(new MoveCommand(Direction.Left));
		}

		public void AtRightButtonPressed()
		{
			if (inputManager.CanBlink)
				inputManager.Execute(new BlinkCommand(Direction.Right));
			else inputManager.Execute(new MoveCommand(Direction.Right));
		}

		public void AtUpButtonPressed()
		{
			if (inputManager.CanBlink)
				inputManager.Execute(new BlinkCommand(Direction.Up));
			else inputManager.Execute(new MoveCommand(Direction.Up));
		}

		public void AtDownButtonPressed()
		{
			if (inputManager.CanBlink)
				inputManager.Execute(new BlinkCommand(Direction.Down));
			else inputManager.Execute(new MoveCommand(Direction.Down));
		}

		public void AtBlinkButtonPressed()
		{
			inputManager.CanBlink = !inputManager.CanBlink;
		}
	}
}
