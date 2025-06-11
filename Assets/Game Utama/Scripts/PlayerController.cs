using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Input Settings")]
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.W;
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

    private bool isDead = false; // status kematian

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        if (rb != null)
            rb.freezeRotation = true;

        originalScale = transform.localScale;
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
            Debug.Log("mati");
            
        }
    }

    IEnumerator DieAfterDelay()
    {
        isDead = true;

        // Tambahkan animasi mati jika ada
        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        // Matikan gerakan
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        // Delay sebelum reload
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
