using UnityEngine;
using System.Collections;

public class MobileBlinkManager : MonoBehaviour
{
	float DeltaTime;

	void OnMouseDown()
	{
		if(PlayerPrefs.GetString("BlinkState") == "off")
			PlayerPrefs.SetString ("BlinkState", "on");
		else if(PlayerPrefs.GetString("BlinkState") == "on")
			PlayerPrefs.SetString ("BlinkState", "off");
	}
}
