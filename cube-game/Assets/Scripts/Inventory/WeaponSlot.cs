using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class WeaponSlot : Slot
{
    [SerializeField]
    private WeaponType weaponType;
    void Start()
    {
        
    }

    protected override void ChangeSlot()
    {
        Slot dragSlot = DragSlot.instance.dragSlot;
        if (!dragSlot.item.Any()) return;
        Weapon weapon = dragSlot.item[0].GetComponent<Weapon>();
        if (weapon != null && weapon.GetWeaponType() == weaponType)
            base.ChangeSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
