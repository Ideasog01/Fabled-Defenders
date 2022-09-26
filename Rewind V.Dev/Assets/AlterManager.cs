using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterManager : MonoBehaviour
{

    private bool tutorial;
    private GameObject alterUI;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = true;
        alterUI = GameObject.Find("AlterUpgradeBox");
        alterUI.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAlter()
    {
        alterUI.transform.localScale = new Vector3(1, 1, 1);
        if (tutorial == true)
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void CloseAlter()
    {
        alterUI.transform.localScale = new Vector3(0, 0, 0);
    }
}
