using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectContolMethod : MonoBehaviour
{
	public void SelectTouchMethod()
	{
		PlayerPrefs.SetString ("ControlMethod", "Touch");
	}

	public void SelectSwipeMethod()
	{
		PlayerPrefs.SetString ("ControlMethod", "Swipe");
	}
}
