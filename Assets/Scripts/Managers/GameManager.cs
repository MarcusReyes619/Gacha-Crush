using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
  
    [Header("Game State")]
    [SerializeField] private bool IsGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager
        }
    }

    public void PauseGame(bool pauseGame)
    {
        IsGamePaused = pauseGame;
        Time.timeScale = IsGamePaused ? 0 : 1;
        Debug.Log("Game " + (IsGamePaused ? "Paused" : "Resumed"));
    }

    public void Load_GameplayScene()
    {
        SceneManager.LoadScene("MainGameplay_Scene");
    }

    public void Load_GachaScene()
    {
        SceneManager.LoadScene("Gacha_Scene");
    }

    public void Load_TitleScene()
    {
        SceneManager.LoadScene("TitleScreen_Scene");
    }

    public void End_BTN()
    {
        Application.Quit();
    }
}
