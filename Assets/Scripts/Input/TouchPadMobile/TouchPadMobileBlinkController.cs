using UnityEngine;
using System.Collections;
using InputManagement;

public class TouchPadMobileBlinkController : MonoBehaviour
{
	public TouchPadMobileInputHandler inputHandler;

	void OnMouseDown()
	{
	    inputHandler.AtBlinkButtonPressed();
	}
}
