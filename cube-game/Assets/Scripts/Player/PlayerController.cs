using UnityEngine;
using UnityEngine.InputSystem;
using CubeGame;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;

    [SerializeField]
    AnimationController animationController;

    [SerializeField]
    CameraController cameraController;

    [SerializeField]
    private Inventory inventory;
    GameObject nearObject = null;

    public float moveSpeed = 4f;
    internal Vector2 moveDirection = Vector2.zero;

    public float sprintSppedRatio = 1.5f;
    internal bool isSprinting = false;

    public float jumpForce = 4f;

    public float mouseSensitivity = 1000F;
    internal float mouseXDelta = 0f;
    internal float mouseYDelta = 0f;

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
        if (animationController == null)
        {
            animationController = GetComponent<AnimationController>();
        }
        if (cameraController == null)
        {
            cameraController = GetComponent<CameraController>();
        }
    }

    void Update()
    {
<<<<<<< Updated upstream
        Move();
        Rotate();
        PickUp();
=======
            Rotate();
            Move();
            PickUp();
        
>>>>>>> Stashed changes
    }

    void Move()
    {
        Vector2 inputVector = moveDirection;
        if (isSprinting)
        {
            inputVector *= sprintSppedRatio;
        }

        Vector3 moveVector = rigidBody.transform.forward * inputVector.y;
        moveVector += Quaternion.Euler(0, 90f, 0f) * rigidBody.transform.forward * inputVector.x;

        rigidBody.MovePosition(rigidBody.position + moveVector * moveSpeed * Time.deltaTime);
        animationController.SetMoveVector(inputVector);
    }

    void Rotate()
    {
        cameraController.RotateX(mouseYDelta * mouseSensitivity);
        rigidBody.transform.Rotate(Vector3.up * mouseXDelta * mouseSensitivity);
    }

    internal void Jump()
    {
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animationController.Jump();
    }

    internal void ChangeCameraViewToFirst()
    {
        cameraController.ChangeCameraViewToFirst();
    }

    internal void ChangeCameraViewToThird()
    {
        cameraController.ChangeCameraViewToThird();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Item")
        {
            //Debug.Log(other.tag + " �ݱ�");
            nearObject = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Item")
            nearObject = null;
    }

    public void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.Z) && nearObject != null)
            GetItem(nearObject);
    }

    private void GetItem(GameObject item){
        inventory.AcquireItem(item);
        nearObject = null;
        item.SetActive(false);
    }
}
