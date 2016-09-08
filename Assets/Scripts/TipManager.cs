using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TipManager : MonoBehaviour
{
	public GameObject GameManager;

	private string MapToLoad;

    private Text textComp;

    void Awake()
    {
        textComp = GetComponent<Text>();
    }
	void Update()
	{
		MapToLoad = GameManager.GetComponent<MapLoader> ().MapToLoad;

		if (MapToLoad == "1-1")
		    textComp.text = "화면을 터치해 루시를 움직여 보세요";
		else if (MapToLoad == "1-2")
			textComp.text = "몬스터와 동일한 색으로 주위 타일을 둘러싸 보세요";
		else if (MapToLoad == "1-3")
			textComp.text = "몬스터의 움직임을 잘 관찰하고 피하면서 잡아 보세요";
		else if (MapToLoad == "1-04")
			textComp.text = "루시를 꾹 누르면 블링크 스킬을 사용 할 수 있어요";
		else if (MapToLoad == "1-05")
			textComp.text = "레버를 당기면 벽이 생기거나 사라져요";
		else if (MapToLoad == "1-06")
			textComp.text = "몬스터가 쏘는 탄환에 주의하세요";
		else if (MapToLoad == "1-07")
			textComp.text = "맵과 몬스터를 잘 관찰하세요";
		else if (MapToLoad == "1-08")
			textComp.text = "둘러싸지 않더라도 경계에 있어도 몬스터를 잡을 수 있어요";
		else if (MapToLoad == "1-09")
			textComp.text = "벽의 색을 이용해보세요";
		else if (MapToLoad == "1-10")
			textComp.text = "기억하세요! 둘러싸지 않고 그 경계에 있어도 몬스터를 잡을 수 있어요!";
		else
			textComp.text = "Error";
	}
}
