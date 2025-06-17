using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Mode Gerakan")]
    public bool rotateInPlace = true;
    public bool moveBackAndForth = false;

    [Header("Pengaturan Rotasi")]
    public float rotationSpeed = 200f;

    [Header("Pengaturan Gerak Bolak-Balik")]
    [SerializeField] private Vector2 pointB; // Hanya pointB yang perlu diatur di Inspector
    public float moveSpeed = 3f;

    private Vector2 pointA; // pointA akan diatur otomatis
    private Vector2 targetPos;

    void Awake()
    {
        // Set pointA ke posisi awal objek
        pointA = transform.position;
    }

    void Start()
    {
        // Set target awal jika mode bolak-balik aktif
        if (moveBackAndForth)
        {
            targetPos = pointB;
        }
    }

    void Update()
    {
        if (rotateInPlace)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (moveBackAndForth)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                targetPos = (targetPos == pointA) ? pointB : pointA;
            }
        }
    }

    // Gambar garis di editor untuk bantu lihat jalur gerak
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        // Untuk preview di Editor, gunakan current position jika pointA belum diset
        Vector2 previewPointA = Application.isPlaying ? pointA : (Vector2)transform.position;
        
        Gizmos.DrawSphere(previewPointA, 0.1f);
        Gizmos.DrawSphere(pointB, 0.1f);
        Gizmos.DrawLine(previewPointA, pointB);
    }
}
