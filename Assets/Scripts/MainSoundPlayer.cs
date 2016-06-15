using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSoundPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);

		if (GameObject.Find("MainSoundPlayer") != null)
			Destroy(gameObject);
		else
			gameObject.name = "MainSoundPlayer";
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "Game")
			Destroy(gameObject);	
	}
}
