using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryWindow;
    [SerializeField]
    private GameObject itemSlots;
    [SerializeField]
    private GameObject CombinationSlots;
    [SerializeField]
    private GameObject CombinationResultSlot;

    public static Inventory instance;
    public bool activeInventory = false;
    private Slot[] slots;
    //private Slot[] CombSlot;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slots = itemSlots.GetComponentsInChildren<Slot>();
        //CombSlot = CombinationSlot.GetComponentsInChildren<Slot>();
        // inventoryWindow.SetActive(activeInventory);
    }

    void Update()
    {
        WindowControl();
        Combination();
    }

    void WindowControl()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryWindow.SetActive(activeInventory);
        }
    }

    void Combination()
    {
        List<Slot> ingredients = new();

        foreach (Slot s in CombinationSlots.GetComponentsInChildren<Slot>())
        //foreach (Slot s in CombSlot)
        {
            if (s.item.Any()) ingredients.Add(s);
        }
        //조합슬롯에 올라간 아이템들을, itemId 순으로 정렬.
        ingredients.Sort((a, b) =>
        {
            if (a.itemId > b.itemId) return -1;
            else if (a.itemId == b.itemId) return 0;
            else return 1;
        }
        );
    }

    public void AcquireItem(GameObject newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].AddCount(newItem))
                return;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].item.Any())
            {
                slots[i].NewSlot(newItem);
                return;
            }
        }
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.Any() && slots[i].RemoveItem(item))
            {
                return;
            }
        }
    }

    public GameObject FindAnyCubeByColor(Color color)
    {
        foreach (var slot in slots)
        {
            if (slot.item.Any())
            {
                var cube = slot.item[0].GetComponent<Cube>();
                if (cube && cube.cubeData.color.IsSameRGB(color))
                {
                    return slot.item[0];
                }
            }
        }
        return null;
    }
}
