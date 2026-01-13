using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrol : Patrol
{
    [SerializeField] private LayerCheck _edgeCheck;
    [SerializeField] private LayerCheck _obstacleChecks;
    private CreatureMovement _movement;
    private bool _flipDirection;

    private void Awake()
    {
        _movement = GetComponent<CreatureMovement>();
    }
    public override IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (_flipDirection) _movement.SetDirection(Vector2.left);
            else _movement.SetDirection(Vector2.right);
            yield return null;
            yield return null;
            if (!_edgeCheck.IsTouchingLayer || _obstacleChecks.IsTouchingLayer)
            {
                _flipDirection = !_flipDirection;
            }
            yield return null;
        }
    }
}