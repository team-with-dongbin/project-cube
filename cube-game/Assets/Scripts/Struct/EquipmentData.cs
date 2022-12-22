using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EquimentData", fileName = "Equiment Data")]
public class EquipmentData : ItemData
{
    public PlayerStatus statusChanging;
    public float partDefense = 0f;
    public float durability = 100f;
    public EquipmentType equipmentType = EquipmentType.Default;
}
