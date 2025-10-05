using System;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected AttackDetector AttackDetector;
    [SerializeField] protected int AttackDamage = 10;
    [SerializeField] protected float AttackCooldown = 1.5f;

    protected float LastAttackTime;
    protected IDamageble CurrentTarget;
    protected bool HaveAttack = false;
    public event Action<bool> Attacked;

    protected virtual void OnEnable()
    {
        AttackDetector.TargetDetected += OnTargetDetected;
        AttackDetector.TargetLost += OnTargetLost;
    }

    protected virtual void OnDisable()
    {
        AttackDetector.TargetDetected -= OnTargetDetected;
        AttackDetector.TargetLost -= OnTargetLost;
    }

    protected  void PerformAttack()
    {
        if (AttackDetector == null)
            return;

        if (CurrentTarget == null)
            return;

            CurrentTarget.TakeDamage(AttackDamage);
    }

    public void ForceAttack()
    {
        if (CurrentTarget != null)
        {
            PerformAttack();
        }
    }

    private void OnTargetDetected(IDamageble target)
    {
        CurrentTarget = target;
        HaveAttack = true;
        Attacked?.Invoke(HaveAttack);
    }

    private void OnTargetLost(IDamageble target)
    {
        if (CurrentTarget == target)
            CurrentTarget = null;

        HaveAttack = false;
        Attacked?.Invoke(HaveAttack);
    }
}
