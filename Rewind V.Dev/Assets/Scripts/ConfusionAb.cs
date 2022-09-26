using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionAb : MonoBehaviour
{
    public Transform mistPrefab;
    private GameObject playerStaff;

    // Start is called before the first frame update
    void Start()
    {
        playerStaff = GameObject.Find("PlayerStaff");
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<AbilityManager>().abilityNumber == 6 && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ActivateMist());
            FindObjectOfType<AbilityManager>().AbilityUsed();
        }
    }

    IEnumerator ActivateMist()
    {
        Instantiate(mistPrefab, playerStaff.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(mistPrefab, playerStaff.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(mistPrefab, playerStaff.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(mistPrefab, playerStaff.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(mistPrefab, playerStaff.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
    }
}
