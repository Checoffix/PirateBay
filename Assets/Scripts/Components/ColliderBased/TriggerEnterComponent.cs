using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private UnityEvent<GameObject> _action;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.IsInLayer(_layer))
        {
            _action?.Invoke(collider.gameObject);
        }
        else if (!string.IsNullOrEmpty(_tag) && collider.CompareTag(_tag))
        {
            _action?.Invoke(collider.gameObject);
        }
    }
}
