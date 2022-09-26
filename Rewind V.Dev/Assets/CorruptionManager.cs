using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionManager : MonoBehaviour
{
    public int corruptionLevel;

    public string mainAreaName;
    public string area1Name;
    public string area2Name;
    public string area3Name;

    public int enemiesDefeated;

    private Text areaText;

    private int areaInt;

    public Vector2[] area1Spawns;
    public Vector2[] area2Spawns;
    public Vector2[] area3Spawns;

    private int waveNumber;

    public Transform[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        areaText = GameObject.Find("AreaText").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(areaInt == 1)
        {
            areaText.text = area1Name + " IS BEING ATTACKED";
        }
        if (areaInt == 2)
        {
            areaText.text = area2Name + " IS BEING ATTACKED";
        }
        if (areaInt == 3)
        {
            areaText.text = area3Name + " IS BEING ATTACKED";
        }

        if(enemiesDefeated == 12)
        {
            corruptionLevel -= 1;
            CancelInvoke("SpawnEnemies");
            StartCoroutine(StartEncounter());
            InvokeRepeating("SpawnEnemies", 1, Random.Range(5, 12));
            enemiesDefeated = 0;
        }
        

        
    }

    public void EncounterReady()
    {
        StartCoroutine(StartEncounter());
        InvokeRepeating("SpawnEnemies", 1, Random.Range(5, 12));
    }

    IEnumerator StartEncounter()
    {
        areaText.text = "The Dark moves fourth";
        areaText.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(2);
        areaText.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(2);
        areaInt = Random.Range(1, 3);
        areaText.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(2);
        areaText.CrossFadeAlpha(0, 1, false);
    }

    public void SpawnEnemies()
    {
        if(areaInt == 1)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], area1Spawns[Random.Range(0, area1Spawns.Length)], this.transform.rotation);
        }
        if (areaInt == 2)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], area2Spawns[Random.Range(0, area2Spawns.Length)], this.transform.rotation);
        }
        if (areaInt == 3)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], area3Spawns[Random.Range(0, area3Spawns.Length)], this.transform.rotation);
        }
    }
}
