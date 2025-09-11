using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private bool _isMoving = false;
    private bool _isGrounded;
    private float _rayLength = 0.2f;

    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetIsGroundedStatus();
    }

    public void Move()
    {
        Flip();
        _isMoving = true;
        _direction.x = Input.GetAxis(Horizontal);
        _rigidbody2D.linearVelocity = new Vector2(_direction.x * _speedMovement, _rigidbody2D.linearVelocity.y);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
    }

    private void SetIsGroundedStatus()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkPoint.position, _rayLength);
        _isGrounded = colliders.Length > 1;
    }

    private void Flip()
    {
        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
