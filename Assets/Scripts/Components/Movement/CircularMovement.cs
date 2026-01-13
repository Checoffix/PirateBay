using Mono.Cecil;
using UnityEngine;

class CircularMovement : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    private Rigidbody2D[] _bodies;
    private Vector2[] _positions;
    private float _time;
    private bool _isAllDead;
    private void Awake()
    {
        UpdateContent();
    }
    private void UpdateContent()
    {
        _bodies = GetComponentsInChildren<Rigidbody2D>();
        _positions = new Vector2[_bodies.Length];
    }
    private void Update()
    {
        CalculatePositions();
        _isAllDead = true;
        for (int i = 0; i < _bodies.Length; i++)
        {
            if (_bodies[i])
            {
                _bodies[i].MovePosition(_positions[i]);
                _isAllDead = false;
            }
        }
        if (_isAllDead)
        {
            Destroy(gameObject);
        }
        _time += Time.deltaTime;
    }

    private void CalculatePositions()
    {
        var step = 2 * Mathf.PI / _bodies.Length;
        Vector2 containerPosition = transform.position;
        for (int i = 0; i < _bodies.Length; i++)
        {
            var angle = step * i;
            var pos = new Vector2(Mathf.Sin(angle + _time * _speed) * _radius, Mathf.Cos(angle + _time * _speed) * _radius);
            _positions[i] = containerPosition + pos;
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateContent();
        CalculatePositions();
        for (int i = 0; i < _bodies.Length; i++)
        {
            _bodies[i].transform.position = _positions[i];
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
