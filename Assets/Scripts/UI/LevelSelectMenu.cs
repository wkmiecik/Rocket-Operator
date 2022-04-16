using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelSelectMenu : MonoBehaviour
{
    // Levels
    [Header("Levels")]
    public GridLayoutGroup levelsGrid;
    public GameObject levelUIPrefab;

    // Pages
    public GameObject nextLevelButton;
    public GameObject prevLevelButton;
    private int levelCount;
    private int pagesCount;
    private int currentPage;

    [Inject]
    private LevelManager levelManager;


    private void OnEnable()
    {
        levelCount = levelManager.levels.Length;
        pagesCount = Mathf.CeilToInt(levelCount / 8f);
        currentPage = 1;

        SelectLevelLoad();
    }

    public void NextPageLoad()
    {
        currentPage++;
        SelectLevelLoad();
    }

    public void PrevPageLoad()
    {
        currentPage--;
        SelectLevelLoad();
    }

    public void SelectLevelLoad()
    {
        // Clear
        foreach (Transform child in levelsGrid.transform)
        {
            Destroy(child.gameObject);
        }
        nextLevelButton.SetActive(true);
        prevLevelButton.SetActive(true);

        // Check if we are on first or last page and show correct buttons
        if (currentPage == 1)
        {
            prevLevelButton.SetActive(false);
        }
        if (currentPage == pagesCount)
        {
            nextLevelButton.SetActive(false);
        }

        // Fill
        int iStart = (currentPage - 1) * 8;
        int iEnd = Mathf.Min( iStart + 8, levelCount );
        for (int i = iStart; i < iEnd; i++)
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
