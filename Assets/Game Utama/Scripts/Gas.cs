using UnityEngine;

public class Gas : MonoBehaviour
{
    public int gasValue = 1; // Bisa juga heart, mana, powerup
    public string pickupSoundName = "Gas"; // Nama sound di SoundManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Tambah score player, bisa pakai GameManager
            ItemManager.Instance.AddCoins(gasValue);

            // Mainkan efek suara
            //SoundManager.Instance.Play(pickupSoundName);

            // Hancurkan koin
            Destroy(gameObject);
        }
    }
}
