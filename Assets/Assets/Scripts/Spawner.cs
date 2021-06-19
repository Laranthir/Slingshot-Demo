using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string itemName = "Coins";
    [SerializeField] private float spawnTime = 2;
    
    private ObjectPooler objectpooler;
    private float timer = 0f;
    

    private void Start()
    {
        objectpooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        
        if (timer > spawnTime)
        {
            objectpooler.SpawnFromPool(itemName, transform.position, transform.rotation, transform);

            timer = 0;
        }
    }
}
