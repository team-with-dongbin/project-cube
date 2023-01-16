using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor Instance;

    void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Inventory.instance.activeInventory)
        {
            // Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            // Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
