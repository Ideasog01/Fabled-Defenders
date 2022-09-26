using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private bool isWalking;
    
    private Animator playerAnim;

    private float moveInput;

    private float direction;
    private float currentDirection;
    public bool faceLeft;
    private bool getDirection;

    public Animator slashVFX;

    private GameObject TimeVFX1;
    private GameObject TimeVFX2;
    private GameObject fireVFX1;
    private GameObject fireVFX2;
    private GameObject shieldVFX1;
    private GameObject shieldVFX2;
    private GameObject healVFX1;
    private GameObject healVFX2;
    private GameObject iceVFX1;
    private GameObject iceVFX2;
    private GameObject creationVFX1;
    private GameObject creationVFX2;
    private GameObject portalVFX1;
    private GameObject portalVFX2;
    private GameObject windVFX;

    public static bool isFacingLeft;
    private bool inAir;

    private bool slammed;
    private void Start()
    {
        playerAnim = this.GetComponent<Animator>();
        TimeVFX1 = GameObject.Find("TimeVFX");
        TimeVFX2 = GameObject.Find("TimeVFX2");
        fireVFX1 = GameObject.Find("FireVFX");
        fireVFX2 = GameObject.Find("FireVFX2");
        shieldVFX1 = GameObject.Find("ShieldVFX");
        shieldVFX2 = GameObject.Find("ShieldVFX2");
        healVFX1 = GameObject.Find("HealVFX");
        healVFX2 = GameObject.Find("HealVFX2");
        iceVFX1 = GameObject.Find("IceVFX");
        iceVFX2 = GameObject.Find("IceVFX2");
        creationVFX1 = GameObject.Find("CreationVFX");
        creationVFX2 = GameObject.Find("CreationVFX2");
        portalVFX1 = GameObject.Find("PortalVFX");
        portalVFX2 = GameObject.Find("PortalVFX2");
        windVFX = GameObject.Find("WindVFX");

    }

    private void Update()
    {

        if (!FindObjectOfType<PlayerMovement_R>().stunned)
        {
            currentDirection = transform.position.x;
            moveInput = Input.GetAxis("Horizontal");



            if (moveInput != 0)
            {
                isWalking = true;
                getDirection = false;
            }
            else
            {
                isWalking = false;
            }

            if (isWalking)
            {
                playerAnim.SetBool("stopped", false);
                if (moveInput > 0)
                {
                    playerAnim.SetBool("left", false);
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        playerAnim.SetBool("stopped", true);
                    }
                }
                if (moveInput < 0)
                {
                    playerAnim.SetBool("left", true);
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        playerAnim.SetBool("stopped", true);
                    }
                }


            }
            else
            {
                playerAnim.StopPlayback();
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 10 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                windVFX.GetComponent<ParticleSystem>().Play();
                playerAnim.Play("PlayerCast_Anim");
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 3 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                StartCoroutine(ShieldVFX());
            }

            if (moveInput == 0)
            {
                playerAnim.SetBool("stopped", true);
            }

            if (PlayerMovement_R.isGrounded)
            {
                playerAnim.SetBool("OnGround", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                playerAnim.Play("PlayerJumpRight_Anim");
                inAir = true;

            }

            if (inAir && PlayerMovement_R.isGrounded)
            {
                playerAnim.SetBool("OnGround", true);
                inAir = false;
            }

            if (slammed && PlayerMovement_R.isGrounded)
            {
                playerAnim.SetBool("slamActivated", true);
                slammed = false;
            }




            if (Input.GetMouseButtonDown(0) && PlayerAttack.disableAttack == false && FindObjectOfType<GameManager>().staffEquiped)
            {
                playerAnim.Play("Cast_Anim");
            }

            if (currentDirection > direction)
            {
                faceLeft = false;
                if (getDirection == false)
                {
                    GetDirection();
                }
            }

            if (currentDirection < direction)
            {
                faceLeft = true;
                if (getDirection == false)
                {
                    GetDirection();
                }
            }

            if (faceLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isFacingLeft = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isFacingLeft = false;

            }

            if (TimeBody.isRewinding)
            {
                playerAnim.Play("PlayerCast_Anim");
                TimeVFX1.GetComponent<ParticleSystem>().Play();
                TimeVFX2.GetComponent<ParticleSystem>().Play();
            }

            if (!TimeBody.isRewinding)
            {
                TimeVFX1.GetComponent<ParticleSystem>().Stop();
                TimeVFX2.GetComponent<ParticleSystem>().Stop();
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 2 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                StartCoroutine(FireVFX());
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 2 && Input.GetKeyUp(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                StopCoroutine(FireVFX());
                fireVFX1.GetComponent<ParticleSystem>().Stop();
                fireVFX2.GetComponent<ParticleSystem>().Stop();
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 4 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                healVFX1.GetComponent<ParticleSystem>().Play();
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 5 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                iceVFX1.GetComponent<ParticleSystem>().Play();
                iceVFX2.GetComponent<ParticleSystem>().Play();
                playerAnim.Play("PlayerCast_Anim");
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 7 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && FindObjectOfType<PlatformAbility>().platformAbilityActive)
            {
                creationVFX1.GetComponent<ParticleSystem>().Play();
                creationVFX2.GetComponent<ParticleSystem>().Play();
                playerAnim.Play("PlayerCast_Anim");
            }

            if (FindObjectOfType<AbilityManager>().abilityNumber == 8 && Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                portalVFX1.GetComponent<ParticleSystem>().Play();
                portalVFX2.GetComponent<ParticleSystem>().Play();
                playerAnim.Play("PlayerCast_Anim");
            }






            IEnumerator ShieldVFX()
            {
                playerAnim.Play("PlayerCast_Anim");
                shieldVFX1.GetComponent<ParticleSystem>().Play();
                shieldVFX2.GetComponent<ParticleSystem>().Play();
                yield return new WaitForSeconds(1);
                shieldVFX1.GetComponent<ParticleSystem>().Stop();
                shieldVFX2.GetComponent<ParticleSystem>().Stop();
            }

            IEnumerator FireVFX()
            {
                playerAnim.Play("PlayerCast_Anim");
                fireVFX1.GetComponent<ParticleSystem>().Play();
                fireVFX2.GetComponent<ParticleSystem>().Play();
                yield return new WaitForSeconds(5);
                fireVFX1.GetComponent<ParticleSystem>().Stop();
                fireVFX2.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

        

        public void GetDirection()
        {
            direction = transform.position.x;
            getDirection = true;
        }

        public void PlaySlashVFX()
        {
            slashVFX.Play("Slash_Anim");
        }

        public void StopHealVFX()
        {
            healVFX1.GetComponent<ParticleSystem>().Stop();
        }

        public void SlamAnim()
        {
            playerAnim.SetBool("slamActivated", false);
            playerAnim.Play("Player_Slam_Anim");
            slammed = true;
        }
    public void Cast()
    {
        playerAnim.Play("PlayerCast_Anim");
    }
}
