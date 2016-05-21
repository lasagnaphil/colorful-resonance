using UnityEngine;
using System.Collections;

public class BackgroundLoader : MonoBehaviour {
	public enum BackgroundColor {gray, colored}

	public GameObject grayBackgroundTilePrefab;
	public GameObject coloredBackgroundTilePrefab;

	public int widthThresold;
	public int heightThresold;
	
	public BackgroundColor backgroundColor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadBackgroundTiles(int width, int height)
	{	
		GameObject bgTilePrefab = grayBackgroundTilePrefab;
		if (backgroundColor == BackgroundColor.colored)
			bgTilePrefab = coloredBackgroundTilePrefab;
			
		for (int x = width - widthThresold; x < width + widthThresold; x++)
		{		
			for (int y = height - heightThresold; y < height + heightThresold; y++)
			{
				GameObject bgTile = Instantiate(bgTilePrefab);
				bgTile.transform.position = new Vector3(x, y);
				bgTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
			}
		}
	}
}
