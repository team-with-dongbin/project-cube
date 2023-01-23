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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slots = itemSlots.GetComponentsInChildren<Slot>();
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

    public Dictionary<int, int> GetIngredients()
    {
        Dictionary<int, int> ingredients = new();
        foreach (Slot s in CombinationSlots.GetComponentsInChildren<Slot>())
            if (s.item.Any()) ingredients.Add(s.itemId, s.item.Count);
        return ingredients;
    }
    void Combination()
    {
        GameObject result = CombinationDictionary.instance.TryCombination(GetIngredients());
        if (result != null) CombinationResultSlot = result;
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
