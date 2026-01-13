using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TotemTower : MonoBehaviour
{
    [SerializeField] private Cooldown _shootingCooldown;
    [SerializeField] private ShootingTrapAI[] _totems;

    private int _currentTotem;
    private int _partsCount;
    private void Start()
    {
        _partsCount = _totems.Length;
        foreach (var totem in _totems)
        {
            totem.enabled = false;
        }
    }
    private void Update()
    {
        if (_totems.Any(x => x._vision.IsTouchingLayer))
        {
            if (_shootingCooldown.IsReady)
            {
                for (int i = 0; i < _totems.Length; i++)
                {
                    if (_totems[_currentTotem])
                    {
                        _totems[_currentTotem].RangeAttack();
                        _shootingCooldown.Reset();
                        _currentTotem = (int)Mathf.Repeat(_currentTotem + 1, _totems.Length);
                        break;
                    }
                    _currentTotem = (int)Mathf.Repeat(_currentTotem + 1, _totems.Length);
                }
            }
        }
    }

    public void OnPartDestroy()
    {
        _partsCount--;
        if (_partsCount == 0) Destroy(gameObject);
    }
}
