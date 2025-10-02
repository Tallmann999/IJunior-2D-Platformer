using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyDetector))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _enemyMover;
    private EnemyPatrol _enemyPatrol;
    private EnemyAnimation _enemyAnimation;
    private EnemyAttacker _enemyAttacker;
    private EnemyDetector _enemyDetector;
    private EnemyNeeds _enemyNeeds;
    private Transform _currentTarget;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _waintingTime;
    private bool _isDead;
    private bool _isWaiting;
    private bool _isChasing;
    private float _waitingTimer = 2f;
    private float _horizontalMover;

    private void OnEnable()
    {
        _waitingTimer = _enemyPatrol.WaitingTime;
        _waintingTime = new WaitForSeconds(_waitingTimer);

        _currentTarget = _enemyPatrol.GetCurrentTarget();
        _enemyMover.SetTarget(_currentTarget);
        _enemyMover.HorizontalMovement += OnHorizontalMove;
        _enemyNeeds.Hit += OnHit;
        _enemyNeeds.Die += OnDead;
        _enemyAttacker.Attacked += Attack;

        _enemyDetector.PlayerDetected += OnPlayerDetected;
        _enemyDetector.PlayerLost += OnPlayerLost;

    }

    private void OnPlayerDetected(Transform player)
    {
        //if (_isDead) // проверить будет ли преследовать игрока?????????
        //    return;
       
        _isChasing = true;
        _currentTarget = player;
        _enemyMover.SetTarget(_currentTarget);
    }

    private void OnPlayerLost()
    {
        //if (_isDead) // проверить будет ли преследовать игрока?????????
        //    return;

        _isChasing = false;
        _currentTarget = _enemyPatrol.GetCurrentTarget(); // возвращаемся к патрулю
        _enemyMover.SetTarget(_currentTarget);
    }

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyNeeds = GetComponent<EnemyNeeds>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _enemyDetector = GetComponent<EnemyDetector>();
    }

    private void OnDisable()
    {
        _enemyMover.HorizontalMovement -= OnHorizontalMove;
        _enemyNeeds.Hit -= OnHit;
        _enemyAttacker.Attacked -= Attack;

    }

    private void FixedUpdate()
    {
        if (_isDead)
            return;

        if (!_isChasing)
        {
            MoveToPoint();

        }

        UpdateAnimation();
    }

    public void OnHit()
    {
        if (_isDead)
            return;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(OnHitActivation());
    }

    private IEnumerator OnHitActivation()
    {
        _enemyAnimation.TriggerHit();
        _enemyMover.Stop();
        yield return _waintingTime;

        if (!_isDead)
        {
            _enemyMover.SetTarget(_currentTarget);

        }

    }



    private void Attack(bool haveAttack)
    {
        if (haveAttack)
        {// Завести счётчик прошла атака или нет. или сделать корутину на удары колличество 
            Debug.Log("Атака врага производится");
            _enemyAttacker.TryAttack();
            _enemyAnimation.TriggerAttack();
        }
    }



    private void OnHorizontalMove(float horizontalMove)
    {
        _horizontalMover = horizontalMove;
    }

    private void UpdateAnimation()
    {
        _enemyAnimation.SetSpeed(_horizontalMover);
    }

    private void MoveToPoint()
    {
        if (_isDead)
            return;

        if (_currentTarget == null)
        {
            if (_enemyPatrol.HasPoints)
            {
                _currentTarget = _enemyPatrol.GetCurrentTarget();
                _enemyMover.SetTarget(_currentTarget);
            }
        }

        Vector3 direction = (_currentTarget.position - transform.position);
        float sqrDistance = direction.sqrMagnitude;
        float threshold = _enemyPatrol.ArrivalThreshold;
        float multiplicationThreshold = threshold * threshold;

        if (sqrDistance <= multiplicationThreshold)
        {
            //if (_currentCoroutine != null)
            //{
            //    StopCoroutine(_currentCoroutine);
            //}

            //_currentCoroutine = StartCoroutine(HeandleReacherPoint());
            StartCoroutine(HeandleReacherPoint());
        }
    }

    private void OnDead(bool IsDie)
    {
        if (IsDie && !_isDead)
        {
            _isDead = true;

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _enemyMover.Stop();
            _enemyAnimation.TriggerDie();
            StartCoroutine(OnDie());
        }
    }

    private IEnumerator OnDie()
    {

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private IEnumerator HeandleReacherPoint()
    {
        if (_isDead)
            yield break;

        _isWaiting = true;
        _enemyMover.Stop();
        yield return _waintingTime;

        if (!_isDead)
        {
            _currentTarget = _enemyPatrol.AdvanceToNext();
            _enemyMover.SetTarget(_currentTarget);
        }
        _isWaiting = false;
    }
}