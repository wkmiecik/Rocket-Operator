using UnityEngine;
using DG.Tweening;

public class MoveBetweenPositions : MonoBehaviour
{
    [Header("Positions")]
    public Vector3 position1;
    public Vector3 position2;

    [Header("Movement")]
    public float duration;
    public float delayStart;
    public float delayEnd;
    public Ease easeStart;
    public Ease easeEnd;

    private Sequence sequence;


    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence
            .AppendInterval(delayStart)
            .Append(transform.DOMove(position2, duration).SetEase(easeStart))
            .AppendInterval(delayEnd)
            .Append(transform.DOMove(position1, duration).SetEase(easeEnd))
            .SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
