using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NoteManager : MonoBehaviour {

	public GameObject popupUI;
	MonsterDictionary monsterDictionary;
	GameObject lastSelectedObject;
	bool alreadyPopup;

	// Use this for initialization
	void Start () {
		monsterDictionary = popupUI.GetComponent<MonsterDictionary>();
		popupUI.SetActive(false);
		alreadyPopup = false;
	}
	
	public void PopupDataUI(int index)
	{
		popupUI.SetActive(true);
		monsterDictionary.LoadMonsterData(index);	

		lastSelectedObject = EventSystem.current.currentSelectedGameObject;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
		{
			if (popupUI.activeInHierarchy == false)
				GoToMain();

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

	public void GoToMain()
	{
		SceneManager.LoadScene("Main");
	}
}
