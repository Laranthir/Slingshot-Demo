using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float startPoint;
    public float endPoint;
    public float speed = 1f;
    public float journeyLength = 1f;
    
    private float startTime;
    private float distanceCovered;

    private Vector3 start;
    private Vector3 end;
    
    private void Start()
    {
        start = new Vector3(startPoint, transform.position.y, transform.position.z);
        end = new Vector3(endPoint, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        MovingPlatform();
    }

    void MovingPlatform()
    {
        distanceCovered = Mathf.PingPong(Time.time - startTime, journeyLength / speed);
        transform.position = Vector3.Lerp(start, end, distanceCovered / journeyLength);
    }
}
