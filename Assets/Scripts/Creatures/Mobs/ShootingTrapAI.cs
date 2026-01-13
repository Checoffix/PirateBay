using System;
using UnityEngine;

public class ShootingTrapAI : MonoBehaviour
{
    [SerializeField] public ColliderLayerCheck _vision;
    [SerializeField] private SpawnComponent _rangeAttack;
    [SerializeField] protected Cooldown RangeCooldown;
    private SpriteAnimation _animation;

    protected virtual void Start()
    {
        _animation = GetComponent<SpriteAnimation>();
    }

    protected virtual void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (RangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
    }

    public virtual void RangeAttack()
    {
        _animation.SetClip("fire");
        RangeCooldown.Reset();
    }
    public void OnRangeAttack()
    {
        _rangeAttack.Spawn("projectile");
    }
}
