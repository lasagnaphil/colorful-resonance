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
            textComp.text = "방향키를 누르면 루시가 움직여요. 오른쪽 위의 팁을 읽어보세요!";
        else if (MapToLoad == "1-2")
            textComp.text = "오브를 먹으면 타일을 칠할 수 있어요. 몬스터와 동일한 색으로 주위 타일을 둘러싸 보세요";
        else if (MapToLoad == "1-3")
            textComp.text = "내가 움직이고, 몬스터가 움직이고... 조급해하지 마세요. 흰둥이는 위로 한칸갔다가 쉬고 아래로 한칸 갔다가 쉬는 것을 반복한답니다.";
        else if (MapToLoad == "1-04")
            textComp.text = "스페이스 키를 누르고 이동하면 3칸을 이동하는 블링크 스킬을 사용할 수 있어요. 사용 제한은 없어요.";
        else if (MapToLoad == "1-05")
            textComp.text = "레버를 당기면 벽이 생기거나 사라져요.";
        else if (MapToLoad == "1-06")
            textComp.text = "몬스터가 위치한 타일이 '다른 색에서 몬스터의 색과 동일하게 변할 때' 몬스터를 잡을 수 있어요.";
        else if (MapToLoad == "1-07")
            textComp.text = "몬스터를 뛰어넘어 레버를 당겨봐요. 벽을 없앨 수 있어요.";
        else if (MapToLoad == "1-08")
            textComp.text = "'둘러싼 타일의 색이 변할 때' 그 경계에 있는 몬스터도 잡을 수 있어요.";
        else if (MapToLoad == "1-09")
            textComp.text = "벽은 색을 칠할 수 없어요. 하지만 이용할 수는 있어요.";
        else if (MapToLoad == "1-10")
            textComp.text = "흰색 몬스터는 내부 타일이 검은색에서 흰색으로 바뀔 때 잡을 수 있어요.";
        else if (MapToLoad == "2-1")
            textComp.text = "모든 몬스터는 시작화면의 Monsters 탭에 특징 설명이 있어요. 몬스터의 패턴을 파악하고 공략해보세요.";
		else
			textComp.text = "Error";
	}
}
