using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeleteTip : MonoBehaviour
{
	public GameObject GameManager;
    public GameObject TipTrickeryPanel;

	private string MapToLoad;

	void Update()
	{
		MapToLoad = GameManager.GetComponent<MapLoader> ().MapToLoad;

		if (MapToLoad != "1-1" && MapToLoad != "1-2" && MapToLoad != "1-3" && MapToLoad != "1-04"
		   && MapToLoad != "1-05" && MapToLoad != "1-06" && MapToLoad != "1-07" && MapToLoad != "1-08"
            && MapToLoad != "1-09" && MapToLoad != "1-10" && MapToLoad != "2-1")
		{
			Destroy (gameObject);
            Destroy (TipTrickeryPanel);
		}
	}
}
