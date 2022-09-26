using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistBehav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());   
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(2, 1, 0) * Time.deltaTime * 0.4f;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyProperties>().EnteredMist();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyProperties>().ExitedMist();
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
