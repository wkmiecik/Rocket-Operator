using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseButton : MonoBehaviour
{
    Button button;

    [Inject]
    private GuiManager guiManager;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => {
            guiManager.ShowPauseMenu();
            GameManager.Pause();
        });
    }
}
