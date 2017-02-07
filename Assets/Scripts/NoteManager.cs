using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NoteManager : MonoBehaviour {

	public GameObject popupUI;
	MonsterDictionary monsterDictionary;
	GameObject lastSelectedObject;
	bool alreadyPopup;
	bool isCoroutinePlaying;

	// Use this for initialization
	void Start () {
		monsterDictionary = popupUI.GetComponent<MonsterDictionary>();
		popupUI.SetActive(false);
		alreadyPopup = false;
		isCoroutinePlaying = false;
	}
	
	public void PopupDataUI(int index)
	{
		popupUI.SetActive(true);
		monsterDictionary.LoadMonsterData(index);	

		lastSelectedObject = EventSystem.current.currentSelectedGameObject;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			if (popupUI.activeInHierarchy == false)
				StartCoroutine(GoToMain());

			if ((popupUI.activeInHierarchy == true) && (alreadyPopup == true))
			{
				popupUI.SetActive(false);
				EventSystem.current.SetSelectedGameObject(lastSelectedObject);
				alreadyPopup = false;
			}
		}

		if ((popupUI.activeInHierarchy == true) && (alreadyPopup == false))
			alreadyPopup = true;
	}

	public void ClosePopupUI()
	{
		popupUI.SetActive(false);
		alreadyPopup = false;
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
