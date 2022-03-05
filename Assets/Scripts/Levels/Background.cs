using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Background : MonoBehaviour
{
    Image[] backgrounds;

    Color fadedOut = new Color(0.93333f, 0.93333f, 0.93333f, 0);

    public float duration = 2;

    private void Start()
    {
        backgrounds = GetComponentsInChildren<Image>();

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].DOColor(fadedOut, duration)
                .SetDelay(i * duration)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
