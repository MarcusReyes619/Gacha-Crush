using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
    public static TogglePauseMenu instance;

    [SerializeField] GameObject PauseMenu_UI;

    [Header("Audio")]
    [SerializeField] private AudioSource Pause_SFX;
    [SerializeField] private AudioSource Resume_SFX;
    [SerializeField] private AudioSource OpenMenu_SFX;
    [SerializeField] private AudioSource Exit_SFX;

    private void Start()
    {
        PauseMenu_UI.SetActive(false);

        // Force UI elements to be interactable after returning to the main menu
        EventSystem.current.SetSelectedGameObject(null);
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
        SwitchScene_Manager.instance.Load_CollectionScene();
    }

    public void Load_MainMenu()
    {
        Exit_SFX.Play();
        StartCoroutine(LoadSceneAsync("TitleScreen_Scene"));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
