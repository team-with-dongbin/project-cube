using System.Collections;
using UnityEngine;
using CubeGame;

public abstract class Weapon : Item
{
    public abstract void Attack(float baseDamage);
    public abstract WeaponType GetWeaponType();
}
