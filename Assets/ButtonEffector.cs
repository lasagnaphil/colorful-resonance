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

	// Use this for initialization
	void Start () {
		int colorIndex = (int)color;
		baseEffect = Instantiate(baseEffects[colorIndex], transform.position + new Vector3(0,0,1), Quaternion.identity) as GameObject;
		highlightEffect = Instantiate(highlightEffects[colorIndex], transform.position, Quaternion.identity) as GameObject;
		highlightEffect.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if ((gameObject == EventSystem.current.currentSelectedGameObject) &&
			(highlightEffect.activeInHierarchy == false))
		{
			highlightEffect.SetActive(true);
		}

		if ((gameObject != EventSystem.current.currentSelectedGameObject) &&
			(highlightEffect.activeInHierarchy == true))
		{
			highlightEffect.SetActive(false);
		}
	}
}
