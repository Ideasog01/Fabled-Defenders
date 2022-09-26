using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragonBehav : MonoBehaviour
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

    private float direction;
    private float currentDirection;

    [SerializeField]
    private bool faceLeft;
    private bool getDirection;

    private bool isNotMoving;

    [SerializeField]
    private bool boss;
    private bool returnToPos;

    private bool attacked;

    private Vector2 playerPosition;

    private Vector2 target;

    private bool playerReached;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
        player = GameObject.Find("Player");
        if (isPatrolling)
        {
            startWaitTime = 3;
            speed = 10;

            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }
        target = player.transform.position + new Vector3(7, 3, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = transform.position.x;
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);


        if (PlayerProperties.attackPlayer == false)
        {
            isPatrolling = true;
        }

        if (distanceToPlayer > 8 && !playerReached)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            playerReached = true;
            returnToPos = true;
        }

        if (isPatrolling)
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

        if (faceLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
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

        if (transform.position.x == target.x && distanceToPlayer > 6)
        {
            if(!attacked)
            {
                StartCoroutine(DragonAttack());
                attacked = true;
            }
            
            target.x = player.transform.position.x - 7;
            faceLeft = true;
        }

        if (transform.position.x == target.x && distanceToPlayer > 6)
        {
            if (!attacked)
            {
                StartCoroutine(DragonAttack());
                attacked = true;
            }

            target.x = player.transform.position.x + 7;
            faceLeft = false;
        }

        if (returnToPos)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }

    }

    public void GetDirection()
    {
        direction = transform.position.x;
        getDirection = true;
    }

    IEnumerator DragonAttack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        getDirection = false;
        this.GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1.2f);
        if(faceLeft)
        {
            Instantiate(FindObjectOfType<GameManager>().IceBall, this.transform.position + new Vector3(-3.9f, -0.25f, 0), this.transform.rotation);
        }
        else
        {
            Instantiate(FindObjectOfType<GameManager>().IceBall, this.transform.position + new Vector3(3.9f, 0.25f, 0), this.transform.rotation);
        }
        this.GetComponent<Animator>().SetBool("Attack", false);
        returnToPos = true;
        playerPosition = player.transform.position;
        attacked = false;
    }

}
