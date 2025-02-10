using UnityEngine;

public class TogglePauseMenu : MonoBehaviour
{
    public static TogglePauseMenu instance;

    [SerializeField] GameObject PauseMenu_UI;

    private void Start()
    {
        PauseMenu_UI.SetActive(false);
    }

    public void Open_PauseMenu()
    {
        PauseMenu_UI.SetActive(true);
        GameManager.instance.PauseGame(true);
    }

    public void Close_PauseMenu()
    {
        PauseMenu_UI.SetActive(false);
        GameManager.instance.PauseGame(false);
    }
}
