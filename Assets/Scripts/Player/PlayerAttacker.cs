using System;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField]private AttackDetector _attackDetector;
    [SerializeField] private int _attackDamage;

    private IDamageble _currentTarget;

    //private void Awake()
    //{
    //    _attackDetector = GetComponent<AttackDetector>();
    //}

    private void OnEnable()
    {
        _attackDetector.TargetDetected += OnAttack;
    }

    private void OnDisable()
    {
        _attackDetector.TargetDetected -= OnAttack;
    }

    private void OnAttack(IDamageble damageble)
    {
        _currentTarget = damageble;
    }

    public void Attack()
    {
        if (_currentTarget != null)
        {
        _currentTarget.TakeDamage(_attackDamage);
        Debug.Log($"{gameObject.name} атаковал {_currentTarget} на {_attackDamage} урона");

        }
    }

}
