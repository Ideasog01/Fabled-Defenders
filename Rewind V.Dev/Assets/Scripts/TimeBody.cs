using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public static bool isRewinding;

    private Rigidbody2D rb;

    List<Vector2> positions;

    private Vector2 lastPos;

    private GameObject abilityManager;

    private void Start()
    {
        positions = new List<Vector2>();
        rb = this.GetComponent<Rigidbody2D>();
        abilityManager = GameObject.Find("AbilityManager");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().abilityNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0)
            {
                FindObjectOfType<AbilityManager>().AbilityUsed();
                StartRewind();
                StartCoroutine(StopAfterTime());
                PlayerAttack.disableAttack = true;
            }
        }
        


    }

    private void FixedUpdate()
    {
        if(isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

        
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

    void Record()
    {
        positions.Insert(0, transform.position);
    }

    void Rewind()
    {
        if(positions.Count > 0)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, positions[100], Time.deltaTime * 24);

         //   transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(3);
        PlayerAttack.disableAttack = false;
        StopRewind();
    }
}
