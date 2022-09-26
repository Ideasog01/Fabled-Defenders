using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyCursorScript : MonoBehaviour
{

    public Vector3 mousePos;
    private GameObject hover;
    private GameObject magnifySprite;

    private void Start()
    {
        magnifySprite = GameObject.Find("MagnifySprite");
        Cursor.visible = false;
    }
    private void Update()

    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 thisPos = this.transform.position;

        

        thisPos.x = mousePos.x;
        thisPos.y = mousePos.y;
        thisPos.z = 2;

        this.transform.position = thisPos;
        Collider2D hover_collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
       

        
        try
        {
            hover = hover_collider.gameObject;
            if (hover.gameObject.tag == "Door" || hover.gameObject.tag == "Tablet" || hover.gameObject.tag == "Chest" || hover.gameObject.tag == "RuneBook")
            {
                PlayInteractAnim();
            }
            else
            {
                magnifySprite.GetComponent<Animator>().SetBool("interact", false);
            }
        }
        catch
        {
            return;
        }


       








    }

    public void PlayInteractAnim()
    {
        magnifySprite.GetComponent<Animator>().SetBool("interact", true);
    }

    public void PlayerTeleported()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
