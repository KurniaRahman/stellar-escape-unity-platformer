using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // Player yang akan diikuti
    public float smoothSpeed = 0.125f;  // Kecepatan smoothing
    public Vector3 offset;              // Jarak offset dari player

    [Header("Clamp Area (Optional)")]
    public bool useLimits = true;
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        if (useLimits)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
