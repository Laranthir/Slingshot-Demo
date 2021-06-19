using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera projectileCam;
    private CinemachineBasicMultiChannelPerlin cmBasicMCPerlin;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    
    private void Awake()
    {
        Instance = this;
        
        projectileCam = GetComponent<CinemachineVirtualCamera>();
        cmBasicMCPerlin = projectileCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cmBasicMCPerlin.m_AmplitudeGain = intensity;
        
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            
            cmBasicMCPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
        }
    }
}
