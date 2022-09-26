using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefab : MonoBehaviour
{
    private bool once;

    private bool triggered;

    [SerializeField]
    private float distanceToPlayer;
    private GameObject player;

    private GameObject levelGenerator;

    [SerializeField]
    private int yIntToAdd;

    [SerializeField]
    private bool setDestination;

 /*   // Start is called before the first frame update
    void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator");
        this.transform.parent = GameObject.Find("LevelVariationParent").transform;
        player = GameObject.Find("Player");
        levelGenerator.GetComponent<LevelGenerator>().yIntToAdd = yIntToAdd;
        levelGenerator.GetComponent<LevelGenerator>().levelVars.Add(this.gameObject);
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
        if (setDestination && distanceToPlayer <= 10 && distanceToPlayer != 0 && !triggered)
        {
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().ShowCompass();
            triggered = true;
        }

        if (distanceToPlayer <= 10 && !triggered && distanceToPlayer != 0 && !levelGenerator.GetComponent<LevelGenerator>().stopGeneration && !setDestination)
        {
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().GenerateVariation();
            triggered = true;
        }
  */  }
