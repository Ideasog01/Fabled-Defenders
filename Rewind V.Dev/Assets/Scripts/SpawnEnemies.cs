using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    [SerializeField]
    private List<Vector2> knightSpawnPos;
    [SerializeField]
    private List<Vector2> dolorWarriorPos;
    [SerializeField]
    private List<Vector2> runtPos;
    private GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");   
    }

    public void SpawnEnemy()
    {
        foreach(Vector2 spawnPoint in knightSpawnPos)
        {
            Instantiate(GameManager.GetComponent<GameManager>().KnightPrefab, spawnPoint, this.transform.rotation);
        }
        foreach (Vector2 spawnPoint in dolorWarriorPos)
        {
            Instantiate(GameManager.GetComponent<GameManager>().DolorPrefab, spawnPoint, this.transform.rotation);
        }
        foreach (Vector2 spawnPoint in runtPos)
        {
            Instantiate(GameManager.GetComponent<GameManager>().runtPrefab, spawnPoint, this.transform.rotation);
        }
    }

    public void SpawnEnemiesInNewScene()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddKnights(knightSpawnPos);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddKnights(runtPos);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddKnights(dolorWarriorPos);
    }
}
