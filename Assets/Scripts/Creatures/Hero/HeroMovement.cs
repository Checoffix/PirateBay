using UnityEngine;
using UnityEngine.EventSystems;

public class HeroMovement : CreatureMovement
{
    [SerializeField] private float _slamDownVelocity;
    [SerializeField] private ColliderLayerCheck _wallCheck;
    private GameSession _gameSession;
    private bool _allowDoubleJump = true;
    private bool _jumpHandled;
    private bool _isOnWall;
    private float _defaultGravityScale;

    private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

    protected override void Start()
    {
        base.Start();
        _defaultGravityScale = Rigidbody.gravityScale;
        _gameSession = FindFirstObjectByType<GameSession>();
        GetComponent<HealthComponent>().SetHp(_gameSession.Data.hp);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_wallCheck.IsTouchingLayer && Movement.x * transform.localScale.x > 0)
        {
            _isOnWall = true;
            Rigidbody.gravityScale = 0;
        }
        else
        {
            _isOnWall = false;
            Rigidbody.gravityScale = _defaultGravityScale;
        }
        _animator.SetBool(IsOnWall, _isOnWall);
    }
    public override void SetJump(float movement)
    {
        Movement.y = movement * Speed;
        if (Movement.y > 0)
        {
            if (!IsJumping)
            {
                _jumpHandled = false;
            }
            IsJumping = true;
        }
        else IsJumping = false;
    }

    protected override float CalculateYVelocity()
    {
        float velocityY = Rigidbody.linearVelocityY;
        if (IsGrounded || _isOnWall)
        {
            _allowDoubleJump = true;
        }
        if (IsJumping)
        {
            if (!_jumpHandled)
            {
                velocityY = CalculateJumpVelocity(velocityY);
            }
        }
        else if (velocityY > 0) velocityY /= 2f;
        return velocityY;
    }
    protected override float CalculateJumpVelocity(float velocityY)
    {
        bool isFalling = velocityY <= 0.001f;
        if (!isFalling) return velocityY;
        if (IsGrounded)
        {
            _jumpHandled = true;
            velocityY += JumpForce;
            SpawnJumpDust();
        }
        else if (_allowDoubleJump && !_isOnWall)
        {
            _jumpHandled = true;
            velocityY = JumpForce;
            _allowDoubleJump = false;
            SpawnJumpDust();
        }
        return velocityY;
    }
    public void OnHpChanged(int currentHp)
    {
        _gameSession.Data.hp = currentHp;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.IsInLayer(GroundLayer))
        {
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y >= _slamDownVelocity || !_allowDoubleJump)
            {
                SpawnLandDust();
            }
        }
    }
}
