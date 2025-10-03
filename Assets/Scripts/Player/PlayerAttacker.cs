using System;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    //private bool _canAttack = true;

    //public void AttackCommand()
    //{
    //    if (_canAttack)
    //    {
    //        ForceAttack(); // ������� ����
    //        _canAttack = false;
    //        Invoke(nameof(ResetAttack), _attackCooldown); // �������
    //    }
    //}

    //private void ResetAttack()
    //{
    //    _canAttack = true;
    //}
    //[SerializeField] private AttackDetector _attackDetector;
    //[SerializeField] private int _attackDamage = 10;

    private IDamageble _currentTarget;
    private bool _haveAttack;

    //public event Action<bool> Attacked;

    private void OnEnable()
    {
        _attackDetector.TargetDetected += OnTargetDetected;
        _attackDetector.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _attackDetector.TargetDetected -= OnTargetDetected;
        _attackDetector.TargetLost -= OnTargetLost;
    }

    private void OnTargetDetected(IDamageble target)
    {
        // ���� ������ ���������� ���� (��� ��������� ��������)
        if (_currentTarget == null)
            _currentTarget = target;
        _haveAttack = true;
        //Attacked?.Invoke(_haveAttack);
    }

    private void OnTargetLost(IDamageble target)
    {
        if (_currentTarget == target)
            _currentTarget = null;
        _haveAttack = false;
    }

    public void Attack()
    {
        if (_currentTarget != null)
        {
            _currentTarget.TakeDamage(_attackDamage);
            Debug.Log($"{gameObject.name} �������� {_currentTarget} �� {_attackDamage} �����");
        }
        else
        {
            Debug.Log("��� ���� � ���� �����!");
        }
    }
}
