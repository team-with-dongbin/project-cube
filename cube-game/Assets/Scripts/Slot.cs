using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text countText;

    public List<GameObject> item;
    int itemId;
    bool isEquip;

    private void Start()
    {
        item.Clear();
    }
    public bool AddCount(GameObject newItem)
    {
        if (!isEquip && itemId == newItem.GetComponent<Item>().data.id)
        {
            item.Add(newItem);
            countText.text = item.Count.ToString();
            return true;
        }
        return false;
    }

    public void NewSlot(GameObject newItem)
    {
        item.Clear();
        item.Add(newItem);
        itemId = newItem.GetComponent<Item>().data.id;
        itemImage.sprite = newItem.GetComponent<Item>().data.icon;
        SetAlpha(1f);
        isEquip = CheckEquip(newItem.GetComponent<Item>().data.itemType);
        if (!isEquip)
            countText.text = item.Count.ToString();
    }

    public void ClearSlot()
    {
        item.Clear();
        itemId = 0;
        SetAlpha(0f);
        countText.text = "";
    }

    public void SetAlpha(float newAlpha)
    {
        Color color = itemImage.color;
        color.a = newAlpha;
        itemImage.color = color;
    }

    private bool CheckEquip(ItemType itemType)
    {
        return (itemType == ItemType.Equipment || itemType == ItemType.Gun || itemType == ItemType.Knife);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragSlot.instance.dragSlot = this;
        DragSlot.instance.SetDragImage(itemImage);
        if (item.Any())
            DragSlot.instance.SetAlpha(1f);
        DragSlot.instance.transform.position = eventData.position;
        DragSlot.instance.SetTransform(eventData.position);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        DragSlot.instance.transform.position = eventData.position;
        DragSlot.instance.SetTransform(eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot.item.Any())
            ChangeSlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetAlpha(0f);
        DragSlot.instance.transform.position = Vector3.zero;
        DragSlot.instance.SetTransform(Vector3.zero);
    }

    private void ChangeSlot()
    {
        if (DragSlot.instance.dragSlot.item.Any())
        {
            List<GameObject> itemTemp = item.ToList();

            NewSlot(DragSlot.instance.dragSlot.item[0]);
            item = DragSlot.instance.dragSlot.item.ToList();
            countText.text = item.Count.ToString();

            if (itemTemp.Any())
            {
                DragSlot.instance.dragSlot.NewSlot(itemTemp[0]);
                DragSlot.instance.dragSlot.item = itemTemp.ToList();
                DragSlot.instance.dragSlot.countText.text = itemTemp.Count.ToString();
            } else
                DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
