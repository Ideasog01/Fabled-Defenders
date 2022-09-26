using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAb : MonoBehaviour
{
    public Transform portalPrefab;
    public int portalsActive;

    public Vector2 portal1Coord;
    public Vector2 portal2Coord;

    public bool teleportEnemyOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<AbilityManager>().abilityNumber == 8 && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && Input.GetKeyDown(KeyCode.Q) && portalsActive < 2)
        {
            Instantiate(portalPrefab, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
            portalsActive += 1;
        }
    }

    public void ResetAbility()
    {
        portalsActive = 0;
    }
}
