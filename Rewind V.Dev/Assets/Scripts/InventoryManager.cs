using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryUI;
    private GameObject WarningUI;
    private GameObject nonDiscardUI;

    public ItemProperties[] items;
    public ItemProperties[] artefacts;

    public GameObject[] inventoryIcons;

    private int numberOfItems;
    private int itemToFocusNum;

    public int itemToDiscardNum;

    public int currentButtonNumber;
    public Sprite emptyIcon;

    private GameObject player;

    private void Start()
    {
        inventoryUI = GameObject.Find("InventoryBack");
        WarningUI = GameObject.Find("ItemWarningBack");
        nonDiscardUI = GameObject.Find("ItemNOTDiscard");
        inventoryUI.transform.localScale = new Vector3(0, 0, 0);
        WarningUI.transform.localScale = new Vector3(0, 0, 0);
        nonDiscardUI.transform.localScale = new Vector3(0, 0, 0);
        player = GameObject.Find("Player");
    }

    public void AddItem(ItemProperties item)
    {
        inventoryIcons[numberOfItems].GetComponent<Image>().sprite = item.itemImage;
        numberOfItems += 1;
    }


    public void ShowRemoveWarning()
    {
        WarningUI.transform.localScale = new Vector3(1, 1, 1);
    }

    public void CloseWarning()
    {
        WarningUI.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowInventory()
    {
        inventoryUI.transform.localScale = new Vector3(1, 1, 1);
        PlayerAttack.disableAttack = true;
        FindObjectOfType<PlayerMovement_R>().stunned = true;
        Cursor.visible = true;
    }

    public void HideInventory()
    {
        inventoryUI.transform.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
    }

    public void UpdateInventoryUI()
    {
        inventoryIcons[0].GetComponent<Image>().sprite = items[0].itemImage;
        inventoryIcons[1].GetComponent<Image>().sprite = items[1].itemImage;
        inventoryIcons[2].GetComponent<Image>().sprite = items[2].itemImage;
    }
    public void CloseNonDiscardMES()
    {
        nonDiscardUI.transform.localScale = new Vector3(0, 0, 0);
    }

}
