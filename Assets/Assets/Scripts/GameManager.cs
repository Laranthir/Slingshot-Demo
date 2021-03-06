using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public float shootSpeedForward;
    [SerializeField] public float speedTouch = 0.001f;
    [SerializeField] public float speedMouse = 0.0005f;
    [SerializeField] private Reloader reloaderScript;

    private LineRenderer _lineRenderer;

    public bool mouseControllerActive;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ActivatePrediction(bool predictionStatus)
    {
        _lineRenderer.enabled = predictionStatus;
    }

    public void Reload()
    {
        reloaderScript.ReloadLambert();
    }
}
