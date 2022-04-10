using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResetSaveButton : MonoBehaviour
{
    // This button
    private Button thisResetButton;

    // Image to fade in and out after resetting
    [SerializeField] private Image resetFadeImage;

    // Objects to disable and enable after fading
    [SerializeField] private GameObject[] disableList;
    [SerializeField] private GameObject[] enableList;

    private void Start()
    {
        thisResetButton = GetComponent<Button>();
        thisResetButton.onClick.AddListener(OnResetButtonClicked);
    }

    private void OnResetButtonClicked()
    {
        // Actually reset save
        GameManager.ResetSave();

        // Enable raycast target so player can't click buttons while fading
        resetFadeImage.raycastTarget = false;

        // Fade in
        resetFadeImage.gameObject.SetActive(true);
        resetFadeImage.DOColor(new Color(0.97169f, 0.97169f, 0.97169f, 1), 1)
            .SetEase(Ease.OutExpo)
            .OnComplete(OnFadeInFinished);
    }

    private void OnFadeInFinished()
    {
        // Disable and enable all needed objects
        foreach (var item in disableList) item.SetActive(false);
        foreach (var item in enableList) item.SetActive(true);

        // Disable raycast target so player can already click buttons
        resetFadeImage.raycastTarget = false;

        // Fade out
        resetFadeImage.DOColor(new Color(0.97169f, 0.97169f, 0.97169f, 0), 1)
            .SetEase(Ease.InExpo)
            .OnComplete( () => {
                resetFadeImage.gameObject.SetActive(false);
            });
    }
}
