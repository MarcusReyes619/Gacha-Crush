using UnityEngine;


public class ScreenShaker : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float shake;
    [SerializeField] float shakeAmount;
    [SerializeField] float decreaseFactor;
    bool shakeCalled;

    public void ShakeCam()
    {
        shakeCalled = true;
    }
    private void Update()
    {
        if (shakeCalled) { 
            if (shake > 0)
            {
            cam.transform.localPosition = Random.insideUnitSphere * shakeAmount;
           
            shake -= Time.deltaTime * decreaseFactor;
            

            }
            else
            {
                shake = 0;
                shakeCalled = false;
            }
        }
}

    
}
