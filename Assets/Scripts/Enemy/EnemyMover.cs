using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            MoveToTarget();
            Flip();
        }
    }

    private void Flip()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _spriteRenderer.flipX = direction.x < 0;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MoveToTarget()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        Vector2 movement = direction * _movementSpeed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + movement);
    }
}