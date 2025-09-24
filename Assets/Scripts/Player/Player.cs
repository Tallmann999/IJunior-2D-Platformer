using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    //[SerializeField] private AttackDetector _attackDetector;

    private PlayerAnimation _playerAnimation;
    private PlayerAttacker _playerAttacker;
    private InputReader _inputReader;
    private Flipper _flipper;
    private PlayerMover _playerMover;
    private float _horizontalInput;
    private bool _isGrounded;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _flipper = GetComponent<Flipper>();
        _playerAttacker = GetComponent<PlayerAttacker>();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void Update()
    {
        UpdateAnimations();
        UpdateRotation();
    }

    private void FixedUpdate()
    {
        _playerMover.Move();
    }

    private void SubscribeToEvents()
    {
        if (_inputReader != null)
        {
            _inputReader.HorizontalMovement += OnHorizontalMovement;
            _inputReader.Jumping += OnJumping;
            _inputReader.Attacking += OnAttacking;
            
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
        }

        //if(_attackDetector!=null)
        //{
        //    _attackDetector.Attacked = 
        //}

        if (_groundDetector != null)
        {
            _groundDetector.GroundedChanged -= OnGroundedChanged;
        }
    }

    private void OnHorizontalMovement(float horizontalDirection)
    {
        _horizontalInput = horizontalDirection;
        _playerMover.SetHorizontalDirection(horizontalDirection);
    }

    private void OnAttacking(bool isAttack)
    {
        if (isAttack)
        {
            _playerAnimation.TriggerAttack();
            Debug.Log("Анимация работает");
            _playerAttacker.Attack();
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
}