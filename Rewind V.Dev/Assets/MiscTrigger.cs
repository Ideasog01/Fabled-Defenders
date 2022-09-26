using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscTrigger : MonoBehaviour
{
    private GameObject villageBoundary;
    [SerializeField]
    private bool barracksTrigger;
    [SerializeField]
    private bool destinationTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(barracksTrigger)
        {
            foreach(GameObject levelVar in GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().levelVars)
            {
                Destroy(levelVar);
                GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().levelVars.Remove(levelVar);
            }
        }
    }
}
