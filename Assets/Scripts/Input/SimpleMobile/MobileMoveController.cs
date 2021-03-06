﻿using UnityEngine;
using System.Collections;
using InputManagement;
using Utils;

public class MobileMoveController : MonoBehaviour
{
	public Direction direction;
    public SimpleMobileInputHandler inputHandler;

	void OnMouseUp()
	{
	    switch (direction)
	    {
            case Direction.Right:
                inputHandler.AtRightButtonPressed();
	            break;
            case Direction.Left:
                inputHandler.AtLeftButtonPressed();
	            break;
            case Direction.Up:
                inputHandler.AtUpButtonPressed();
	            break;
            case Direction.Down:
	            inputHandler.AtDownButtonPressed();
	            break;
	    }
	}
}
