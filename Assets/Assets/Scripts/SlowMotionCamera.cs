using UnityEngine;
using Cinemachine;

public class SlowMotionCamera : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Orbit orbitScript;
    private Transform ragdoll;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        orbitScript = GetComponent<Orbit>();
    }

    public void ChangeTarget(Transform newProjectile)
    {
        ragdoll = newProjectile.transform.GetChild(0).GetChild(5).gameObject.transform;
        vcam.LookAt = ragdoll;
        orbitScript.target = ragdoll;
    }
}
