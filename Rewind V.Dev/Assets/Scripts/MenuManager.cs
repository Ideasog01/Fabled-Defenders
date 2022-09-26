using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private GameObject testMessage;
    private GameObject backstoryMessage;
    private GameObject testGuide;

    public bool saveExists;
    

    private void Start()
    {
        SaveManager.LoadData();
        testMessage = GameObject.Find("MessageBox");
        backstoryMessage = GameObject.Find("MessageBackStory");
        testGuide = GameObject.Find("TestGuide");
        testMessage.transform.localScale = new Vector3(0, 0, 0);
        backstoryMessage.transform.localScale = new Vector3(0, 0, 0);
        testGuide.transform.localScale = new Vector3(0, 0, 0);
       
    }

    private void OnLevelWasLoaded(int level)
    {
        /*     testMessage.transform.localScale = new Vector3(0, 0, 0);
             backstoryMessage.transform.localScale = new Vector3(0, 0, 0);
            testGuide.transform.localScale = new Vector3(0, 0, 0);
            */
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTestMessage()
    {
        testMessage.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ShowStoryMessage()
    {
        testMessage.transform.localScale = new Vector3(0, 0, 0);
        backstoryMessage.transform.localScale = new Vector3(1, 1, 1);
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Stop("MainTheme");
        FindObjectOfType<AudioManager>().Stop("Forest");
        GameObject.Find("TutorialManager").GetComponent<TutorialManager>().IntroStart();
    }

    public void ShowGuide()
    {
        testGuide.transform.localScale = new Vector3(1, 1, 1);
    }

    public void CloseGuide()
    {
        testGuide.transform.localScale = new Vector3(0, 0, 0);
    }

    public void LoadNilermsWatch()
    {
        SceneManager.LoadScene("Nilerm's Watch");
    }
    
}
