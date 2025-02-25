using UnityEngine;


public class ScreenShaker : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float shake;
    [SerializeField] float shakeAmount;
    [SerializeField] float decreaseFactor;


    private void Update()
    {
        if (shake > 0)
        {
            cam.transform.localPosition = Random.insideUnitSphere * shakeAmount;
           
            shake -= Time.deltaTime * decreaseFactor;
            

        }
        else shake = 0.0f;
    }

    
}
