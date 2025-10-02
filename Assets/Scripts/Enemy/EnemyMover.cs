using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private Flipper _flipper;
    private Transform _target;
    public event Action<float> HorizontalMovement;

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
        HorizontalMovement?.Invoke(0f); 
    }


    private void Move()
    {
         float flipThreshold = 0.05f;

        if (_target == null) return;

        Vector2 currentPosition = _rigidbody.position;
        Vector2 targetPosition = _target.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;
        Vector2 newVelocity = direction * _movementSpeed;

        _rigidbody.linearVelocity = newVelocity;

        float distanceX = targetPosition.x - currentPosition.x;

        if (_flipper != null && Mathf.Abs(distanceX) > flipThreshold)
            _flipper.Flip(distanceX);

        HorizontalMovement?.Invoke(newVelocity.x);

        //Vector2 currentPosition = _rigidbody.position;
        //Vector2 targetPosition = _target.position;
        //Vector2 nextPosition = Vector2.MoveTowards(currentPosition, targetPosition, _movementSpeed * Time.fixedDeltaTime);

        //_rigidbody.MovePosition(nextPosition);

        //float distanceX = targetPosition.x - currentPosition.x;

        //if (_flipper != null && Mathf.Abs(distanceX) > flipThreshold)
        //    _flipper.Flip(distanceX);

        //if (_rigidbody.linearVelocity.x!=0)
        //{
        //    HorizontalMovement?.Invoke(_rigidbody.linearVelocity.x);
        //}
    }
}
