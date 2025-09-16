using UnityEngine;

[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private Rotation _rotation;
    private float _horizontalDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rotation = GetComponent<Rotation>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            MoveToTarget();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
   
    private void MoveToTarget()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        _horizontalDirection =direction.x;

        _rotation.RotationInspector(_horizontalDirection);
        Vector2 movement = direction * _movementSpeed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + movement);
    }
}