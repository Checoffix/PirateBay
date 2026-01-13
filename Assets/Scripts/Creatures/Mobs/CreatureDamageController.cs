using UnityEngine;
using UnityEngine.Events;

public class CreatureDamageController : MonoBehaviour
{
    [SerializeField] private UnityEvent _die;
    [SerializeField] private UnityEvent _hitMovement;
    private Animator _animator;
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int Die = Animator.StringToHash("die");

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public virtual void OnHit()
    {
        _animator.SetTrigger(Hit);
        _hitMovement?.Invoke();
    }
    public void OnDie()
    {
        _animator.SetTrigger(Die);
        _die?.Invoke();
    }
}
