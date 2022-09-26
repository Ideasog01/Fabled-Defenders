using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private bool spawnKnights;

    [SerializeField]
    private bool spawnRunts;

    [SerializeField]
    private bool spawnDolors;

    private float distanceToPlayer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        if(spawnKnights)
        {
            InvokeRepeating("SpawnKnight", 1, Random.Range(10, 25));
        }

        if (spawnRunts)
        {
            InvokeRepeating("SpawnRunts", Random.Range(1, 2), Random.Range(10, 15));
        }
        if (spawnDolors)
        {
            InvokeRepeating("SpawnDolor", 1, Random.Range(15, 25));
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
    }

    void SpawnKnight()
    {
        if(distanceToPlayer < 10)
        {
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().KnightPrefab, this.transform.position, this.transform.rotation);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CorruptionSound");
        }
        
    }

    void SpawnRunts()
    {
        if (distanceToPlayer < 10)
        {
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().runtPrefab, this.transform.position, this.transform.rotation);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CorruptionSound");
        }
        
    }

    void SpawnDolor()
    {
        if (distanceToPlayer < 10)
        {
            Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().DolorPrefab, this.transform.position, this.transform.rotation);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CorruptionSound");
        }
        
    }
}
