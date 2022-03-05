using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
