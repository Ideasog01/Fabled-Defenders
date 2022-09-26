using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeProjectileBehav : MonoBehaviour
{
    private bool goUp;
    private bool continueOnPath;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
        if(FindObjectOfType<PlayerAnimations>().faceLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if(FindObjectOfType<IceAttackBehav>().IceSpikeGoUp && !FindObjectOfType<PlayerAnimations>().faceLeft)
        {
            goUp = true;
        }
        if(FindObjectOfType<IceAttackBehav>().IceSpikeGoUp == false && !FindObjectOfType<PlayerAnimations>().faceLeft)
        {
            goUp = false;
        }


        if (FindObjectOfType<IceAttackBehav>().IceSpikeGoUp == false && FindObjectOfType<PlayerAnimations>().faceLeft)
        {
            goUp = true;
        }



        if (FindObjectOfType<IceAttackBehav>().continueForward)
        {
            continueOnPath = true;
        }




    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector2.up * 12 * Time.deltaTime);


        if (goUp && continueOnPath == false)
        {
            transform.Rotate(Vector3.back * 10 * Time.deltaTime);
        }
        if(!goUp && continueOnPath == false)
        {
            transform.Rotate(Vector3.forward * 10 * Time.deltaTime);
        }
            
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyProperties>().HitByIce(); 
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
