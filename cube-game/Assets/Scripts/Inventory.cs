using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject Content;
    
    private bool activeInventory = false;
    private Slot[] slot;
    GameObject nearObject;

    void Start()
    {
        slot = Content.GetComponentsInChildren<Slot>();
        inventoryPanel.SetActive(activeInventory);
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
}
