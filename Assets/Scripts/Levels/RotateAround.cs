using UnityEngine;
using DG.Tweening;

public class RotateAround : MonoBehaviour
{
    [Header("Movement")]
    public float duration;
    public Ease ease;

    private Sequence sequence;


    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence
            .Append(transform.DORotate(Vector3.back * 360, duration)
            .SetEase(ease))
            .SetRelative()
            .SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
