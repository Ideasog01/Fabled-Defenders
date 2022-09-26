using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    private bool groundSlamActivated;
    public LayerMask enemyLayers;
    private Vector2 playerPos;
    private GameObject groundSlam;
    private GameObject playerStaff;

    private GameObject playerAnim;
    private GameObject pointLightStaffSlam;

    private float cooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        groundSlam = GameObject.Find("SlamVFX");
        playerStaff = GameObject.Find("PlayerStaff");
        playerAnim = GameObject.Find("PlayerSprite");
        pointLightStaffSlam = GameObject.Find("PointLightStaffSlam");
        pointLightStaffSlam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime * 1;
        }

        if (PlayerMovement_R.isGrounded == false && Input.GetMouseButtonDown(0) && FindObjectOfType<GameManager>().staffEquiped && cooldownTime <= 0)
        {
            groundSlamActivated = true;
            FindObjectOfType<PlayerAnimations>().SlamAnim();
            pointLightStaffSlam.SetActive(true);
            PlayerAttack.disableAttack = true;
            playerStaff.SetActive(false);
            Debug.Log("Ground Slam Activated");
            cooldownTime = 1.5f;
        }

        if(PlayerMovement_R.isGrounded && Input.GetKeyDown(KeyCode.Space) && PlayerAttack.disableAttack)
        {
            playerStaff.SetActive(true);
        }

        if(groundSlamActivated == true && PlayerMovement_R.isGrounded)
        {
            PerformGroundSlam();
            StartCoroutine(HidePlayerStaff());
            groundSlamActivated = false;
        }

        playerPos = this.transform.position;
    }

    public void PerformGroundSlam()
    {
        groundSlam.GetComponent<ParticleSystem>().Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("GroundSlam");
        Collider2D[] hitGroundEnemy = Physics2D.OverlapCircleAll(playerPos, 4, enemyLayers);
        foreach (Collider2D groundEnemy in hitGroundEnemy)
        {
            Debug.Log(groundEnemy);
            groundEnemy.gameObject.GetComponent<EnemyProperties>().GroundSlamDamage();
        }

    }

    IEnumerator HidePlayerStaff()
    {
        yield return new WaitForSeconds(0.9f);
        playerStaff.SetActive(true);
        pointLightStaffSlam.SetActive(false);
        PlayerAttack.disableAttack = false;
    }
}
