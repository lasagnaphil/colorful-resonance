using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

public class PausePopUpUI : MonoBehaviour
{
	bool isCoroutinePlaying = false;

	public void GoToMain()
	{
		StartCoroutine(GoToScene ("Main"));
	}

	public void Restart()
	{
		// 재시작 코드
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
