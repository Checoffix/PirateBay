using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private int _direction; // 1 - up, 0 - down, -1 - left, -2 - right
    [SerializeField] private float _duration;
    [SerializeField] private bool _moveBack;
    [SerializeField] private UnityEvent _action;
    private bool _moveStarted = false;
    private float _speed;
    private float _posX, _posY;
    private float _startTime;
    private bool _isGoingBackwards;

    private void Start()
    {
        _speed = _distance / _duration;
    }

    void Update()
    {
        if (_moveStarted)
        {
            switch (_direction)
            {
                case 1:
                    _posX = transform.position.x;
                    _posY = transform.position.y + _speed * Time.deltaTime;
                    break;
                case 0:
                    _posX = transform.position.x;
                    _posY = transform.position.y - _speed * Time.deltaTime;
                    break;
                case -1:
                    _posX = transform.position.x - _speed * Time.deltaTime;
                    _posY = transform.position.y;
                    break;
                case -2:
                    _posX = transform.position.x + _speed * Time.deltaTime;
                    _posY = transform.position.y;
                    break;
                default:
                    _moveStarted = false;
                    break;
            }
            transform.position = new Vector3(_posX, _posY, transform.position.z);
            if (Time.time - _startTime >= _duration)
            {
                if (!_isGoingBackwards)
                {
                    if (!_moveBack) _action?.Invoke();
                    _isGoingBackwards = true;
                }
                else
                {
                    if (_moveBack) _action?.Invoke();
                    _moveStarted = false;
                }
                InverseDirection();
            }
        }
    }

    public void StartMovement()
    {
        if (!_moveStarted)
        {
            _moveStarted = true;
            _isGoingBackwards = false;
            _startTime = Time.time;
        }
    }
    private void InverseDirection()
    {
        switch (_direction)
        {
            case 1:
                _direction = 0;
                break;
            case 0:
                _direction = 1;
                break;
            case -1:
                _direction = -2;
                break;
            case -2:
                _direction = -1;
                break;
        }
        _startTime = Time.time;
    }
}
