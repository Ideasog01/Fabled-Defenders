using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntBoltBehav : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 6 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("RuntDamageVFX").GetComponent<ParticleSystem>().Play();
            GameObject.Find("RuntDamageVFX2").GetComponent<ParticleSystem>().Play();
            FindObjectOfType<PlayerMovement_R>().stunned = true;
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
