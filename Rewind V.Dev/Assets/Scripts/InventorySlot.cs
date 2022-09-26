using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private int inventorySlotNumber;

    private Button thisButton;

    private GameObject invManager;

    private void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
        invManager = GameObject.Find("InventoryManager");
    }

    private void TaskOnClick()
    {
        invManager.GetComponent<InventoryManager>().currentButtonNumber = inventorySlotNumber;
    }



}
