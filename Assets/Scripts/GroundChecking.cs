using Assets.Scripts.Components.Extensions;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _collider;
    private float _startAirTime;
    public bool _isGrounded;
    public float _airTime;
    private void OnTriggerEnter2D(Collider2D val)
    {
        _isGrounded = _collider.IsTouchingLayers(_groundLayer);
        if (_isGrounded)
        {
            _airTime = Time.time - _startAirTime;
        }
    }
    private void OnTriggerExit2D(Collider2D val)
    {
        _isGrounded = _collider.IsTouchingLayers(_groundLayer);
        if (!_isGrounded)
        {
            _startAirTime = Time.time;
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(_groundLayerMask))
        {
            var contact = collision.contacts[0];
            if (contact.relativeVelocity.y >= _minLandVelocity || !_allowDoubleJump)
            {
                SpawnLandDust();
            }
        }
    }
    */
}
