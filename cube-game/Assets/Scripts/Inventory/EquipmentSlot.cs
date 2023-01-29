using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class EquipmentSlot : Slot
{
    [SerializeField]
    private EquipmentType equipmentType;
    void Start()
    {
        
    }

    protected override void ChangeSlot()
    {
        Slot dragSlot = DragSlot.instance.dragSlot;
        if (!dragSlot.item.Any()) return;
        //WeaponSlot 보고 똑같이 고쳐야됨.
        EquipmentData equipmentData = dragSlot.item[0].GetComponent<EquipmentData>();
        if (equipmentData != null && equipmentData.equipmentType == equipmentType)
            base.ChangeSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
