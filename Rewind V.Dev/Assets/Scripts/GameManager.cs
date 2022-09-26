using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform IceSpike;
    public Transform IceBall;
    public GameObject Letter;

    public InputField inputName;

    public static string playerName;
    public Text playerNameTitle;
    public Text playerNameWords;
    public GameObject nameInputObj;
    private GameObject playerStaff;

    public Transform padlockPrefab;

    public bool staffEquiped;
    public GameObject staffOBJ;

    public Color damagedColor;

    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject heart4;

    public Sprite chestUnlockedSprite;

    public Transform KnightPrefab;
    public GameObject blackImage;

    private Scene currentScene;

    public GameObject areaText;

    [SerializeField]
    private bool skipBeginning;

    public Transform runtBoltPrefab;

    public Transform dolorProjectilePrefab;
    public Transform dolorCorruptionPrefab;

    public GameObject DolorOwner;

    public Transform runtPrefab;
    public Transform DolorPrefab;

    private GameObject player;

    private GameObject optionsTab;
    private GameObject testGuide;
    public Transform arrowPrefab;

    public Transform playerCorruptedPrefab;
    public Transform corruptedPlayer;

    private Text currentAmmoText;

    public bool inBattle;

    public ItemProperties[] artefacts;
    public GameObject artefactEquipButton;

    private int numberOfHearts;
    private int recordedHearts;

    private GameObject shieldStatus;

    private Text powerText;

    public Transform corruptionPickup;

    private bool playerDiedFirstTime;
    private bool saveOnce;
  

    // Start is called before the first frame update
    void Start()
    {
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3");
        heart4 = GameObject.Find("Heart4");
        blackImage = GameObject.Find("blackImageLoading");
        playerStaff = GameObject.Find("PlayerStaff");
        areaText = GameObject.Find("AreaText");
        player = GameObject.Find("Player");
        testGuide = GameObject.Find("TestGuide");
        areaText.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        currentAmmoText = GameObject.Find("CurrentAmmoText").GetComponent<Text>();
        shieldStatus = GameObject.Find("ShieldStatus");
        Cursor.visible = true;
        blackImage.SetActive(false);

        /* optionsTab = GameObject.Find("OptionsTab");
         optionsTab.transform.localScale = new Vector3(0, 0, 0);
         testGuide.transform.localScale = new Vector3(0, 0, 0);
 */
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "Menu")
        {
            FindObjectOfType<CameraFollow>().SetTarget();
        }

        recordedHearts = 4;
        numberOfHearts = 4;
        
    }

    private void OnLevelWasLoaded(int level)
    {
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3");
        heart4 = GameObject.Find("Heart4");
        blackImage = GameObject.Find("blackImageLoading");
        playerStaff = GameObject.Find("PlayerStaff");
        areaText = GameObject.Find("AreaText");
        player = GameObject.Find("Player");
        testGuide = GameObject.Find("TestGuide");
        areaText.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        currentAmmoText = GameObject.Find("CurrentAmmoText").GetComponent<Text>();
        Cursor.visible = true;
        

        /* optionsTab = GameObject.Find("OptionsTab");
         optionsTab.transform.localScale = new Vector3(0, 0, 0);
         testGuide.transform.localScale = new Vector3(0, 0, 0);
 */
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "Menu")
        {
            FindObjectOfType<CameraFollow>().SetTarget();
        }

    }

    // Update is called once per frame
    void Update()
    {
       

        if(player.GetComponent<PlayerShieldAbility>().shieldAvailable)
        {
            shieldStatus.GetComponent<Image>().color = Color.white;
        }
        else
        {
            shieldStatus.GetComponent<Image>().color = Color.grey;
        }

        currentScene = SceneManager.GetActiveScene();

        int threeQuarters = Mathf.RoundToInt(player.GetComponent<PlayerProperties>().maxHealth / 4) * 3;
        int half = Mathf.RoundToInt(player.GetComponent<PlayerProperties>().maxHealth / 2);
        int Quarter = Mathf.RoundToInt(player.GetComponent<PlayerProperties>().maxHealth / 4);

        if(numberOfHearts < recordedHearts)
        {
            HeartLoss();
            recordedHearts = numberOfHearts;
        }

        if (numberOfHearts > recordedHearts)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
            recordedHearts = numberOfHearts;
        }

        if (player.GetComponent<PlayerProperties>().health > (threeQuarters))
        {
            heart1.GetComponent<Image>().color = Color.white;
            heart2.GetComponent<Image>().color = Color.white;
            heart3.GetComponent<Image>().color = Color.white;
            heart4.GetComponent<Image>().color = Color.white;
        }

        if (player.GetComponent<PlayerProperties>().health <= threeQuarters && player.GetComponent<PlayerProperties>().health > half)
        {
            heart1.GetComponent<Image>().color = Color.white;
            heart2.GetComponent<Image>().color = Color.white;
            heart3.GetComponent<Image>().color = Color.white;
            heart4.GetComponent<Image>().color = Color.grey;
            numberOfHearts = 3;
        }

        if (player.GetComponent<PlayerProperties>().health <= half && player.GetComponent<PlayerProperties>().health > Quarter)
        {
            heart1.GetComponent<Image>().color = Color.white;
            heart2.GetComponent<Image>().color = Color.white;
            heart3.GetComponent<Image>().color = Color.grey;
            heart4.GetComponent<Image>().color = Color.grey;
            numberOfHearts = 2;
        }

        if (player.GetComponent<PlayerProperties>().health <= Quarter && player.GetComponent<PlayerProperties>().health > 0)
        {
            heart1.GetComponent<Image>().color = Color.white;
            heart2.GetComponent<Image>().color = Color.grey;
            heart3.GetComponent<Image>().color = Color.grey;
            heart4.GetComponent<Image>().color = Color.grey;
            numberOfHearts = 1;
        }

        if (FindObjectOfType<PlayerProperties>().health <= 0)
        {
            heart1.GetComponent<Image>().color = Color.grey;
            heart2.GetComponent<Image>().color = Color.grey;
            heart3.GetComponent<Image>().color = Color.grey;
            heart4.GetComponent<Image>().color = Color.grey;
            numberOfHearts = 0;
            playerDiedFirstTime = true;
        }

        if (!saveOnce && playerDiedFirstTime)
        {
            StartCoroutine(SaveMessage());
            saveOnce = true;
        }

        currentAmmoText.text = player.GetComponent<PlayerAttack>().rayAmmo.ToString();


    }

    IEnumerator SaveMessage()
    {
        yield return new WaitForSeconds(6);
        GameObject.Find("SaveMessage").GetComponent<Animator>().Play("SaveMessageAnim");
    }

    public void HeartLoss()
    {
        GameObject.Find("AbilityManager").GetComponent<AbilityManager>().ChargeFocus(7);
        if(numberOfHearts != 0)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("HeartLost");
        }
        

    }

    public void playerNameSelected()
    {
        if(inputName.text != null)
        {
            playerName = inputName.text;
        }
        if(playerName == null)
        {
            playerName = "Arthur";
        }
        nameInputObj.SetActive(false);
        Cursor.visible = false;
    }

    public void StaffEquip()
    {
        staffOBJ.SetActive(true);
    }

    public void SetTarget()
    {
        FindObjectOfType<CameraFollow>().target = player.transform;
    }

    public void Save()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
        player.GetComponent<PlayerProperties>().respawnPoint = player.transform.position;
        player.GetComponent<PlayerProperties>().currentPos = player.transform.position;
        SaveManager.SaveGame(player.GetComponent<PlayerProperties>());
        Data savedData = SaveManager.LoadData();
        savedData.corruption = PlayerProperties.corruption;
        savedData.coins = PlayerProperties.gold;
        savedData.honour = PlayerProperties.honour;
        savedData.powerPoints = player.GetComponent<PlayerProperties>().power;
        savedData.health = player.GetComponent<PlayerProperties>().health;
        savedData.xC = player.transform.position.x;
        savedData.yC = player.transform.position.y;
    }

    public void Load()
    {
        Data data = SaveManager.LoadData();
        player.transform.position = new Vector2(data.xC, data.yC);
        PlayerProperties.gold = data.coins;
        PlayerProperties.honour = data.honour;
        PlayerProperties.corruption = data.corruption;
        player.GetComponent<PlayerProperties>().power = data.powerPoints;
        player.GetComponent<PlayerProperties>().health = data.health;
    }

    public void LoadLevel()
    {
        Data levelData = SaveManager.LoadData();
        SceneManager.LoadScene(levelData.lastSceneName);
    }


}
