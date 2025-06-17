using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerNumber = 1; // 1 untuk Player 1, 2 untuk Player 2

    [Header("Input Settings")]
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode interact = KeyCode.E;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Settings")]
    public LayerMask platform;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool facingRight = true;
    private Vector3 originalScale;

    private bool run = false;
    private bool grounded = false;
    private Animator animator;

    private bool isDead = false; 



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        if (rb != null)
            rb.freezeRotation = true;

        originalScale = transform.localScale;

        // Atur input berdasarkan playerNumber
        if (playerNumber == 2)
        {
            moveLeft = KeyCode.LeftArrow;
            moveRight = KeyCode.RightArrow;
            jump = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
            interact = KeyCode.RightShift;
        }
    }

    void Update()
    {
        if (isDead) return; // Hentikan kontrol jika mati

        HandleMovement();
        HandleJump();
        UpdateAnimationState();
    }

void HandleMovement()
{
    float moveDir = 0f;
    if (Input.GetKey(moveLeft)) moveDir = -1f;
    else if (Input.GetKey(moveRight)) moveDir = 1f;

    rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);
    run = moveDir != 0;

    if (moveDir > 0 && !facingRight) Flip();
    else if (moveDir < 0 && facingRight) Flip();

}

    void HandleJump()
    {
        grounded = IsGrounded();

        if (Input.GetKeyDown(jump) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (animator != null)
                animator.SetTrigger("jump");
                SoundManager.Instance.Play3D("Jump", transform.position);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, 1f, platform);
        return hit.collider != null;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            Mathf.Abs(originalScale.x) * (facingRight ? 1 : -1),
            originalScale.y,
            originalScale.z
        );
    }

    private void UpdateAnimationState()
    {
        if (animator != null)
        {
            animator.SetBool("run", run);
            animator.SetBool("grounded", grounded);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Saw"))
        {
            Debug.Log("mati terpotong");
            StartCoroutine(DieAfterDelay());
        }
        else if (other.CompareTag("Duri"))
        {
            Debug.Log("mati di duri");
            StartCoroutine(DieAfterDelay());
        }
        else if (other.CompareTag("Laser"))
        {
            Debug.Log("mati di laser");
            StartCoroutine(DieAfterDelay());
        }
        else if (other.CompareTag("Lava"))
        {
            Debug.Log("mati di lava");
            StartCoroutine(DieAfterDelay());
        }
    }

IEnumerator DieAfterDelay()
{
    isDead = true;
    // Matikan gerakan sementara
    rb.linearVelocity = Vector2.zero;
    rb.bodyType = RigidbodyType2D.Kinematic;

    // Efek blinking
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    for (int i = 0; i < 6; i++) // Berkedip 6 kali
    {
        spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibilitas
        yield return new WaitForSeconds(0.2f); // Tunggu 0.2 detik
    }
    spriteRenderer.enabled = true; // Pastikan sprite terlihat

    // Respawn ke checkpoint
    RespawnManager.instance.Respawn(gameObject);

    // Aktifkan kembali gerakan
    rb.bodyType = RigidbodyType2D.Dynamic;

    isDead = false;
}

    private void OnDrawGizmosSelected()
    {
        if (capsuleCollider != null)
        {
            Gizmos.color = Color.green;
            Vector3 rayOrigin = capsuleCollider.bounds.center;
            float rayLength = capsuleCollider.bounds.extents.y + 0.05f;
            Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayLength);
        }
    }
}
