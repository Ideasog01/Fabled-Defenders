using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    //This object managers all quests. Contains: communicates whether a quest is active

    public Sprite corruptionSprite;
    public Sprite honourSprite;
    public Sprite coinSprite;

    private GameObject questLog;
    private GameObject logButton;

    private GameObject QNTitle;
    private GameObject QNDesc;

    public Quest currentQuest;

    private bool QNActive;

    private GameObject player;

    private GameObject questLogCheck;
    private Text questLogCheckName;
    private Text questLogCheckDesc;

    private void Start()
    {
        questLog = GameObject.Find("QuestNotification");
        questLogCheck = GameObject.Find("QuestLogBack");
        questLogCheckName = GameObject.Find("CurrentQuestName").GetComponent<Text>();
        questLogCheckDesc = GameObject.Find("CurrentQuestDesc").GetComponent<Text>();
        CloseQuestNotification();
        QNTitle = GameObject.Find("QNTitle");
        QNDesc = GameObject.Find("QNDesc");
        questLogCheck.transform.localScale = new Vector3(0, 0, 0);

    }

    private void Update()
    {
        if (QNActive)
        {
            Cursor.visible = true;
        }
    }

    public void ShowQuestLogCheck()
    {
        questLogCheck.transform.localScale = new Vector3(1, 1, 1);
    }

    public void CloseQuestLogCheck()
    {
        questLogCheck.transform.localScale = new Vector3(0, 0, 0);
    }

    public void AddQuest(Quest quest)
    {
        questLog.transform.localScale = new Vector3(1, 1, 1);
        QNTitle.GetComponent<Text>().text = quest.questName;
        QNDesc.GetComponent<Text>().text = quest.questDesc;
        questLogCheckName.text = quest.questName;
        questLogCheckDesc.text = quest.questDesc;
        Time.timeScale = 0;
        Cursor.visible = true;
        QNActive = true;
    }

    public void CloseQuestNotification()
    {
        QNActive = false;
        Cursor.visible = false;
        Time.timeScale = 1;
        questLog.transform.localScale = new Vector3(0, 0, 0);
    }

    public void QuestComplete(Quest quest)
    {
        GameObject.Find("QCTitle").GetComponent<Text>().text = quest.questName;
        GameObject.Find("QuestCompleteText").GetComponent<Animator>().Play("QCAnim");
        GameObject.Find("AbilityManager").GetComponent<AbilityManager>().ChargeFocus(25);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PosSound");
        PlayerProperties.gold += quest.goldReward;
        PlayerProperties.honour += quest.honourReward;
    }
}
