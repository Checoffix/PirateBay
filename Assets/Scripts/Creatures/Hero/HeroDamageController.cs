using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HeroDamageController : CreatureDamageController
{
    [SerializeField] protected ParticleSystem _particleSystem;
    private HeroInventory _heroInventory;
    private int _money;
    protected override void Start()
    {
        base.Start();
        _heroInventory = GetComponent<HeroInventory>();
    }
    public override void OnHit()
    {
        var coinCount = _heroInventory.Count("coin");
        if (coinCount > 0)
        {
            _money = Math.Min(coinCount, 5);
            var burst = _particleSystem.emission.GetBurst(0);
            burst.count = _money;
            _particleSystem.emission.SetBurst(0, burst);
            _particleSystem.gameObject.SetActive(true);
            _heroInventory.Remove("coint", _money);
            _particleSystem.Play();

        }
        base.OnHit();
    }
}
