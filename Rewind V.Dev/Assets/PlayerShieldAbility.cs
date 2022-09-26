using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldAbility : MonoBehaviour
{
    public bool shieldActive;
    public bool shieldAvailable;
    private GameObject shield;
    private float shieldCooldown;

    // Start is called before the first frame update
    void Start()
    {
        shield = GameObject.Find("PlayerShieldAb");
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

       

        if (Input.GetMouseButtonUp(1) && shieldActive)
        {
            shieldCooldown = 3;
            GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned = false;

        }

        if (shieldAvailable == false && Input.GetMouseButtonUp(1))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned = false;
        }

        if (shieldActive && shieldCooldown < 3)
        {
            shieldCooldown += Time.deltaTime * 1;
        }

        if (shieldCooldown >= 3)
        {
            shieldActive = false;
            shield.SetActive(false);
        }

        if (shieldCooldown >= 0 && shieldActive == false)
        {
            shieldCooldown -= Time.deltaTime * 1;
        }

        if (shieldCooldown <= 0)
        {
            shieldAvailable = true;
        }
        
        if(!shieldActive && shieldCooldown > 0)
        {
            shieldAvailable = false;
        }
    }

    public void ActivateShieldAb()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned = true;
        GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().Cast();
        shieldActive = true;
        shield.SetActive(true);
    }
}
