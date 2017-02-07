using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDataManager : MonoBehaviour
{
    public GameObject[] Stage;
    public Sprite UnlockTile;

    void Start()
    {
        SetAllCleared();

        if (PlayerPrefs.GetString("1-1 Clear") == "Yes")
        {
            Stage[1].GetComponent<Image>().sprite = UnlockTile;
            Stage[1].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-2 Clear") == "Yes")
        {
            Stage[2].GetComponent<Image>().sprite = UnlockTile;
            Stage[2].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-3 Clear") == "Yes")
        {
            Stage[3].GetComponent<Image>().sprite = UnlockTile;
            Stage[3].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-4 Clear") == "Yes")
        {
            Stage[4].GetComponent<Image>().sprite = UnlockTile;
            Stage[4].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-5 Clear") == "Yes")
        {
            Stage[5].GetComponent<Image>().sprite = UnlockTile;
            Stage[5].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-6 Clear") == "Yes")
        {
            Stage[6].GetComponent<Image>().sprite = UnlockTile;
            Stage[6].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-7 Clear") == "Yes")
        {
            Stage[7].GetComponent<Image>().sprite = UnlockTile;
            Stage[7].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-8 Clear") == "Yes")
        {
            Stage[8].GetComponent<Image>().sprite = UnlockTile;
            Stage[8].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-9 Clear") == "Yes")
        {
            Stage[9].GetComponent<Image>().sprite = UnlockTile;
            Stage[9].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("1-10 Clear") == "Yes")
        {
            Stage[10].GetComponent<Image>().sprite = UnlockTile;
            Stage[10].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-1 Clear") == "Yes")
        {
            Stage[11].GetComponent<Image>().sprite = UnlockTile;
            Stage[11].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-2 Clear") == "Yes")
        {
            Stage[12].GetComponent<Image>().sprite = UnlockTile;
            Stage[12].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-3 Clear") == "Yes")
        {
            Stage[13].GetComponent<Image>().sprite = UnlockTile;
            Stage[13].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-4 Clear") == "Yes")
        {
            Stage[14].GetComponent<Image>().sprite = UnlockTile;
            Stage[14].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-5 Clear") == "Yes")
        {
            Stage[15].GetComponent<Image>().sprite = UnlockTile;
            Stage[15].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("2-6 Clear") == "Yes")
        {
            Stage[16].GetComponent<Image>().sprite = UnlockTile;
            Stage[16].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("3-1 Clear") == "Yes")
        {
            Stage[17].GetComponent<Image>().sprite = UnlockTile;
            Stage[17].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("3-2 Clear") == "Yes")
        {
            Stage[18].GetComponent<Image>().sprite = UnlockTile;
            Stage[18].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("3-3 Clear") == "Yes")
        {
            Stage[19].GetComponent<Image>().sprite = UnlockTile;
            Stage[19].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("3-4 Clear") == "Yes")
        {
            Stage[20].GetComponent<Image>().sprite = UnlockTile;
            Stage[20].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("3-5 Clear") == "Yes")
        {
            Stage[21].GetComponent<Image>().sprite = UnlockTile;
            Stage[21].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("4-1 Clear") == "Yes")
        {
            Stage[22].GetComponent<Image>().sprite = UnlockTile;
            Stage[22].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("4-2 Clear") == "Yes")
        {
            Stage[23].GetComponent<Image>().sprite = UnlockTile;
            Stage[23].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("4-3 Clear") == "Yes")
        {
            Stage[24].GetComponent<Image>().sprite = UnlockTile;
            Stage[24].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("4-4 Clear") == "Yes")
        {
            Stage[25].GetComponent<Image>().sprite = UnlockTile;
            Stage[25].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("4-5 Clear") == "Yes")
        {
            Stage[26].GetComponent<Image>().sprite = UnlockTile;
            Stage[26].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("5-1 Clear") == "Yes")
        {
            Stage[27].GetComponent<Image>().sprite = UnlockTile;
            Stage[27].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("5-2 Clear") == "Yes")
        {
            Stage[28].GetComponent<Image>().sprite = UnlockTile;
            Stage[28].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("5-3 Clear") == "Yes")
        {
            Stage[29].GetComponent<Image>().sprite = UnlockTile;
            Stage[29].GetComponent<Button>().enabled = true;
        }
        if (PlayerPrefs.GetString("5-4 Clear") == "Yes")
        {
            Stage[30].GetComponent<Image>().sprite = UnlockTile;
            Stage[30].GetComponent<Button>().enabled = true;
        }
    }

    void SetAllCleared()
    {
        PlayerPrefs.SetString("1-1 Clear", "Yes");
        PlayerPrefs.SetString("1-2 Clear", "Yes");
        PlayerPrefs.SetString("1-3 Clear", "Yes");
        PlayerPrefs.SetString("1-4 Clear", "Yes");
        PlayerPrefs.SetString("1-5 Clear", "Yes");
        PlayerPrefs.SetString("1-6 Clear", "Yes");
        PlayerPrefs.SetString("1-7 Clear", "Yes");
        PlayerPrefs.SetString("1-8 Clear", "Yes");
        PlayerPrefs.SetString("1-9 Clear", "Yes");
        PlayerPrefs.SetString("1-10 Clear", "Yes");
        PlayerPrefs.SetString("2-1 Clear", "Yes");
        PlayerPrefs.SetString("2-2 Clear", "Yes");
        PlayerPrefs.SetString("2-3 Clear", "Yes");
        PlayerPrefs.SetString("2-4 Clear", "Yes");
        PlayerPrefs.SetString("2-5 Clear", "Yes");
        PlayerPrefs.SetString("2-6 Clear", "Yes");
        PlayerPrefs.SetString("2-7 Clear", "Yes");
        PlayerPrefs.SetString("3-1 Clear", "Yes");
        PlayerPrefs.SetString("3-2 Clear", "Yes");
        PlayerPrefs.SetString("3-3 Clear", "Yes");
        PlayerPrefs.SetString("3-4 Clear", "Yes");
        PlayerPrefs.SetString("3-5 Clear", "Yes");
        PlayerPrefs.SetString("4-1 Clear", "Yes");
        PlayerPrefs.SetString("4-2 Clear", "Yes");
        PlayerPrefs.SetString("4-3 Clear", "Yes");
        PlayerPrefs.SetString("4-4 Clear", "Yes");
        PlayerPrefs.SetString("4-5 Clear", "Yes");
        PlayerPrefs.SetString("5-1 Clear", "Yes");
        PlayerPrefs.SetString("5-2 Clear", "Yes");
        PlayerPrefs.SetString("5-3 Clear", "Yes");
        PlayerPrefs.SetString("5-4 Clear", "Yes");
        PlayerPrefs.SetString("5-5 Clear", "Yes");
    }
}
