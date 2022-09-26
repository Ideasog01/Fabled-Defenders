using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionPickup : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerProperties.corruption += 1;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Creation");
            GameObject.Find("Player").GetComponent<PlayerProperties>().IncreasePower(2);
            Destroy(this.gameObject);
        }
    }
}
