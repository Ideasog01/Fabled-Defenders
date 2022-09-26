using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolorWarriorBehav : MonoBehaviour
{

    private bool isFollowing;
    private GameObject player;
    private float speed;

    [SerializeField]
    private bool isPatrolling;

    public Vector2[] moveSpots;
    private int randomSpot;

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

    private bool stopped;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
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

        if (!isConfused)
        {
            currentDirection = transform.position.x;

            float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

            if (PlayerProperties.attackPlayer == false && !isFrozen)
            {
                isPatrolling = true;
            }
            if (distanceToPlayer < 10 && distanceToPlayer > 2 && !isFrozen)
            {
                isFollowing = true;
                isPatrolling = false;
            }
            else
            {
                isFollowing = false;
            }

            if (distanceToPlayer < 2 && inRange == false && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !isFrozen && !stopped)
            {
                getDirection = false;
                stopped = true;
            }

            if (isFollowing && PlayerProperties.attackPlayer && !isFrozen && !stopped)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }

            if (isPatrolling && !isFrozen)
            {

                transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot], speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, moveSpots[randomSpot]) < 0.2f)
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

        if (isConfused)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot], speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot]) < 0.2f)
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


        if (stopped && !once)
        {
            //Play Animation
            StartCoroutine(WarriorMainAttack());
            once = true;

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
            isConfused = true;
        }

    }

    public void Unconfused()
    {
        if (isConfused)
        {
            isConfused = false;
        }
    }

    IEnumerator WarriorMainAttack()
    {
        this.GetComponent<Animator>().Play("Warrior_Attack_Anim");
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().dolorProjectilePrefab, this.transform.position, this.transform.rotation);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("DolorAttack");
        FindObjectOfType<GameManager>().DolorOwner = this.gameObject;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Animator>().Play("Warrior_Attack_Anim");
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().dolorProjectilePrefab, this.transform.position, this.transform.rotation);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("DolorAttack");
        FindObjectOfType<GameManager>().DolorOwner = this.gameObject;
        yield return new WaitForSeconds(5);
        GetDirection();
        stopped = false;
        once = false;
    }
}
