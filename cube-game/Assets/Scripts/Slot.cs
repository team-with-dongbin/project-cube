using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public string itemname;
    public bool isWeapon;
    public int itemCount;

    [SerializeField]
    private Text countText;
    [SerializeField]
    private GameObject countImage;
    [SerializeField]
    private Image itemImage;

    public bool AddItem(Item newItem)
    {
        if(newItem.itemName == itemname && !isWeapon)
        {
            itemCount++;
            countText.text = itemCount.ToString();
            return true;
        }
        return false;
    }

    public void NewSlot(Item newItem)
    {
        itemname = newItem.itemName;
        itemCount = 1;
        itemImage.sprite = newItem.itemImage;
        isWeapon = (newItem.itemType == Item.ItemType.Gun || newItem.itemType == Item.ItemType.Knife);
        if (isWeapon)
        {
            Color c = countImage.GetComponent<Renderer>().material.color;
            c.a = 0f;
            countImage.GetComponent<Renderer>().material.color = c;
        }
        else
            countText.text = itemCount.ToString();
    }
}