using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolorProjectile : MonoBehaviour
{
    private GameObject dolorOwner;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
        dolorOwner = FindObjectOfType<GameManager>().DolorOwner;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 7.5f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerProperties>().DolorProjectileDamage();
            dolorOwner.GetComponent<EnemyProperties>().Heal();
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "FireCharge")
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
