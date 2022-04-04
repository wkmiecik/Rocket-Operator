using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

#if UNITY_EDITOR
using UnityEngine.InputSystem;
#endif


public class InputManager : MonoBehaviour 
{
    public bool upPressed { get; private set; }
    public bool leftPressed { get; private set; }
    public bool rightPressed { get; private set; }

    private const string upTag = "UpButton";
    private const string leftTag = "LeftButton";
    private const string rightTag = "RightButton";


    private void Awake() 
    {
        EnhancedTouchSupport.Enable();
    }

    private void Update() 
    {
        // Reset input variables
        upPressed = false;
        leftPressed = false;
        rightPressed = false;


        #if UNITY_EDITOR
        // WASD INPUT
        if (Keyboard.current.wKey.isPressed) upPressed = true;
        if (Keyboard.current.aKey.isPressed) leftPressed = true;
        if (Keyboard.current.dKey.isPressed) rightPressed = true;
        #endif


        // TOUCH INPUT
        foreach (Touch touch in Touch.activeTouches) 
        {
            Vector2 screenCoords = touch.screenPosition;
            Vector2 worldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
            RaycastHit2D hit = Physics2D.Raycast(worldCoords, Vector2.zero);

            if (hit.collider) {
                if (hit.collider.gameObject.CompareTag(upTag)) upPressed = true;
                if (hit.collider.gameObject.CompareTag(leftTag)) leftPressed = true;
                if (hit.collider.gameObject.CompareTag(rightTag)) rightPressed = true;
            }
        }
    }
}
