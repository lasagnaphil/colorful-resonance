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
		FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
		IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
		yield return StartCoroutine(coroutine);
		isCoroutinePlaying = false;
		SceneManager.LoadScene("Main");
	}
}
