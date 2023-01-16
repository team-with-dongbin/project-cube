using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryWindow;
    [SerializeField]
    private GameObject itemSlot;

    public static Inventory instance;
    public bool activeInventory = false;
    private Slot[] slot;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slot = itemSlot.GetComponentsInChildren<Slot>();
        inventoryWindow.SetActive(activeInventory);
    }

    void Update()
    {
        WindowControl();
    }

    void WindowControl()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryWindow.SetActive(activeInventory);

        }
    }

    public void AcquireItem(GameObject newItem)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].AddCount(newItem))
                return;
        }
        for (int i = 0; i < slot.Length; i++)
        {
            if (!slot[i].item.Any())
            {
                slot[i].NewSlot(newItem);
                return;
            }
        }
    }
}
