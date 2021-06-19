using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    [SerializeField] private GameObject lambert;

    public void ReloadLambert()
    {
        Instantiate(lambert, transform.position, transform.rotation);
    }

}
