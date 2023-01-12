using System.Collections;
using UnityEngine;

public abstract class Weapon : Item
{
    public abstract void Attack(float basePower);
    public abstract float CalculateDamage(float basePower);
    public abstract WeaponType GetWeaponType();
}
