using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _minValue;
    [SerializeField] private float _startValue;
    [SerializeField] private float _currentValue;
    [SerializeField] private float _regenRate;
    [SerializeField] private float _decayRate;

    private bool _isDie;

    public event Action Hit;
    public event Action<bool> Die;

    private void Awake()
    {
        _currentValue = _startValue;
    }

    public void TakeDamage(int damage)
    {
        DeclineValue(damage);
        Hit?.Invoke();

        if (_currentValue <= 0)
        {
            _isDie = true;
            Die?.Invoke(_isDie);
        }
    }

    public void AddValue(float amount)
    {
        if (amount < 0)
            return;

        _currentValue = Mathf.Min(_currentValue + amount, _maxValue);
    }

    public void DeclineValue(float amount)
    {
        if (amount < 0)
            return;

        _currentValue = Mathf.Max(_currentValue - amount, _minValue);
    }

    public float GetPercent()
    {
        return _maxValue > 0 ? _currentValue / _maxValue : 0f;
    }
}
