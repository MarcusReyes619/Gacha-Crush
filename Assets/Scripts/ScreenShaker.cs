using UnityEngine;


public class ScreenShaker : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float shake;
    [SerializeField] float shakeAmount;
    [SerializeField] float decreaseFactor;
    bool shakeCalled;
    Vector3 oringalPos;

    private void Start()
    {
        oringalPos = cam.transform.position;
    }
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
                cam.transform.position = oringalPos;
                shakeCalled = false;
            }
        }
}

    
}
