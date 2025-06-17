using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserVisual; // Sprite laser
    public Collider2D laserHitbox; // Collider untuk bunuh player

    private bool isActive = true;

    void Update()
    {
        laserVisual.SetActive(isActive);
        laserHitbox.enabled = isActive;
    }

    public void SetLaserState(bool active)
    {
        isActive = active;
    }
}
