using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private PlayerController sonicController;

    void Start()
    {
        sonicController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        float deltaX = endTouchPosition.x - startTouchPosition.x;
        float deltaY = endTouchPosition.y - startTouchPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0) sonicController.MoveRight();
            else sonicController.MoveLeft();
        }
        else
        {
            if (deltaY > 0) sonicController.Jump();
            else sonicController.Slide();
        }
    }
}
