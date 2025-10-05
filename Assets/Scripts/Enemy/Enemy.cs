using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Health))]
[RequireComponent(typeof(EnemyPatrol), typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyAttacker), typeof(EnemyDetector))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _enemyMover;
    private EnemyPatrol _enemyPatrol;
    private EnemyAnimation _enemyAnimation;
    private EnemyAttacker _enemyAttacker;
    private EnemyDetector _enemyDetector;
    private Health _enemyHealth;

    private Transform _currentTarget;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _waintingTimer;
    private WaitForSeconds _waintingTimeDie;
    private bool _isDead;
    private bool _isWaiting;
    private bool _isChasing;
    private float _waitingTimer = 2f;
    private float _waitingTimerDie = 3f;
    private float _horizontalMover;

    private void OnEnable()
    {
        ActivationEvents();

        _currentTarget = _enemyPatrol.GetCurrentTarget();
        _enemyMover.SetTarget(_currentTarget);
    }

    private void Awake()
    {
        _waintingTimer = new WaitForSeconds(_waitingTimer);
        _waintingTimeDie = new WaitForSeconds(_waitingTimerDie);

        _enemyMover = GetComponent<EnemyMover>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyHealth = GetComponent<Health>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _enemyDetector = GetComponent<EnemyDetector>();
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

    private void OnDisable()
    {
        DeactivationEvents();
    }

    private void ActivationEvents()
    {
        _enemyMover.HorizontalMovement += OnHorizontalMove;
        _enemyHealth.Hit += OnHitActivator;
        _enemyHealth.Die += OnDieActivator;
        _enemyAttacker.Attacked += OnAttackActivator;
        _enemyDetector.PlayerDetected += OnPlayerDetected;
        _enemyDetector.PlayerLost += OnPlayerLost;
    }

    private void DeactivationEvents()
    {
        _enemyMover.HorizontalMovement -= OnHorizontalMove;
        _enemyHealth.Hit -= OnHitActivator;
        _enemyHealth.Die -= OnDieActivator;
        _enemyAttacker.Attacked -= OnAttackActivator;
        _enemyDetector.PlayerDetected -= OnPlayerDetected;
        _enemyDetector.PlayerLost -= OnPlayerLost;
    }

    private void OnPlayerDetected(Transform player)
    {
        _isChasing = true;
        _currentTarget = player;
        _enemyMover.SetTarget(_currentTarget);
    }

    private void OnPlayerLost()
    {
        _isChasing = false;
        _currentTarget = _enemyPatrol.GetCurrentTarget();
        _enemyMover.SetTarget(_currentTarget);
    }

    private void OnHitActivator()
    {
        if (_isDead)
            return;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(OnHit());
    }

    private void OnDieActivator(bool IsDie)
    {
        if (IsDie && !_isDead)
        {
            _isDead = true;
            _enemyMover.Die();
            _enemyAnimation.TriggerDie();

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

             StartCoroutine(OnDie());
        }
    }

    private void OnAttackActivator(bool haveAttack)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(OnAttack(haveAttack));
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

        if (sqrDistance <= multiplicationThreshold && !_isWaiting)
        {
            _isWaiting = true;

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(HeandleReacherPoint());
        }
    }

    private IEnumerator OnAttack(bool haveAttack)
    {
        if (haveAttack)
        {
            _enemyAnimation.TriggerAttack();
            yield return _waintingTimer;
            _enemyAttacker.ForceAttack();
        }
    }

    private IEnumerator OnHit()
    {
        _enemyAnimation.TriggerHit();
        _enemyMover.Stop();
        yield return _waintingTimer;

        if (!_isDead)
        {
            _enemyMover.SetTarget(_currentTarget);
        }
    }

    private IEnumerator OnDie()
    {
        yield return _waintingTimeDie;
        Destroy(gameObject);
    }

    private IEnumerator HeandleReacherPoint()
    {
        if (_isDead)
        {
            yield break;
        }

        _isWaiting = true;
        yield return _waintingTimer;

        if (!_isDead)
        {
            _currentTarget = _enemyPatrol.AdvanceToNext();
            _enemyMover.SetTarget(_currentTarget);
        }

        _isWaiting = false;
    }
}