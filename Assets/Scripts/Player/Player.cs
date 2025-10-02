using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerNeeds))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private PlayerAnimation _playerAnimation;
    private PlayerAttacker _playerAttacker;
    private PlayerNeeds _playerNeeds;
    private PlayerMover _playerMover;
    private InputReader _inputReader;
    private Flipper _flipper;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _waintingTime;

    private float _waitingTimer = 2f;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _haveAttack;
    private bool _isDead;

    //public Action<bool> OnDie { get; private set; }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _playerNeeds = GetComponent<PlayerNeeds>();
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
        _waintingTime = new WaitForSeconds(_waitingTimer);

        if (_inputReader != null)
        {
            _playerAttacker.Attacked += OnAttack;
            _inputReader.HorizontalMovement += OnHorizontalMovement;
            _inputReader.Jumping += OnJumping;
            _inputReader.Attacking += OnAttacking;
            _playerNeeds.Hit += OnHit;
            _playerNeeds.Die += OnDead;
        }

        if (_groundDetector != null)
        {
            _groundDetector.GroundedChanged += OnGroundedChanged;
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
            StartCoroutine(OnDie());
        }
    }

    private IEnumerator OnDie()
    {

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
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
        _playerAnimation.TriggerHit();
        _playerMover.Stop();
        yield return _waintingTime;
    }

    private void UnsubscribeFromEvents()
    {
        if (_inputReader != null)
        {
            _playerAttacker.Attacked -= OnAttack;
            _inputReader.HorizontalMovement -= OnHorizontalMovement;
            _inputReader.Jumping -= OnJumping;
            _inputReader.Attacking -= OnAttacking;
            _playerNeeds.Hit -= OnHit;
            _playerNeeds.Die -= OnDead;

        }


        if (_groundDetector != null)
        {
            _groundDetector.GroundedChanged -= OnGroundedChanged;
        }
    }

    private void OnAttack(bool haveAttack)
    {
        _haveAttack = haveAttack;

    }

    private void OnHorizontalMovement(float horizontalDirection)
    {
        if (!_isDead)
        {
            _horizontalInput = horizontalDirection;
            _playerMover.SetHorizontalDirection(horizontalDirection);
        }
    }

    //private void  OnAttack(bool haveAttack)
    //{
    //    _haveAttack= haveAttack;
    //}

    private void OnAttacking(bool isAttack)
    {
        Debug.Log($"атака кнопка {isAttack}, атака Попадание в тригер {_haveAttack}");
        if (isAttack)
        {
            StartCoroutine(AttackSequence(isAttack));
        }

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

    private IEnumerator AttackSequence(bool haveAttack)
    {
        if (haveAttack && !_haveAttack)
        {
            _haveAttack = true;
            _playerMover.Stop();
            _playerAnimation.TriggerAttack();
            yield return new WaitForSeconds(0.5f);

            _playerAttacker.AttackCommand();
            _haveAttack = false;
        }

    }
}