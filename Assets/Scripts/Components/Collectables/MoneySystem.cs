using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private void Start()
    {
        _gameSession = FindFirstObjectByType<GameSession>();
    }
    public void AddMoney(int value)
    {
        _gameSession.Data.Inventory.Add("coin", value);
    }
}
