using UnityEngine;
using System.Collections;

public class BackgroundLoader : MonoBehaviour {
	public enum BackgroundColor {gray, colored}

	public GameObject backgroundTilePrefab;

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
		// GameObject bgTilePrefab = grayBackgroundTilePrefab;
		// if (backgroundColor == BackgroundColor.colored)
		// 	bgTilePrefab = coloredBackgroundTilePrefab;
			
		for (int x = 0 - widthThresold; x < width + widthThresold; x++)
		{		
			for (int y = 0 - heightThresold; y < height + heightThresold; y++)
			{
				GameObject bgTile = Instantiate(backgroundTilePrefab);
				bgTile.transform.position = new Vector3(x, y);
				bgTile.transform.parent = gameObject.transform;
				
				if (backgroundColor == BackgroundColor.colored)
					bgTile.GetComponent<BackgroundTile>().SetColor(true);
				else
					bgTile.GetComponent<BackgroundTile>().SetColor(false);
				
				bgTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
			}
		}
	}
}
