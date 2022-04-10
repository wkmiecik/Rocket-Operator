using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum LevelLockState {
        Locked = 0,
        Unlocked = 1,
        Golden = 2
    }


    // Array of properties for all levels
    [SerializeField] private LevelsProperties levelsProperties;
    [HideInInspector] public LevelsProperties.Properties[] levels { get => levelsProperties.levels; }


    private int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    

    public int GetCurrentLevelGoldTime()
    {
        return levels[GetCurrentLevel()-1].goldTimeLimit;
    }


    public void SetLevelLockState(int level, LevelLockState lockState)
    {
        PlayerPrefs.SetInt("Level" + level, (int)lockState);
    }
    public void SetLevelLockState(LevelLockState lockState)
    {
        PlayerPrefs.SetInt("Level" + GetCurrentLevel(), (int)lockState);
    }

    public LevelLockState GetLevelLockState(int level)
    {
        return (LevelLockState)PlayerPrefs.GetInt("Level" + level, level==1 ? 1 : 0);
    }


    public void CompleteLevel(LevelLockState completeState)
    {
        LevelLockState currectLevelState = GetLevelLockState(GetCurrentLevel());

        // Set correct completed state
        if (completeState == LevelLockState.Golden || currectLevelState == LevelLockState.Golden)
        {
            SetLevelLockState(GetCurrentLevel(), LevelLockState.Golden);
        } else
        {
            SetLevelLockState(GetCurrentLevel(), LevelLockState.Unlocked);
        }

        // Unlock next level if not yet unlocked or set completed if last level finished
        if (PlayerPrefs.GetInt("Level" + (GetCurrentLevel() + 1), 0) == 0)
        {
            SetLevelLockState(GetCurrentLevel() + 1, LevelLockState.Unlocked);
            if (GetCurrentLevel() + 2 == SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.SetString("LastUnlocked", "Completed");
            } 
            else
            {
                PlayerPrefs.SetString("LastUnlocked", "Level" + (GetCurrentLevel() + 1));
            }
        }
    }
}
