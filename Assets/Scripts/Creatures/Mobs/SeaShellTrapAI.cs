using System;
using UnityEngine;

public class SeaShellTrapAI : ShootingTrapAI
{
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] private ColliderLayerCheck _meleeCanAttack;
    [SerializeField] private Cooldown _meleeCooldown;

    private Animator _animator;
    private static readonly int _range = Animator.StringToHash("fire");
    private static readonly int _melee = Animator.StringToHash("bite");

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (_meleeCanAttack.IsTouchingLayer && _meleeCooldown.IsReady)
            {
                MeleeAttack();
                return;
            }
            if (RangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
    }

    public override void RangeAttack()
    {
        _animator.SetTrigger(_range);
        ResetCooldowns();
    }

    private void MeleeAttack()
    {
        _animator.SetTrigger(_melee);
        ResetCooldowns();
    }
    public void OnMeleeAttack()
    {
        _meleeAttack.Check();
    }

    private void ResetCooldowns()
    {
        RangeCooldown.Reset();
        _meleeCooldown.Reset();
    }
}
