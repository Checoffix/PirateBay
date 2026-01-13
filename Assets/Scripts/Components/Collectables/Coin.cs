using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinValue = 0;
    private GameSession _gameSession;
    private void Start()
    {
        _gameSession = FindFirstObjectByType<GameSession>();
    }
    public void AddMoneyToBank(GameObject player)
    {
        _gameSession.Data.Inventory.Add("coin", _coinValue);
    }
}
