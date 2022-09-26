using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    private GameObject player;

    private bool isRaising;
    private bool isLowering;

    private bool once;

    private GameObject pointLightStaff;
    public Sprite timeStaff;
    public Sprite defaultStaff;

    private GameObject playerStaff;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pointLightStaff = GameObject.Find("PointLightStaff");
        playerStaff = GameObject.Find("PlayerStaff");
       // this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(TimeBody.isRewinding && !once)
        {
            StartCoroutine(RaiseStaff());
            once = true;
        }

        if(Input.GetMouseButtonDown(0) && PlayerAttack.disableAttack == false)
        {
            if(!FindObjectOfType<PlayerAnimations>().faceLeft)
            {
                playerStaff.GetComponent<Animator>().Play("PlayerStaffBounce_Anim");
            }
            if (FindObjectOfType<PlayerAnimations>().faceLeft)
            {
                playerStaff.GetComponent<Animator>().Play("PlayerStaffBounceLeft_Anim");
            }

        }

        if(TimeBody.isRewinding == false)
        {
            once = false;
        }

        if(isRaising)
        {
            this.transform.Translate(Vector2.up * Time.fixedDeltaTime * 6f);
            pointLightStaff.GetComponent<Light>().color = Color.green;
            pointLightStaff.GetComponent<Light>().intensity = 4;
        }

        if (isLowering)
        {
            this.transform.Translate(Vector2.down * Time.fixedDeltaTime * 6f);
            pointLightStaff.GetComponent<Light>().color = Color.white;
            pointLightStaff.GetComponent<Light>().intensity = 0.5f;
        }


    }

    IEnumerator RaiseStaff()
    {
        this.GetComponent<SpriteRenderer>().sprite = timeStaff;
        isRaising = true;
        yield return new WaitForSeconds(1f);
        isRaising = false;
        yield return new WaitForSeconds(0.5f);
        isLowering = true;
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = defaultStaff;
        isLowering = false;
    }
}
