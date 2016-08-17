using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StageButtonManager : MonoBehaviour {

    List<GameObject> stage1Buttons;
    List<GameObject> stage2Buttons;
    List<GameObject> stage3Buttons;
    List<GameObject> stage4Buttons;

    // Use this for initialization
    void Start () {
        stage1Buttons = new List<GameObject>();
        stage2Buttons = new List<GameObject>();
        stage3Buttons = new List<GameObject>();
        stage4Buttons = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
