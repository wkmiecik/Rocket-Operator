using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    private TMP_Text fpsText;

    void Start()
    {
        fpsText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        fpsText.text = Mathf.Round(1 / Time.unscaledDeltaTime).ToString();
    }
}
