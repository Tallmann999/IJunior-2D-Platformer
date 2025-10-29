using System;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected AttackDetector AttackDetector;
    [SerializeField] protected int AttackDamage = 10;
    [SerializeField] protected float AttackCooldown = 1.5f;

    protected IDamageble CurrentTarget;
    protected bool HaveAttack;

    public event Action<bool> Attacked;

    protected virtual void OnEnable()
    {
        AttackDetector.TargetDetected += OnTargetDetected;
        AttackDetector.TargetLost += OnTargetLost;
        AttackDetector.TargetInRange += OnTargetInRange;
    }

    protected virtual void OnDisable()
    {
        AttackDetector.TargetDetected -= OnTargetDetected;
        AttackDetector.TargetLost -= OnTargetLost;
        AttackDetector.TargetInRange -= OnTargetInRange;
    }

    protected void PerformAttack()
    {
        if (AttackDetector == null)
            return;

        if (CurrentTarget == null)
            return;

        CurrentTarget.TakeDamage(AttackDamage);
    }

    protected void InvokeAttackEvent(bool value)
    {
        Attacked?.Invoke(value);
    }

    private void OnTargetInRange(IDamageble target)
    {
        HaveAttack = true;
        InvokeAttackEvent(HaveAttack);
    }

    private void OnTargetDetected(IDamageble target)
    {
        CurrentTarget = target;
        HaveAttack = true;
        InvokeAttackEvent(HaveAttack);
    }

    private void OnTargetLost(IDamageble target)
    {
        if (CurrentTarget == target)
            CurrentTarget = null;

        HaveAttack = false;
        InvokeAttackEvent(HaveAttack);
    }
}
