using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainButton : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
			GoToMain();
	}

	public void GoToMain()
	{
		FindObjectOfType<FadeEffectCanvas>().FadeOut();
		SceneManager.LoadScene("Main");
	}
}
