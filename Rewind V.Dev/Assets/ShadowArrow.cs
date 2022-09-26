using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArrow : MonoBehaviour
{
    private Transform corruptedPlayer;

    private bool once;

    private void Start()
    {
        corruptedPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().corruptedPlayer;
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        transform.Translate(Vector2.right * 4 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !once)
        {
            Instantiate(corruptedPlayer, this.transform.position, this.transform.rotation);
            once = true;
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
