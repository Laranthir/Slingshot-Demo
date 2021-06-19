using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera slingCam;
    [SerializeField] private CinemachineVirtualCamera projectileCam;
    [SerializeField] private CinemachineVirtualCamera slowMotionCam;

    public void SwitchPriority(bool projectileActive)
    {
        if (projectileActive)
        {
            slingCam.Priority = 0;
            projectileCam.Priority = 1;
        }
        else
        {
            StartCoroutine(DelayedCameraExit());
        }
    }

    public void SlowMotionCam(bool isActive)
    {
        if (isActive)
        {
            slowMotionCam.Priority = 2;
        }
        else
        {
            slowMotionCam.Priority = 0;
        }
    }

    IEnumerator DelayedCameraExit()
    {
        yield return new WaitForSeconds(.4f);
        slingCam.Priority = 1;
        projectileCam.Priority = 0;
    }
}
