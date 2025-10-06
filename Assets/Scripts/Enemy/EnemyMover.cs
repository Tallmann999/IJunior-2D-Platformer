using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private Flipper _flipper;
    private Vector2 _currentVelocity;
    private Transform _target;
    private bool _isDead;
    private bool _isMoving;

    public event Action<float> HorizontalMovement;

    private void Awake()
    {
        _isMoving = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetTarget(Transform target)
    {
        if (_isDead)
            return;

        _target = target;
    }

    public void Stop()
    {
        _isMoving = false;
        _target = null;
        _currentVelocity = Vector2.zero;
        _rigidbody.linearVelocity = _currentVelocity;
        HorizontalMovement?.Invoke(0f);
    }

    public void ContinumMove()
    {
        _isMoving = true;
    }

    public void Die()
    {
        _isDead = true;
        Stop();
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void Move()
    {
        if (_isMoving)
        {
            float flipThreshold = 0.05f;

            if (_isDead || _target == null)
                return;

            Vector2 currentPosition = _rigidbody.position;
            Vector2 targetPosition = _target.position;

            float distanceX = targetPosition.x - currentPosition.x;
            float directionX = Mathf.Sign(distanceX);
            _currentVelocity = new Vector2(directionX * _movementSpeed, _rigidbody.linearVelocity.y);
            _rigidbody.linearVelocity = _currentVelocity;

            if (_flipper != null && Mathf.Abs(distanceX) > flipThreshold)
                _flipper.Flip(distanceX);

            HorizontalMovement?.Invoke(_currentVelocity.x);
        }
    }
}
