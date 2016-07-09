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
}
