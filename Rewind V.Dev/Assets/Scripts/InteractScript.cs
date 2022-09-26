using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    Camera cam;
    private GameObject player;

    

    private void Start()
    {
        cam = Camera.main;
        player = GameObject.Find("Player");

     
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            clickedGameObject();
        }
    }

    public void clickedGameObject()
    {
        GameObject clicked;
        Collider2D clicked_collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        
        if(clicked_collider == null)
        {
            if (Input.GetMouseButtonDown(1) && GameObject.Find("Player").GetComponent<PlayerShieldAbility>().shieldAvailable && GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned == false)
            {
                GameObject.Find("Player").GetComponent<PlayerShieldAbility>().ActivateShieldAb();
            }
            return;
        }
        else
        {
            clicked = clicked_collider.gameObject;

            if(clicked.gameObject.tag == "Tablet" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                GameObject.Find("RuneManager").GetComponent<RuneManager>().ShowRuneTablet();
                GameObject.Find("RuneManager").GetComponent<RuneManager>().currentRuneTablet = clicked.gameObject;
            }

            if (clicked.gameObject.name == "LetterOnTable" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                PlayerAttack.disableAttack = true;
                FindObjectOfType<GameManager>().Letter.SetActive(true);
                Cursor.visible = true;
            }

            if (clicked.gameObject.tag == "Door" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                clicked.gameObject.GetComponent<DoorTeleport>().TeleportPlayer();
            }

            if (clicked.gameObject.name == "PickupStaff" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                FindObjectOfType<GameManager>().StaffEquip();
                FindObjectOfType<GameManager>().staffEquiped = true;
                Destroy(clicked.gameObject);
            }

            if(clicked.gameObject.tag == "RuneBook")
            {
                clicked.gameObject.GetComponent<BookInteract>().RuneBookFound();
            }

            if (clicked.gameObject.tag == "Key")
            {
                clicked.gameObject.GetComponent<KeyBehav>().PickUpKey();
            }

            if (clicked.gameObject.tag == "Chest" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                clicked.GetComponent<ChestBehav>().GiveItems();
            }

            if (clicked.gameObject.tag == "Book" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                FindObjectOfType<AbilityManager>().EquipBook();
                Destroy(clicked.gameObject);

            }

            if (clicked.gameObject.tag == "Alter" && Vector2.Distance(clicked.transform.position, player.transform.position) < 5)
            {
                clicked.gameObject.GetComponent<AlterManager>().OpenAlter();
            }
        }
        
    }
}
