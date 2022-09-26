using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    private GameObject BOMDefault;
    private GameObject BOMRunes;

    //Does the Player Have Abilities?
    public bool hasTime;
    public bool hasFire;
    public bool hasShield;
    public bool hasHeal;
    public bool hasFrost;
    public bool hasConfusion;
    public bool hasCreation;
    public bool hasPortal;
    public bool hasLife;
    public bool hasWind;

    //WhatAbilityIsEquiped?

    public int abilityNumber;

    private GameObject timeRune;
    private GameObject fireRune;
    private GameObject shieldRune;
    private GameObject healRune;
    private GameObject freezeRune;
    private GameObject confusionRune;
    private GameObject creationRune;
    private GameObject portalRune;
    private GameObject lifeRune;
    private GameObject windRune;

    private GameObject abilityEquipedRune1;
    private GameObject abilityEquipedRune2;
    private GameObject abilityEquipedRune3;

    public Sprite lockedSprite;
    public Sprite timeRuneSprite;
    public Sprite fireRuneSprite;
    public Sprite shieldRuneSprite;
    public Sprite healRuneSprite;
    public Sprite freezeRuneSprite;
    public Sprite confusionRuneSprite;
    public Sprite creationRuneSprite;
    public Sprite portalRuneSprite;
    public Sprite lifeRuneSprite;
    public Sprite windRuneSprite;


    public Color equipedColor;

    private GameObject abilityCooldownText;
    public float cooldownTime;
    private bool startCounting;

    private GameObject bookCanvas;

    private string ability1Name;
    private string ability2Name;
    private string ability3Name;

    [SerializeField]
    private bool showBook;

    public float focusCharge;

    private GameObject focusReadyButton;
    private GameObject focusMenu;
    private Image focusBar;

    public Transform checkpointVFX;
    private GameObject currentCheckpointVFX;
    private bool once;

    private Text nextAbText;

    // Start is called before the first frame update
    void Start()
    {
        abilityCooldownText = GameObject.Find("AbilityCooldownText");
        BOMDefault = GameObject.Find("BOMDefault");
        BOMRunes = GameObject.Find("BOMRunes");
        timeRune = GameObject.Find("TimeRune");
        fireRune = GameObject.Find("FireRune");
        shieldRune = GameObject.Find("ShieldRune");
        healRune = GameObject.Find("HealRune");
        freezeRune = GameObject.Find("IceRune");
        creationRune = GameObject.Find("CreationRune");
        portalRune = GameObject.Find("PortalRune");
        lifeRune = GameObject.Find("LifeRune");
        windRune = GameObject.Find("WindRune");
        bookCanvas = GameObject.Find("BookCanvas");
        focusMenu = GameObject.Find("FocusMenu");
        focusBar = GameObject.Find("Circle").GetComponent<Image>();
        nextAbText = GameObject.Find("NextAbilityText").GetComponent<Text>();
        nextAbText.text = "NEXT ABILITY AT: 20";
        focusReadyButton = GameObject.Find("FocusReadyButton");

        abilityEquipedRune1 = GameObject.Find("AbilityEquipedRune1");
        
        BOMDefault.SetActive(false);
        BOMRunes.SetActive(false);
        bookCanvas.SetActive(false);
        focusMenu.SetActive(false);

        abilityEquipedRune1.SetActive(false);

        hasTime = true;
        hasFire = true;
        hasShield = true;
        focusCharge = 0;
        if(showBook)
        {
            EquipBook();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 20)
        {
            hasHeal = true;
            nextAbText.text = "NEXT ABILITY AT: 100";
        }

        if (GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 100)
        {
            hasFrost = true;
            nextAbText.text = "NEXT ABILITY AT: 300";
        }

        if (GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 300)
        {
            hasCreation = true;
            nextAbText.text = "NEXT ABILITY AT: 600";
        }

        if (GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 600)
        {
            hasPortal = true;
            nextAbText.text = "NEXT ABILITY AT: 1000";
        }

        if (GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 1000)
        {
            hasLife = true;
            nextAbText.text = "NEXT ABILITY AT: 2000";
        }

        if (GameObject.Find("Player").GetComponent<PlayerProperties>().power >= 2000)
        {
            hasWind = true;
            bool once = false;
            if(once == false)
            {
                Destroy(nextAbText.gameObject);
                once = true;
            }
        }



        if (focusCharge > 100)
        {
            focusCharge = 100;
        }

        if(!hasTime)
        {
            timeRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            timeRune.GetComponent<Image>().sprite = timeRuneSprite;
        }

        if(!hasFire)
        {
            fireRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            fireRune.GetComponent<Image>().sprite = fireRuneSprite;
        }

        if (!hasShield)
        {
            shieldRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            shieldRune.GetComponent<Image>().sprite = shieldRuneSprite;
        }

        if (!hasHeal)
        {
            healRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            healRune.GetComponent<Image>().sprite = healRuneSprite;
        }

        if (!hasFrost)
        {
            freezeRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            freezeRune.GetComponent<Image>().sprite = freezeRuneSprite;
        }

        if (!hasCreation)
        {
            creationRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            creationRune.GetComponent<Image>().sprite = creationRuneSprite;
        }

        if (!hasPortal)
        {
            portalRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            portalRune.GetComponent<Image>().sprite = portalRuneSprite;
        }

        if (!hasLife)
        {
            lifeRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            lifeRune.GetComponent<Image>().sprite = lifeRuneSprite;
        }

        if (!hasWind)
        {
            windRune.GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            windRune.GetComponent<Image>().sprite = windRuneSprite;
        }

        if (hasTime && abilityNumber != 1)
        {
            timeRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasFire && abilityNumber != 2)
        {
            fireRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasShield && abilityNumber != 3)
        {
            shieldRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasHeal && abilityNumber != 4)
        {
            healRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasFrost && abilityNumber != 5)
        {
            freezeRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasCreation && abilityNumber != 7)
        {
            creationRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasPortal && abilityNumber != 8)
        {
            portalRune.GetComponent<Image>().color = Color.grey;
        }

        if(hasLife && abilityNumber != 9)
        {
            lifeRune.GetComponent<Image>().color = Color.grey;
        }

        if (hasWind && abilityNumber != 10)
        {
            windRune.GetComponent<Image>().color = Color.grey;
        }

        if (abilityNumber > 0)
        {
            abilityEquipedRune1.SetActive(true);
        }
        if(ability1Name == "Time")
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = timeRuneSprite;
        }

        if (abilityNumber == 2)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = fireRuneSprite;
        }

        if (abilityNumber == 3)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = shieldRuneSprite;
        }

        if (abilityNumber == 4)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = healRuneSprite;
        }

        if (abilityNumber == 5)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = freezeRuneSprite;
        }

        if (abilityNumber == 7)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = creationRuneSprite;
        }

        if (abilityNumber == 8)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = portalRuneSprite;
        }

        if (abilityNumber == 9)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = lifeRuneSprite;
        }

        if (abilityNumber == 10)
        {
            abilityEquipedRune1.GetComponent<Image>().sprite = windRuneSprite;
        }

        if (startCounting && cooldownTime > 0)
        {
            abilityEquipedRune1.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            abilityEquipedRune1.GetComponent<Image>().color = Color.white;
        }

        if(cooldownTime < 0.2f)
        {
            startCounting = false;
            cooldownTime = 0;
           
        }

        if(cooldownTime != 0)
        {
            abilityCooldownText.SetActive(false);
            abilityCooldownText.GetComponent<Text>().text = cooldownTime.ToString("F0");
        }

        if(cooldownTime <= 0)
        {
            abilityCooldownText.GetComponent<Text>().text = "Q";
        }

        if(focusCharge >= 100)
        {
            focusReadyButton.gameObject.SetActive(true);
            if(once == false)
            {
                PlayFocusTune();
                once = true;
            }
        }
        else
        {
            focusReadyButton.gameObject.SetActive(false);
            ChargeFocus(0.1f * Time.deltaTime);
            once = false;
        }
    }

    public void PlayFocusTune()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("AbilityTheme");
    }


    public void ShowBOM()
    {
        cooldownTime = 0;
        BOMDefault.SetActive(true);
        BOMRunes.SetActive(true);
       
        PlayerAttack.disableAttack = true;
        Cursor.visible = true;
    }

    public void ChargeFocus(float charge)
    {
        focusCharge += charge;
        focusBar.fillAmount = focusCharge/100.0f;
        

    }

    public void ShowFocusMenu()
    {
        BOMDefault.SetActive(true);
        focusMenu.SetActive(true);
        PlayerAttack.disableAttack = true;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void SaveOnMenu()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Save();
        GameObject.Find("LevelVFX").GetComponent<ParticleSystem>().Play();
        if(currentCheckpointVFX != null)
        {
            Destroy(currentCheckpointVFX);
        }
        Instantiate(checkpointVFX, GameObject.Find("Player").transform.position, this.transform.rotation);
        currentCheckpointVFX = GameObject.Find("CheckpointVFX");
        CloseBOM();
    }

    public void AbilityOnMenu()
    {
        BOMRunes.SetActive(true);
        focusMenu.SetActive(false);
    }

    public void CloseBOM()
    {
        cooldownTime = 0;
        focusCharge = 0;
        Time.timeScale = 1;
        BOMDefault.SetActive(false);
        BOMRunes.SetActive(false);
        focusMenu.SetActive(false);
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
    }

    public void EquipTime()
    {
        if (hasTime)
        {
            abilityNumber = 1;
            timeRune.GetComponent<Image>().color = Color.white;
            cooldownTime = 0;
            CloseBOM();

        }
        
    }

    public void EquipFire()
    {
        if (hasFire)
        {
            abilityNumber = 2;
            fireRune.GetComponent<Image>().color = Color.white;
            CloseBOM();


        }
        
    }

    public void EquipShield()
    {
        
        if(hasShield)
        {
            abilityNumber = 3;
            shieldRune.GetComponent<Image>().color = Color.white;
            CloseBOM();

        }
    }

    public void EquipHeal()
    {
        
        if (hasHeal)
        {
            abilityNumber = 4;
            healRune.GetComponent<Image>().color = Color.white;
            CloseBOM();

        }
    }

    public void EquipIce()
    {
        if (hasFrost)
        {
            abilityNumber = 5;
            freezeRune.GetComponent<Image>().color = Color.white;
            CloseBOM();


        }
    }

    public void EquipCreation()
    {

        if (hasCreation)
        {
            abilityNumber = 7;
            creationRune.GetComponent<Image>().color = Color.white;
            CloseBOM();


        }
    }

    public void EquipPortal()
    {
        if (hasPortal)
        {
            abilityNumber = 8;
            portalRune.GetComponent<Image>().color = Color.white;

            CloseBOM();

        }
    }

    public void EquipLife()
    {
        if(hasLife)
        {
            abilityNumber = 9;
            lifeRune.GetComponent<Image>().color = Color.white;

            CloseBOM();


        }
    }

    public void EquipWind()
    {
        if (hasWind)
        {
            abilityNumber = 10;
            windRune.GetComponent<Image>().color = Color.white;
            CloseBOM();


        }
    }


    public void AbilityUsed()
    {
        startCounting = true;
        cooldownTime = 1;
    }

    public void EquipBook()
    {
        bookCanvas.SetActive(true);
    }

    
}
