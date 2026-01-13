using System;
using UnityEngine;

class ArcProjectile : BaseProjectile
{
    [SerializeField] private float _trajectoryMaxHeight;
    [Header("Curves")]
    [SerializeField] private AnimationCurve _shapeCurve;
    [SerializeField] private AnimationCurve _heightCorrelationCurve;
    [SerializeField] private AnimationCurve _speedCorrelationCurve;
    private Vector2 _targetPos;
    private float _distance;
    private float _currentSpeed;
    private float _dy;
    private Vector2 _startPos;
    private float NextPointX, NextPointXNormalized, NextPointYNormalized, NextPointYCurve, NextPointYAbsolute;
    protected override void Start()
    {
        base.Start();
        GameObject _target = GameObject.Find("Hero");
        _targetPos = _target.transform.position;
        _distance = Math.Abs(_targetPos.x - transform.position.x);
        _dy = _targetPos.y - transform.position.y;
        _startPos = transform.position;
        _currentSpeed = Speed;
    }

    void FixedUpdate()
    {
        NextPointX = transform.position.x + _currentSpeed * Direction;
        NextPointXNormalized = (_distance - Math.Abs(_targetPos.x - transform.position.x)) / _distance;

        _currentSpeed = _speedCorrelationCurve.Evaluate(NextPointXNormalized) * Speed;
        NextPointYNormalized = _heightCorrelationCurve.Evaluate(NextPointXNormalized);
        NextPointYCurve = _shapeCurve.Evaluate(NextPointXNormalized);
        NextPointYAbsolute = (NextPointYNormalized * _dy + _startPos.y) + NextPointYCurve * _distance * _trajectoryMaxHeight;
        
        Rigidbody.MovePosition(new Vector2(NextPointX, NextPointYAbsolute));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}