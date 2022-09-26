using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastBlast : MonoBehaviour
{
    private int speed;

    private Vector3 mousePos;

    private float distanceToMouse;

    private float ammo;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(mousePos.x, mousePos.y), 12 * Time.deltaTime);

        transform.right = new Vector2(mousePos.x, mousePos.y) - new Vector2(transform.position.x, transform.position.y);

        distanceToMouse = Vector2.Distance(this.transform.position, new Vector2(mousePos.x, mousePos.y));
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyProperties>().DamageThisRayGun();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("EnemyDamaged");
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
