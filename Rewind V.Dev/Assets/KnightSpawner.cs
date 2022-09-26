using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void SpawnKnight()
    {
        Instantiate(GameManager.KnightPrefab, this.transform.position, this.transform.rotation);
    }
}
