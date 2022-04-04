using UnityEngine;

[CreateAssetMenu(fileName = "GuiSettings", menuName = "Scriptable Objects/Gui Settings")]
public class GuiSettings : ScriptableObject
{
    [Header("Colors")]
    public Color GoldColor;

    [Header("Button Colors")]
    public Color ButtonDefaultColor;
    public Color ButtonPressedColor;

    [Header("Text Colors")]
    public Color TextDefaultColor;
    public Color TextFailColor;
}