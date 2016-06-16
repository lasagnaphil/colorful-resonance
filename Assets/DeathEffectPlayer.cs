using UnityEngine;
using System.Collections;

public class DeathEffectPlayer : MonoBehaviour {

	float playTime = 0.4f;
	int defaultFrame = 5;

	public Sprite[] sprites;

	// Use this for initialization
	IEnumerator Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		int frame = sprites.Length;
		
		if (frame > 1)
		{
			for (int i = 0; i < frame; i++)
			{
				sr.sprite = sprites[i];
				yield return new WaitForSeconds(playTime/(float)frame);
			}
		}
		else
		{
			sr.sprite = sprites[0];
			for (int i = 0; i < defaultFrame; i++)
			{
				sr.color -= new Color(0,0,0, 0.8f/(float)defaultFrame);
				yield return new WaitForSeconds(playTime/(float)defaultFrame);
			}
		}
		Destroy(gameObject);;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
