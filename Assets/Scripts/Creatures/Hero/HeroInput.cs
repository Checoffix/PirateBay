using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    private HeroMovement _heroMovement;
    private HeroInteraction _heroInteraction;
    private HeroAttack _heroAttack;
    private HeroInventory _heroInventory;
    [SerializeField] private Cooldown _chargeAttackCooldown;

    private void Start()
    {
        _heroMovement = GetComponent<HeroMovement>();
        _heroInteraction = GetComponent<HeroInteraction>();
        _heroAttack = GetComponent<HeroAttack>();
        _heroInventory = GetComponent<HeroInventory>();
    }
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        var val = ctx.ReadValue<Vector2>();
        _heroMovement.SetDirection(val);
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _heroInteraction.Interact();
        }
    }
    public void OnHeal(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _heroInventory.Heal();
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        var val = ctx.ReadValue<float>();
        _heroMovement.SetJump(val);
    }
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _heroAttack.Attack();
        }
    }
    public void OnThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _chargeAttackCooldown.Reset();
            return;
        }
        if (ctx.canceled)
        {
            if (_chargeAttackCooldown.IsReady) StartCoroutine(_heroAttack.MultiThrow());
            else _heroAttack.Throw();
        }
    }
}
