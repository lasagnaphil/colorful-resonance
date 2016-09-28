using UnityEngine;
using System.Collections;

public class ControlSetting : MonoBehaviour
{
	public GameObject SimpleTouchControlButton;
	public GameObject SwipeControlButton;
	public GameObject TouchPadControlButton;

	void Start()
	{
		if (PlayerPrefs.GetString ("Control") == "Simple")
		{
			SimpleTouchControlButton.SetActive (true);
			SwipeControlButton.SetActive (false);
			TouchPadControlButton.SetActive (false);
		}
		else if (PlayerPrefs.GetString ("Control") == "Swipe")
		{
			SimpleTouchControlButton.SetActive (false);
			SwipeControlButton.SetActive (true);
			TouchPadControlButton.SetActive (false);
		}
		else if (PlayerPrefs.GetString ("Control") == "TouchPad")
		{
			SimpleTouchControlButton.SetActive (false);
			SwipeControlButton.SetActive (false);
			TouchPadControlButton.SetActive (true);
		}
	}

	public void SimpleTouchControl()
	{
		PlayerPrefs.SetString ("Control", "Simple");
		Debug.Log ("Simple Apply");
	}

	public void SwipeControl()
	{
		PlayerPrefs.SetString ("Control", "Swipe");
		Debug.Log ("Control Apply");
	}

	public void TouchPadControl()
	{
		PlayerPrefs.SetString ("Control", "TouchPad");
		Debug.Log ("Touch Pad Apply");
	}
}
