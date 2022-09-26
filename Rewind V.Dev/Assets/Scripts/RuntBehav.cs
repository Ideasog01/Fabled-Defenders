using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntBehav : MonoBehaviour
{
    private bool isConfused;
    [SerializeField]
    private bool isPatrolling;
    [SerializeField]
    private bool faceLeft;
    private bool isFrozen;
    private bool isFollowing;
    private bool inRange;
    private bool getDirection;
    private bool isNotMoving;

    private float direction;
    private float currentDirection;

    public Vector2[] moveSpots;
    private int randomSpot;
    private float waitTime;
    private float startWaitTime;

    private float speed;
    private GameObject player;

    private int numberOfAttacks;
    private int numberOfAttacksTillAbility;
    private bool abilityActive;

    private bool stopFollowing;



    private Transform runtBoltPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        numberOfAttacksTillAbility = Random.Range(5, 10);
        speed = 10;
        runtBoltPrefab = FindObjectOfType<GameManager>().runtBoltPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (!faceLeft && !isFrozen)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (numberOfAttacks >= numberOfAttacksTillAbility && abilityActive == false)
        {
            RuntAbility();
            abilityActive = true;
        }

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

            if (distanceToPlayer <= 2 && inRange == false && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !isFrozen)
            {
                this.GetComponent<RuntAnimations>().PlayRuntAttackAnimation();
                getDirection = false;
                if(!abilityActive)
                {
                    StartCoroutine(RuntAttack());
                }
            }

            if (isFollowing && PlayerProperties.attackPlayer && !isFrozen && !stopFollowing)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + new Vector3(0, 0.5f, 0), speed * Time.deltaTime);
            }

            if (isPatrolling && !isFrozen && !abilityActive) 
            {

                transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot], speed * Time.fixedDeltaTime);

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

            

            if (currentDirection == direction)
            {
                isNotMoving = true;

            }
            else
            {
                isNotMoving = false;
            }
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

    IEnumerator RuntAttack()
    {
        getDirection = false;
        inRange = true;
        yield return new WaitForSeconds(0.25f);
        player.GetComponent<PlayerProperties>().TakeRuntDamage();
        numberOfAttacks += 1;
        yield return new WaitForSeconds(1f);
        inRange = false;
    }

    public void RuntAbility()
    {
        StartCoroutine(RuntAbilityBehav());
        stopFollowing = true;
    }

    IEnumerator RuntAbilityBehav()
    {
        
        this.GetComponent<RuntAnimations>().PlayRuntAttackAnimation();
        yield return new WaitForSeconds(0.2f);
        Instantiate(runtBoltPrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        stopFollowing = false;
        numberOfAttacks = 0;
        abilityActive = false;
    }
}



