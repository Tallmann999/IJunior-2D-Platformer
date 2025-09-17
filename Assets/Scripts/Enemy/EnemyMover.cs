using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private Flipper _flipper;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Stop()
    {
        _target = null;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    private void Move()
    {
        if (_target == null) return;

        Vector2 currentPosition = _rigidbody.position;
        Vector2 targetPosition = _target.position;
        Vector2 nextPosition = Vector2.MoveTowards(currentPosition, targetPosition, _movementSpeed * Time.fixedDeltaTime);

        _rigidbody.MovePosition(nextPosition);

        float distanceX = targetPosition.x - currentPosition.x;

        if (_flipper != null && Mathf.Abs(distanceX) > 0.05f)
            _flipper.Flip(distanceX);
    }
}
