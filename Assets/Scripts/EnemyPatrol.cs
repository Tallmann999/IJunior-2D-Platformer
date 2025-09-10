using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pathTarget;
    //[SerializeField] private float _movementSpeed;
    [SerializeField] private Transform[] _points;
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
        // Проверяем достижение цели и переключаемся на следующую
        if (Vector2.Distance(transform.position, _points[_currentPointIndex].position) < 0.1f)
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
        if (_points.Length > 0)
        {
            _enemyMover.SetTarget(_points[_currentPointIndex]);
        }
    }
}