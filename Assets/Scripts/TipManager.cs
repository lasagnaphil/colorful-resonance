using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TipManager : MonoBehaviour
{
	public GameObject GameManager;

	private string MapToLoad;

	void Update()
	{
		MapToLoad = GameManager.GetComponent<MapLoader> ().MapToLoad;

		if (MapToLoad == "1-1")
			GetComponent<Text> ().text = "방향키를 눌러 루시를 움직여 보세요";
		else if (MapToLoad == "1-2")
			GetComponent<Text> ().text = "몬스터와 동일한 색으로 주위 타일을 둘러싸 보세요";
		else if (MapToLoad == "1-3")
			GetComponent<Text> ().text = "몬스터의 움직임을 잘 관찰하고 피하면서 잡아 보세요";
		else if (MapToLoad == "1-04")
			GetComponent<Text> ().text = "스페이스 바를 누고 있으면 블링크 스킬을 사용 할 수 있어요";
		else if (MapToLoad == "1-05")
			GetComponent<Text> ().text = "레버를 당기면 벽이 생기거나 사라져요";
		else if (MapToLoad == "1-06")
			GetComponent<Text> ().text = "몬스터가 쏘는 탄환에 주의하세요";
		else if (MapToLoad == "1-07")
			GetComponent<Text> ().text = "맵과 몬스터를 잘 관찰하세요";
		else if (MapToLoad == "1-08")
			GetComponent<Text> ().text = "둘러싸지 않더라도 경계에 있어도 몬스터를 잡을 수 있어요";
		else if (MapToLoad == "1-09")
			GetComponent<Text> ().text = "벽의 색을 이용해보세요";
		else if (MapToLoad == "1-10")
			GetComponent<Text> ().text = "기억하세요! 둘러싸지 않고 그 경계에 있어도 몬스터를 잡을 수 있어요!";
		else
			GetComponent<Text> ().text = "Error";
	}
}
