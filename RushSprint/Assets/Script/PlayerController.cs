using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f; // Distance between lanes
    public float jumpForce = 10f;
    public float gravity = -20f;
    public float slideDuration = 0.8f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private int lane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private bool isJumping = false;
    private bool isSliding = false;
    private bool isBoosted = false;
    private float normalSpeed;

    private Animator anim;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool swipeUp, swipeDown, swipeLeft, swipeRight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        normalSpeed = forwardSpeed; // Store default speed
    }

    void Update()
    {
        moveDirection.z = forwardSpeed;
        DetectSwipe(); // Call swipe detection

        // Lane Switching (Left/Right)
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || swipeLeft) && lane > 0)
        {
            lane--;
            swipeLeft = false; // Reset swipe
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || swipeRight) && lane < 2)
        {
            lane++;
            swipeRight = false; // Reset swipe
        }

        float targetX = (lane - 1) * laneDistance;
        moveDirection.x = (targetX - transform.position.x) * 10f;

        // Jumping (Spacebar or Swipe Up)
        if (controller.isGrounded)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || swipeUp) && !isSliding)
            {
                swipeUp = false;
                StartCoroutine(Jump());
            }
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Sliding (Down Arrow or Swipe Down)
        if ((Input.GetKeyDown(KeyCode.DownArrow) || swipeDown) && controller.isGrounded && !isJumping && !isSliding)
        {
            swipeDown = false;
            StartCoroutine(Slide());
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator Jump()
    {
        isJumping = true;
        anim.SetBool("isJumping", true);
        moveDirection.y = jumpForce;

        yield return new WaitForSeconds(0.2f);
        while (!controller.isGrounded)
        {
            yield return null;
        }

        isJumping = false;
        anim.SetBool("isJumping", false);
        anim.SetTrigger("RunTrigger");
    }

    IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        controller.height = 0.5f;

        yield return new WaitForSeconds(slideDuration);

        controller.height = 2.0f;
        isSliding = false;
        anim.SetBool("isSliding", false);
        anim.SetTrigger("RunTrigger");
    }

    void DetectSwipe()
    {
        swipeUp = swipeDown = swipeLeft = swipeRight = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                Vector2 swipeDelta = touchEndPos - touchStartPos;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal Swipe
                {
                    if (swipeDelta.x > 50) // Swipe Right
                        swipeRight = true;
                    else if (swipeDelta.x < -50) // Swipe Left
                        swipeLeft = true;
                }
                else // Vertical Swipe
                {
                    if (swipeDelta.y > 50) // Swipe Up
                        swipeUp = true;
                    else if (swipeDelta.y < -50) // Swipe Down
                        swipeDown = true;
                }
            }
        }
    }

    // Speed Boost and Temporary Obstacle Collision Disable
    public void ActivateSpeedBoost(float boostAmount, float duration)
    {
        if (!isBoosted)
        {
            isBoosted = true;
            normalSpeed = forwardSpeed; // Store normal speed
            forwardSpeed += boostAmount; // Increase speed

            // Disable collision with obstacles
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);
            Debug.Log("Boost Activated: Collision Disabled with Obstacles");

            StartCoroutine(ResetSpeedAfterDelay(duration));
        }
    }

    IEnumerator ResetSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Restore speed
        forwardSpeed = normalSpeed;
        isBoosted = false;

        // Re-enable collision with obstacles
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
        Debug.Log("Boost Ended: Collision Enabled with Obstacles");
    }
}




/*using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f; // Distance between lanes
    public float jumpForce = 10f;
    public float gravity = -20f;
    public float slideDuration = 0.8f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private int lane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private bool isJumping = false;
    private bool isSliding = false;

    private Animator anim;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool swipeUp, swipeDown, swipeLeft, swipeRight;

    // Boost Speed
    private bool isBoosted = false;
    private float normalSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveDirection.z = forwardSpeed;
        DetectSwipe(); // Call swipe detection

        // Lane Switching (Left/Right)
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || swipeLeft) && lane > 0)
        {
            lane--;
            swipeLeft = false; // Reset swipe
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || swipeRight) && lane < 2)
        {
            lane++;
            swipeRight = false; // Reset swipe
        }

        float targetX = (lane - 1) * laneDistance;
        moveDirection.x = (targetX - transform.position.x) * 10f;

        // Jumping (Spacebar or Swipe Up)
        if (controller.isGrounded)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || swipeUp) && !isSliding)
            {
                swipeUp = false;
                StartCoroutine(Jump());
            }
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Sliding (Down Arrow or Swipe Down)
        if ((Input.GetKeyDown(KeyCode.DownArrow) || swipeDown) && controller.isGrounded && !isJumping && !isSliding)
        {
            swipeDown = false;
            StartCoroutine(Slide());
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator Jump()
    {
        isJumping = true;
        anim.SetBool("isJumping", true);
        moveDirection.y = jumpForce;

        yield return new WaitForSeconds(0.2f);
        while (!controller.isGrounded)
        {
            yield return null;
        }

        isJumping = false;
        anim.SetBool("isJumping", false);
        anim.SetTrigger("RunTrigger");
    }

    IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        controller.height = 0.5f;

        yield return new WaitForSeconds(slideDuration);

        controller.height = 2.0f;
        isSliding = false;
        anim.SetBool("isSliding", false);
        anim.SetTrigger("RunTrigger");
    }

    void DetectSwipe()
    {
        swipeUp = swipeDown = swipeLeft = swipeRight = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                Vector2 swipeDelta = touchEndPos - touchStartPos;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal Swipe
                {
                    if (swipeDelta.x > 50) // Swipe Right
                        swipeRight = true;
                    else if (swipeDelta.x < -50) // Swipe Left
                        swipeLeft = true;
                }
                else // Vertical Swipe
                {
                    if (swipeDelta.y > 50) // Swipe Up
                        swipeUp = true;
                    else if (swipeDelta.y < -50) // Swipe Down
                        swipeDown = true;
                }
            }
        }
    }
    public void ActivateSpeedBoost(float boostAmount, float duration)
    {
        if (!isBoosted)
        {
            isBoosted = true;
            normalSpeed = forwardSpeed; // Store the normal speed
            forwardSpeed += boostAmount; // Increase speed

            StartCoroutine(ResetSpeedAfterDelay(duration));
        }
    }

    IEnumerator ResetSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        forwardSpeed = normalSpeed; // Revert to normal speed
        isBoosted = false;
    }
}*/
