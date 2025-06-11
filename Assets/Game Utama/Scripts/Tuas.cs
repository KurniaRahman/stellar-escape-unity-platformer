using UnityEngine;

public class Tuas : MonoBehaviour
{
    public Lift lift;
    public Tuas linkedLever; // lever lain yang terhubung

    private bool leverOn = false;
    private bool isPlayerNear = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightShift))
            {
                ToggleLever();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
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

