using UnityEngine;
using System.Collections;

public class MobileControllerManager : MonoBehaviour
{
	public GameObject MobileTouchManager;
	public GameObject MobileSwipeManager;

	void Start()
	{
		if (PlayerPrefs.GetString ("ControlMethod") == "Touch")
			MobileSwipeManager.SetActive (false);
		else if (PlayerPrefs.GetString ("ControlMethod") == "Swipe")
			MobileTouchManager.SetActive (false);
		else
			MobileSwipeManager.SetActive (false);
	}
}
