using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Vector2 teleportPos;
    private GameObject doorToTeleport;
    private Image blackImage;
    private GameObject cam;

    [SerializeField]
    private bool doorLocked;

    [SerializeField]
    private string keyCode;

    private bool createPadlock;

    private GameObject instantiatedPadlock;
    private GameObject fadeCanvas;
    private bool moveToLoad;

    [SerializeField]
    private bool instantiateEnemiesOnUse;

    private GameObject areaTextOBJ;

    [SerializeField]
    private string areaTextContent;

    [SerializeField]
    private bool loadScene;
    [SerializeField]
    private string newSceneName;

    [SerializeField]
    private Transform teleportPosObj;



    [SerializeField]
    private bool setCheckpoint;

    [SerializeField]
    private Vector2 checkpointPos;

    private void Start()
    {
        player = GameObject.Find("Player");
        blackImage = FindObjectOfType<GameManager>().blackImage.GetComponent<Image>();
        fadeCanvas = GameObject.Find("FadeLoadingCanvas");
        cam = GameObject.Find("Main Camera");
        areaTextOBJ = FindObjectOfType<GameManager>().areaText;
        blackImage.canvasRenderer.SetAlpha(0.0f);
        if(teleportPosObj != null)
        {
            teleportPos = teleportPosObj.transform.position;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Player");
        blackImage = FindObjectOfType<GameManager>().blackImage.GetComponent<Image>();
        fadeCanvas = GameObject.Find("FadeLoadingCanvas");
        cam = GameObject.Find("Main Camera");
        areaTextOBJ = FindObjectOfType<GameManager>().areaText;
        blackImage.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        if (doorLocked && createPadlock == false)
        {
            this.GetComponent<SpriteRenderer>().color = Color.grey;
            instantiatedPadlock = Instantiate(FindObjectOfType<GameManager>().padlockPrefab.gameObject, this.transform.position + new Vector3(0, 2, 0), this.transform.rotation);
            instantiatedPadlock.transform.parent = this.gameObject.transform;
            createPadlock = true;
        }
        if (doorLocked == false)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(instantiatedPadlock);
        }

        if (moveToLoad == true)
        {
            player.transform.Translate(Vector2.right * 0.8f * Time.deltaTime);
        }

        if (FindObjectOfType<PlayerProperties>().keyCodes.Contains(keyCode))
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(instantiatedPadlock);
        }



    }




    public void TeleportPlayer()
    {
        if (doorLocked == false)
        {
            if(!loadScene)
            {
                StartCoroutine(TeleportThePlayer());
                if (instantiateEnemiesOnUse)
                {
                    this.gameObject.GetComponent<SpawnEnemies>().SpawnEnemy();
                    instantiateEnemiesOnUse = false;
                }

                if (setCheckpoint)
                {
                    FindObjectOfType<PlayerProperties>().nearestCheckpoint = checkpointPos;
                }
            }
            

            if(loadScene)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel(newSceneName);
                if(instantiateEnemiesOnUse)
                {
                    this.gameObject.GetComponent<SpawnEnemies>().SpawnEnemiesInNewScene();
                }
                
            }

        }
        else
        {
            if (FindObjectOfType<PlayerProperties>().keyCodes.Contains(keyCode))
            {
                if(!loadScene)
                {
                    StartCoroutine(TeleportThePlayer());
                    if (instantiateEnemiesOnUse)
                    {
                        this.gameObject.GetComponent<SpawnEnemies>().SpawnEnemy();
                        instantiateEnemiesOnUse = false;
                    }
                    doorLocked = false;
                }
                

                if (loadScene)
                {
                    GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel(newSceneName);
                    if (instantiateEnemiesOnUse)
                    {
                        this.gameObject.GetComponent<SpawnEnemies>().SpawnEnemiesInNewScene();
                    }
                }
            }



        }

        IEnumerator TeleportThePlayer()
        {
            blackImage.canvasRenderer.SetAlpha(0.0f);
            fadeCanvas.SetActive(true);
            blackImage.gameObject.SetActive(true);
            blackImage.CrossFadeAlpha(1, 1, false);
            yield return new WaitForSeconds(1);
            player.transform.position = teleportPos;
            GameObject.Find("MagnifyCursor").GetComponent<MagnifyCursorScript>().PlayerTeleported();
            moveToLoad = true;
            cam.transform.position = player.transform.position;
            yield return new WaitForSeconds(5);
            moveToLoad = false;
            blackImage.CrossFadeAlpha(0, 1, false);
            areaTextOBJ.GetComponent<Text>().text = areaTextContent;
            yield return new WaitForSeconds(1);
            areaTextOBJ.GetComponent<Text>().CrossFadeAlpha(1, 1, false);
            blackImage.canvasRenderer.SetAlpha(0.0f);
            fadeCanvas.SetActive(false);
            yield return new WaitForSeconds(2.5f);
            areaTextOBJ.GetComponent<Text>().CrossFadeAlpha(0, 1, false);
            yield return new WaitForSeconds(1);
            areaTextOBJ.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        }


    }
}
