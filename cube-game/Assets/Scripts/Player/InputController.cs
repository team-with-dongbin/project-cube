using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    WeaponController weaponController;

    [SerializeField]
    Weapon basicWeapon1;

    [SerializeField]
    Weapon basicWeapon2;

    public float mouseSensitivity = 1000F;

    void Start()
    {
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        if (weaponController == null)
        {
            weaponController = GetComponent<WeaponController>();
            weaponController.SetWeapon(basicWeapon1);
        }
    }

    void Update()
    {
        playerController.mouseXDelta = Input.GetAxis("Mouse X") * Time.deltaTime;
        playerController.mouseYDelta = Input.GetAxis("Mouse Y") * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            weaponController.SetWeapon(basicWeapon1);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            weaponController.SetWeapon(basicWeapon2);
        }
    }

    void OnAttackInput()
    {
        weaponController.Attack();
    }

    void OnReloadInput()
    {
        weaponController.Reload();
    }

    void OnMoveInput(InputValue value)
    {
        playerController.moveDirection = value.Get<Vector2>();
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
        else
        {
            playerController.isSprinting = false;
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
