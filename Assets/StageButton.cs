using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageButton : MonoBehaviour {

	public Sprite activatedImage;
	public Sprite deactivatedImage;

	Image sr;

	public void Active()
	{
		sr.sprite = activatedImage;
	}

	public void Deactive()
	{
		sr.sprite = deactivatedImage;
	}

	// Use this for initialization
	void Awake () {
		sr = transform.Find("Image").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
