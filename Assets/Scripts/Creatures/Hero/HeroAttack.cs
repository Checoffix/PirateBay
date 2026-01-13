using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class HeroAttack : CreatureAttack
{
    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _unarmed;
    [SerializeField] private float _swordQueueCooldown;

    private HeroInventory _heroInventory;
    private GameSession _gameSession;
    protected override void Start()
    {
        base.Start();
        _gameSession = FindFirstObjectByType<GameSession>();
        _gameSession.Data.Inventory.OnChanged += OnInventoryChanged;
        _heroInventory = GetComponent<HeroInventory>();
        UpdateHeroWeapon();
    }

    private void OnDestroy()
    {
        _gameSession.Data.Inventory.OnChanged -= OnInventoryChanged;
    }

    private void OnInventoryChanged(string id, int value)
    {
        if (id == "Sword") UpdateHeroWeapon();
    }
    public override void Attack()
    {
        if (_heroInventory.Count("Sword") == 0) return;
        base.Attack();
    }

    public void ArmHero()
    {
        if (_heroInventory.Count("Sword") == 0)
        {
            UpdateHeroWeapon();
        }
        _heroInventory.Add("coin", 1);
    }
    public void DisarmHero()
    {
        _heroInventory.SetValue("Sword", 0);
        UpdateHeroWeapon();
    }
    public void UpdateHeroWeapon()
    {
        if (_heroInventory.Count("Sword") != 0)
        {
            Animator.runtimeAnimatorController = _armed;
        }
        else
        {
            Animator.runtimeAnimatorController = _unarmed;
        }
    }
    public override void Throw()
    {
        if (_heroInventory.Count("Sword") > 1)
        {
            base.Throw();
        }
    }
    public IEnumerator MultiThrow()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_heroInventory.Count("Sword") > 1)
            {
                base.ThrowWithoutCooldown();
                yield return new WaitForSeconds(_swordQueueCooldown);
            }
            else break;
        }
    }

    public override void OnDoThrow()
    {
        base.OnDoThrow();
        _heroInventory.Remove("Sword", 1);
    }
}
