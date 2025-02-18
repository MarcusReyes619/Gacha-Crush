using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
    public static TogglePauseMenu instance;

    [SerializeField] GameObject PauseMenu_UI;

    [Header("Audio")]
    [SerializeField] private AudioSource Pause_SFX;
    [SerializeField] private AudioSource Resume_SFX;
    [SerializeField] private AudioSource Exit_SFX;

    private void Start()
    {
        PauseMenu_UI.SetActive(false);
    }

    public void Open_PauseMenu()
    {
        PauseMenu_UI.SetActive(true);
        Pause_SFX.Play();

        GameManager.instance.PauseGame(true);
    }

    public void Close_PauseMenu()
    {
        Resume_SFX.Play();

        PauseMenu_UI.SetActive(false);
        GameManager.instance.PauseGame(false);
    }

    public void Load_CollectionScene()
    {
        SceneManager.LoadScene("Collection_Scene");
    }

    public void Load_MainMenu()
    {
        Exit_SFX.Play();

        // Load Lobby Scene
        GameManager.instance.Load_TitleScene();
    }
}
