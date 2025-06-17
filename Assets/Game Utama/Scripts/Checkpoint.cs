using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            RespawnManager.instance.UpdateCheckpoint(other.tag, transform.position);
            Debug.Log($"Checkpoint diperbarui untuk {other.tag} di posisi {transform.position}");
        }
    }
}