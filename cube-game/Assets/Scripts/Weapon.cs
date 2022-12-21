using System.Collections;
using UnityEngine;

public abstract class Weapon : Item
{
    public abstract void Attack(float baseDamage);
    public abstract WeaponType GetWeaponType();
}
