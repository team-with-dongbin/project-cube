using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/WeaponData", fileName = "Weapon Data")]
public class WeaponData : ItemData
{
    public float damage = 0;
    public WeaponType weaponType = WeaponType.Default;
}
