using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    RaycastHit hit;
    const int CubeLayer = 9;
    public float baseDamage = 10;

    public override void Attack(float baseDamage)
    {

    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Sword;
    }

    
}
