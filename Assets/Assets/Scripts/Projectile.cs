using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool initialCollision = false;
    private bool ragdollActive = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (!initialCollision)
            {
                UIManager.Instance.UpdateScore(-100);
                CinemachineShake.Instance.ShakeCamera(CameraManager.Instance.cameraShakeIntensity, CameraManager.Instance.cameraShakeDuration);
                
                initialCollision = true;
                
                if (!ragdollActive)
                {
                    GameManager.Instance.Reload();
                    ragdollActive = true;
                    animator.enabled = false;
                    CameraManager.Instance.SwitchCamera(false);
                }
            }
        }
        
        else if (other.gameObject.CompareTag("Target"))
        {
            if (!initialCollision)
            {
                UIManager.Instance.UpdateScore(+1000);

                initialCollision = true;
                
                if (!ragdollActive)
                {
                    TimeManager.Instance.DoSlowMotion();
                    CameraManager.Instance.SlowMotionCameraStatus(true);
                    GameManager.Instance.Reload();
                    ragdollActive = true;
                    animator.enabled = false;
                    CameraManager.Instance.SwitchCamera(false);
                }
            }
        }
        
        else if (other.gameObject.layer == 3)
        {
            if (!ragdollActive)
            {
                GameManager.Instance.Reload();
                ragdollActive = true;
                animator.enabled = false;
                CameraManager.Instance.SwitchCamera(false);
            }
        }
    }
}
