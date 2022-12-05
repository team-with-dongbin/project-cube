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
        Rotate();
        Move();
    }


    void OnMoveInput(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    void OnJumpInput()
    {
        if (Utils.IsGrounded(this.transform.position))
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector3 moveDistance =
            moveInputValue.y * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        float turn = moveInputValue.x * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation =
            playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
