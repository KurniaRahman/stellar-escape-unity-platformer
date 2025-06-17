using UnityEngine;

public class Tuas : MonoBehaviour
{
    public Lift lift;
    public Tuas linkedLever; // lever lain yang terhubung

    private bool leverOn = false;
    private bool isPlayerNear = false;
    private Vector3 originalScale;

    private int playerNumberNear = 0; // Menyimpan nomor pemain yang berada di dekat tuas

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // Cek interaksi berdasarkan playerNumber
            if (playerNumberNear == 1 && Input.GetKeyDown(KeyCode.E)) // Player 1
            {
                ToggleLever();
            }
            else if (playerNumberNear == 2 && Input.GetKeyDown(KeyCode.RightShift)) // Player 2
            {
                ToggleLever();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isPlayerNear = true;

            // Ambil nomor pemain dari PlayerController
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerNumberNear = playerController.playerNumber;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isPlayerNear = false;
            playerNumberNear = 0; // Reset nomor pemain saat keluar
        }
    }

    public void ToggleLever()
    {
        leverOn = !leverOn;
        UpdateVisual();
        lift.SetMoveState(leverOn);

        if (linkedLever != null && linkedLever.leverOn != this.leverOn)
        {
            linkedLever.SyncLeverState(leverOn);
        }

        Debug.Log("Lever toggle: " + leverOn);
    }

    public void SyncLeverState(bool newState)
    {
        leverOn = newState;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Vector3 newScale = originalScale;
        newScale.x = leverOn ? -Mathf.Abs(originalScale.x) : Mathf.Abs(originalScale.x);
        transform.localScale = newScale;
    }
}

