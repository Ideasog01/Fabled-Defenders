using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private Image blackBack;
    private Text introText;

    private GameObject introCan;

    // Start is called before the first frame update
    void Start()
    {
        blackBack = GameObject.Find("BlackBack").GetComponent<Image>();
        introText = GameObject.Find("WordText").GetComponent<Text>();
        introText.canvasRenderer.SetAlpha(0.0f);
        blackBack.canvasRenderer.SetAlpha(0.0f);
        blackBack.CrossFadeAlpha(0, 0, false);
        introCan = GameObject.Find("IntroCanvas");
        introCan.SetActive(false);
    }

    public void IntroStart()
    {
        introCan.SetActive(true);
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        blackBack.CrossFadeAlpha(0, 0, false);
        introText.CrossFadeAlpha(0, 0, false);
        yield return new WaitForSeconds(1);
        blackBack.CrossFadeAlpha(1, 1, false);
        introText.CrossFadeAlpha(0, 0, false);
        yield return new WaitForSeconds(1);
        introText.text = "BEFORE THE WORLD GREW CRUEL";
        yield return new WaitForSeconds(1);
        introText.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(4);
        introText.CrossFadeAlpha(0, 2, false);
        blackBack.CrossFadeAlpha(0, 2, false);
        GameObject.Find("MenuManager").GetComponent<MenuManager>().LoadNilermsWatch();
    }
}
