using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
