using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent<int> _onChange;
    [Header("IFrames")]
    [SerializeField] private bool _isHavingInvincibleFrames;
    [SerializeField] private float _invincibleDuration;
    private bool _isInvincible = false;
    private bool _iFramesStarted = false;
    private float _startInvicibleTime;

    public void ApplyHpChange(int hpChange)
    {
        if (hpChange < 0 && _isInvincible) return;
        _health += hpChange;
        _onChange?.Invoke(_health);
        if (_health > 0)
        {
            if (hpChange < 0)
            {
                if (_isHavingInvincibleFrames) _isInvincible = true;
                _onDamage?.Invoke();
            }
        }
        else _onDie?.Invoke();
    }

    private void Update()
    {
        if (_iFramesStarted)
        {
            if (Time.time - _startInvicibleTime >= _invincibleDuration)
            {
                _isInvincible = false;
                _iFramesStarted = false;
            }
        }
    }

    public void DisableInvincibleFrames()
    {
        if (!_iFramesStarted) _isInvincible = false;
    }

    public void EnableInvincibleFrames(float time)
    {
        _isInvincible = true;
        _iFramesStarted = true;
        _startInvicibleTime = Time.time;
        _invincibleDuration = time;
    }

    [ContextMenu("Hit")]
    public void Minus1HP()
    {
        ApplyHpChange(-1);
    }

    public void SetHp(int newhp)
    {
        _health = newhp;
    }
}
