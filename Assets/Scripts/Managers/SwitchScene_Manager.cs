using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene_Manager : MonoBehaviour
{
    public static SwitchScene_Manager instance;

    public enum Scenes
    {
        TitleScreen_Scene,
        MainGameplay_Scene,
        Gacha_Scene,
        Collection_Scene
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Load_GameplayScene()
    {
        AudioManager.instance.ChangeSceneWithMusic(Scenes.MainGameplay_Scene, "Main_Music");
    }

    public void Load_GachaScene()
    {
        AudioManager.instance.ChangeSceneWithMusic(Scenes.Gacha_Scene, "Gacha_Music");
    }

    public void Load_TitleScene()
    {
        AudioManager.instance.ChangeSceneWithMusic(Scenes.TitleScreen_Scene, "Main_Music");
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
