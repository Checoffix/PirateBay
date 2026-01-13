using UnityEngine;

public class CreatureAttack : MonoBehaviour
{
    [SerializeField] private CheckCircleOverlap _attackRange;
    [SerializeField] protected SpawnComponent Particles;
    [SerializeField] private Cooldown _throwCooldown;


    protected PlaySoundsComponent _playSounds;


    protected Animator Animator;
    private static readonly int _attack = Animator.StringToHash("attack");
    private static readonly int _throw = Animator.StringToHash("throw");

    protected virtual void Start()
    {
        _playSounds = GetComponent<PlaySoundsComponent>();
        Animator = GetComponent<Animator>();
    }
    public virtual void Attack()
    {
        _playSounds.Play("Melee");
        Particles.Spawn("attack");
        Animator.SetTrigger(_attack);
    }
    public void DealDamage()
    {
        _attackRange.Check();
    }
    public virtual void Throw()
    {
        if (_throwCooldown.IsReady)
        {
            ThrowWithoutCooldown();
        }
    }
    public virtual void ThrowWithoutCooldown()
    {
        Animator.SetTrigger(_throw);
        _throwCooldown.Reset();
    }
    public virtual void OnDoThrow()
    {
        _playSounds.Play("Range");
        Particles.Spawn("projectile");
    }
}
