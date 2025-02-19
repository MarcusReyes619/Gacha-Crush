using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //[Header("SFX")]
    //[Header("BG Music")]

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
        
    }
}
