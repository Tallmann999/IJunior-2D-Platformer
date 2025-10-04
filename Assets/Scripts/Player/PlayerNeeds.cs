using System;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour, IDamageble
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

    public void AddPotionHealth(int health)
    {
        _health.AddValue(health);
    }

    public void TakeDamage(int damage)
    {
        _health.DeclineValue(damage);
        Hit?.Invoke();

        if (_health.CurrentValue <= 0)
        {
            _isDie = true;
            Die?.Invoke(_isDie);
        }
    }
}
