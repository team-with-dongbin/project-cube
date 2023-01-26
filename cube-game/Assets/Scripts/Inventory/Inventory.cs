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
    private GameObject combinationSlots;
    [SerializeField]
    private GameObject combinationResultSlot;

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

    void PutBackItems()
    {
        CombinationSlot[] s = combinationSlots.GetComponentsInChildren<CombinationSlot>();
        for (int i = 0; i < s.Length; i++)
        {
            //조합 슬롯에 있는 아이템을 인벤토리 슬롯으로 옮기는 작업.
            s[i].ClearSlot();
        }
    }

    public Dictionary<int, int> GetIngredients(GameObject KindOfSlot)
    {
        Dictionary<int, int> ingredients = new();
        foreach (Slot s in KindOfSlot.GetComponentsInChildren<Slot>())
            if (s.item.Any()) ingredients.Add(s.itemId, s.item.Count);
        return ingredients;
    }

    public void TryCombination(Dictionary<int, int> ingredients)
    {
        (int, int) result = (-1, -1);
        //모든 조합식에 대해 체크.
        foreach ((Dictionary<int, int> combination, (int, int) output) in CombinationDictionary.instance.GetCombinationList())
        {
            bool ok = ingredients.Count == combination.Count;
            //해당 조합식의 모든 아이템을 정해진 개수 이상 가지고 있는 지 체크.
            if (!ok) continue;
            foreach ((int item, int need) in combination)
            {
                if (!ingredients.ContainsKey(item) || need > ingredients[item])
                {
                    ok = false;
                    break;
                }
            }
            if (ok)
            {
                result = output;
                break;
            }
        }
        if (result.Item1 == -1000) result.Item1 = UnityEngine.Random.Range(1001, 1008);
        CombinationResultSlot s = combinationResultSlot.GetComponentInChildren<CombinationResultSlot>();
        s.printResult(result.Item1, result.Item2);
    }

    void Update()
    {
        WindowControl();
        if (activeInventory)
        {
            TryCombination(GetIngredients(combinationSlots));
        }
    }

    void WindowControl()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            if (!activeInventory)
            {
                PutBackItems();
            }
            inventoryWindow.SetActive(activeInventory);
        }
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
