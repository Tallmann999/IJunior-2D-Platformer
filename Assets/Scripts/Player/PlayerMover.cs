using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMover : MonoBehaviour
{    // отдельно вывести GroundChek  
    [SerializeField] private float _speedMovement = 8f;
    [SerializeField] private float _jumpHeight = 12f;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2D;
    private PlayerAnimation _playerAnimation;
    private InputReader _inputReader;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isMoving;

    //public bool IsGrounded => _isGrounded;
    //public bool IsMoving => _isMoving;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalMovement += OnHorizontalVelocity;
        _inputReader.Jumping += OnJumping;
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.HorizontalMovement -= OnHorizontalVelocity;
            _inputReader.Jumping -= OnJumping;
        }
    }

    private void Update()
    {
        CheckGround();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Обработчик события движения
    private void OnHorizontalVelocity(float horizontalDirection)
    {
        _horizontalInput = horizontalDirection;
        _isMoving = Mathf.Abs(horizontalDirection) > 0.1f;
    }

    // Обработчик события прыжка
    private void OnJumping(bool shouldJump)
    {
        if (shouldJump && _isGrounded)
        {
            Jump();
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
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
        }
        else
        {
            // Останавливаем горизонтальное движение, но сохраняем вертикальное (гравитацию)
            _rigidbody2D.linearVelocity = new Vector2(0f, _rigidbody2D.linearVelocity.y);
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
        _playerAnimation.TriggerJump();
    }

    // Для визуализации в редакторе
    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }

}