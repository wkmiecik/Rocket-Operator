using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    // Rocket
    private Rigidbody2D rb;
    private ParticleSystem smokeFX;

    // Movement
    [SerializeField] private float accelerationForce;
    [SerializeField] private float rotationForce;
    [SerializeField] private float crashSpeedLimit;

    // Landing/Crashing
    private const string landPlatformTag = "LandPlatform";
    private const string startPlatformTag = "StartPlatform";
    private bool onLandingPlatform;
    private bool crashed;
    private float stableLandingTimer;

    // Gold
    private float goldTimer;


    [Inject]
    private InputManager inputManager;
    [Inject]
    private GuiManager guiManager;
    [Inject]
    private LevelManager levelManager;


    private void Start()
    {
        // Find objects
        rb = gameObject.GetComponent<Rigidbody2D>();
        smokeFX = gameObject.GetComponentInChildren<ParticleSystem>(true);

        // Set start parameters
        goldTimer = levelManager.GetCurrentLevelGoldTime();
        guiManager.SetGoldTimer(goldTimer);
    }


    private void Update()
    {
        // Check if crashed
        if (crashed)
        {
            guiManager.ShowCrashMenu();
            Time.timeScale = 0;
            enabled = false;
            return;
        }

        // Check if landed
        if (onLandingPlatform && (transform.rotation.eulerAngles.z < 0.5 || transform.rotation.eulerAngles.z > 359.5))
        {
            stableLandingTimer -= Time.deltaTime;

            if (stableLandingTimer <= 0)
            {
                // Stop timer
                Time.timeScale = 0;
                onLandingPlatform = false;

                // Check if gold and show win panel
                guiManager.ShowWinMenu(goldTimer > 0);

                // Set correct level states
                levelManager.CompleteLevel(goldTimer > 0 ? LevelManager.LevelLockState.Golden : LevelManager.LevelLockState.Unlocked);
                enabled = false;
            }
        }
        else
        {
            stableLandingTimer = 0.4f;
        }

        // Update speed
        guiManager.SetSpeedIndicator(rb.velocity.magnitude, crashSpeedLimit);

        // Update gold timer
        goldTimer -= Time.deltaTime;
        guiManager.SetGoldTimer(goldTimer);
    }
    

    private void FixedUpdate()
    {
        if (inputManager.upPressed) 
        {
            rb.AddForce(accelerationForce * transform.up);
            smokeFX.Emit(4);
        }
        if (inputManager.leftPressed) rb.AddTorque(rotationForce);
        if (inputManager.rightPressed) rb.AddTorque(-rotationForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = collision.relativeVelocity.magnitude;

        if (speed > crashSpeedLimit)
        {
            crashed = true;
            guiManager.SetSpeedIndicator(speed, crashSpeedLimit);
        }
        if (!collision.gameObject.CompareTag(landPlatformTag) && !collision.gameObject.CompareTag(startPlatformTag))
        {
            crashed = true;
        }
        if (collision.gameObject.CompareTag(landPlatformTag))
        {
            onLandingPlatform = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        onLandingPlatform = false;
    }
}
