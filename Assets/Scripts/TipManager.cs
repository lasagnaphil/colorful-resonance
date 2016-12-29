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
		    textComp.text = "화면을 조작해 루시를 움직여 보세요. 조작방식은 설정에서 바꿀 수 있습니다.";
		else if (MapToLoad == "1-2")
			textComp.text = "오브를 먹으면 타일을 칠할 수 있어요. 몬스터와 동일한 색으로 주위 타일을 둘러싸 보세요";
		else if (MapToLoad == "1-3")
			textComp.text = "내가 움직이고, 몬스터가 움직이고... 조급해하지 마세요. 흰둥이는 위로 한칸갔다가 쉬고 아래로 한칸 갔다가 쉬는 것을 반복한답니다.";
		else if (MapToLoad == "1-04")
			textComp.text = "루시를 꾹 누르면 3칸을 이동 할 수 있어요. 사용 제한은 없어요.";
		else if (MapToLoad == "1-05")
			textComp.text = "레버를 당기면 벽이 생기거나 사라져요.";
		else if (MapToLoad == "1-06")
			textComp.text = "몬스터가 위치한 타일이 '다른 색에서 몬스터의 색과 동일하게 변할 때' 몬스터를 잡을 수 있어요.";
		else if (MapToLoad == "1-07")
			textComp.text = "몬스터를 뛰어넘어 레버를 당겨봐요. 벽을 없앨 수 있어요.";
		else if (MapToLoad == "1-08")
            textComp.text = "몬스터를 둘러싸지 않고 그 경계에 있어도 잡을 수 있어요.";
		else if (MapToLoad == "1-09")
			textComp.text = "벽은 색을 칠할 수 없어요. 하지만 이용할 수는 있어요.";
		else if (MapToLoad == "1-10")
			textComp.text = "흰색에서 검은색, 검은색에서 흰색으로 바뀌는 판정을 만들어내세요.";
		else
			textComp.text = "Error";
	}
}
