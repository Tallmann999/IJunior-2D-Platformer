using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _speedMovement = 8f;
    [SerializeField] private float _jumpHeight = 12f;

    private Rigidbody2D _rigidbody2D;
    private PlayerAnimation _playerAnimation;
    private InputReader _inputReader;
    private Rotation _rotation;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isMoving;

    private void OnEnable()
    {
        _inputReader.HorizontalMovement += OnHorizontalVelocity;
        _groundDetector.GroundedChanged += OnGrounded;
        _inputReader.Jumping += OnJumping;
    }

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inputReader = GetComponent<InputReader>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rotation = GetComponent<Rotation>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateAnimations();
    }

    private void OnDisable()
    {
        _inputReader.HorizontalMovement -= OnHorizontalVelocity;
        _groundDetector.GroundedChanged -= OnGrounded;
        _inputReader.Jumping -= OnJumping;
    }

    private void OnHorizontalVelocity(float horizontalDirection)
    {
        _horizontalInput = horizontalDirection;
        _isMoving = Mathf.Abs(horizontalDirection) > 0.1f;
    }

    private void OnJumping(bool shouldJump)
    {
        if (shouldJump && _isGrounded)
        {
            Jump();
        }
    }

    private void OnGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }

    private void UpdateAnimations()
    {
        _playerAnimation.SetSpeed(_horizontalInput);
        _playerAnimation.SetIsGrounded(_isGrounded);
        _playerAnimation.SetVerticalVelocity(_rigidbody2D.linearVelocity.y);
    }

    private void Move()
    {
        if (_isMoving)
        {
            Vector2 movement = new Vector2(_horizontalInput * _speedMovement, _rigidbody2D.linearVelocity.y);
            _rigidbody2D.linearVelocity = movement;
            _rotation.RotationInspector(_horizontalInput);
        }
        else
        {
            _rotation.RotationInspector(_horizontalInput);
            _rigidbody2D.linearVelocity = new Vector2(0f, _rigidbody2D.linearVelocity.y);
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
        _playerAnimation.TriggerJump();
    }
}