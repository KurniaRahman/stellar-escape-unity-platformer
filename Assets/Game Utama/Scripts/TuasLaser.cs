using UnityEngine;

public class TuasLaser : MonoBehaviour
{
    public GameObject laserObject;
    private bool playerInRange = false;
    private string playerTagInRange;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (playerInRange)
        {
            bool isHolding = false;

            if (playerTagInRange == "Player1" && Input.GetKey(KeyCode.E))
            {
                laserObject.SetActive(false);
                isHolding = true;
            }
            else if (playerTagInRange == "Player2" && Input.GetKey(KeyCode.RightShift))
            {
                laserObject.SetActive(false);
                isHolding = true;
            }
            else
            {
                laserObject.SetActive(true);
            }

            // Visual tuas ditekan (balik sumbu X)
            if (isHolding)
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = originalScale;
            }
        }
        else
        {
            laserObject.SetActive(true);
            transform.localScale = originalScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playerInRange = true;
            playerTagInRange = other.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTagInRange))
        {
            playerInRange = false;
            playerTagInRange = "";
            transform.localScale = originalScale; // Reset
        }
    }
}
