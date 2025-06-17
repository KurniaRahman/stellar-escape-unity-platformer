using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 5f;

    private bool isNearLadder = false;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Cek jika pemain berada di dekat tangga dan menekan tombol interaksi
        if (isNearLadder && Input.GetKeyDown(playerController.interact))
        {
            isClimbing = !isClimbing;
            rb.gravityScale = isClimbing ? 0 : 0.9f;
            rb.linearVelocity = Vector2.zero; // Hentikan gerakan saat mulai/berhenti memanjat
        }

        // Jika sedang memanjat tangga
        if (isClimbing)
        {
            float vertical = 0f;
            int blokLayer = LayerMask.NameToLayer("BlokTembus");

            // Nonaktifkan tabrakan antara pemain dan layer BlokTembus
            Physics2D.IgnoreLayerCollision(gameObject.layer, blokLayer, true);

            // Gunakan input vertikal berdasarkan playerNumber
            if (playerController.playerNumber == 1)
            {
                if (Input.GetKey(KeyCode.W)) vertical = 1f;
                else if (Input.GetKey(KeyCode.S)) vertical = -1f;
            }
            else if (playerController.playerNumber == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
                else if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;
            }

            // Gerakan vertikal saat memanjat
            rb.linearVelocity = new Vector2(0, vertical * climbSpeed);
        }
        else
        {
            // Aktifkan kembali tabrakan antara pemain dan layer BlokTembus jika tidak memanjat
            int blokLayer = LayerMask.NameToLayer("BlokTembus");
            Physics2D.IgnoreLayerCollision(gameObject.layer, blokLayer, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = true;
        }

        // Jika pemain menyentuh layer "Platform" saat sedang memanjat
        if (isClimbing && other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isClimbing = false;
            rb.gravityScale = 0.9f; // Kembalikan gravitasi
            rb.linearVelocity = Vector2.zero; // Hentikan gerakan vertikal
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = false;
            isClimbing = false;
            rb.gravityScale = 0.9f; // Kembalikan gravitasi saat keluar dari tangga

            // Pastikan gerakan horizontal kembali diaktifkan
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Aktifkan kembali tabrakan antara pemain dan layer BlokTembus
            int blokLayer = LayerMask.NameToLayer("BlokTembus");
            Physics2D.IgnoreLayerCollision(gameObject.layer, blokLayer, false);
        }
    }
}