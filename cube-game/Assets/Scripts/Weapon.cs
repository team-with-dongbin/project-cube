using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public abstract class Weapon : Item
{
    protected override void OnEnable()
    {
        base.OnEnable();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
    }
    public abstract void Attack(float basePower);
    public abstract float CalculateDamage(float basePower);
    public abstract WeaponType GetWeaponType();
}
