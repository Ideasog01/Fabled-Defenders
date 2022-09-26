using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehav : MonoBehaviour
{
    [SerializeField]
    private string keyCode;

    public void PickUpKey()
    {
        FindObjectOfType<PlayerProperties>().keyCodes.Add(keyCode);
        GameObject.Find("ItemManager").GetComponent<ItemManager>().ShowItem(this.GetComponent<ItemSettings>().item);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PickUpKey();
        }
    }
}
