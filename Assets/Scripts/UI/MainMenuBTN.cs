using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBTN : MonoBehaviour
{
    public GameObject fadeScreen;
    public float fadeDuration = 0.75f;

    private void Start()
    {
        fadeScreen.SetActive(false); // Ensure it's disabled initially
    }

    public void Gem_Click()
    {
        StartCoroutine(FadeToBlackAndLoadScene("MainGameplay_Scene"));
    }

    public void Gacha_Click()
    {
        SwitchScene_Manager.instance.Load_GachaScene();
    }

    private IEnumerator FadeToBlackAndLoadScene(string sceneName)
    {
        //fadeScreen.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(sceneName);
        //fadeScreen.SetActive(false);
    }
}
