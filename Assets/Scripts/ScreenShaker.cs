using UnityEngine;


public class ScreenShaker : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float shake;
    [SerializeField] public float shakeAmount;
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
                Vector3 rand = Random.insideUnitSphere * shakeAmount;
                rand.z = -10;
                cam.transform.localPosition = rand;
                shake -= Time.deltaTime * decreaseFactor;

            }
            else
            {
                shake = 1;
                cam.transform.position = oringalPos;
                shakeCalled = false;
            }
        }
}

    
}
