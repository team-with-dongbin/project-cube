using UnityEngine;
using UnityEngine.InputSystem;
using CubeGame;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerRigidbody;

    [SerializeField]
    Animator playerAnimator;

    public float rotateSpeed = 180f;
    public float moveSpeed = 4f;
    public float jumpForce = 3f;

    Vector2 moveInputValue = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void OnMoveInput(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    void OnJumpInput()
    {
        if (Utils.IsGrounded(playerRigidbody.transform.position))
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector3 moveVector = playerRigidbody.transform.forward * moveInputValue.y;
        moveVector += Quaternion.Euler(0, 90f, 0f) * playerRigidbody.transform.forward * moveInputValue.x;
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
