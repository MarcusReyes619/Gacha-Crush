using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    
   
    [Header("Game State")]
    [SerializeField] private bool IsGamePaused = false;

    [Header("Scenes")]
    [SerializeField] private string[] Scenes; // StartMenu, Lobby, Main Gameplay, Gacha, Gacha Gallery

    private void Awake()
    {
        // Singleton Pattern
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

    public void Start_BTN()
    {
        SceneManager.LoadScene("MainGameplay_Scene");
    }

    public void End_BTN()
    {
        Application.Quit();
    }
}
