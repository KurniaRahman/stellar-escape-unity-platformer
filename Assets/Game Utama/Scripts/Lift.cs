using UnityEngine;

public class Lift : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float setpointY; // Titik tujuan Y (atas misalnya)
    
    private Vector3 startPoint;
    private Vector3 targetPoint;
    private bool shouldGoToSetpoint = false;

    void Start()
    {
        startPoint = transform.position;
    }

    void Update()
    {
        targetPoint = shouldGoToSetpoint
            ? new Vector3(startPoint.x, setpointY, startPoint.z)
            : startPoint;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    // Fungsi dipanggil dari LeverTrigger
    public void SetMoveState(bool goToSetpoint)
    {
        shouldGoToSetpoint = goToSetpoint;
    }
}
