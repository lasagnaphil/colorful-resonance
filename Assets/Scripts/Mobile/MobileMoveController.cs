using UnityEngine;
using System.Collections;

public class MobileMoveController : MonoBehaviour
{
	public Direction direction;

	void OnMouseUp()
	{
		if (direction == Direction.Right)
			PlayerPrefs.SetString ("MoveDirection", "Right");
		if (direction == Direction.Left)
			PlayerPrefs.SetString ("MoveDirection", "Left");
		if (direction == Direction.Up)
			PlayerPrefs.SetString ("MoveDirection", "Up");
		if (direction == Direction.Down)
			PlayerPrefs.SetString ("MoveDirection", "Down");
	}

	public enum Direction
	{
		Right,
		Left,
		Up,
		Down
	}
}
