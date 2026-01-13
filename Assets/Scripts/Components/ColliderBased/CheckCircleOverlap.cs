using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PixelCrew.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private string[] _tags;
    [SerializeField] private UnityEvent<GameObject> _onOverlap;

    public void Check()
    {
        var size = Physics2D.OverlapCircleAll(transform.position, _radius, _mask);
        if (_tags.Length != 0) {
            for (int i = 0; i < size.Length; i++)
            {
                if (_tags.Any(tag => size[i].CompareTag(tag)))
                {
                    _onOverlap?.Invoke(size[i].gameObject);
                }
            }
        }
        else if (size.Length != 0) _onOverlap?.Invoke(size[0].gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentGreen;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }
}
