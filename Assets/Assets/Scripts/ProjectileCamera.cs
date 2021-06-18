using Cinemachine;
using UnityEngine;

public class ProjectileCamera : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeTarget(Transform newProjectile)
    {
        vcam.LookAt = newProjectile;
        vcam.Follow = newProjectile;
    }
}
