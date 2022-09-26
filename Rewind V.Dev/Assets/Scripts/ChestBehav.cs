using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehav : MonoBehaviour
{
    private bool isUnlocked;

    private int numberOfCoins;
    private int numberOfHonour;

    [SerializeField]
    private int valueScale;

    private GameObject player;

    [SerializeField]
    private bool activateKey;

    [SerializeField]
    private bool mayHaveArtefact;
    public GameObject key;

    private int randomNum;
    // Start is called before the first frame update
    void Start()
    {
        if(mayHaveArtefact)
        {
            randomNum = Random.Range(1, 2);
        }
        player = GameObject.Find("Player");
        if(activateKey)
        {
            key.SetActive(false);
        }
        
        if (valueScale <= 0)
        {
            numberOfCoins = 0;
            numberOfHonour = 0;
        }

        if(valueScale == 1)
        {
            numberOfHonour = 0;
            numberOfCoins = Random.Range(0, 2);
        }

        if(valueScale == 2)
        {
                numberOfCoins = Random.Range(5, 7);
        }

        if (valueScale >= 3)
        {
                numberOfCoins = Random.Range(5, 11);
                numberOfHonour = Random.Range(1, 5);
        }
    }

    public void GiveItems()
    {
        if(!isUnlocked)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("ChestOpen");
            PlayerProperties.gold += numberOfCoins;
            PlayerProperties.honour += numberOfHonour;

            if(mayHaveArtefact && randomNum == 2)
            {
                GameObject.Find("ItemManager").GetComponent<ItemManager>().ShowItem(GameObject.Find("GameManager").GetComponent<GameManager>().artefacts[Random.Range(0, GameObject.Find("GameManager").GetComponent<GameManager>().artefacts.Length)]);
                GameObject.Find("GameManager").GetComponent<GameManager>().artefactEquipButton.SetActive(true);
            }
           

            this.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<GameManager>().chestUnlockedSprite;

            if(activateKey)
            {
                key.SetActive(true);
            }
            isUnlocked = true;
        }
    }
}
