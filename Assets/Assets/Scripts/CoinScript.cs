using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, IPooledObject
{
    public float moveSpeed = 5f;
    public int degreePerSecond = 180;
    
    private Rigidbody rb;
    private bool initialCollision = false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        rb.AddForce(Vector3.up * moveSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(0, degreePerSecond * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lambert"))
        {
            if (!initialCollision)
            {
                UIManager.Instance.UpdateScore(+50);
                gameObject.SetActive(false);
                
                initialCollision = true;
            }
        }
        
        if (other.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
    }
}
