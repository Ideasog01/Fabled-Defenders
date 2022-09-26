using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGuideBehav : MonoBehaviour
{

    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePos.x, mousePos.y, -9);

        if (Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<PlatformAbility>().platformAbilityActive) 
        {
            Instantiate(FindObjectOfType<PlatformAbility>().platformPrefab, this.transform.position, this.transform.rotation);
            FindObjectOfType<PlatformAbility>().numberOfPlatforms += 1;
        }

        if(FindObjectOfType<PlatformAbility>().platformAbilityActive == false)
        {
            Destroy(this.gameObject);
        }
    }
}
