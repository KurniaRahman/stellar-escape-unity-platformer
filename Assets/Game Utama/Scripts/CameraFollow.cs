using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player References")]
    public Transform player1;
    public Transform player2;

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f;
    public float minZoom = 5f;         // Minimum camera size (zoomed in)
    public float maxZoom = 10f;        // Maximum camera size (zoomed out)
    public float zoomLimiter = 50f;    // How fast camera zooms based on player distance
    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset, -10 for 2D games

    [Header("Boundary Settings")]
    public bool useLimits = true;
    public float minX, maxX, minY, maxY;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // Get the center point between players
        Vector3 centerPoint = GetCenterPoint();

        // Move camera to center point
        Vector3 newPosition = centerPoint + offset;

        if (useLimits)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);

        // Adjust zoom based on player distance
        if (cam != null)
        {
            float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        }
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(player1.position, Vector3.zero);
        bounds.Encapsulate(player2.position);
        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(player1.position, Vector3.zero);
        bounds.Encapsulate(player2.position);
        return Mathf.Max(bounds.size.x, bounds.size.y);
    }

    // Optional: Visualize camera boundaries in editor
    void OnDrawGizmos()
    {
        if (!useLimits) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0),
                           new Vector3(maxX - minX, maxY - minY, 0));
    }
}
