using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Bisa juga heart, mana, powerup
    public string pickupSoundName = "Coin"; // Nama sound di SoundManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Tambah score player, bisa pakai GameManager
            ItemManager.Instance.AddCoins(coinValue);

            // Mainkan efek suara
            SoundManager.Instance.Play3D(pickupSoundName, transform.position);


            // Hancurkan koin
            Destroy(gameObject);
        }
    }
}
