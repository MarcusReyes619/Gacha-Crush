using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene_Manager : MonoBehaviour
{
    public static SwitchScene_Manager instance;

    [Header("Scenes")]
    [SerializeField] private string[] Scenes; // StartMenu, Lobby, Main Gameplay, Gacha, Gacha Gallery

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
    
    public void Load_CollectionScene()
    {
        SceneManager.LoadScene("Collection_Scene");
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
