using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyPatrol))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _enemyMover;
    private EnemyPatrol _enemyPatrol;
    private Transform _currentTarget;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _waintingTime;
    private bool _isWaiting;
    private float _waitingTimer;

    private void OnEnable()
    {
        _waitingTimer = _enemyPatrol.WaitingTime;
        _waintingTime = new WaitForSeconds(_waitingTimer);

        _currentTarget = _enemyPatrol.GetCurrentTarget();
        _enemyMover.SetTarget(_currentTarget);
    }

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void FixedUpdate()
    {
        MoveToPoint();
    }

    private void MoveToPoint()
    {
        if (_currentTarget == null)
        {
            if (_enemyPatrol.HasPoints)
            {
                _currentTarget = _enemyPatrol.GetCurrentTarget();
                _enemyMover.SetTarget(_currentTarget);
            }
        }

        Vector3 direction = (_currentTarget.position - transform.position);
        // здесь нужно прописывать переключение на цель  и преследование за игроком. Через скрипт FolowerDetector
        // потом работа с AttackDetector который выявляет что персонаж попадает в коллайдер EnemyAttacker атакует его 
        // и активизируется анимация атаки 
        float sqrDistance = direction.sqrMagnitude;
        float threshold = _enemyPatrol.ArrivalThreshold;
        float multiplicationThreshold = threshold * threshold;

        if (sqrDistance <= multiplicationThreshold)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(HeandleReacherPoint());
        }
    }

    private IEnumerator HeandleReacherPoint()
    {
        _isWaiting = true;
        _enemyMover.Stop();
        yield return _waintingTime;

        _currentTarget = _enemyPatrol.AdvanceToNext();
        _enemyMover.SetTarget(_currentTarget);
        _isWaiting = false;
    }
}