using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

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
