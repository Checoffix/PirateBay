using UnityEngine;

public class DropMoneyComponent : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int _silverCoinChance;
    private SpawnComponent _spawnComponent;

    private void Start()
    {
        _spawnComponent = GetComponent<SpawnComponent>();
    }
    public void Drop()
    {
        float rndVal = Random.Range(0, 101);
        if (rndVal <= _silverCoinChance)
        {
            _spawnComponent.Spawn("silver");
        }
        else _spawnComponent.Spawn("gold");
    }
}
