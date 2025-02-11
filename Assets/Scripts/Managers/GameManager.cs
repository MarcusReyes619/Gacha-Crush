using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer { get; private set; }
    public int score;
    [Header("Game State")]
    [SerializeField] private bool IsGamePaused = false;

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
    
    public void AddToTime()
    {

    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

   
}
