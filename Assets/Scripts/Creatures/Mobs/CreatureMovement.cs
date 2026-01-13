using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float JumpForce;
    [SerializeField] private float _damageVelocity;
    [SerializeField] protected LayerMask GroundLayer;
    [SerializeField] protected ColliderLayerCheck GroundCheck;
    [SerializeField] private SpawnComponent _particles;
    protected Vector2 Movement;

    protected Rigidbody2D Rigidbody;
    protected Animator _animator;
    protected bool IsGrounded;
    protected bool IsJumping;
    protected PlaySoundsComponent _playSounds;

    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int IsGround = Animator.StringToHash("is-grounded");
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");


    protected virtual void Start()
    {
        _playSounds = GetComponent<PlaySoundsComponent>();
        _animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    public virtual void SetJump(float movement)
    {
        Movement.y = movement * Speed;
        if (Movement.y > 0)
        {
            IsJumping = true;
        }
        else IsJumping = false;
    }

    public void SetDirection(Vector2 movement)
    {
        SetJump(movement.y);
        Movement.x = movement.x * Speed;
        if (Movement.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (Movement.x > 0) transform.localScale = Vector3.one;
    }

    protected virtual void FixedUpdate()
    {
        _animator.SetBool(IsRunning, Movement.x != 0);
        _animator.SetBool(IsGround, IsGrounded);
        _animator.SetFloat(VerticalVelocity, Rigidbody.linearVelocityY);
        float velocityX = Movement.x;
        float velocityY = CalculateYVelocity();
        Rigidbody.linearVelocity = new Vector2(velocityX, velocityY);
        IsGrounded = GroundCheck.IsTouchingLayer;
    }

    protected virtual float CalculateYVelocity()
    {
        float velocityY = Rigidbody.linearVelocityY;
        if (IsJumping)
        {
            velocityY = CalculateJumpVelocity(velocityY);
        }
        else if (velocityY > 0) velocityY /= 2f;
        return velocityY;
    }
    protected virtual float CalculateJumpVelocity(float velocityY)
    {
        bool isFalling = velocityY <= 0.001f;
        if (!isFalling) return velocityY;
        if (IsGrounded)
        {
            velocityY += JumpForce;
            SpawnJumpDust();
        }
        return velocityY;
    }
    public void OnHit()
    {
        Rigidbody.linearVelocityY += _damageVelocity;
    }
    public void SpawnFootDust()
    {
        _particles.Spawn("run");
    }
    public void SpawnJumpDust()
    {
        _playSounds.Play("Jump");
        _particles.Spawn("jump");
    }
    public void SpawnLandDust()
    {
        _particles.Spawn("land");
    }
}
