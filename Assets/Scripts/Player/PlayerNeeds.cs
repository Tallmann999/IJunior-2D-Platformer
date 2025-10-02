using System;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour, IDamageble
{
    [SerializeField] private Need _health;

    public event Action Hit;
    public event Action<bool> Die;
    private bool IsDie;
    public bool IsAlive => _health.CurrentValue > 0;

    private void Start()
    {
        _health.CurrentValue = _health.StartValue;

    }

    public void TakeDamage(int damage)
    {
        Debug.Log($" текущий уровень здоровья   {_health.CurrentValue}");
        Hit?.Invoke();
        // можно сделать отсрочку по нанесению урона в 2 сеунды .
        // чтоб при нажатии на клавишу мышки сразу не уходили все жизни
        _health.Decline(damage);

        if (_health.CurrentValue <= 0)
        {
            IsDie = true;
            Die?.Invoke(IsDie);
        }

        //StartCoroutine(OnDestroyActivator());

    }
}
