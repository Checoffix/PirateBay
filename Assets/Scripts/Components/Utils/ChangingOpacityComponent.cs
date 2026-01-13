using UnityEngine;
using UnityEngine.Events;

public class ChangingOpacityComponent : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent _action;
    private float _startTime;
    private float _alphaChangePerTick;
    private SpriteRenderer[] _childrens;
    private Color _childColor;
    void Start()
    {
        _startTime = Time.time;
        _alphaChangePerTick = 1.0f / (_time * 50);
        _childrens = GetComponentsInChildren<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        foreach (var child in _childrens)
        {
            _childColor = child.color;
            _childColor.a -= _alphaChangePerTick;
            child.color = _childColor;
        }
        if (Time.time - _startTime >= _time)
        {
            _action?.Invoke();
        }
    }
}
