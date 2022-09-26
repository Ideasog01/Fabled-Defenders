using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGroundDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            this.transform.parent.GetComponent<GiantBehav>().isGrounded1();
        }
        
    }
}
