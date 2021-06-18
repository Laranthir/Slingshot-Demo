using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour, IPooledObject
{
    public float verticalSpeed = 1f;
    public float horizontalSpeed = 1f;
    public int degreePerSecond = 180;
    public bool flipDirection = false;
    
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        if (flipDirection)
        {
            rb.AddForce((Vector3.up * verticalSpeed + Vector3.right * -horizontalSpeed), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce((Vector3.up * verticalSpeed + Vector3.right * horizontalSpeed), ForceMode.Impulse);
        }
    }

    private void Update()
    {
        transform.Rotate(0, degreePerSecond * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
    }
}
