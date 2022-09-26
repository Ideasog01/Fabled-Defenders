using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{

    private bool isFollowing;
    private GameObject player;
    private float speed;

    [SerializeField]
    private bool isPatrolling;

    public int[] moveSpots;
    private int randomSpot;
    private Vector3 patrolTarget;

    private float waitTime;
    private float startWaitTime;

    private bool inRange;

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

    private Transform arrow;
    private bool attacked;
    // Start is called before the first frame update
    void Start()
    {

        arrow = GameObject.Find("GameManager").GetComponent<GameManager>().arrowPrefab;
        speed = 6;
        player = GameObject.Find("Player");

        if (isPatrolling)
        {


            startWaitTime = 3;
            speed = 5;

            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }

        InvokeRepeating("GetDirection", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {

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

        if (!isConfused && !pushBack)
        {
            currentDirection = transform.position.x;

            float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

            if (PlayerProperties.attackPlayer == false && !isFrozen)
            {
                isPatrolling = true;
            }
            if (distanceToPlayer < 10 && distanceToPlayer > 4 && !isFrozen)
            {
                isFollowing = true;
                isPatrolling = false;
            }
            else
            {
                isFollowing = false;
            }

            if (distanceToPlayer < 2 && inRange == false && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !isFrozen && !pushBack && !attacked)
            {
                getDirection = false;
                StartCoroutine(ShadowAttack());
                attacked = true;
            }

            if (isFollowing && PlayerProperties.attackPlayer && !isFrozen)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + new Vector3(0, 1, 0), speed * Time.deltaTime);
            }

            if (isPatrolling && !isFrozen && !pushBack)
            {

                transform.position = Vector2.MoveTowards(transform.position, this.transform.position + new Vector3(moveSpots[randomSpot], 0, 0), speed * Time.deltaTime);
                patrolTarget = this.transform.position + new Vector3(moveSpots[randomSpot], 0, 0);

                if (Vector2.Distance(transform.position, patrolTarget) < 0.2f)
                {
                    getDirection = false;
                    if (waitTime <= 0)
                    {
                        randomSpot = Random.Range(0, moveSpots.Length);
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
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
        }

        if (isConfused && !pushBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, this.transform.position + new Vector3(moveSpots[randomSpot], 0, 0), speed * Time.deltaTime);
            patrolTarget = this.transform.position + new Vector3(moveSpots[randomSpot], 0, 0);

            if (Vector2.Distance(transform.position, patrolTarget) < 0.2f)
            {
                getDirection = false;
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
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
        }


        

        if (faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (!faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (currentDirection == direction)
        {
            isNotMoving = true;

        }
        else
        {
            isNotMoving = false;
        }

        if (isNotMoving == true || isFrozen)
        {

        }
        else
        {

        }




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

    IEnumerator ShadowAttack()
    {
        this.GetComponent<Animator>().Play("DarkShadowAttack");
        Instantiate(arrow, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(10);
        this.GetComponent<Animator>().Play("DarkShadowAttack");
        Instantiate(arrow, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        attacked = false;
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
