using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

#if UNITY_EDITOR
using UnityEngine.InputSystem;
#endif


public class InputManager : MonoBehaviour 
{

    private bool _upPressed;
    public bool upPressed { get => _upPressed; }

    private bool _leftPressed;
    public bool leftPressed { get => _leftPressed; }

    private bool _rightPressed;
    public bool rightPressed { get => _rightPressed; }


    private const string upTag = "UpButton";
    private const string leftTag = "LeftButton";
    private const string rightTag = "RightButton";


    private void Awake() 
    {
        EnhancedTouchSupport.Enable();
    }


    private void Update() 
    {
        // Reset input
        _leftPressed = false;
        _rightPressed = false;
        _upPressed = false;


        #if UNITY_EDITOR
        // WASD INPUT
        if (Keyboard.current.wKey.isPressed) _upPressed = true;
        if (Keyboard.current.aKey.isPressed) _leftPressed = true;
        if (Keyboard.current.dKey.isPressed) _rightPressed = true;
        #endif


        // TOUCH INPUT
        foreach (Touch touch in Touch.activeTouches) 
        {
            Vector2 screenCoords = touch.screenPosition;
            Vector2 worldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
            RaycastHit2D hit = Physics2D.Raycast(worldCoords, Vector2.zero);

            if (hit.collider) {
                if (hit.collider.gameObject.CompareTag(upTag)) _upPressed = true;
                if (hit.collider.gameObject.CompareTag(leftTag)) _leftPressed = true;
                if (hit.collider.gameObject.CompareTag(rightTag)) _rightPressed = true;
            }
        }
    }
}
