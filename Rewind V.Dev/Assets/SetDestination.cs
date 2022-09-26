using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetDestination : MonoBehaviour
{
    [SerializeField]
    private int destinationNum;
    private Button thisButton;

    private 
    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().SetDestination(destinationNum);
    }
}
