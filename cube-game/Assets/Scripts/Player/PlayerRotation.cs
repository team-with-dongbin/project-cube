using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    public Transform playerBody;

    [SerializeField]
    public Transform playerHead;
    public float mouseSensitivity = 1000F;

    float ZRotationOfHead = 0f;
    bool isMouseInitialized = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseXDelta = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseYDelta = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // TODO(rdd6584) : 코루틴으로 대체. 1회만 체크하기.
        if (isMouseInitialized == false && mouseYDelta > 1.0f)
        {
            isMouseInitialized = true;
            return;
        }

        ZRotationOfHead += mouseYDelta;
        ZRotationOfHead = Mathf.Clamp(ZRotationOfHead, -90f, 90f);

        playerHead.transform.localRotation = Quaternion.Euler(0f, 0f, ZRotationOfHead);
        playerBody.Rotate(Vector3.up * mouseXDelta);
    }
}
