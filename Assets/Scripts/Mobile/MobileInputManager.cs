using UnityEngine;
using System.Collections;

public class MobileInputManager : MonoBehaviour {

	// public enum Direction 
	// {None, Left, Right, Up, Down}
	public bool isBlinkButtonClicked;
	public string destDirection;

	private bool inputState;
	private Vector2 startTouchPosition;
	private Vector2 currentTouchPosition;
	private float thresold = 1.5f;
	private float deltaTime;


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
		deltaTime = 0;
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

			else if (Mathf.Abs(delta.x) < thresold && Mathf.Abs(delta.y) < thresold)
			{
				deltaTime = deltaTime + Time.deltaTime;
			}

			if (destDirection != "")
				PlayerPrefs.SetString ("MoveDirection", destDirection);
			if (deltaTime > 1f)
				PlayerPrefs.SetString ("BlinkState", "on");
		}
	}
	
	// Update is called once per frame
	void Update () {
		destDirection = "";
		
		if (Input.GetMouseButtonUp(0))
		{
			inputState = false;
			deltaTime = 0;
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
