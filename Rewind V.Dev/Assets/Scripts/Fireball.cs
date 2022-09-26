using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private bool isCurrentlyFacingLeft;

    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerAnimations.isFacingLeft)
        {
            isCurrentlyFacingLeft = true;
        }
        else
        {
            isCurrentlyFacingLeft = false;
        }

        StartCoroutine(DestroyAfterTime());

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isCurrentlyFacingLeft == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(mousePos.x, mousePos.y), 12 * Time.deltaTime);
        }

        if (isCurrentlyFacingLeft == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(mousePos.x, mousePos.y), 12 * Time.deltaTime);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyProperties>().FireBallHit();
        }

        if(collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }

        
    }
}
