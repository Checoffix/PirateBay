using UnityEngine;

public class HeroInventory : MonoBehaviour, ICanAddInInventrory
{
    [InventoryId][SerializeField] private string _healItem;
    [SerializeField] private int _healValue;
    private GameSession _gameSession;
    private HealthComponent _healthComponent;
    protected PlaySoundsComponent _playSounds;

    private void Awake()
    {
        _gameSession = FindFirstObjectByType<GameSession>();
    }
    private void Start()
    {
        _playSounds = GetComponent<PlaySoundsComponent>();
        _healthComponent = GetComponent<HealthComponent>();
    }
    public bool Add(string id, int value)
    {
        return _gameSession.Data.Inventory.Add(id, value);
    }
    public void Remove(string id, int value)
    {
        _gameSession.Data.Inventory.Remove(id, value);
    }
    public void SetValue(string id, int value)
    {
        _gameSession.Data.Inventory.SetValue(id, value);
    }
    public int Count(string id)
    {
        return _gameSession.Data.Inventory.Count(id);
    }
    public void Heal()
    {
        if (_gameSession.Data.Inventory.Count(_healItem) != 0)
        {
            _gameSession.Data.Inventory.Remove(_healItem, 1);
            _healthComponent.ApplyHpChange(_healValue);
        }
    }
}