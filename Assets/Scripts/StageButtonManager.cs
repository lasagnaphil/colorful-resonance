using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StageButtonManager : MonoBehaviour {

    public List<GameObject> stage1Buttons;
    public List<GameObject> stage2Buttons;
    public List<GameObject> stage3Buttons;
    public List<GameObject> stage4Buttons;

    int levelindex;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveLeft()
    {
        if (levelindex > 0)
        {
            levelindex -= 1;
            foreach (GameObject g in stage1Buttons)
            {
                g.transform.position += new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage2Buttons)
            {
                g.transform.position += new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage3Buttons)
            {
                g.transform.position += new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage4Buttons)
            {
                g.transform.position += new Vector3(Screen.width, 0, 0);
            }
        }
        Debug.Log(levelindex.ToString());
    }

    public void MoveRight()
    {
        if (levelindex < 2)
        {
            levelindex += 1;
            foreach (GameObject g in stage1Buttons)
            {
                g.transform.position -= new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage2Buttons)
            {
                g.transform.position -= new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage3Buttons)
            {
                g.transform.position -= new Vector3(Screen.width, 0, 0);
            }
            foreach (GameObject g in stage4Buttons)
            {
                g.transform.position -= new Vector3(Screen.width, 0, 0);
            }
        }
        Debug.Log(levelindex.ToString());
    }

    public void ResetMove()
    {
        while (levelindex != 0)
        {
            MoveLeft();
        }
    }
    
}
