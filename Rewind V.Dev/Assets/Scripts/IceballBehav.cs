using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballBehav : MonoBehaviour
{
    private bool touchedGround;
    private GameObject groundCheckPos;
    private float checkRadius = 5;
    private float yCoord;
    private float xCoord;

    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 6 * Time.deltaTime);
        transform.Translate(Vector2.right * 6 * Time.deltaTime);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !once)
        {
            StartCoroutine(CreateIceSpikes());
            once = true;
        }
    }

    IEnumerator CreateIceSpikes()
    {
        yCoord = this.transform.position.y;
        xCoord = this.transform.position.x;
        Instantiate(FindObjectOfType<GameManager>().IceSpike, this.transform.position, this.transform.rotation);
        this.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().IceSpike, new Vector3(xCoord + -3, yCoord, 0), this.transform.rotation);
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().IceSpike, new Vector3(xCoord + -5, yCoord, 0), this.transform.rotation);
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().IceSpike, new Vector3(xCoord + -7, yCoord, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(FindObjectOfType<GameManager>().IceSpike, new Vector3(xCoord + -9, yCoord, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(FindObjectOfType<GameManager>().IceSpike, new Vector3(xCoord + -11, yCoord, 0), this.transform.rotation);
        yield return new WaitForSeconds(9);
        Destroy(this.gameObject);
    }

}
