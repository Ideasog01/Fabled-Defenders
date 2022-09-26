using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackAb : MonoBehaviour
{
    public Transform fireBallPrefab;

    private int numberOfFB;

    private bool isUsingAb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(FindObjectOfType<AbilityManager>().abilityNumber == 2 && Input.GetKeyDown(KeyCode.Q) && numberOfFB < 15 && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
        {
            InvokeRepeating("CreateFireBallPrefab", 0, 0.1f);
            isUsingAb = true;
        }

        if(FindObjectOfType<AbilityManager>().cooldownTime <= 0 && FindObjectOfType<AbilityManager>().abilityNumber == 2)
        {
            if (Input.GetKeyUp(KeyCode.Q) || numberOfFB > 14)
            {
                EndAbility();
            }
        }
        

    }

    public void CreateFireBallPrefab()
    {
        Instantiate(fireBallPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        numberOfFB += 1;

    }

    public void EndAbility()
    {
        CancelInvoke("CreateFireBallPrefab");
        FindObjectOfType<AbilityManager>().AbilityUsed();
        numberOfFB = 0;
        isUsingAb = false;
    }
    
}
