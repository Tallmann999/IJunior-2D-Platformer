using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pathTarget;
    [SerializeField] private float _arrivalThreshold = 0.4f;

    private Transform[] _points;
    private int _currentPointIndex = -1;

    public float ArrivalThreshold => _arrivalThreshold;
    public bool HasPoints => _points != null && _points.Length > 0;

    private void Awake()
    {
        InitPoints();
    }

    [ContextMenu("Refresh Array")]
    private void InitPoints()
    {
        _points = new Transform[_pathTarget.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _pathTarget.GetChild(i);
        }
    }

    public Transform GetCurrentTarget()
    {
        if (!HasPoints)
            return null;

        if (_currentPointIndex < 0)
        {
            _currentPointIndex = 0;
        }

        return _points[_currentPointIndex];
    }

    public Transform AdvanceToNext()
    {
        if (!HasPoints)
            return null;

        _currentPointIndex = ++_currentPointIndex % _points.Length;
        return _points[_currentPointIndex];
    }
}