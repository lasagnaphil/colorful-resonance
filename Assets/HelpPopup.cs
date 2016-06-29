using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpPopup : MonoBehaviour {

	public GameObject helpPopupUI;
	public Text helpText;

	MapLoader mapLoader;
	int currentMapIndex;
	int pastMapIndex = -1;

	// Use this for initialization
	void Start () {
		mapLoader = GameStateManager.Instance.mapLoader;
		helpPopupUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		currentMapIndex = mapLoader.mapIndex;
		if (currentMapIndex != pastMapIndex)
		{
			UpdateHelpPopupUI();
			pastMapIndex = currentMapIndex;
		}
	}

	void UpdateHelpPopupUI()
	{
		if (currentMapIndex > 4) helpPopupUI.SetActive(false);
		else
		{
			helpPopupUI.SetActive(true);

			if (currentMapIndex == 0)
			{
				helpText.text = "- 방향키를 이용해 이동합니다.\n- 구슬에 닿으면 색이 생깁니다.\n- 뾰족하게 솟은 벽은 통과할 수 없습니다.";
			}
			else if (currentMapIndex == 1)
			{
				helpText.text = "- 같은 색으로 둘러쌓이면 채워집니다.\n- 채워질 때, 몬스터에게 영향을 줍니다.\n- 몬스터들은 고유한 색을 가집니다.\n- 일치하는 색에만 영향을 받습니다.";
			}
			else if (currentMapIndex == 2)
			{
				helpText.text = "- 적절하게 색을 바꾸며 퍼즐을 풀어보세요.\n- 몬스터에게 부딪히지 않게 조심하세요!\n- 다양한 몬스터의 특징을 파악하세요.";
			}
			else if (currentMapIndex == 3)
			{
				helpText.text = "- 뾰족한 벽도 색칠된 타일로 인식됩니다.\n- 스페이스 키를 누르고 이동하면\n  한 번에 멀리 이동할 수 있습니다.\n- 모든 것들은 한 턴에 한 번 행동합니다.\n- 움직이지 않으면 턴은 흐르지 않습니다.\n  조급해하지 마세요!";
			}
			else if (currentMapIndex == 4)
			{
				helpText.text = "- 타일이 없는 곳, 즉 구멍 뚫린 곳을\n  포함해서는 색을 채울 수 없습니다.\n- 위험한 순간에도 당황하지 마세요.\n- 뛰어넘기를 잘 사용하세요!";
			}
		}
	}
}
