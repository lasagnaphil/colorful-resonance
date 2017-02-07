using UnityEngine;
using System.Collections;

public class MobileKeyManager : MonoBehaviour
{
	public void NextStage()
	{
		PlayerPrefs.SetString ("GoToNextStage", "yes");
	}

	public void Restart()
	{
		PlayerPrefs.SetString ("Restart", "yes");
	}

	void Update()
	{
		if (GameObject.Find("WinUI") != null)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
				NextStage();
		}
		if (GameObject.Find("LoseUI") != null)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
				Restart();
		}
	}
}
