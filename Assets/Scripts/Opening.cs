using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Opening : MonoBehaviour
{
    public SpriteRenderer LogoIcon1;
    public SpriteRenderer LogoIcon2;
    public SpriteRenderer LogoIcon3;
    public SpriteRenderer LogoIcon4;
    public SpriteRenderer LogoFace;
    public SpriteRenderer LogoWord1;
    public SpriteRenderer LogoWord2;
    public SpriteRenderer LogoWord3;
    public SpriteRenderer LogoName;
    public GameObject Panel;

    private float OpeningTime;
    private bool OpeningOnGoing;
    private bool isCoroutinePlaying = false;

    void Start()
    {
        OpeningOnGoing = true;

        LogoIcon1.color = new Color(1, 1, 1, 0);
        LogoIcon2.color = new Color(1, 1, 1, 0);
        LogoIcon3.color = new Color(1, 1, 1, 0);
        LogoIcon4.color = new Color(1, 1, 1, 0);
        LogoFace.color = new Color(1, 1, 1, 0);
        LogoWord1.color = new Color(1, 1, 1, 0);
        LogoWord2.color = new Color(1, 1, 1, 0);
        LogoWord3.color = new Color(1, 1, 1, 0);
        LogoName.color = new Color(1, 1, 1, 0);

        OpeningTime = 0;
    }

    void Update()
    {
        OpeningTime = OpeningTime + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SkipLogo();
            OpeningTime = 4.5f;
        }

        if (OpeningTime >= 6f)
        {
            OpeningOnGoing = false;
            StartCoroutine(GoToScene("Main"));
        }

        if (OpeningOnGoing == true)
        {
            LogoFace.color = new Color(1, 1, 1, OpeningTime);

            if (OpeningTime >= 1f)
            {
                LogoIcon1.color = new Color(1, 1, 1, 1);
            }
            if (OpeningTime >= 1.5f)
            {
                LogoIcon2.color = new Color(1, 1, 1, 1);
            }
            if (OpeningTime >= 2f)
            {
                LogoIcon3.color = new Color(1, 1, 1, 1);
            }
            if (OpeningTime >= 2.5f)
            {
                LogoIcon4.color = new Color(1, 1, 1, 1);
            }
            if (OpeningTime >= 3f)
            {
                LogoWord1.color = new Color(1, 1, 1, OpeningTime - 3f);
            }
            if (OpeningTime >= 3.5f)
            {
                LogoWord2.color = new Color(1, 1, 1, OpeningTime - 3.5f);
            }
            if (OpeningTime >= 4f)
            {
                LogoWord3.color = new Color(1, 1, 1, OpeningTime - 4f);
            }
            if (OpeningTime >= 4.5f)
            {
                Panel.GetComponent<Transform>().position = new Vector3(Panel.GetComponent<Transform>().position.x + (OpeningTime / 60), Panel.GetComponent<Transform>().position.y, 0);
                LogoName.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (OpeningTime / 1.5f) - 3f);
            }
        }
        else
        {
            LogoIcon1.color = new Color(1, 1, 1, 1);
            LogoIcon2.color = new Color(1, 1, 1, 1);
            LogoIcon3.color = new Color(1, 1, 1, 1);
            LogoIcon4.color = new Color(1, 1, 1, 1);
            LogoFace.color = new Color(1, 1, 1, 1);
            LogoWord1.color = new Color(1, 1, 1, 1);
            LogoWord2.color = new Color(1, 1, 1, 1);
            LogoWord3.color = new Color(1, 1, 1, 1);
            LogoName.color = new Color(1, 1, 1, 1);
            Panel.SetActive(false);
        }
    }

    public void SkipLogo()
    {
        if (OpeningOnGoing == true)
        {
            OpeningOnGoing = false;
        }
        else
        {
            StartCoroutine(GoToScene("Main"));
        }
    }

    IEnumerator GoToScene(string sceneName)
    {
        isCoroutinePlaying = true;
        FadeEffectCanvas fadeEffectCanvas = FindObjectOfType<FadeEffectCanvas>();
        IEnumerator coroutine = fadeEffectCanvas.PlayFadeOutEffect();
        yield return StartCoroutine(coroutine);
        isCoroutinePlaying = false;
        SceneManager.LoadScene(sceneName);
    }
}
