using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using States;

public class PausePopUpUI : MonoBehaviour
{
	bool isCoroutinePlaying = false;

	public void GoToStage()
	{
		StartCoroutine(GoToScene ("Select_Final"));
	}

	public void GoToHome()
	{
		StartCoroutine(GoToScene ("Main"));
	}

	public void Restart(GameStateManager gsm)
	{
		gsm.ChangeState<GameStateLoad> ();
	}

	IEnumerator GoToScene(string sceneName)
	{
		isCoroutinePlaying = true;
		FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
		IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
		yield return StartCoroutine(coroutine);
		isCoroutinePlaying = false;
        Destroy(GameStateManager.Instance.gameObject);
		SceneManager.LoadScene(sceneName);
	}
}