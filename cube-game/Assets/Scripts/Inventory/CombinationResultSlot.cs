using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class CombinationResultSlot : Slot, IPointerDownHandler
{
    public static CombinationResultSlot instance;
    CombinationResultSlot combinationResultSlot;

    private void Awake()
    {
        instance = this;
        combinationResultSlot = GetComponent<CombinationResultSlot>();
    }

    private void Update()
    {
        printResult(Inventory.instance.TryCombination().Item2);
    }
    public void printResult((int itemId,int count) result)
    {
        if (result.itemId == -1)
        {
            combinationResultSlot.ClearSlot();
            return;
        }
        else
        {
            itemImage.sprite = ItemDictionary.instance.FindById(result.itemId).icon;
            SetAlpha(1f);
            countText.text = result.count.ToString();
        }
    }

    protected override bool Validate(ItemData itemData)
    {
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.DoCombination();
    }
}
