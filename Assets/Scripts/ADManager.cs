using UnityEngine;


public class ADManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IronSource.Agent.init("appKey");
        IronSource.Agent.setMetaData("is_test_suite", "enable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }


}
