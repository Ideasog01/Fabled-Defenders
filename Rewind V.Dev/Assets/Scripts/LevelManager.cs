using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    private Image blackImage;
    private GameObject fadeCanvas;
    private GameObject player;

    private Vector2 spawnOnLoad;
    private string sceneName;

    [SerializeField]
    private List<Vector2> knightSpawnPos;
    [SerializeField]
    private List<Vector2> dolorWarriorPos;
    [SerializeField]
    private List<Vector2> runtPos;

    private GameObject GameManager;

    private Text areaTextOBJ;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        blackImage = FindObjectOfType<GameManager>().blackImage.GetComponent<Image>();
        fadeCanvas = GameObject.Find("FadeLoadingCanvas");
        areaTextOBJ = FindObjectOfType<GameManager>().areaText.GetComponent<Text>();
        blackImage.canvasRenderer.SetAlpha(0.0f);
        player = GameObject.Find("Player");
        CheckSceneName();
    }

    private void OnLevelWasLoaded(int level)
    {
        GameManager = GameObject.Find("GameManager");
        blackImage = FindObjectOfType<GameManager>().blackImage.GetComponent<Image>();
        fadeCanvas = GameObject.Find("FadeLoadingCanvas");
        blackImage.canvasRenderer.SetAlpha(0.0f);
        player = GameObject.Find("Player");
        CheckSceneName();
    }

    private void CheckSceneName()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == "Nilerm's Watch")
        {
            spawnOnLoad = new Vector2(216, -2);
        }
        if(sceneName == "Dungeons")
        {
            spawnOnLoad = new Vector2(0, 0);
        }
        if(sceneName == "Farhaven")
        {
            spawnOnLoad = new Vector2(0, 4);
            player.GetComponent<PlayerProperties>().respawnPoint = new Vector2(0, 4);

        }

        player.GetComponent<PlayerProperties>().playerScene = sceneName;
        SaveManager.SaveGame(GameObject.Find("Player").GetComponent<PlayerProperties>());
    }

    

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        

        blackImage.canvasRenderer.SetAlpha(0.0f);
        fadeCanvas.SetActive(true);
        blackImage.gameObject.SetActive(true);
        blackImage.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);



        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);

            yield return null;
        }
       

        if(operation.isDone)
        {
            blackImage.canvasRenderer.SetAlpha(1);
            CheckSceneName();
            player.transform.position = spawnOnLoad;

            

            foreach (Vector2 spawnPoint in knightSpawnPos)
            {
                Instantiate(GameManager.GetComponent<GameManager>().KnightPrefab, spawnPoint, this.transform.rotation);
                knightSpawnPos.Remove(spawnPoint);
            }
            foreach (Vector2 spawnPoint1 in dolorWarriorPos)
            {
                Instantiate(GameManager.GetComponent<GameManager>().DolorPrefab, spawnPoint1, this.transform.rotation);
                runtPos.Remove(spawnPoint1);
            }
            foreach (Vector2 spawnPoint2 in runtPos)
            {
                Instantiate(GameManager.GetComponent<GameManager>().runtPrefab, spawnPoint2, this.transform.rotation);
                dolorWarriorPos.Remove(spawnPoint2);
            }
            yield return new WaitForSeconds(5);
            blackImage.CrossFadeAlpha(0, 1, false);
            yield return new WaitForSeconds(1);
            areaTextOBJ.GetComponent<Text>().text = sceneName;
            yield return new WaitForSeconds(1);
            areaTextOBJ.GetComponent<Text>().CrossFadeAlpha(1, 1, false);
            blackImage.canvasRenderer.SetAlpha(0.0f);
            fadeCanvas.SetActive(false);
            yield return new WaitForSeconds(2.5f);
            areaTextOBJ.GetComponent<Text>().CrossFadeAlpha(0, 1, false);
            yield return new WaitForSeconds(1);
            areaTextOBJ.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
            blackImage.canvasRenderer.SetAlpha(0.0f);
            fadeCanvas.SetActive(false);
            blackImage.gameObject.SetActive(false);

        }
    }

    public void AddKnights(List<Vector2> knightSpawnList)
    {
        foreach(Vector2 spawn in knightSpawnList)
        {
            knightSpawnPos.Add(spawn);
        }
    }

    public void AddRunts(List<Vector2> runtSpawnList)
    {
        foreach (Vector2 spawn1 in runtSpawnList)
        {
            runtPos.Add(spawn1);
        }
    }

    public void AddDolors(List<Vector2> dolorSpawnList)
    {
        foreach (Vector2 spawn2 in dolorSpawnList)
        {
            dolorWarriorPos.Add(spawn2);
        }
    }
}
