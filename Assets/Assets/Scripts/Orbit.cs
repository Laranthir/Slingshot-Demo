using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float xSpread;
    public float zSpread;
    public float yOffset;
    public Transform target;

    public float rotSpeed;
    public bool rotateClockwise;
    
    public float smoothSpeed = 0.125f;
    private Vector3 desiredPos;
    private Vector3 smoothedPos;

    float timer = 0;

    private void FixedUpdate () 
    {
        timer += Time.deltaTime * rotSpeed;
        
        SmoothOrbit();		
    }

    void OrbitAround() 
    {
        if(rotateClockwise) 
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            desiredPos = pos + target.position;
        } else 
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            desiredPos = pos + target.position;
        }
    }

    void SmoothOrbit()
    {
        OrbitAround();
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}
