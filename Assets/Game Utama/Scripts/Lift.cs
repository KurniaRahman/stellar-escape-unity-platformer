using UnityEngine;

public class Lift : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    
    [Header("Mode (Centang memilih mode)")]
    public bool Otomatis = false;    // Mode otomatis atau trigger
    public bool Horizontal = false;   // Mode horizontal atau vertikal
    
    [Header("Position Settings")]
    public float moveDistance = 5f;      // Jarak pergerakan
    
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool movingToEnd = true;
    private bool shouldMove = false;

    void Start()
    {
        startPoint = transform.position;
        
        // Set endpoint berdasarkan mode (horizontal/vertical)
        if (Horizontal)
        {
            endPoint = startPoint + Vector3.right * moveDistance;
        }
        else
        {
            endPoint = startPoint + Vector3.up * moveDistance;
        }
    }

    void Update()
    {
        // Jika mode otomatis, bergerak terus-menerus
        if (Otomatis)
        {
            MoveAutomatic();
        }
        // Jika mode trigger, hanya bergerak saat shouldMove true
        else if (shouldMove)
        {
            MoveToTarget();
        }
    }

    void MoveAutomatic()
    {
        Vector3 targetPosition = movingToEnd ? endPoint : startPoint;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Cek apakah sudah sampai di target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingToEnd = !movingToEnd; // Balik arah
        }
    }

    void MoveToTarget()
    {
        Vector3 targetPosition = movingToEnd ? endPoint : startPoint;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Cek apakah sudah sampai di target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            shouldMove = false; // Berhenti bergerak
        }
    }

    // Fungsi dipanggil dari LeverTrigger
    public void SetMoveState(bool triggered)
    {
        if (!Otomatis)
        {
            shouldMove = true;
            movingToEnd = triggered;
        }
    }

    // Optional: Untuk memvisualisasikan path di Scene view
    void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Vector3 moveDirection = Horizontal ? Vector3.right : Vector3.up;
        Gizmos.DrawLine(transform.position, transform.position + moveDirection * moveDistance);
    }
}
