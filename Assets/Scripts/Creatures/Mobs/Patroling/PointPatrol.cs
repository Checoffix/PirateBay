using System.Collections;
using UnityEngine;

public class PointPatrol : Patrol
{
    [SerializeField] Transform[] _waypoints;
    [SerializeField] float _treshold = 1f;

    private CreatureMovement _movement;
    private int _destinationPointIndex;

    private void Awake()
    {
        _movement = GetComponent<CreatureMovement>();
    }
    public override IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (IsOnPoint())
            {
                _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, +_waypoints.Length);
            }
            var direction = _waypoints[_destinationPointIndex].position - transform.position;
            direction.y = 0;
            _movement.SetDirection(direction.normalized);
            yield return null;
        }
    }

    private bool IsOnPoint()
    {
        return (_waypoints[_destinationPointIndex].position - transform.position).magnitude < _treshold;
    }
}