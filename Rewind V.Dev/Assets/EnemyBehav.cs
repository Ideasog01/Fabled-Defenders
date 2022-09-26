using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehav : MonoBehaviour
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

    [SerializeField]
    private float distanceToTarget;

    private bool playerSeen;
    private GameObject enemyManager;

    [SerializeField]
    private string enemyType;

    private int numberOfAttacks;

    [SerializeField]
    private int radiusOfAttack;

    [SerializeField]
    private int minRadius;

    [SerializeField]
    private bool dialogueEnded;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
        player = GameObject.Find("Player");
        currentPos = this.transform.position.x;
        InvokeRepeating("CheckMovement", 1, 2);

        if(!dialogueEnded)
        {
            this.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void CheckMovement()
    {
        currentPos = this.transform.position.x;
    }

    public void DialogueFinished()
    {
        dialogueEnded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueEnded)
        {
            this.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        }

        distanceToTarget = Vector2.Distance(this.transform.position, player.transform.position);
        
        if(enemyType != "Dragon")
        {
            target = player.transform.position;
        }
        else
        {
            target = player.transform.position + new Vector3(3, 2, 0);
        }

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
            StartCoroutine(TimePushBack());
        }

        if (!isConfused && !pushBack)
        {
            if (distanceToTarget < radiusOfAttack && inRange == false && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !isFrozen && !pushBack && dialogueEnded)
            {
                AttackPlayer();
                inRange = true;
            }

            if (playerSeen && this.GetComponent<Rigidbody2D>().velocity.normalized.x <= 0 && distanceToTarget > radiusOfAttack && !pushBack)
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

            

            if (!isFrozen && PlayerProperties.attackPlayer && this.GetComponent<SpriteRenderer>().isVisible && !pushBack && playerSeen && distanceToTarget > minRadius)
            {
                Walk();
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

    public void EnemyTookDamage()
    {
        playerSeen = true;
    }

    void AttackPlayer()
    {
        
        if(enemyType == "Knight")
        {
            StartCoroutine(KnightAttack());
            this.GetComponent<Animator>().Play("AttackAnimation");
        }
        if(enemyType == "Runt")
        {
            if(numberOfAttacks < 4)
            {
                StartCoroutine(RuntAttack());
                this.GetComponent<Animator>().Play("AttackAnimation");
            }
            else
            {
                this.GetComponent<Animator>().Play("AttackAnimation");
                RuntAbility();
                numberOfAttacks = 0;
            }
        }

        if (enemyType == "Dolor")
        {
            if (this.transform.position.x < target.x)
            {
                faceLeft = false;
            }
            else
            {
                faceLeft = true;
            }
            StartCoroutine(WarriorMainAttack());
        }

        if(enemyType == "Mariam")
        {
            StartCoroutine(MariamAttack());
        }

        if(enemyType == "Dragon")
        {
            StartCoroutine(DragonAttack());
        }
       
    }

    IEnumerator TimePushBack()
    {
        if(pushBack)
        {
            yield return new WaitForSeconds(2);
            pushBack = false;
        }
        
    }

    IEnumerator DragonAttack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        this.GetComponent<Animator>().Play("AttackAnimation");
        yield return new WaitForSeconds(1.2f);
        if (faceLeft)
        {
            Instantiate(FindObjectOfType<GameManager>().IceBall, this.transform.position + new Vector3(-3.9f, -0.25f, 0), this.transform.rotation);
        }
        else
        {
            Instantiate(FindObjectOfType<GameManager>().IceBall, this.transform.position + new Vector3(3.9f, 0.25f, 0), this.transform.rotation);
        }
        yield return new WaitForSeconds(2);
        if(faceLeft)
        {
            faceLeft = false;
        }
        else
        {
            faceLeft = true;
        }
        inRange = false;
    }

    IEnumerator WarriorMainAttack()
    {
        this.GetComponent<Animator>().Play("AttackAnimation");
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().dolorProjectilePrefab, this.transform.position, this.transform.rotation);
        this.GetComponent<Animator>().Play("AttackAnimation");
        FindObjectOfType<GameManager>().DolorOwner = this.gameObject;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Animator>().Play("AttackAnimation");
        yield return new WaitForSeconds(1);
        Instantiate(FindObjectOfType<GameManager>().dolorProjectilePrefab, this.transform.position, this.transform.rotation);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("DolorAttack");
        FindObjectOfType<GameManager>().DolorOwner = this.gameObject;
        yield return new WaitForSeconds(5);
        inRange = false;
    }


    public void RuntAbility()
    {
        StartCoroutine(RuntAbilityBehav());
    }

    IEnumerator RuntAbilityBehav()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().runtBoltPrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        numberOfAttacks = 0;
        inRange = false;
    }

    IEnumerator RuntAttack()
    {
        yield return new WaitForSeconds(0.25f);
        

        if(!player.GetComponent<PlayerShieldAbility>().shieldActive)
        {
            player.GetComponent<PlayerProperties>().TakeRuntDamage();
        }
        else
        {
            if (!faceLeft && !GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().faceLeft)
            {
                player.GetComponent<PlayerProperties>().TakeRuntDamage();
            }
            if (faceLeft && GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().faceLeft)
            {
                player.GetComponent<PlayerProperties>().TakeRuntDamage();
            }
        }
        
        numberOfAttacks += 1;
        yield return new WaitForSeconds(1f);
        inRange = false;
    }

    void Walk()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        this.GetComponent<Animator>().Play("WalkAnimation");
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
        if(enemyType == "Knight" || enemyType == "Dolor" || enemyType == "Mariam")
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 14);
            Debug.Log("Jump");
        }
        
    }

    void PushBack()
    {
        if(player.transform.position.x < this.transform.position.x)
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 14);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 14);
        }
       

        Debug.Log("KnockedBack");
    }


    IEnumerator KnightAttack()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("KnightAttackSound");
        if (!player.GetComponent<PlayerShieldAbility>().shieldActive)
        {
            player.GetComponent<PlayerProperties>().TakeKnightDamage();
        }
        else
        {
            if (!faceLeft && !GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().faceLeft)
            {
                player.GetComponent<PlayerProperties>().TakeKnightDamage();
            }
            if (faceLeft && GameObject.Find("PlayerSprite").GetComponent<PlayerAnimations>().faceLeft)
            {
                player.GetComponent<PlayerProperties>().TakeKnightDamage();
            }
        }


        yield return new WaitForSeconds(1f);
        inRange = false;
    }

    IEnumerator MariamAttack()
    {
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerProperties>().TakeKnightDamage();
        yield return new WaitForSeconds(Random.Range(3, 5));
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

