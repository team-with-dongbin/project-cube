using UnityEngine;
using UnityEngine.InputSystem;
using CubeGame;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;

    [SerializeField]
    Animator animator;

    [SerializeField]
    CameraController cameraController;

    public float moveSpeed = 4f;
    internal Vector3 moveDirection = Vector3.zero;

    public float sprintSppedRatio = 1.5f;
    internal bool isSprinting = false;

    public float jumpForce = 4f;

    public float mouseSensitivity = 1000F;
    internal float mouseXDelta = 0f;
    internal float mouseYDelta = 0f;

    float XRotationOfHead = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (cameraController == null)
        {
            cameraController = GetComponent<CameraController>();
        }
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 moveVector = rigidBody.transform.forward * moveDirection.z;
        moveVector += Quaternion.Euler(0, 90f, 0f) * rigidBody.transform.forward * moveDirection.x;
        rigidBody.MovePosition(rigidBody.position + moveVector * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        cameraController.RotateX(mouseYDelta * mouseSensitivity);
        rigidBody.transform.Rotate(Vector3.up * mouseXDelta * mouseSensitivity);
    }

    internal void Jump()
    {
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    internal void ChangeCameraViewToFirst()
    {
        cameraController.ChangeCameraViewToFirst();
    }

    internal void ChangeCameraViewToThird()
    {
        cameraController.ChangeCameraViewToThird();
    }
}
