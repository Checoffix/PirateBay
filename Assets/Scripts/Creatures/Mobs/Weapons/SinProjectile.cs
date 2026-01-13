using System;
using UnityEngine;

class SinProjectile : BaseProjectile
{
    [SerializeField] private float _frequancy;
    [SerializeField] private float _amplitude;
    private float _originalY;
    private float _time;
    protected override void Start()
    {
        base.Start();
        _originalY = transform.position.y;
    }
    void FixedUpdate()
    {
        var y = MathF.Sin(_time * _frequancy) * _amplitude;
        var x = transform.position.x + Direction * Speed;
        Rigidbody.MovePosition(new Vector2(x, _originalY + y));
        _time += Time.deltaTime;
    }
}