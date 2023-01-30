using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public UnityEvent<PlayerStatus> OnInventoryStatusChanged;
    InventoryStatusHandler _statusHandler;

    void Awake()
    {
        instance = this;
        _statusHandler = gameObject.AddComponent<InventoryStatusHandler>();
        OnInventoryStatusChanged = _statusHandler.OnInventoryStatusChanged;
    }

    void Start()
    {
        slots = itemSlots.GetComponentsInChildren<Slot>();

        //테스트용으로 아이템 할당.
        for (int i = 1000; i <= 1007; i++)
            for (int j = 0; j < 10; j++)
            {
                GameObject item = ItemDictionary.instance.InstantiateWithData(i);
                item.GetComponent<Item>().Drop();
                item.SetActive(false);
                AcquireItem(item);
            }
        // inventoryWindow.SetActive(activeInventory);
    }

    void Update()
    {
        WindowControl();
        if (activeInventory)
            TryCombination();

        // FixMe(rdd6584) : Do not renew on Update()
        _statusHandler.RenewInventoryStatus();
    }

    void PutBackItems()
    {
        CombinationSlot[] s = combinationSlots.GetComponentsInChildren<CombinationSlot>();
        for (int i = 0; i < s.Length; i++)
        {
            foreach (GameObject item in s[i].item)
                AcquireItem(item);
            s[i].ClearSlot();
        }
    }

    (Dictionary<int, int> ingredients, (int, int) result) cur;
    public (Dictionary<int, int>, (int, int)) TryCombination()
    {
        Dictionary<int, int> ingredients = new();
        foreach (Slot s in combinationSlots.GetComponentsInChildren<Slot>())
            if (s.item.Any()) ingredients.Add(s.itemId, s.item.Count);
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
                cur = (combination, output);
                //랜덤아이템 예외처리
                if (output.Item1 == -1000) cur.result.Item1 = UnityEngine.Random.Range(1001, 1008);
                return cur;
            }
        }
        return cur = (null, (-1, -1));
    }

    public void DoCombination()
    {
        if (cur.ingredients == null) return;
        for (int i = 0; i < cur.result.Item2; i++)
        {
            GameObject item = ItemDictionary.instance.InstantiateWithData(cur.result.Item1);
            item.GetComponent<Item>().Drop();
            item.SetActive(false);
            AcquireItem(item);
        }

        foreach ((int itemId, int count) in cur.ingredients)
        {
            //조합슬롯에있는 조합식만큼의 아이템 삭제.
            for (int i = 0; i < count; i++)
            {
                RemoveItemInCombinationSlot(itemId);
            }
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

    public void RemoveItemInCombinationSlot(int itemId)
    {
        CombinationSlot[] s = combinationSlots.GetComponentsInChildren<CombinationSlot>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].item.Any() && s[i].itemId == itemId)
            {
                s[i].RemoveItem(s[i].item[0]);
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
