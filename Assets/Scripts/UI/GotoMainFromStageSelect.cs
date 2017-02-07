using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GotoMainFromStageSelect : MonoBehaviour
{
	bool isCoroutinePlaying = false;

	public void GoToMain()
	{
		StartCoroutine(GoToScene ("Main"));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isCoroutinePlaying)
		{
			StartCoroutine(GoToScene("Main"));
		}
	}

	IEnumerator GoToScene(string sceneName)
	{
		isCoroutinePlaying = true;
		FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
		IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
		yield return StartCoroutine(coroutine);
		isCoroutinePlaying = false;
		SceneManager.LoadScene(sceneName);
	}
}
