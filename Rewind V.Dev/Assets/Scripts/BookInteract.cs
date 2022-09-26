using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{

    public void RuneBookFound()
    {
        GameObject.Find("ItemManager").GetComponent<ItemManager>().ShowItem(GameObject.Find("RuneBook1").GetComponent<ItemSettings>().item);
        GameObject.Find("RuneManager").GetComponent<RuneManager>().RecieveRandomRune();
        this.enabled = false;
    }
}
