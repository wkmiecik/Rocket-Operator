using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinRestartButton : MonoBehaviour
{
    Button button;

    [Inject]
    private AdsManager adsManager;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => {
            adsManager.ShowAd();
            GameManager.ReloadScene();
        });
    }
}
