using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    void Start()
    {
        // This is set to make sure FPS won't be limited to 30 on some devices
        // Instead it will be locked to screen refresh rate
        Application.targetFrameRate = 1000;
    }

    public static void Pause() 
    {
        Time.timeScale = 0;
    }

    public static void Resume() 
    {
        Time.timeScale = 1;
    }

    public static void LoadScene(string SceneToLoad) 
    {
        Resume();
        if (SceneToLoad == "Menu") SceneManager.LoadScene(SceneToLoad);
        if (PlayerPrefs.GetInt(SceneToLoad) >= 1 || SceneToLoad == "Level1") 
        {
            Pause();
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    public static void ReloadScene() 
    {
        Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadLastLevel() 
    {
        Pause();
        SceneManager.LoadScene(PlayerPrefs.GetString("LastUnlocked", "Level1"));
    }

    public static void LoadNextLevel() 
    {
        Pause();
        SceneManager.LoadScene("Level" + (SceneManager.GetActiveScene().buildIndex + 1));
    }

    public static void Exit()
    {
        Application.Quit();
    }

    public static void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level1", 1);
    }
}
