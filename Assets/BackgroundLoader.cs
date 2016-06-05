using UnityEngine;
using System.Collections;

public class BackgroundLoader : MonoBehaviour {
	public GameObject backgroundTilePrefab;

	public int widthThresold;
	public int heightThresold;
	
	public void LoadBackgroundTiles(string bkgType, int width, int height)
	{
		for (int x = 0 - widthThresold; x < width + widthThresold; x++)
		{		
			for (int y = 0 - heightThresold; y < height + heightThresold; y++)
			{
				BackgroundTile bgTile = Instantiate(backgroundTilePrefab).GetComponent<BackgroundTile>();
				bgTile.transform.position = new Vector3(x, y);
				bgTile.transform.parent = gameObject.transform;
			    bgTile.type = bkgType;

			    bgTile.GetComponent<SpriteRenderer>().sprite =
			        SpriteDictionary.Instance.bkgSpriteDictionary.GetSprite(bkgType);
				bgTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
                GameStateManager.Instance.bkgTiles.Add(bgTile);
			}
		}
	}
}
