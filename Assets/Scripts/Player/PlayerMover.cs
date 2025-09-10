using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField]private float _speedMovement;
    [SerializeField]private float _jumpHeight;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private bool _isMoving;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _isMoving;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void Move()
    {
        _direction.x = Input.GetAxis("Horizontal");
       _rigidbody2D.linearVelocity = new Vector2(_direction.x * _speedMovement, _rigidbody2D.linearVelocity.y);
    }

    public void Jump()
    {
        _isGrounded = false;
        _rigidbody2D.AddForce(Vector2.up* _jumpHeight,ForceMode2D.Impulse);
    }
}
