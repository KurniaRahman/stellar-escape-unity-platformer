using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    private Vector2 checkpointPlayer1;
    private Vector2 checkpointPlayer2;

    private GameObject player1;
    private GameObject player2;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player1 != null) checkpointPlayer1 = player1.transform.position;
        if (player2 != null) checkpointPlayer2 = player2.transform.position;
    }

    public void UpdateCheckpoint(string playerTag, Vector2 newCheckpoint)
    {
        if (playerTag == "Player1") checkpointPlayer1 = newCheckpoint;
        else if (playerTag == "Player2") checkpointPlayer2 = newCheckpoint;
    }

    public void Respawn(GameObject player)
    {
        if (player.CompareTag("Player1"))
            player.transform.position = checkpointPlayer1;
        else if (player.CompareTag("Player2"))
            player.transform.position = checkpointPlayer2;
    }
}
