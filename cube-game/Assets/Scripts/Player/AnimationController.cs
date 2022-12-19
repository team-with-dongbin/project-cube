using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeGame;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    RuntimeAnimatorController noWeaponAnimator;

    [SerializeField]
    RuntimeAnimatorController pistolAnimator;

    private const float IdleMagnitude = 0f;
    private const float WalkMagnitude = 1f;
    private const float RunningMagnitude = 2f;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void SetWeaponType(WeaponType weaponType)
    {
        if (weaponType == WeaponType.NoWeapon || weaponType == WeaponType.Sword)
        {
            animator.runtimeAnimatorController = noWeaponAnimator;
        }
        else if (weaponType == WeaponType.Pistol)
        {
            animator.runtimeAnimatorController = pistolAnimator;
        }
    }

    public void Attack()
    {
        animator.CrossFadeInFixedTime("Attack", 0.1f);
    }

    public void Jump()
    {
        animator.CrossFadeInFixedTime("Jump", 0.1f);
    }

    public void Reload()
    {
        if (animator.runtimeAnimatorController == pistolAnimator)
        {
            animator.CrossFadeInFixedTime("Reload", 0.1f);
        }
    }

    public void SetMoveVector(Vector2 moveInputVector)
    {
        if (moveInputVector.magnitude > 1.01f)
        {
            animator.SetFloat(AnimatorParameters.MoveMagnitude, RunningMagnitude);
        }
        else if (moveInputVector.magnitude < 0.01f)
        {
            animator.SetFloat(AnimatorParameters.MoveMagnitude, IdleMagnitude);
        }
        else
        {
            animator.SetFloat(AnimatorParameters.MoveMagnitude, WalkMagnitude);
        }
        animator.SetFloat(AnimatorParameters.X, moveInputVector.x);
        animator.SetFloat(AnimatorParameters.Y, moveInputVector.y);
    }

    public static partial class AnimatorParameters
    {
        public static int MoveMagnitude = Animator.StringToHash("MoveMagnitude");
        public static int X = Animator.StringToHash("X");
        public static int Y = Animator.StringToHash("Y");
    }
}
