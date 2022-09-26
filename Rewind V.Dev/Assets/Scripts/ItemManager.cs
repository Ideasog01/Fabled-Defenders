using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private GameObject itemDisplay;
    private Text itemNameText;
    private Text itemDescText;
    private Image itemImage;

    private ItemProperties currentArtefact;

    private void Start()
    {
        itemDisplay = GameObject.Find("ItemBack1");
        itemNameText = GameObject.Find("ItemNameText").GetComponent<Text>();
        itemDescText = GameObject.Find("ItemDescription").GetComponent<Text>();
        itemImage = GameObject.Find("ItemSprite").GetComponent<Image>();
        itemDisplay.SetActive(false);
    }

    public void ShowItem(ItemProperties item)
    {
        itemDisplay.SetActive(true);
        itemNameText.text = item.itemName;
        itemDescText.text = item.itemDescription;
        itemImage.sprite = item.itemImage;
        PlayerAttack.disableAttack = true;
        FindObjectOfType<PlayerMovement_R>().stunned = true;
        Cursor.visible = true;
        currentArtefact = item;
    }

    public void HideItem()
    {
        itemDisplay.SetActive(false);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
    }

    public void EquipArtefact()
    {
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddItem(currentArtefact);
        GameObject.Find("GameManager").GetComponent<GameManager>().artefactEquipButton.SetActive(false);
    }
}
