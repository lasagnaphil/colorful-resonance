using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {

	int currentChapter;
	int maxChapter;

	float scrollDelay = 0.01f;
	
	bool isCoroutinePlayed;

	public GameObject[] chapterButtons;
	public GameObject stageButtonGroups;
	public GameObject leftArrowButton;
	public GameObject rightArrowButton;
	public GameObject dummyButton; // 스크롤 도중 버튼 누르는 것을 막기 위한 더미 버튼

	// Use this for initialization
	void Start () {
		currentChapter = 1;
		maxChapter = 5;

		isCoroutinePlayed = false;

		foreach (var button in chapterButtons)
		{
			button.GetComponent<StageButton>().Deactive();
		}

		chapterButtons[0].GetComponent<StageButton>().Active();

		leftArrowButton.SetActive(false);
		dummyButton.SetActive(false);

		// Invoke("ScrollToRight", 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			ScrollToLeft();

		if (Input.GetKeyDown(KeyCode.RightArrow))
			ScrollToRight();
	}

	// ch2 -> ch1
	public void ScrollToLeft()
	{
		if ((currentChapter == 1) || (isCoroutinePlayed)) return;
		StartCoroutine(ScrollLeftCoroutine());


		if (currentChapter == maxChapter)
			rightArrowButton.SetActive(true);

		chapterButtons[currentChapter-1].GetComponent<StageButton>().Deactive();
		currentChapter -= 1;
		chapterButtons[currentChapter-1].GetComponent<StageButton>().Active();

		if (currentChapter == 1)
			leftArrowButton.SetActive(false);
	}

	// ch1 -> ch2
	public void ScrollToRight()
	{
		if ((currentChapter == maxChapter) || (isCoroutinePlayed)) return;
		StartCoroutine(ScrollRightCoroutine());

		if (currentChapter == 1)
			leftArrowButton.SetActive(true);

		chapterButtons[currentChapter-1].GetComponent<StageButton>().Deactive();
		currentChapter += 1;
		chapterButtons[currentChapter-1].GetComponent<StageButton>().Active();

		if (currentChapter == maxChapter)
			rightArrowButton.SetActive(false);
	}

	IEnumerator ScrollLeftCoroutine()
	{
		isCoroutinePlayed = true;
		dummyButton.SetActive(true);
		for (int i = 0; i < 40; i++)
		{
			stageButtonGroups.GetComponent<RectTransform>().anchoredPosition += new Vector2(1280f/40f, 0);
			yield return new WaitForSeconds(scrollDelay);
		}
		dummyButton.SetActive(false);
		isCoroutinePlayed = false;
		yield return null;
	}

	IEnumerator ScrollRightCoroutine()
	{
		isCoroutinePlayed = true;
		dummyButton.SetActive(true);
		for (int i = 0; i < 40; i++)
		{
			stageButtonGroups.GetComponent<RectTransform>().anchoredPosition -= new Vector2(1280f/40f, 0);
			yield return new WaitForSeconds(scrollDelay);
		}
		dummyButton.SetActive(false);
		isCoroutinePlayed = false;
		yield return null;
	}
}
