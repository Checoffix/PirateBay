using UnityEngine;
using UnityEngine.UIElements;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float Speed;

    protected Rigidbody2D Rigidbody;
    protected int Direction;

    protected virtual void Start()
    {
        Direction = (int)transform.lossyScale.x;
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
