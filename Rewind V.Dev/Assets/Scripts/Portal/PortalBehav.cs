using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehav : MonoBehaviour
{
    

    private bool portal1Instantiated;
    private bool portal1;

    private Transform interactPortalUI;
    private bool isInRad;
    private GameObject player;

    private Transform portalVFX;

    private void Start()
    {
        player = GameObject.Find("Player");
        interactPortalUI = this.gameObject.transform.GetChild(0);
        interactPortalUI.transform.gameObject.SetActive(false);
        portalVFX = this.gameObject.transform.GetChild(2);



        if(FindObjectOfType<PortalAb>().portalsActive == 1)
        {
            portal1 = true;
        }

        if (FindObjectOfType<PortalAb>().portalsActive == 2)
        {
            StartCoroutine(DestroyAfterTime());
        }


    }

    private void Update()
    {
        if(portal1)
        {
            FindObjectOfType<PortalAb>().portal1Coord = this.transform.position;
        }

        if(!portal1)
        {
            FindObjectOfType<PortalAb>().portal2Coord = this.transform.position;
        }

        if(isInRad && Input.GetKeyDown(KeyCode.E) && FindObjectOfType<PortalAb>().portalsActive > 1)
        {

            portalVFX.gameObject.GetComponent<ParticleSystem>().Play();
            if(portal1)
            {
                player.transform.position = FindObjectOfType<PortalAb>().portal2Coord;
            }
            else
            {
                player.transform.position = FindObjectOfType<PortalAb>().portal1Coord;
            }
        }

        if(FindObjectOfType<PortalAb>().portalsActive == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            interactPortalUI.gameObject.SetActive(true);
            isInRad = true;
        }

        if(collision.gameObject.tag == "Enemy" && FindObjectOfType<PortalAb>().portalsActive == 2 && FindObjectOfType<PortalAb>().teleportEnemyOnce == false)
        {
            if(portal1)
            {
                collision.gameObject.transform.position = FindObjectOfType<PortalAb>().portal2Coord;
                FindObjectOfType<PortalAb>().teleportEnemyOnce = true;
                StartCoroutine(WaitForEnemy());
            }
            else
            {
                collision.gameObject.transform.position = FindObjectOfType<PortalAb>().portal1Coord;
                FindObjectOfType<PortalAb>().teleportEnemyOnce = true;
                StartCoroutine(WaitForEnemy());
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactPortalUI.gameObject.SetActive(false);
            isInRad = false;
        }

       


    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(10);
        FindObjectOfType<PortalAb>().ResetAbility();
        FindObjectOfType<AbilityManager>().AbilityUsed();
    }

    IEnumerator WaitForEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<PortalAb>().teleportEnemyOnce = false;
    }
}
