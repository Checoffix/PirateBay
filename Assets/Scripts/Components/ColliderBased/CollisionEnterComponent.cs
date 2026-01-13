using UnityEngine;
using UnityEngine.Events;

public class CollisionEnterComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent<GameObject> _action;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tag))
        {
            _action?.Invoke(collision.gameObject);
        }
    }
}
