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
    [SerializeField]
    private Sprite defaultSprite;
    void Start()
    {
    }

    private void OnEnable()
    {
        itemImage.sprite = defaultSprite;
        SetAlpha(1f);
    }

    protected override bool Validate(ItemData itemData)
    {
        if ((itemData as WeaponData)?.weaponType != weaponType)
            return false;
        if ((DragSlot.instance.dragSlot.item[0]?.GetComponent<Item>().data as WeaponData)?.weaponType != weaponType)
            return false;
        return true;
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        itemImage.sprite = defaultSprite;
        SetAlpha(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
