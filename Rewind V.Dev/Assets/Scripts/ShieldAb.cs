using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldAb : MonoBehaviour
{
    //Represents the different shield textures to display damage
    public Sprite shield1;
    public Sprite shield2;
    public Sprite shield3;

    public int shieldHealth;

    private GameObject playerSprite;
    private GameObject shieldHealthBar;

    private GameObject shieldDestroyVFX1;
    private GameObject shieldDamageVFX;

    private float timer;

    private bool timerActive;

    private bool once;

    void Start()
    {
        shieldHealthBar = GameObject.Find("ShieldHealthBar");
        shieldHealthBar.SetActive(false);
        shieldDestroyVFX1 = GameObject.Find("ShieldDestroyVFX");
        shieldDamageVFX = GameObject.Find("ShieldDamageVFX");
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {

        if(FindObjectOfType<AbilityManager>().abilityNumber == 3 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
        {
            StartCoroutine(ShieldStart());
        }

        if(shieldHealth == 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = shield1;
            shieldHealthBar.GetComponent<Slider>().value = 1;
            
        }
        if(shieldHealth == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = shield2;
            shieldHealthBar.GetComponent<Slider>().value = 0.5f;
        }
        if (shieldHealth == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = shield3;
            shieldHealthBar.GetComponent<Slider>().value = 0;
        }

        if(shieldHealth <= 0 && !once && FindObjectOfType<PlayerProperties>().shieldActive == true)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(shieldDestroyVFX());
            FindObjectOfType<PlayerProperties>().shieldActive = false;
            once = true;
        }

        if(shieldHealth > 0)
        {
            FindObjectOfType<PlayerProperties>().shieldActive = true;
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

        if(shieldHealth > 3)
        {
            shieldHealth = 3;
        }

        if(timerActive == true)
        {
            timer -= 1 * Time.deltaTime;
        }

        if(timer < 10 && timer > 5 && shieldHealth == 3 && timerActive)
        {
            shieldHealth = 2;
        }

        if (timer < 5 && timer > 0 && shieldHealth == 2 && timerActive)
        {
            shieldHealth = 1;
        }

        if(timer <= 0 && timerActive)
        {
            shieldHealth = 0;
            timerActive = false;
        }


    }

    IEnumerator shieldDestroyVFX()
    {
        shieldDamageVFX.GetComponent<ParticleSystem>().Play();
        shieldDestroyVFX1.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        shieldHealthBar.SetActive(false);
    }

    IEnumerator ShieldStart()
    {
        yield return new WaitForSeconds(0.5f);
        shieldHealth = 3;
        shieldHealthBar.SetActive(true);
        timer = 20;
        timerActive = true;
        once = false;
        FindObjectOfType<AbilityManager>().AbilityUsed();

    }

    public void playShieldVFX()
    {
        shieldDamageVFX.GetComponent<ParticleSystem>().Play();
    }
}
