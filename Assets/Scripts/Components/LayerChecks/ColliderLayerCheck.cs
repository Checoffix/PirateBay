using UnityEngine;

public class ColliderLayerCheck : LayerCheck
{
    [SerializeField] private LayerMask _layerMask;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isTouchingLayer = _collider.IsTouchingLayers(_layerMask);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isTouchingLayer = _collider.IsTouchingLayers(_layerMask);
    }
}
