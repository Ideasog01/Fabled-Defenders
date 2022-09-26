using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttackBehav : MonoBehaviour
{
    public Transform iceSpikeProjectilePrefab;

    public bool IceSpikeGoUp;
    public bool continueForward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<AbilityManager>().abilityNumber == 5 && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            IceSpikeGoUp = true;
            Instantiate(iceSpikeProjectilePrefab, this.transform.position + new Vector3(0, 3, 0), this.transform.rotation);
            StartCoroutine(IceSpike());
            FindObjectOfType<AbilityManager>().AbilityUsed();
        }
    }

    IEnumerator IceSpike()
    {
        yield return new WaitForSeconds(0.05f);
        continueForward = true;
        yield return new WaitForSeconds(0.05f);
        Instantiate(iceSpikeProjectilePrefab, this.transform.position + new Vector3(0, 2, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(iceSpikeProjectilePrefab, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.05f);
        continueForward = false;
        IceSpikeGoUp = false;
        Instantiate(iceSpikeProjectilePrefab, this.transform.position + new Vector3(0, 0, 0), this.transform.rotation);
        
    
    }
}
