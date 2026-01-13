
using System.Linq;
using PixelCrew.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class RaycastLayerCheck : LayerCheck
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private string[] _tags;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private UnityEvent<GameObject> _onOverlap;
    private void FixedUpdate()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2) transform.position + _startPos * transform.lossyScale, _direction * transform.lossyScale, _direction.magnitude, _layerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (_tags.Length > 0)
            {
                if (_tags.Any(tag => hits[i].collider.CompareTag(tag)))
                {
                    _isTouchingLayer = true;
                    return;
                }
            }
            else
            {
                _isTouchingLayer = true;
                return;
            }
        }
        _isTouchingLayer = false;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawLine((Vector2) transform.position + _startPos * transform.lossyScale, (Vector2)transform.position + (_startPos + _direction) * transform.lossyScale);
    }
}
