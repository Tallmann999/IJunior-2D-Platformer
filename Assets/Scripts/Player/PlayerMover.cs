using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 8f;
    [SerializeField] private float _jumpHeight = 12f;

    private Rigidbody2D _rigidbody2D;
    private float _horizontalInput;
    private bool _isMoving;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetHorizontalDirection(float horizontalDirection)
    {
        _horizontalInput = horizontalDirection;
        _isMoving = Mathf.Abs(horizontalDirection) > 0.1f;
    }

    public void Move()
    {
        if (_isMoving)
        {
            Vector2 movement = new Vector2(_horizontalInput * _speedMovement, _rigidbody2D.linearVelocity.y);
            _rigidbody2D.linearVelocity = movement;
        }
        else
        {
            _rigidbody2D.linearVelocity = new Vector2(0f, _rigidbody2D.linearVelocity.y);
        }
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
    }
}