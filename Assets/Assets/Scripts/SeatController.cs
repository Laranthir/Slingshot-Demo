using UnityEngine;

public class SeatController : MonoBehaviour
{
    [SerializeField] private Transform drawFrom;
    [SerializeField] private Transform drawTo;
    [SerializeField] private Transform LookAt;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject lambert;
    [SerializeField] private GameObject decoy;

    private GameManager gameManager;
    private CameraManager cameraManager;
    private PredictionManager predictionManager;
    
    private Vector3 newSeatPos;
    private Vector3 initialPos;
    private Vector3 initialRot;
    private Vector3 localPos;
    private Vector3 direction;
    private Quaternion rotation;
    
    private bool seatPulledBack = false;
    private bool seatEmpty = false;
    private Touch touch;

    private float pullTimer;
    private float pullTimerTotal;
    private float seatAxisZ;

    private void Start()
    {
        initialPos = new Vector3(0, 0.8f, 0);
        initialRot = Vector3.zero;

        gameManager = GameManager.Instance;
        cameraManager = CameraManager.Instance;
        predictionManager = PredictionManager.Instance;
    }

    private void Update()
    {
        if (!seatEmpty)
        {
            TouchController();
        }
    }
    
    #region Fundemental Methods
    
    void TouchController()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Predict();
            
            if (touch.phase == TouchPhase.Began)
            {
                if (!seatPulledBack)
                {
                    StartPulling(0.25f);
                    seatPulledBack = true;
                    gameManager.ActivatePrediction(true);

                    Debug.Log("Touch started!");
                }
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                PullSeatBackOverTime();
                newSeatPos = transform.localPosition;
                newSeatPos += new Vector3(touch.deltaPosition.x * gameManager.speedTouch, touch.deltaPosition.y * gameManager.speedTouch, 0);
                newSeatPos = new Vector3(Mathf.Clamp(newSeatPos.x, -0.25f, 0.25f),
                    Mathf.Clamp(newSeatPos.y, 0.8f - 0.25f, 0.8f + 0.25f), newSeatPos.z);

                transform.localPosition = newSeatPos;
                RotateTowards();
            }
            
            else if (touch.phase == TouchPhase.Ended)
            {
                //Finish shooting
                ShootLambert();
                gameManager.ActivatePrediction(false);
                seatPulledBack = false;
                //When chamber is Empty Chamber
                decoy.SetActive(false);
                seatEmpty = true;


                transform.localPosition = initialPos;
                transform.localEulerAngles = initialRot;

                Debug.Log("Touch ended!");
            }
        }
    }

    void RotateTowards()
    {
        direction = LookAt.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public Vector3 CalculateForce()
    {
        return direction * gameManager.shootSpeedForward;
    }

    void ShootLambert()
    {
        GameObject currentProjectile = Instantiate(lambert, firePoint.position, firePoint.rotation);
        currentProjectile.GetComponent<Rigidbody>().AddForce(CalculateForce(), ForceMode.Impulse);
        
        //Change camera target
        cameraManager.FollowProjectile(currentProjectile.transform);
        cameraManager.SwitchCamera(true);
    }
    
    void Predict()
    {
        predictionManager.Predict(lambert, firePoint.position, firePoint.rotation, CalculateForce());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            decoy.SetActive(true);
            seatEmpty = false;
            Destroy(other.gameObject);
        }
    }
    
    #endregion

    private void PullSeatBackOverTime()
    {
        if (pullTimer > 0)
        {
            pullTimer -= Time.deltaTime;
            
            seatAxisZ = Mathf.Lerp(0, -0.75f, (1 - (pullTimer / pullTimerTotal)));
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, seatAxisZ);
        }
    }
    
    public void StartPulling(float time)
    {
        pullTimerTotal = time;
        pullTimer = time;
    }
}
