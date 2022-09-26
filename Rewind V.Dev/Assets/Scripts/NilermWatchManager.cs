using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NilermWatchManager : MonoBehaviour
{
    public bool DSdialogueTriggerd1;
   


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KnightDialogueFinished()
    {
        Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().KnightPrefab, GameObject.Find("KnightDummy1").transform.position, GameObject.Find("KnightDummy1").transform.rotation);
        Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().KnightPrefab, GameObject.Find("KnightDummy2").transform.position, GameObject.Find("KnightDummy2").transform.rotation);
    }

    public void NilermDialogueFinished()
    {

    }

}
