using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProperties : MonoBehaviour
{
    private GameObject player;
    private bool isDead;

    private bool positionGot;
    private Vector3 deathPos;

    private bool isOnFire;
    private bool isFrozen;

    private Transform flamesPrefab;
    private Transform icePrefab;
    private Transform characterCanvas;

    [SerializeField]
    private int health;

    private float distanceToPlayer;

    private Rigidbody2D rb;
    private Vector2 distance;

    private bool knockBack;
    private bool knockBackStopped;

    private int knockBackStrength;

    private Slider healthBar;

    private int currentHealth;

    private Vector3 flamesScale;
    private Vector3 iceScale;

    [SerializeField]
    private bool dragonBoss;

    [SerializeField]
    private bool isMariamBoss;

    [SerializeField]
    private bool isMelee;
    private GameObject mariamNPC;

    private int maxHealth;

    private bool once;
    private bool once2;

    private void Start()
    {
        player = GameObject.Find("Player");
        
        flamesPrefab = this.gameObject.transform.GetChild(0);  
        icePrefab = this.gameObject.transform.GetChild(1);
        characterCanvas = this.gameObject.transform.GetChild(2);
        flamesScale = flamesPrefab.transform.localScale;
        iceScale = icePrefab.transform.localScale;
        flamesPrefab.transform.localScale = new Vector3(0, 0, 0);
        icePrefab.transform.localScale = new Vector3(0, 0, 0);
        healthBar = characterCanvas.transform.GetChild(1).GetComponent<Slider>();
        
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        if(player.transform.position.x > this.transform.position.x)
        {
            icePrefab.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            flamesPrefab.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);

        }
        if (player.transform.position.x < this.transform.position.x)
        {
            icePrefab.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1);
            flamesPrefab.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        }


        healthBar.maxValue = health;
        healthBar.value = health;
        maxHealth = health;

        healthBar.gameObject.SetActive(true);
        characterCanvas.GetChild(0).gameObject.SetActive(true);

        


    }


    private void Update()
    {

        if(health < maxHealth)
        {
            this.GetComponent<EnemyBehav>().EnemyTookDamage();
        }

        distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        distance = (player.transform.position - this.transform.position).normalized;

        if (isMariamBoss)
        {
            mariamNPC = GameObject.Find("MariamNPC");
            mariamNPC.SetActive(false);
        }

        if (dragonBoss && health <= 600 && !once)
        {
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().KnightPrefab, this.transform.position, this.transform.rotation);
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().KnightPrefab, this.transform.position, this.transform.rotation);
            once = true;
        }

        if (dragonBoss && health <= 300 && !once2)
        {
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().DolorPrefab, this.transform.position, this.transform.rotation);
            once2 = true;
        }

        if (health <= 0)
        {
            isDead = true;
            player.GetComponent<PlayerProperties>().IncreasePower(5);
            if(dragonBoss)
            {
                FindObjectOfType<PlayerProperties>().keyCodes.Add("3333");
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("Forest");
                player.GetComponent<PlayerProperties>().IncreasePower(250);
            }

            int randomInt = Random.Range(0, 100);

            if(randomInt <= 10)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().corruptionPickup, this.transform.position, this.transform.rotation);
            }

            if(dragonBoss)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().QuestComplete(GameObject.Find("StrangerQuest").GetComponent<QuestProperties>().quest);
            }

            if(isMariamBoss)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().QuestComplete(GameObject.Find("BogQuest").GetComponent<QuestProperties>().quest);
                mariamNPC.SetActive(true);
                mariamNPC.transform.position = this.transform.position;
            }

            

            GameObject.Find("AbilityManager").GetComponent<AbilityManager>().ChargeFocus(15);
            Destroy(this.gameObject);
        }

       

        if(isFrozen)
        {
            icePrefab.transform.localScale = iceScale;
        }
        else
        {
            icePrefab.transform.localScale = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<AbilityManager>().cooldownTime <= 0 && FindObjectOfType<AbilityManager>().abilityNumber == 10)
        {
            knockBack = true;
        }

        if(knockBack == true && distanceToPlayer < 6 && player.transform.position.x < this.transform.position.x)
        {
            knockBackStrength = 50;
            KnockBack();
        }

        if (knockBack == true && distanceToPlayer < 6 && player.transform.position.x > this.transform.position.x)
        {
            knockBackStrength = 50;
            KnockBack();
        }




    }

    public void KnockBack()
    {
        if(isMelee)
        {
            knockBackStopped = false;
            this.GetComponent<EnemyBehav>().Confused();
        }
       

        if (knockBackStopped == false)
        {
            StartCoroutine(StopKnockBack());
            knockBackStopped = true;
        }
        Debug.Log("Knocked");
    }

    IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(1);
        knockBack = false;
        if(isMelee)
        {
            this.GetComponent<EnemyBehav>().Unconfused();
        }
        
    }

    public void DamageThisRayGun()
    {
        StartCoroutine(StaffDamageBehav());
    }

    IEnumerator StaffDamageBehav()
    {
        Take30Damage();
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void FireBallHit()
    {
        if (!isOnFire)
        {
            StartCoroutine(FireBehav());
            isOnFire = true;
        }
    }

   

    IEnumerator FireBehav()
    {
        Debug.Log("FireStarted");
        flamesPrefab.transform.localScale = flamesScale;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        Take10Damage();
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        Take10Damage();
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        Take10Damage();
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        Take10Damage();
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        flamesPrefab.transform.localScale = new Vector3(0, 0, 0);
        isOnFire = false;

    }

    public void HitByIce()
    {
        if(isMelee)
        {
            this.GetComponent<EnemyBehav>().Freeze();
            isFrozen = true;
            StartCoroutine(WaitForIce());
        }

        
    }

    IEnumerator WaitForIce()
    {
        yield return new WaitForSeconds(4);
        isFrozen = false;
    }

    public void EnteredMist()
    {
        if(isMelee)
        {
            this.GetComponent<EnemyBehav>().Confused();
        }
    }

    public void ExitedMist()
    {
        if (isMelee)
        {
            this.GetComponent<EnemyBehav>().Unconfused();
        }
    }

    public void GroundSlamDamage()
    {
        if(distanceToPlayer < 4 && distanceToPlayer > 2 && isMelee)
        {
            StartCoroutine(MediumDamage());
            knockBackStrength = -5;
            KnockBack();
        }

        if(distanceToPlayer < 2 && isMelee)
        {
            StartCoroutine(BigDamage());
            knockBackStrength = -10;
            KnockBack();
        }
    }

    IEnumerator MediumDamage()
    {
        Take10Damage();
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator BigDamage()
    {
        Take20Damage();
        this.GetComponent<SpriteRenderer>().color = FindObjectOfType<GameManager>().damagedColor;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Take10Damage()
    {
        health -= player.GetComponent<PlayerProperties>().minDamage;
        healthBar.value = health;
    }

    private void Take20Damage()
    {
        health -= player.GetComponent<PlayerProperties>().medDamage;
        healthBar.value = health;
    }

    private void Take30Damage()
    {
        health -= player.GetComponent<PlayerProperties>().maxDamage;
        healthBar.value = health;
    }

    public void Heal()
    {
        health += 40;
        healthBar.value = health;
    }

    


}
