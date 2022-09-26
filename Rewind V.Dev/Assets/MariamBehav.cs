using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariamBehav : MonoBehaviour
{
    private GameObject player;
    private float speed;

    private float waitTime;
    private float startWaitTime;

    private bool inRange;

    [SerializeField]
    private bool faceLeft;

    private bool isFrozen;

    [SerializeField]
    private bool isConfused;

    private bool pushBack;

    private float currentPos;

    private Vector2 target;

    private float distanceToTarget;

    private bool playerSeen;

    public bool dialogueFinished;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
        player = GameObject.Find("Player");
        currentPos = this.transform.position.x;
        InvokeRepeating("CheckMovement", 1, 2);
    }

    void CheckMovement()
    {
        currentPos = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueFinished)
        {
            this.transform.GetChild(2).transform.GetChild(0).transform.localScale = new Vector3(0.2f, 0.2f, 1);
            this.transform.GetChild(2).transform.GetChild(1).transform.localScale = new Vector3(0.009f, 0.009f, 1);
        }
        else
        {
            faceLeft = true;
        }

        if(dialogueFinished)
        {
            distanceToTarget = Vector2.Distance(this.transform.position, player.transform.position);
            target = player.transform.position;
            if (faceLeft)
            {
                transform.eulerAngles = new Vector2(0, -180);

            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
            }

            if (pushBack)
            {
                PushBack();
            }

            if (!isConfused && !pushBack)
            {
                if (distanceToTarget < 2 && inRange == false && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !isFrozen && !pushBack)
                {
                    AttackPlayer();
                }

                if (playerSeen && this.GetComponent<Rigidbody2D>().velocity.normalized.x <= 0 && distanceToTarget > 2 && !pushBack)
                {
                    Jump();
                }

                if (distanceToTarget < 8)
                {
                    playerSeen = true;
                }
                if (distanceToTarget > 20)
                {
                    playerSeen = false;
                }

                if (!isFrozen && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !pushBack && playerSeen && distanceToTarget > 2)
                {
                    Walk();
                }
            }

            
        }

        if (faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (!faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void AttackPlayer()
    {
        StartCoroutine(KnightAttack());
    }

    void Walk()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (this.transform.position.x < target.x)
        {
            faceLeft = false;
        }
        else
        {
            faceLeft = true;
        }
    }

    void Jump()
    {
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 14);
        Debug.Log("Jump");
    }

    void PushBack()
    {
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 14);

        Debug.Log("KnockedBack");
    }


    IEnumerator KnightAttack()
    {
        inRange = true;
        yield return new WaitForSeconds(0.25f);
        player.GetComponent<PlayerProperties>().TakeKnightDamage();
        yield return new WaitForSeconds(1f);
        inRange = false;
    }

    public void Freeze()
    {
        isFrozen = true;
        StartCoroutine(FreezeBehaviour());
    }

    IEnumerator FreezeBehaviour()
    {
        yield return new WaitForSeconds(4);
        isFrozen = false;
        speed = 3;
        yield return new WaitForSeconds(5);
        speed = 6;
    }

    public void Confused()
    {
        pushBack = true;

    }

    public void Unconfused()
    {
        pushBack = false;
    }


}
