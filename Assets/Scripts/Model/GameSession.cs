using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _startLevelData;
    [SerializeField] private PlayerData _currentData;
    public PlayerData Data { get { return _currentData; } }

    private void Awake()
    {
        if (GameSessionExist())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            ReloadData();
            DontDestroyOnLoad(this);
        }
    }
    
    private bool GameSessionExist()
    {
        var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
        foreach (var session in sessions)
        {
            if (session != this) return true;
        }
        return false;
    }
    public void ReloadData()
    {
        _currentData.hp = _startLevelData.hp;
        _currentData.Inventory.Reset();
        foreach (var item in _startLevelData.Inventory.GetAllItems())
        {
            _currentData.Inventory.SetValue(item.Id, item.Value);
        }
    }

    public void ChangeStartState()
    {
        _startLevelData.hp = _currentData.hp;
        _startLevelData.Inventory.Reset();
        foreach (var item in _currentData.Inventory.GetAllItems())
        {
            _startLevelData.Inventory.SetValue(item.Id, item.Value);
        }
    }
}
