using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public int Coins = 0;
    public int Gas = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Debug.Log("Coins: " + Coins);
        // Bisa update UI di sini
    }
    public void AddGas(int value)
{
    Gas += value;
    Debug.Log("Gas: " + Gas);
    // Tambahkan update UI jika perlu
}
}