using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAcceptButton : MonoBehaviour
{
    private Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        GameObject.Find("QuestManager").GetComponent<QuestManager>().CloseQuestNotification();
    }
}
