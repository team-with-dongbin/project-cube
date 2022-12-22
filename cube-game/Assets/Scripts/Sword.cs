using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    const int CubeLayer = 9;
    public float baseDamage = 10;

    public override void Attack(float baseDamage)
    {

    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Sword;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CubeLayer)
        {
            Debug.Log("Attack Cube !!");
        }
    }
}
