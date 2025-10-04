using System;
using UnityEngine;

public class EnemyNeeds : MonoBehaviour, IDamageble
{
    [SerializeField] private Need _health;

    private bool _isDie;

    public event Action Hit;
    public event Action<bool> Die;

    public bool IsAlive => _health.CurrentValue > 0;

    private void Start()
    {
        _health.CurrentValue = _health.StartValue;
    }

    public void TakeDamage(int damage)
    {
        Hit?.Invoke();
        _health.DeclineValue(damage);

        if (_health.CurrentValue <= 0)
        {
            _isDie = true;
            Die?.Invoke(_isDie);
        }
    }
}
