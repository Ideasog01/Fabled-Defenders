using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().QuestComplete(GameObject.Find("TutorialQuest").GetComponent<QuestProperties>().quest);
        }
    }
}
