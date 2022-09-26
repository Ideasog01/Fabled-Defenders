using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickupBehav : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerProperties>().HeartPickup();
            Destroy(this.gameObject);
        }
    }
}
