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
    [SerializeField]
    private Sprite defaultSprite;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        itemImage.sprite = defaultSprite;
        itemImage.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        SetAlpha(1f);
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        itemImage.sprite = defaultSprite;
        SetAlpha(1f);
    }

    protected override bool Validate(ItemData itemData)
    {
        if ((itemData as EquipmentData)?.equipmentType != equipmentType)
            return false;
        if ((DragSlot.instance.dragSlot.item[0]?.GetComponent<Item>().data as EquipmentData)?.equipmentType != equipmentType)
            return false;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
