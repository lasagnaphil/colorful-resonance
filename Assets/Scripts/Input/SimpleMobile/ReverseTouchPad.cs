using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTouchPad : MonoBehaviour
{
    public Transform BlinkButton;
    public Transform MovingButton;

    private void Start()
    {
        if (PlayerPrefs.GetString("Hander") == "Right")
        {
            BlinkButton.position = new Vector3(-5, -2, 0);
            MovingButton.position = new Vector3(4.5f, -1.7f, 0);
        }
        else if (PlayerPrefs.GetString("Hander") == "Left")
        {
            BlinkButton.position = new Vector3(5, -2, 0);
            MovingButton.position = new Vector3(-4.5f, -1.7f, 0);
        }
    }
}
