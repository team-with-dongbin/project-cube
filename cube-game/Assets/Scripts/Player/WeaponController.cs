using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    AnimationController animationController;
    Weapon weapon;

    void Start()
    {
        if (animationController == null)
        {
            animationController = GetComponent<AnimationController>();
        }
    }

    void Update()
    {

    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon?.gameObject.SetActive(false);
        this.weapon = weapon;

        this.weapon.gameObject.SetActive(true);
        animationController.SetWeaponType(weapon.GetWeaponType());
    }

    public void Attack()
    {
        weapon.Attack(1.0f);
        animationController.Attack();
    }

    public void Reload()
    {
        if (weapon is IReloadable)
        {
            var reloadableWeapon = (weapon as IReloadable);
            reloadableWeapon.Reload();
            animationController.Reload();
        }
    }
}
