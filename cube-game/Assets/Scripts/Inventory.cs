using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject Content;
    [SerializeField]
    private GameObject Slot;

    private bool activeInventory = false;
    private List<Slot> slot;

    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
        slot = new List<Slot>();
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
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void AcquireItem(Item newItem)
    {
        for(int i = 0; i < slot.Count; i++)
        {
            if(slot[i].AddItem(newItem))
                return;
        }
        Slot newSlot = Instantiate(Slot).GetComponent<Slot>();
        newSlot.NewSlot(newItem);
        slot.Add(newSlot);
        newSlot.transform.SetParent(Content.transform);
        newSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}