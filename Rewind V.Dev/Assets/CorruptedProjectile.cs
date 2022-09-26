using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedProjectile : MonoBehaviour
{
    private bool travelPlayer;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(ProjectileBehav());
    }

    // Update is called once per frame
    void Update()
    {
       if(!travelPlayer)
        {
            transform.Translate(Vector2.right * 4 * Time.fixedDeltaTime);
        }
       if(travelPlayer)
        {
            transform.position = Vector2.MoveTowards(player.transform.position, this.transform.position, 5 * Time.fixedDeltaTime);
        }
    }

    IEnumerator ProjectileBehav()
    {
        yield return new WaitForSeconds(2);
        travelPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerProperties>().DarkArrowHit();
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "FireCharge")
        {
            Destroy(this.gameObject);
        }
    }
}
