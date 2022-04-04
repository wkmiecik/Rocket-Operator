using UnityEngine;

public class SetSameWidth : MonoBehaviour
{
    RectTransform thisRect;
    public RectTransform constraintWith;

    private void Start() {
        thisRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        thisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, constraintWith.rect.width);
    }
}
