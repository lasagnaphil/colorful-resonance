using UnityEngine;
using System.Collections;

public class DeathEffectPlayer : MonoBehaviour {

	public float playTime = 0.3f;
	public int defaultFrame = 3;

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
				yield return new WaitForSeconds(0.3f/(float)frame);
			}
		}
		else
		{
			for (int i = 0; i < defaultFrame; i++)
			{
				sr.color -= new Color(0,0,0, 1f/(float)defaultFrame);
				yield return new WaitForSeconds(0.3f/(float)defaultFrame);
			}
		}
		Destroy(gameObject);;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
