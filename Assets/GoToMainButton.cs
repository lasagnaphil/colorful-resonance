using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainButton : MonoBehaviour {

	public void GoToMain()
	{
		SceneManager.LoadScene("Main");
	}
}
