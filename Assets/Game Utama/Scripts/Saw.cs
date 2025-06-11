using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Mode Gerakan")]
    public bool rotateInPlace = true;
    public bool moveBackAndForth = false;

    [Header("Pengaturan Rotasi")]
    public float rotationSpeed = 200f;

    [Header("Pengaturan Gerak Bolak-Balik")]
    public Vector2 pointA;
    public Vector2 pointB;
    public float moveSpeed = 3f;

    private Vector2 targetPos;

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
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.DrawSphere(pointB, 0.1f);
        Gizmos.DrawLine(pointA, pointB);
    }
}
