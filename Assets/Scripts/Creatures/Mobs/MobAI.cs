using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private SpawnComponent _particles;
    [SerializeField] private ColliderLayerCheck _vision;
    [SerializeField] private ColliderLayerCheck _canAtack;
    [SerializeField] private float _alarmDelay;
    [SerializeField] private float _attackCooldown;

    private Patrol _patrol;
    private Coroutine _current;
    private GameObject _target;
    private CreatureMovement _movement;
    private CreatureAttack _attack;
    [SerializeField] private bool _isDead;
    private CapsuleCollider2D _collider;


    private void Awake()
    {
        _movement = GetComponent<CreatureMovement>();
        _attack = GetComponent<CreatureAttack>();
        _patrol = GetComponent<Patrol>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        StartState(_patrol.DoPatrol());
    }

    public void OnHeroInVision(GameObject go)
    {
        if (_isDead) return;
        _target = go;

        StartState(AgroToHero());
    }

    private IEnumerator AgroToHero()
    {
        _movement.SetDirection(Vector2.zero);
        _particles.Spawn("exclamation");
        yield return new WaitForSeconds(_alarmDelay);

        StartState(GoToHero());
    }

    private IEnumerator GoToHero()
    {
        while (_vision.IsTouchingLayer)
        {
            if (_canAtack.IsTouchingLayer) StartState(Attack());
            else SetDirectionToTarget();
            yield return null;
        }

        StartState(Interrogation());
    }

    private IEnumerator Interrogation()
    {
        _movement.SetDirection(Vector2.zero);
        _particles.Spawn("interrogation");
        yield return new WaitForSeconds(_alarmDelay);

        StartState(_patrol.DoPatrol());
    }

    private IEnumerator Attack()
    {
        while (_canAtack.IsTouchingLayer)
        {
            _attack.Attack();
            yield return new WaitForSeconds(_attackCooldown);
        }

        StartState(GoToHero());
    }

    private void SetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        _movement.SetDirection(direction.normalized);
    }
    private void StartState(IEnumerator coroutine)
    {
        if (_current != null) StopCoroutine(_current);
        if (!_isDead) _current = StartCoroutine(coroutine);
    }

    public void OnDie()
    {
        if (_current != null) StopCoroutine(_current);
        _isDead = true;
        _movement.SetDirection(Vector2.zero);
    }
}
