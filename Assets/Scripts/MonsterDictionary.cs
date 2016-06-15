using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterDictionary : MonoBehaviour {

	public Image monsterImage;
	public Text monsterText;

	public Sprite[] monsterImageArray;
	public string[] monsterTextArray;

	void Awake()
	{
		monsterTextArray = new string[16];
		//gray
		monsterTextArray[0] = "이쪽을 힐끔힐끔 보고있다.\n보기만 하고 움직이지 않는 것 같다.";
		monsterTextArray[1] = "비슷하게 생긴 애를 본 것 같은데...\n가끔 움직인다. 저건 다리였던 걸까?";
		monsterTextArray[2] = "웃고 있는 것 같은데 왠지 무서워...!\n옆을 지나가는데 뭔가 날아와서 깜짝 놀랐다.";
		monsterTextArray[3] = "까만 애랑 쌍둥이일까?\n비슷하면서도 다르다.";
		//red
		monsterTextArray[4] = "항상 화가 나 있는 것 같다.\n사라진 다음에도 뭔가가 계속 쫓아와...!\n벽 뒤로 숨으면...안전할지도?";
		monsterTextArray[5] = "화가 더 많이 난 걸까...?\n쫓아오지는 않지만, 폭발하니까 위험!\n그 폭탄, 어디 숨겨놨던거지...";
		monsterTextArray[6] = "두 배로 무서워졌어...\n엄청 큰 폭탄을 남길까봐 걱정했는데,\n바닥째로 사라져버렸다.";
		monsterTextArray[7] = "다른 애들이랑은 좀 다르게 생겼다.\n그렇지만 또 폭탄이야!\n이번엔 페인트라도 들어있는걸까?";
		//blue
		monsterTextArray[8] = "셋이 항상 같이 다닌다.\n파란색인데, 왠지 파란색을 싫어하는 것 같다.";
		monsterTextArray[9] = "지나간 자리가 깔끔해진다.\n바닥에 반짝거리던 색은, 페인트였던 건가...";
		monsterTextArray[10] = "어떻게 서 있는지 궁금하다.\n멀리서도 자꾸 물을 뿌려...!";
		monsterTextArray[11] = "비슷한 빨간 애를 본 것 같다.\n이번엔 물풍선이라니.\n파란 애들은, 청소를 좋아하나보다.";
		//yellow
		monsterTextArray[12] = "동그랗다.\n왠지 모르지만 단단하다. 두 배 정도?";
		monsterTextArray[13] = "머리에 있는건 새싹일까?\n처음엔 몰랐지만,\n갑자기 눈을 붉히며 따라오던 건 무서웠어!";
		monsterTextArray[14] = "꽃이다. 동그란 애들은 씨앗이었던걸까?\n움직이지 않는다고 생각했는데,\n순식간에 다가와서 깜짝 놀랐다.";
		monsterTextArray[15] = "부스러기인줄 알았는데, 불쑥 나타났다.\n첫 등장엔 깜짝 놀랐지만...";
	}	

	public void LoadMonsterData(int index)
	{
		monsterImage.sprite = monsterImageArray[index];
		monsterText.text = monsterTextArray[index];
	}
}
