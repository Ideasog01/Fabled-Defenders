using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseButton : MonoBehaviour
{
    [SerializeField]
    private int buttonNumber;

    private Button thisButton;

    private ItemProperties assignedItem;




    private void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().currentButtonNumber = buttonNumber;
    }
}
