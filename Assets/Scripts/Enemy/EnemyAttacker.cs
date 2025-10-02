using System;
using TMPro;
using UnityEngine;

public class EnemyAttacker : Attacker
{
    //[SerializeField] private AttackDetector _attackDetector;
    //[SerializeField] private int _attackDamage = 10;
    //[SerializeField] private float _attackCooldown = 1.5f;

    //private IDamageble _currentTarget;
    //private bool _haveAttack;// может атаковать
    //public event Action<bool> Attacked;

    //private void OnEnable()
    //{
    //    _attackDetector.TargetDetected += OnTargetDetected;
    //    _attackDetector.TargetLost += OnTargetLost;
    //}

    //private void OnDisable()
    //{
    //    _attackDetector.TargetDetected -= OnTargetDetected;
    //    _attackDetector.TargetLost -= OnTargetLost;
    //}

    //private void OnTargetDetected(IDamageble target)
    //{
    //    if (_currentTarget == null)
    //        _currentTarget = target;

    //    _haveAttack = true;
    //    Attacked?.Invoke(_haveAttack);
    //    Debug.Log(_haveAttack);// если игрок находится в зоне тригера у него автоматом списываюся жизни 
       
    //}

    //private void OnTargetLost(IDamageble target)
    //{
    //    if (_currentTarget == target)
    //        _currentTarget = null;
    //    _haveAttack = false;
    //}


    //public void Attack()
    //{
    //    if (_currentTarget != null)
    //    {          

    //        _currentTarget.TakeDamage(_attackDamage);
    //        Debug.Log($"{gameObject.name} атаковал {_currentTarget} на {_attackDamage} урона");
    //    }
    //    else
    //    {
    //        Debug.Log("Нет цели в зоне атаки!");
    //    }
    //}
}
