using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;

public class FadeEffectCanvas : MonoBehaviour {

	List<GameObject> tiles;

	public Sprite[] tileSprites;
	public int numberSpeed;
	public float timeSpeed;

	public void FadeOut()
	{
		StartCoroutine(PlayFadeOutEffect());
	}

	public void FadeIn()
	{
		StartCoroutine(PlayFadeInEffect());
	}

	public IEnumerator PlayFadeOutEffect()
	{
		int i = 0;
		foreach (var tile in tiles)
		{	
			tile.GetComponent<Image>().enabled = true;
			i += 1;
			if (i > numberSpeed)
			{
				i = 0;
				yield return new WaitForSeconds(timeSpeed);
			}
		}
	}

	IEnumerator PlayFadeInEffect()
	{
		int i = 0;
		foreach (var tile in tiles)
		{	
			tile.GetComponent<Image>().enabled = false;
			i += 1;
			if (i > numberSpeed)
			{
				i = 0;
				yield return new WaitForSeconds(timeSpeed);
			}
		}
	}

	List<GameObject> ShuffleTileList(List<GameObject> tileList)
	{
		var random = new System.Random();
		List<GameObject> randomized = tileList.OrderBy(x => random.Next()).ToList();

		return randomized;
	}

	void FillEachTileByColor()
	{
		int i = 2;
		foreach (var tile in tiles)
		{
			tile.GetComponent<Image>().sprite = tileSprites[i];
			i += 1;
			if (i > 4) i = 2;
		}
	}

	void FillEachTileByGray()
	{
		int i = 0;
		foreach (var tile in tiles)
		{
			tile.GetComponent<Image>().sprite = tileSprites[i];
			i += 1;
			if (i > 1) i = 0;
		}
	}

	// Use this for initialization
	void Start () {
		tiles = new List<GameObject>();
		Image[] tileImages = GetComponentsInChildren<Image>();
		foreach (var image in tileImages)
			tiles.Add(image.gameObject);

		FillEachTileByColor();

		tiles = ShuffleTileList(tiles);

		if (!GameStartChecker.isStart)
		{
			foreach (var tile in tiles)
				tile.GetComponent<Image>().enabled = false;
			GameStartChecker.isStart = true;
		}
		else
			FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
