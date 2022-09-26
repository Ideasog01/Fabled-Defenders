using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSettings : MonoBehaviour
{
    public ItemProperties item;

    public void ShowItemDetails()
    {

        if (!item.letter)
        {
            FindObjectOfType<InventoryManager>().AddItem(item);
        }

        item.hasItem = true;

        
        
    }
}
