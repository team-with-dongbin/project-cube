using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine.Events;

public class InventoryStatusHandler : MonoBehaviour
{
    List<Slot> _targetSlots;
    PlayerStatus _inventoryStatus;
    public UnityEvent<PlayerStatus> OnInventoryStatusChanged = new UnityEvent<PlayerStatus>();

    public void Start()
    {
        _targetSlots = new List<Slot>(gameObject.GetComponentsInChildren<Slot>(true));
        var excluded = gameObject.GetComponentInChildren<CombinationResultSlot>();

        if (excluded != null)
        {
            _targetSlots.Remove(excluded);
        }
    }

    public void RenewInventoryStatus()
    {
        PlayerStatus result = new PlayerStatus();
        if (_targetSlots != null)
        {
            foreach (var slot in _targetSlots)
            {
                foreach (var item in slot.item)
                {
                    var data = item.GetComponent<Item>().data;
                    result += data.GetField<PlayerStatus>(ItemData.FieldNames.statusChanging);
                }
            }
        }

        if (_inventoryStatus == null || _inventoryStatus != result)
        {
            _inventoryStatus = result;
            OnInventoryStatusChanged.Invoke(_inventoryStatus);
        }
    }
}
