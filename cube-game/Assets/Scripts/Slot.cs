using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Text countText;

    public List<GameObject> item;
    int itemNumber;
    bool isEquip;

    private void Start()
    {
        item.Clear();
    }
    public bool AddCount(GameObject newItem)
    {
        if(!isEquip && itemNumber == newItem.GetComponent<Item>().data.ItemNumber)
        {
            item.Add(newItem);
            countText.text = item.Count.ToString();
            return true;
        }
        return false;
    }
    public void NewSlot(GameObject newItem)
    {
        item.Add(newItem);
        itemNumber = newItem.GetComponent<Item>().data.ItemNumber;
        itemImage.sprite = newItem.GetComponent<Item>().data.ItemImage.sprite;
        itemImage.color = SetAlpha(itemImage.color, 1f);
        isEquip = CheckEquip(newItem.GetComponent<Item>().itemType);
        if (!isEquip)
            countText.text = item.Count.ToString();
    }
    private Color SetAlpha(Color color, float newAlpha)
    {
        Color newColor = color;
        newColor.a = newAlpha;
        return newColor;
    }
    private bool CheckEquip(Item.ItemType itemType)
    {
        return (itemType == Item.ItemType.Equipment || itemType == Item.ItemType.Gun || itemType == Item.ItemType.Knife);
    }
}
