using UnityEngine;
using System.Collections;

public class BackgroundTile : MonoBehaviour {

	public Sprite graySprite;
	public Sprite coloredSprite;
	
	public float delay;
	public float minAlphaValue;
	
	new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		StartCoroutine(RepeatFade());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetColor(bool isColored)
	{
		renderer = GetComponent<SpriteRenderer>();
		if (!isColored)
			renderer.sprite = graySprite;
		else
			renderer.sprite = coloredSprite;
	}	
	
	IEnumerator RepeatFade()
	{
		renderer = GetComponent<SpriteRenderer>();
		// float delta = (1.0f - minAlphaValue)/20f;

		int remain6 = (((int)transform.position.x+20) + ((int)transform.position.y+20)) % 6;
		
		if (remain6 == 0)
			yield return null;
		else if (remain6 == 1 || remain6 == 5)
			yield return new WaitForSeconds(delay*5);
		else if (remain6 == 2 || remain6 == 4)
			yield return new WaitForSeconds(delay*5*2);
		else if (remain6 == 3)
			yield return new WaitForSeconds(delay*5*3);
		
		int remain18 = (((int)transform.position.x+20) + ((int)transform.position.y+20)) % 18;
		
		if (remain18 < 6)
			while (true)
			{
				// Yellow
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Red
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Blue
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				yield return null;
			}
		else if (remain18 < 12)
			while (true)
			{
				// Blue
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Yellow
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Red
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				yield return null;
			}		
		else
			while (true)
			{
				// Red
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-238f)/(20f*255f), 
												(255f-219f)/(20f*255f), 
												(255f-213f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Blue
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-206f)/(20f*255f), 
												(255f-226f)/(20f*255f), 
												(255f-231f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				
				// Yellow
				for (int i = 0; i < 20; i++)
				{
					renderer.color -= new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}	
				for (int i = 0; i < 20; i++)
				{
					renderer.color += new Color((255f-240f)/(20f*255f), 
												(255f-244f)/(20f*255f), 
												(255f-219f)/(20f*255f), 0);
					yield return new WaitForSeconds(delay);
				}
				yield return null;
			}
	}
}
