using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    // UI
    [Header("UI")]
    public RectTransform resetButton;

    // Levels
    [Header("Levels")]
    public GridLayoutGroup levelsGrid;
    public GameObject levelUIPrefab;

    [Inject]
    private LevelManager levelManager;


    public void SelectLevelLoad()
    {
        // Clear
        foreach (Transform child in levelsGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // Fill
        for (int i = 0; i < levelManager.levels.Length; i++)
        {
            // Get all level UI elements
            GameObject level = Instantiate(levelUIPrefab, levelsGrid.transform);

            level.name = "Level" + (i + 1).ToString();
            Button button = level.transform.Find("Button").GetComponent<Button>();
            Image buttonImage = level.transform.Find("Button").GetComponent<Image>();
            GameObject border = level.transform.Find("Border").gameObject;
            GameObject lockpad = level.transform.Find("Lockpad").gameObject;

            // Set them to correct values according to player prefs
            button.onClick.AddListener(() => GameManager.LoadScene(level.name));
            buttonImage.sprite = levelManager.levels[i].icon;
            lockpad.SetActive(levelManager.GetLevelLockState(i+1) == LevelManager.LevelLockState.Locked);
            border.SetActive(levelManager.GetLevelLockState(i+1) == LevelManager.LevelLockState.Golden);
        }
    }
}
