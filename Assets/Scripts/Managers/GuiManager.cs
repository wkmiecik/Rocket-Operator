using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class GuiManager : MonoBehaviour
{
    // Settings
    public GuiSettings settings;

    // Cameras
    private Camera mainCamera;
    private Blur mainCameraBlur;

    // Texts
    private TMP_Text winMenuText;
    private TMP_Text speedIndicatorText;
    private TMP_Text goldTimerText;

    // Menus
    private GameObject winMenu;
    private GameObject crashMenu;
    private GameObject pauseMenu;

    // Input
    [Inject]
    private InputManager inputManager;
    private Image upButtonImage;
    private Image leftButtonImage;
    private Image rightButtonImage;


    private void Awake()
    {
        // Find objects and setup
        mainCamera = Camera.main;
        mainCameraBlur = mainCamera.GetComponent<Blur>();

        winMenuText = GameObject.FindWithTag("WinMenuText").GetComponent<TMP_Text>();
        speedIndicatorText = GameObject.FindWithTag("SpeedIndicator").GetComponent<TMP_Text>();
        goldTimerText = GameObject.FindWithTag("GoldTimer").GetComponent<TMP_Text>();

        upButtonImage = GameObject.FindWithTag("UpButton").GetComponent<Image>();
        leftButtonImage = GameObject.FindWithTag("LeftButton").GetComponent<Image>();
        rightButtonImage = GameObject.FindWithTag("RightButton").GetComponent<Image>();

        winMenu = GameObject.FindWithTag("WinMenu");
        winMenu.SetActive(false);

        crashMenu = GameObject.FindWithTag("CrashMenu");
        crashMenu.SetActive(false);

        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Update button colors when pressed
        upButtonImage.color = inputManager.upPressed ? settings.ButtonPressedColor : settings.ButtonDefaultColor;
        leftButtonImage.color = inputManager.leftPressed ? settings.ButtonPressedColor : settings.ButtonDefaultColor;
        rightButtonImage.color = inputManager.rightPressed ? settings.ButtonPressedColor : settings.ButtonDefaultColor;
    }


    public void SetSpeedIndicator(float speed, float maxSpeed)
    {
        speedIndicatorText.color = speed > maxSpeed ? settings.TextFailColor : settings.TextDefaultColor;
        speedIndicatorText.text = speed.ToString("F1");
    }

    public void SetGoldTimer(float time)
    {
        if (time <= 0)
        {
            goldTimerText.color = settings.TextFailColor;
            goldTimerText.text = "Time to gold: 0.0";
        } 
        else
        {
            goldTimerText.text = "Time to gold: " + time.ToString("F1");
        }
    }


    public void ShowWinMenu(bool isGoldWin)
    {
        winMenu.SetActive(true);
        if (isGoldWin) winMenuText.color = settings.GoldColor;
        SetBlur(true);
    }

    public void ShowCrashMenu()
    {
        crashMenu.SetActive(true);
        SetBlur(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        SetBlur(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        SetBlur(false);
    }


    public void SetBlur(bool on)
    {
        if (on)
        {
            mainCameraBlur.enabled = true;
            mainCameraBlur.BlurIn(); 
        }
        else mainCameraBlur.BlurOut();
    }
}
