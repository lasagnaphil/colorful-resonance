using UnityEngine;
using System.Collections;

public class MobileInputManager : MonoBehaviour {

	// public enum Direction 
	// {None, Left, Right, Up, Down}
	public bool isBlinkButtonClicked;
	bool inputState;
	Vector2 startTouchPosition;
	Vector2 currentTouchPosition;
	float thresold = 1.5f;
	public string destDirection;

	public void ChangeBlinkButtonState(bool state)
    {
        isBlinkButtonClicked = state;
    }

	// Use this for initialization
	void Start () {
		isBlinkButtonClicked = false;
		inputState = false;
		startTouchPosition = Vector2.zero;
		currentTouchPosition = Vector2.zero;
		destDirection = "";
	}
	
	void DetectSwipe()
	{
		if (inputState)
		{
			Vector2 delta = currentTouchPosition - startTouchPosition;
			if (delta.x > thresold)
			{
				destDirection = "Right";
				inputState = false;
			}
			else if (delta.x < -1 * thresold)
			{
				destDirection = "Left";
				inputState = false;
			}
			else if (delta.y > thresold)
			{
				destDirection = "Up";
				inputState = false;
			}
			else if (delta.y < -1 * thresold)
			{
				destDirection = "Down";
				inputState = false;
			}
			
			if (destDirection != "")
				Debug.Log(destDirection);
		}
	}
	
	// Update is called once per frame
	void Update () {
		destDirection = "";
		
		if (Input.GetMouseButtonUp(0))
		{
			inputState = false;
		}

		if (Input.GetMouseButtonDown(0))
		{
			inputState = true;
			startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentTouchPosition = startTouchPosition;
			destDirection = "";
			Debug.Log(startTouchPosition);
		}
		
		currentTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		DetectSwipe();	
	}
}
