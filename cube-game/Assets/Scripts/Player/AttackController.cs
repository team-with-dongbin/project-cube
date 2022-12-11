using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    AnimationController animationController;

    void Start()
    {
        if (animationController == null)
        {
            animationController = GetComponent<AnimationController>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animationController.Attack();
    }
}
