using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMainButton : MonoBehaviour {

	bool isCoroutinePlaying;

	void Start()
	{
		isCoroutinePlaying = false;
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && (!isCoroutinePlaying))
			StartCoroutine(GoToMain());
	}

	IEnumerator GoToMain()
	{
		isCoroutinePlaying = true;
		FindObjectOfType<FadeEffectCanvas>().FadeOut();
		yield return new WaitForSeconds (0.5f);
		isCoroutinePlaying = false;
		SceneManager.LoadScene("Main");
	}
}
