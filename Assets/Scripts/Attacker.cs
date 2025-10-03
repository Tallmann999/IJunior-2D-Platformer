using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected AttackDetector _attackDetector;
    [SerializeField] protected int _attackDamage = 10;
    [SerializeField] protected float _attackCooldown = 1.5f;

    protected float _lastAttackTime;
    protected IDamageble _currentTarget;
    protected bool _haveAttack = false;// может атаковать
    public event Action<bool> Attacked;

    protected void OnEnable()
    {
        _attackDetector.TargetDetected += OnTargetDetected;
        _attackDetector.TargetLost += OnTargetLost;
    }

    protected void OnDisable()
    {
        _attackDetector.TargetDetected -= OnTargetDetected;
        _attackDetector.TargetLost -= OnTargetLost;
    }

    private void OnTargetDetected(IDamageble target)
    {
        _currentTarget = target;
        _haveAttack = true;
        Attacked?.Invoke(_haveAttack);
    }

    private void OnTargetLost(IDamageble target)
    {
        if (_currentTarget == target)
            _currentTarget = null;

        _haveAttack = false;
        Attacked?.Invoke(_haveAttack);

    }

    public void TryAttack()
    {
        if (_currentTarget == null)
            return;

        if (Time.time - _lastAttackTime < _attackCooldown)
            return;

        _lastAttackTime = Time.time;

        PerformAttack();
    }

    public void ForceAttack()
    {
        if (_currentTarget != null)
        {
            PerformAttack();
        }
    }

     protected void PerformAttack()
    {
        _currentTarget.TakeDamage(_attackDamage);
        Debug.Log($"{gameObject.name} атаковал {_currentTarget} на {_attackDamage} урона");
    }
}
