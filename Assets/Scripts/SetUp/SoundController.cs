using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Text BGMOn;
    public Text BGMOff;
    public Text SEOn;
    public Text SEOff;

    public void MakeBGMOn()
    {
        BGMOn.color = new Color(0.2f, 0.2f, 0.2f);
        BGMOff.color = new Color(1f, 1f, 1f);
        Debug.Log("BGM On");
    }

    public void MakeBGMOff()
    {
        BGMOff.color = new Color(0.2f, 0.2f, 0.2f);
        BGMOn.color = new Color(1f, 1f, 1f);
        Debug.Log("BGM Off");
    }

    public void MakeSEOn()
    {
        SEOn.color = new Color(0.2f, 0.2f, 0.2f);
        SEOff.color = new Color(1f, 1f, 1f);
        Debug.Log("SE On");
    }

    public void MakeSEOff()
    {
        SEOff.color = new Color(0.2f, 0.2f, 0.2f);
        SEOn.color = new Color(1f, 1f, 1f);
        Debug.Log("SE Off");
    }
}
