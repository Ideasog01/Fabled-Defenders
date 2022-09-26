using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBehav : MonoBehaviour
{

    private GameObject player;
    private bool jump;

    private float distance;
    private bool abilityStarted;

    private bool attack;

    private float direction;
    private float currentDirection;

    [SerializeField]
    private bool faceLeft;
    private bool getDirection;

    private bool isNotMoving;

    private bool isFrozen;

    [SerializeField]
    private bool isConfused;

    private bool pushBack;

    private float waitTime;
    private float startWaitTime;

    private Rigidbody2D rb;

    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
        distance = 100;

        speed = 5;

        InvokeRepeating("GetDirection", 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, this.transform.position);

        if (distance > 3 && distance < 12 && !attack && !pushBack && !isFrozen)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            GetDirection();
        }


        if (pushBack == true)
        {
            GetDirection();
            if (faceLeft)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + new Vector3(2, 0, 0), 8 * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + new Vector3(-2, 0, 0), 8 * Time.fixedDeltaTime);
            }
        }

        if (faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (!faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (!isConfused && !pushBack && distance > 12)
        {
            currentDirection = transform.position.x;

            if (currentDirection > direction && !isFrozen)
            {
                faceLeft = false;
                if (getDirection == false)
                {
                    GetDirection();
                }
            }



            if (currentDirection < direction && !isFrozen)
            {
                faceLeft = true;
                if (getDirection == false)
                {
                    GetDirection();
                }
            }
        }

        if (currentDirection > direction && !isFrozen)
        {
            faceLeft = false;
            if (getDirection == false)
            {
                GetDirection();
            }
        }



        if (currentDirection < direction && !isFrozen)
        {
            faceLeft = true;
            if (getDirection == false)
            {
                GetDirection();
            }
        }

        if (distance < 4 && !attack)
        {
            StartCoroutine(JumpAttack());
            attack = true;
        }

        if (jump)
        {
            this.transform.Translate(Vector2.up * 5 * Time.fixedDeltaTime);
            Debug.Log("Jump");
        }
    }


    private void GiantLanded()
    {
        if (distance < 5)
        {
            FindObjectOfType<CameraShake>().Shake();
            player.GetComponent<PlayerProperties>().GiantDamage();
        }
        jump = false;
        GetDirection();
        this.GetComponent<Animator>().Play("Giant_Landed_Anim");
    }

    public void isGrounded1()
    {
        if (attack)
        {
            GiantLanded();
            Debug.Log("IsGrounded");
        }

    }

    IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(2);
        jump = true;
        getDirection = false;
        this.GetComponent<Animator>().Play("Giant_Jump_Anim");
        yield return new WaitForSeconds(2);
        attack = false;
    }


        public void GetDirection()
        {
            direction = transform.position.x;
            getDirection = true;
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
            if (!isConfused)
            {
                pushBack = true;
            }

        }

        public void Unconfused()
        {
            pushBack = false;
            if (isConfused)
            {
                //  isConfused = false;
            }
        }



    }
