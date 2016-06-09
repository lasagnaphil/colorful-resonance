using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultUIManager : MonoBehaviour {

    public GameObject winUI;
    public Image winWhiteBg;	
    public Image winBg;
    public GameObject[] winMarks;
    public GameObject loseUI;
    public Image loseWhiteBg;
	public Image loseBg;
    public GameObject[] loseMarks;

	// Use this for initialization
	void Start () {
		Initialize();
	}
		
	public void PopupWinUI()
	{
		StartCoroutine(PopupWinUICoroutine());
	}
	
	IEnumerator PopupWinUICoroutine()
	{
		winUI.SetActive(true);
		
		foreach (var winMark in winMarks)
			winMark.GetComponent<Image>().color = new Color(1,1,1,0);
		
		winBg.color = new Color(1,1,1,0);
		winWhiteBg.color = new Color(1,1,1,0);
		
		for (int i = 0; i < 20; i++)
		{
			winBg.color += new Color(0,0,0,0.05f);
			winWhiteBg.color += new Color(0,0,0,0.05f*(100f/255f));
			yield return new WaitForSeconds(0.05f);
		}

		foreach (var winMark in winMarks)
		{
			winMark.GetComponent<Image>().color = new Color(1,1,1,1);
			for (int i = 0; i < 36; i++)
			{
				winMark.transform.Rotate(new Vector3(0, 10, 0));
				yield return new WaitForSeconds(0.005f);
			}
		}
		
		yield return null;
	}
	
	public void PopupLoseUI()
	{
		StartCoroutine(PopupLoseUICoroutine());		
	}

	IEnumerator PopupLoseUICoroutine()
	{
		loseUI.SetActive(true);
		
		foreach (var loseMark in loseMarks)
			loseMark.GetComponent<Image>().color = new Color(1,1,1,0);
				
		loseBg.color = new Color(1,1,1,0);
		loseWhiteBg.color = new Color(1,1,1,0);
		
		for (int i = 0; i < 20; i++)
		{
			loseBg.color += new Color(0,0,0,0.05f);
			loseWhiteBg.color += new Color(0,0,0,0.05f*(100f/255f));
			yield return new WaitForSeconds(0.05f);
		}

		foreach (var loseMark in loseMarks)
		{
			loseMark.GetComponent<Image>().color = new Color(1,1,1,1);
			for (int i = 0; i < 36; i++)
			{
				loseMark.transform.Rotate(new Vector3(0, 10, 0));
				yield return new WaitForSeconds(0.005f);
			}
		}
		
		yield return null;
	}
	
	public void Initialize()
	{
		StopCoroutine("PopupWinUICoroutine");
		StopCoroutine("PopupLoseUICoroutine");
		
		winUI.SetActive(false);
		loseUI.SetActive(false);
	}
}
