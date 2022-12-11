using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CubeGame;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    public float mouseSensitivity = 1000F;

    void Start()
    {
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        playerController.mouseXDelta = Input.GetAxis("Mouse X") * Time.deltaTime;
        playerController.mouseYDelta = Input.GetAxis("Mouse Y") * Time.deltaTime;
    }

    void OnMoveInput(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        playerController.moveDirection.x = input.x;
        playerController.moveDirection.z = input.y;
    }

    void OnJumpInput()
    {
        if (Utils.IsGrounded(playerController.transform.position))
        {
            playerController.Jump();
        }
    }

    void OnSprintInput(InputValue value)
    {
        float input = value.Get<float>();
        if (input > 0.5f)
        {
            playerController.isSprinting = true;
        }
    }

    void OnFirstViewCameraInput()
    {
        Debug.Log("[OnFirstViewCameraInput]");
        playerController.ChangeCameraViewToFirst();
    }

    void OnThirdViewCameraInput()
    {
        Debug.Log("[OnThirdViewCameraInput]");
        playerController.ChangeCameraViewToThird();
    }
}
