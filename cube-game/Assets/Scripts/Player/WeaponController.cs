using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    StatusController statusController;

    [SerializeField]
    AnimationController animationController;
    Weapon weapon;

    void Start()
    {
        if (statusController == null)
        {
            statusController = GetComponent<StatusController>();
        }

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
        weapon.Attack(statusController.nowStatus.attackPower);
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
