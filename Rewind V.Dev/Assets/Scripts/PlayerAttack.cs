﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform playerProjectile;
    private bool attackFinished;

    public static bool playerAttacked;

    public static float charge;
    private bool isCharging;

    public LayerMask enemies;
    public Transform attackPoint;

    private float moveInput;

    public static bool disableAttack;

    public int playerAmmo;

    private void Start()
    {
        playerAmmo = 4;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.R) && playerAmmo < 4)
        {
            ActivateReload();
        }

        //Spawn a projectile and play a random attack animation.
        if (Input.GetMouseButton(0) && !attackFinished && !disableAttack && PlayerMovement_R.isGrounded && FindObjectOfType<GameManager>().staffEquiped && !FindObjectOfType<PlayerMovement_R>().stunned && playerAmmo > 0)
        {
            if(moveInput > 0 || moveInput == 0) //If player is facing right, fire projectile to the right of the player
            {                Instantiate(playerProjectile, this.transform.position + new Vector3(1, 1, 0), this.transform.rotation);
                int randomAttackNum = Random.Range(1, 3);
                if(randomAttackNum == 1)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack1");
                }
                if (randomAttackNum == 2)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack2");
                }
                if (randomAttackNum == 3)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack3");
                }
                playerAmmo -= 1;
            }
            else //If player is facing left, fire projectile to the left of the player
            {
                Instantiate(playerProjectile, this.transform.position + new Vector3(-1, 1, 0), Quaternion.Euler(0, 0, 180));
                int randomAttackNum = Random.Range(1, 3);
                if (randomAttackNum == 1)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack1");
                }
                if (randomAttackNum == 2)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack2");
                }
                if (randomAttackNum == 3)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PlayerMainAttack3");
                }
                playerAmmo -= 1;
            }

           
           
            attackFinished = true;
            StartCoroutine(CooldownTime());
        }
    }

    IEnumerator CooldownTime() //Attack Cooldown
    {
        yield return new WaitForSeconds(1);
        attackFinished = false;
    }

    public void ActivateReload()
    {
        if(playerAmmo < 4)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() //Resets the player's main attack
    {
        disableAttack = true;
        this.GetComponent<PlayerMovement_R>().stunned = true;
        GameObject.Find("ReloadVFX").GetComponent<ParticleSystem>().Play();
        GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().Cast();
        yield return new WaitForSeconds(1);
        this.GetComponent<PlayerMovement_R>().stunned = false;
        playerAmmo = 4;
        disableAttack = false;
    }
}
