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

	// Use this for initialization
	void Start () {
		buttonImage = gameObject.GetComponent<Image>();

		int colorIndex = (int)color;
		baseEffect = Instantiate(baseEffects[colorIndex], transform.position + new Vector3(0,0,1), Quaternion.identity) as GameObject;
		highlightEffect = Instantiate(highlightEffects[colorIndex], transform.position, Quaternion.identity) as GameObject;
		highlightEffect.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject == EventSystem.current.currentSelectedGameObject)
		{
			buttonImage.color = new Color(1, 1, 1, 1);

			if (highlightEffect.activeInHierarchy == false)
				highlightEffect.SetActive(true);

			if (baseEffect.activeInHierarchy == false)
				baseEffect.SetActive(true);
		}
		else
		{
			buttonImage.color = new Color(0.6f, 0.6f, 0.6f, 1);

			if (highlightEffect.activeInHierarchy == true)
				highlightEffect.SetActive(false);

			if (baseEffect.activeInHierarchy == true)
				baseEffect.SetActive(false);
		}
	}
}
