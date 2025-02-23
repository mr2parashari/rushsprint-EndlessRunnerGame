using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f; // Distance between lanes
    public float jumpForce = 10f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private int lane = 1; // 0 = left, 1 = center, 2 = right
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Move forward automatically
        moveDirection.z = forwardSpeed;

        // Handle lane switching
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lane > 0)
        {
            lane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && lane < 2)
        {
            lane++;
        }

        float targetX = (lane - 1) * laneDistance;
        moveDirection.x = (targetX - transform.position.x) * 10f; // Smooth transition

        // Handle jumping
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime; // Apply gravity
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    public void MoveLeft()
    {
        if (lane > 0) lane--;
    }

    public void MoveRight()
    {
        if (lane < 2) lane++;
    }

    public void Jump()
    {
        if (controller.isGrounded) moveDirection.y = jumpForce;
    }

    public void Slide()
    {
        // Implement slide animation
    }
}
