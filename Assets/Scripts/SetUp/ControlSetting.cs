using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlSetting : MonoBehaviour
{
	public GameObject SimpleTouchControlButton;
	public GameObject SwipeControlButton;
	public GameObject TouchPadControlButton;
    public Text LeftHanderPadText;
    public Text RightHanderPadText1;
    public Text RightHanderPadText2;

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
            if (PlayerPrefs.GetString("Hander") == "Right")
            {
                RightHanderPadText1.color = new Color(0.2f, 0.2f, 0.2f);
                RightHanderPadText2.color = new Color(0.2f, 0.2f, 0.2f);
                LeftHanderPadText.color = new Color(1f, 1f, 1f);
            }
            else if (PlayerPrefs.GetString("Hander") == "Left")
            {
                RightHanderPadText1.color = new Color(1f, 1f, 1f);
                RightHanderPadText2.color = new Color(1f, 1f, 1f);
                LeftHanderPadText.color = new Color(0.2f, 0.2f, 0.2f);
            }
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
        PlayerPrefs.SetString("Hander", "Right");
        RightHanderPadText1.color = new Color(0.2f, 0.2f, 0.2f);
        RightHanderPadText2.color = new Color(0.2f, 0.2f, 0.2f);
        LeftHanderPadText.color = new Color(1f, 1f, 1f);
        Debug.Log ("Touch Pad Apply");
	}

    public void RightHanderTouchContorl()
    {
        if (PlayerPrefs.GetString("Control") == "TouchPad")
        {
            PlayerPrefs.SetString("Hander", "Right");
            RightHanderPadText1.color = new Color(0.2f, 0.2f, 0.2f);
            RightHanderPadText2.color = new Color(0.2f, 0.2f, 0.2f);
            LeftHanderPadText.color = new Color(1f, 1f, 1f);
            Debug.Log("Right Hander Touch Apply");
        }
        else
        {
            Debug.Log("터치 패드가 아닌데 어떻게 이 버튼이 뜨는거야?");
        }
    }

    public void LeftHanderTouchControl()
    {
        if (PlayerPrefs.GetString("Control") == "TouchPad")
        {
            PlayerPrefs.SetString("Hander", "Left");
            RightHanderPadText1.color = new Color(1f, 1f, 1f);
            RightHanderPadText2.color = new Color(1f, 1f, 1f);
            LeftHanderPadText.color = new Color(0.2f, 0.2f, 0.2f);
            Debug.Log("Left Hander Touch Apply");
        }
        else
        {
            Debug.Log("터치 패드가 아닌데 어떻게 이 버튼이 뜨는거야?");
        }
    }
}
