using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeBehav : MonoBehaviour
{

    private bool once;
    private void Start()
    {
        StartCoroutine(DestoryAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !once)
        {
            FindObjectOfType<PlayerProperties>().TakeIceSpikeDamage();
            once = true;
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestoryAfterTime()
    {
        yield return new WaitForSeconds(3);
        this.GetComponent<Animator>().SetBool("Destroy", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
