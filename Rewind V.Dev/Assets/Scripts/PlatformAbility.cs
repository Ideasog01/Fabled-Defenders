using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAbility : MonoBehaviour
{
    public Transform platformGuidePrefab;
    public Transform platformPrefab;

    public bool platformAbilityActive;
    public int numberOfPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<AbilityManager>().abilityNumber == 7 && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Creation");
            Instantiate(platformGuidePrefab, this.transform.position, this.transform.rotation);
            platformAbilityActive = true;
        }

        if(numberOfPlatforms >= 3)
        {
            platformAbilityActive = false;
            numberOfPlatforms = 0;
            FindObjectOfType<AbilityManager>().AbilityUsed();
        }

        
    }

    IEnumerator StopAbility()
    {
        yield return new WaitForSeconds(8);
        platformAbilityActive = false;
        
    }
}
