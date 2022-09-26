using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public int maxDamage;
    public int medDamage;
    public int minDamage;

    private Text areaText;

    public Vector2 respawnPoint;

    public int power;

    IEnumerator LevelText()
    {
        areaText.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(0.5f);
        areaText.text = "Level Up!";
        areaText.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(3);
        areaText.CrossFadeAlpha(0, 2, false);
    }

    private bool playerIsAlive;
    public GameObject playerSprite;
    public Color damageColor;

    public static bool attackPlayer;

    public bool shieldActive;

    public List<string> keyCodes;

    private GameObject healVFX;

    public Vector2 nearestCheckpoint;

    private GameObject blackImage;
    private GameObject FadeCanvas;
    private bool revivePlayer;
    private bool resetPlayer;

    private GameObject lifeVFX;
    private GameObject lifeVFX2;

    private GameObject ArdenJewelsText;
    private GameObject HonourText;
    private GameObject CorruptionText;

    public static int gold;
    public static int corruption;
    public static int honour;

    private bool moveToLoad;

    private bool stunned;

    private Text LevelTexts;
    private Slider LevelSlider;


    public Color activeColor;
    public Color deactiveColor;

    private Text powerText;

    public string playerScene;
    public bool saveExisted;
    public Vector2 currentPos;


    // Start is called before the first frame update
    void Start()
    {
        maxDamage = 30;
        medDamage = 20;
        minDamage = 10;
        maxHealth = 100;
        health = maxHealth;
        playerIsAlive = true;

        healVFX = GameObject.Find("HealVFXAB");
        blackImage = FindObjectOfType<GameManager>().blackImage;
        FadeCanvas = GameObject.Find("FadeLoadingCanvas");
        lifeVFX = GameObject.Find("LifeVFXAB");
        lifeVFX2 = GameObject.Find("LifeVFXAB2");
        powerText = GameObject.Find("PowerText").GetComponent<Text>();
        power = 0;
        powerText.text = "POWER: " + power;
        ArdenJewelsText = GameObject.Find("CoinText");
        HonourText = GameObject.Find("HonourText");
        CorruptionText = GameObject.Find("CorruptionText");
        if(saveExisted)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel(playerScene);
            this.transform.position = nearestCheckpoint;
        }
        saveExisted = true;

        blackImage.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        power = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (health <= 0)
        {
            attackPlayer = false;

            if(FindObjectOfType<AbilityManager>().abilityNumber != 9 || FindObjectOfType<AbilityManager>().cooldownTime > 0)
            {
                playerIsAlive = false;
                
                Debug.Log("playerDied");
            }
            if(revivePlayer == false && FindObjectOfType<AbilityManager>().abilityNumber == 9 && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                StartCoroutine(RevivePlayer());
                revivePlayer = true;
            }

            if(moveToLoad == true)
            {
                this.transform.Translate(Vector2.right * 0.8f * Time.deltaTime);
            }
            
        }

        if (playerIsAlive == false && resetPlayer == false)
        {
            StartCoroutine(PlayerDied());
            resetPlayer = true;
        }

        if(health > 0)
        {
            revivePlayer = false;
            playerIsAlive = true;
            attackPlayer = true;
        }

        if (FindObjectOfType<AbilityManager>().abilityNumber == 4 && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(HealingAbility());
            FindObjectOfType<AbilityManager>().AbilityUsed();
        }

        ArdenJewelsText.GetComponent<Text>().text = PlayerProperties.gold.ToString();
        CorruptionText.GetComponent<Text>().text = PlayerProperties.corruption.ToString();
        HonourText.GetComponent<Text>().text = PlayerProperties.honour.ToString();
    }

    IEnumerator StartKnightDamage()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<CameraShake>().Shake();
        playerSprite.GetComponent<PlayerAnimations>().PlaySlashVFX();
        yield return new WaitForSeconds(0.5f);
        health -= 10;


        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.25f);
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator ShadowDamage()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 5;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 5;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void DarkArrowHit()
    {
        StartCoroutine(ShadowDamage());
    }

    public void TakeKnightDamage()
    {
        if(!shieldActive)
        {
            StartCoroutine(StartKnightDamage());
        }
        else
        {
            FindObjectOfType<ShieldAb>().shieldHealth -= 1;
            FindObjectOfType<ShieldAb>().playShieldVFX();
        }

    }

    public void TakeRuntDamage()
    {
        if (!shieldActive)
        {
            StartCoroutine(StartRuntDamage());
        }
        else
        {
            FindObjectOfType<ShieldAb>().shieldHealth -= 1;
            FindObjectOfType<ShieldAb>().playShieldVFX();
        }
    }

    IEnumerator StartRuntDamage()
    {
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<CameraShake>().Shake();
        playerSprite.GetComponent<PlayerAnimations>().PlaySlashVFX();
        yield return new WaitForSeconds(0.2f);
        health -= 5;

        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.25f);
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator StartIceSpikeDamage()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 20;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 20;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeIceSpikeDamage()
    {
        if(!shieldActive)
        {
            StartCoroutine(StartIceSpikeDamage());
        }
        else
        {
            FindObjectOfType<ShieldAb>().shieldHealth -= 1;
            FindObjectOfType<ShieldAb>().playShieldVFX();
        }
        
    }

    public void GiantDamage()
    {
        StartCoroutine(GiantDamageBehav());
    }

    IEnumerator GiantDamageBehav()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 50;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator HealingAbility()
    {
        yield return new WaitForSeconds(1);
        healSelf();
        healVFX.GetComponent<ParticleSystem>().Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
        yield return new WaitForSeconds(5);
        healSelf();
        healVFX.GetComponent<ParticleSystem>().Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
        yield return new WaitForSeconds(5);
        healSelf();
        healVFX.GetComponent<ParticleSystem>().Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
        yield return new WaitForSeconds(4);
        FindObjectOfType<PlayerAnimations>().StopHealVFX();
    }

    public void healSelf()
    {
        health += 25;
    }

    IEnumerator PlayerDied()
    {
        FadeCanvas.SetActive(true);
        blackImage.gameObject.SetActive(true);
        blackImage.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        blackImage.GetComponent<Image>().CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1);
        GameObject.Find("GameManager").GetComponent<GameManager>().Load();
        power -= 50;
        moveToLoad = true;
        yield return new WaitForSeconds(5);
        moveToLoad = false;
        blackImage.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);
        blackImage.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        FadeCanvas.SetActive(false);
        playerIsAlive = true;
        health = maxHealth;
        resetPlayer = false;
        yield return new WaitForSeconds(1);
    }

    IEnumerator RevivePlayer()
    {
        FadeCanvas.SetActive(true);
        blackImage.gameObject.SetActive(true);
        blackImage.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        blackImage.GetComponent<Image>().CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2);
        blackImage.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(1);
        blackImage.GetComponent<Image>().CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2);
        blackImage.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        blackImage.GetComponent<Image>().color = Color.black;
        FadeCanvas.SetActive(false);
        health = maxHealth;
        lifeVFX.GetComponent<ParticleSystem>().Play();
        lifeVFX2.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        FindObjectOfType<AbilityManager>().AbilityUsed();
        resetPlayer = false;
    }

    public void HeartPickup()
    {
        health += 25;
        healVFX.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<PlayerAnimations>().StopHealVFX();
    }

    public IEnumerator RuntAbility()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
    }

    IEnumerator CorruptionDamage()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = damageColor;
        health -= 20;
        FindObjectOfType<CameraShake>().Shake();
        yield return new WaitForSeconds(0.25f);
        playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void DolorProjectileDamage()
    {
        if (!shieldActive)
        {
            StartCoroutine(CorruptionDamage());
        }
        else
        {
            FindObjectOfType<ShieldAb>().shieldHealth -= 1;
            FindObjectOfType<ShieldAb>().playShieldVFX();
        }
        
    }

    public void IncreasePower(int powerIncrease)
    {
        power += powerIncrease;
        powerText.text = "Level: " + power;
    }
}
