using UnityEngine;
using System.Collections;

public class MobileBlinkManager : MonoBehaviour
{
	void OnMouseDown()
	{
		if(PlayerPrefs.GetString("BlinkState") != "on")
			PlayerPrefs.SetString ("BlinkState", "on");
		else if(PlayerPrefs.GetString("BlinkState") == "on")
			PlayerPrefs.SetString ("BlinkState", "off");
	}
}
