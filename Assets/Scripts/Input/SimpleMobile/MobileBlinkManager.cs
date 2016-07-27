using UnityEngine;
using System.Collections;
using InputManagement;

public class MobileBlinkManager : MonoBehaviour
{
    public SimpleMobileInputHandler inputHandler;

	void OnMouseDown()
	{
	    inputHandler.AtBlinkButtonPressed();
	}
}
