using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private ProjectileCamera projectileCamera;
    [SerializeField] private CameraSwitcher cameraSwitcherScript;
    [SerializeField] private SlowMotionCamera slowMotionCameraScript;

    public float cameraShakeIntensity = 4f;
    public float cameraShakeDuration = 0.3f;


    public void FollowProjectile(Transform projectile)
    {
        projectileCamera.ChangeTarget(projectile);
        slowMotionCameraScript.ChangeTarget(projectile);
    }

    public void SwitchCamera(bool projectileActive)
    {
        cameraSwitcherScript.SwitchPriority(projectileActive);
    }

    public void SlowMotionCameraStatus(bool isActive)
    {
        cameraSwitcherScript.SlowMotionCam(isActive);
        StartCoroutine(DeactivateSlowMotionAfterDelay());
    }

    IEnumerator DeactivateSlowMotionAfterDelay()
    {
        yield return new WaitForSeconds(TimeManager.Instance.slowDownLength);
        cameraSwitcherScript.SlowMotionCam(false);
    }
}
