using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonEffector : MonoBehaviour {

	public GameObject[] baseEffects;
	public GameObject[] highlightEffects;
	public TileColor color;
	GameObject baseEffect;
	GameObject highlightEffect;
	Image buttonImage;
	GameObject textObject;

	// Use this for initialization
	void Start () {
		buttonImage = gameObject.GetComponent<Image>();
		textObject = gameObject.transform.Find("Text").gameObject;

		int colorIndex = (int)color;
		baseEffect = Instantiate(baseEffects[colorIndex], transform.position, Quaternion.identity) as GameObject;
		baseEffect.transform.parent = gameObject.transform;
		baseEffect.transform.localPosition += new Vector3(0,-40f,-30f);
		baseEffect.transform.localScale = new Vector3(1, 1, 1);
		highlightEffect = Instantiate(highlightEffects[colorIndex], transform.position, Quaternion.identity) as GameObject;
		highlightEffect.transform.parent = gameObject.transform;
		highlightEffect.SetActive(false);
		textObject.transform.localPosition += new Vector3(0,0,-60f);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject == EventSystem.current.currentSelectedGameObject)
		{
			// buttonImage.color = new Color(1, 1, 1, 1);

			if (highlightEffect.activeInHierarchy == false)
				highlightEffect.SetActive(true);

			if (baseEffect.activeInHierarchy == false)
				baseEffect.SetActive(true);
		}
		else
		{
			// buttonImage.color = new Color(0.6f, 0.6f, 0.6f, 1);

			if (highlightEffect.activeInHierarchy == true)
				highlightEffect.SetActive(false);

			if (baseEffect.activeInHierarchy == true)
				baseEffect.SetActive(false);
		}
	}
}
