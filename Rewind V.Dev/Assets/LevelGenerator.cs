using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public Transform[] levelPrefabs;
    public List<GameObject> levelVars;
    public Transform strangerPrefab;

    public Transform ChildOfNixia;

    public int xInt;
    public int yInt;
    public int lastPrefabNum;
    public int randomGenNum;

    private int numberOfPrefabs;

    public int distanceCovered;
    public int distanceTarget;

    public int targetNum;

    public bool stopGeneration;

    public int yIntToAdd;

    public Transform bogLevelVar;

    public GameObject Zar;
    // Start is called before the first frame update
    void Start()
    {
        Zar = GameObject.Find("Zar");
        Zar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
         //   Instantiate(bogLevelVar, new Vector2(xInt, yInt), this.transform.rotation);
          //  xInt += 50;
        }


    }

    public void ShowCompass()
    {
        PlayerAttack.disableAttack = true;
        FindObjectOfType<PlayerMovement_R>().stunned = true;
        Cursor.visible = true;


        
    }

    public void CloseCompass()
    {
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
    }

    public void SetDestination(int destNum)
    {
        if(destNum == 1)
        {
            ResetLevelVariations("Bog");
        }
        stopGeneration = false;
        CloseCompass();
        GenerateVariation();
    }

    public void SpawnDragonBoss()
    {
        Instantiate(ChildOfNixia, new Vector3(330, yInt + 8, 0), this.transform.rotation);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Forest");
    }

    public void ResetLevelVariations(string destName)
    {
        distanceCovered = 0;
        stopGeneration = false;
    }

    public void GenerateVariation()
    {
        yInt += yIntToAdd;
        if (!stopGeneration)
        {
            if (distanceCovered != targetNum)
            {
                GenerateLevel();
            }
            else
            {
                GenerateSpecificLevel();
            }
        }
        

    }

    public void GenerateSpecificLevel()
    {
        if(distanceCovered == 20)
        {
            GameObject.Find("VillageLevel").transform.position = new Vector2(xInt, yInt);
            targetNum = 15;
            stopGeneration = true;
            xInt += 70;
        }
        if(distanceCovered == 10)
        {
            Instantiate(strangerPrefab, new Vector2(xInt, yInt), this.transform.rotation);
            
            targetNum = 20;
            xInt += 30;
        }
        if(distanceCovered == 15)
        {
            Instantiate(bogLevelVar, new Vector2(xInt, yInt), this.transform.rotation);
            xInt += 50;
        }

        
        numberOfPrefabs += 1;
        distanceCovered += 1;
    }

    public void GenerateLevel()
    {

        if (distanceCovered != 20 && distanceCovered != 10)
        {
            randomGenNum = Random.Range(0, levelPrefabs.Length);

            if (randomGenNum != lastPrefabNum)
            {
                if (levelPrefabs[randomGenNum] != null)
                {
                    Instantiate(levelPrefabs[randomGenNum], new Vector2(xInt, yInt), this.transform.rotation);
                }
                lastPrefabNum = randomGenNum;
                
            }
            else
            {
                randomGenNum = Random.Range(0, levelPrefabs.Length);
                Instantiate(levelPrefabs[randomGenNum], new Vector2(xInt, yInt), this.transform.rotation);
                lastPrefabNum = randomGenNum;
            }
        }
        xInt += 30;
        numberOfPrefabs += 1;
        distanceCovered += 1;
        Debug.Log("Created LevelVar at X " + xInt + "Y " + yInt);

    }
}
