using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] Music_Audios, SFX_Audios;
    public AudioSource Music_Source, SFX_Source;

    public float fadeDuration = 0.75f; // Adjust as needed

    private string nextMusicName = "";

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

    private void Start()
    {
        // Play when loading MainMenu Scene
        PlayMusic("Main_Music");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(Music_Audios, x => x.Name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            Music_Source.clip = sound.clip;
            Music_Source.loop = true;
            Music_Source.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFX_Audios, x => x.Name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            SFX_Source.clip = sound.clip;
            SFX_Source.Play();
        }
    }

    public void ChangeSceneWithMusic(SwitchScene_Manager.Scenes targetScene, string newMusic)
    {
        //if (!isFading)
        {
            nextMusicName = newMusic;
            //StartCoroutine(FadeToBlackAndLoadScene(targetScene));
            GameManager.instance.PauseGame(false);
            SceneManager.LoadScene(targetScene.ToString());

        }
    }

    private IEnumerator FadeToBlackAndLoadScene(SwitchScene_Manager.Scenes scene)
    {
        StartCoroutine(FadeOutMusic());
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(scene.ToString());
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = Music_Source.volume;
        while (Music_Source.volume > 0)
        {
            Music_Source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        Music_Source.Stop();
        Music_Source.volume = startVolume; // Reset volume for next music
    }

    public void OnSceneLoaded()
    {
        if (!string.IsNullOrEmpty(nextMusicName))
        {
            StartCoroutine(FadeInMusic(nextMusicName));
        }

        // Reset UI elements if needed
        if (SceneManager.GetActiveScene().name == "TitleScreen_Scene")
        {
            TogglePauseMenu.instance?.Close_PauseMenu(); // Ensure UI is properly reset
        }
    }


    private IEnumerator FadeInMusic(string musicName)
    {
        PlayMusic(musicName);
        Music_Source.volume = 0;
        float targetVolume = 1.0f;

        while (Music_Source.volume < targetVolume)
        {
            Music_Source.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}
