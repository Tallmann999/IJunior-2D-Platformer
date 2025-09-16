using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pathTarget;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _movingSpeed = 2f;

    private int _currentPointIndex;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void Start()
    {
        InitPoint();
        SetNextTarget();
    }

    private void Update()
    {
        MoveToPoints();

        if (transform.position == _points[_currentPointIndex].position)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
            SetNextTarget();
        }
    }

    [ContextMenu("Refresh Allay")]
    private void InitPoint()
    {
        _points = new Transform[_pathTarget.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _pathTarget.GetChild(i);
        }
    }

    private void SetNextTarget()
    {
        if (_points.Length > 0 && _enemyMover != null)
        {
            _enemyMover.SetTarget(_points[_currentPointIndex]);
        }
    }

    private void MoveToPoints()
    {
        if (_points.Length == 0) return;

        Transform target = _points[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position,
            _movingSpeed * Time.deltaTime);
    }
}