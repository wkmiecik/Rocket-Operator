using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class FreezeButton : MonoBehaviour, IPointerDownHandler
{
    [Inject]
    private GuiManager guiManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Resume();
        guiManager.SetBlur(false);
        gameObject.SetActive(false);
    }
}
