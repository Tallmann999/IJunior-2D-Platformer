using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Flipper))]
[RequireComponent(typeof(PlayerAnimation), typeof(Health))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private PlayerAnimation _playerAnimation;
    private Health _playerHealth;
    private PlayerMover _playerMover;
    private InputReader _inputReader;
    private Flipper _flipper;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _waintingTime;

    private float _waitingTimer = 3f;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isDead;

    private void OnEnable()
    {
        SubscribeToEvents();

        _waintingTime = new WaitForSeconds(_waitingTimer);
    }

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _playerHealth = GetComponent<Health>();
        _flipper = GetComponent<Flipper>();
    }

    private void FixedUpdate()
    {
        UpdateAnimations();
        UpdateRotation();
        _playerMover.Move();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        if (_inputReader != null)
        {
            _inputReader.HorizontalMovement += OnHorizontalMovement;
            _inputReader.Jumping += OnJumping;
            _inputReader.Attacking += OnAttacking;
            _playerHealth.Hit += OnHit;
            _playerHealth.Die += OnDead;
        }

        if (_groundDetector != null)
        {
            _groundDetector.GroundedChanged += OnGroundedChanged;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (_inputReader != null)
        {
            _inputReader.HorizontalMovement -= OnHorizontalMovement;
            _inputReader.Jumping -= OnJumping;
            _inputReader.Attacking -= OnAttacking;
            _playerHealth.Hit -= OnHit;
            _playerHealth.Die -= OnDead;
        }

        if (_groundDetector != null)
        {
            _groundDetector.GroundedChanged -= OnGroundedChanged;
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

            _playerMover.Stop();
            _playerAnimation.TriggerDie();
            _currentCoroutine = StartCoroutine(DieActivator());
        }
    }

    public void OnHit()
    {
        if (_isDead)
            return;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(HitActivator());
    }

    private void OnHorizontalMovement(float horizontalDirection)
    {
        if (!_isDead)
        {
            _horizontalInput = horizontalDirection;
            _playerMover.SetHorizontalDirection(horizontalDirection);
        }
    }

    private void OnAttacking(bool isAttack)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(AttackActivator(isAttack));
    }

    private void OnJumping(bool shouldJump)
    {
        if (shouldJump && _isGrounded)
        {
            _playerMover.Jump();
            _playerAnimation.TriggerJump();
        }
    }

    private void OnGroundedChanged(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }

    private void UpdateAnimations()
    {
        _playerAnimation.SetSpeed(_horizontalInput);
        _playerAnimation.SetIsGrounded(_isGrounded);
        _playerAnimation.SetVerticalVelocity(_playerMover.Rigidbody2D.linearVelocity.y);
    }

    private void UpdateRotation()
    {
        float flipThreshold = 0.1f;

        if (_flipper != null && Mathf.Abs(_horizontalInput) > flipThreshold)
        {
            _flipper.Flip(_horizontalInput);
        }
    }

    private IEnumerator HitActivator()
    {
        _playerAnimation.TriggerHit();
        _playerMover.Stop();
        yield break;
    }

    private IEnumerator DieActivator()
    {
        yield return _waintingTime;
        Destroy(gameObject);
    }

    private IEnumerator AttackActivator(bool haveAttack)
    {
        if (haveAttack)
        {
            _playerMover.Stop();
            _playerAnimation.TriggerAttack();
            yield break;
        }
    }
}