using UnityEngine;

public abstract class LayerCheck : MonoBehaviour
{
    [SerializeField] protected bool _isTouchingLayer;
    [SerializeField] private bool _isSupposedToTouch = true;
    public bool IsTouchingLayer => _isTouchingLayer;
    public bool IsSupposedToTouch => _isSupposedToTouch;
}
