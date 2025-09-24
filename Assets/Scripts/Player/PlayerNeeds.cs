using System;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour, IDamageble
{
  [SerializeField] private  Need _health;
    public bool IsAlive => _health.CurrentValue > 0;

    //public event Action HiTakeDamage;// в перспективе переделать под смерть героя.Action<bool> Died 

    public void TakeDamage(int damage)
    {
        _health.Decline(damage);
        //HiTakeDamage?.Invoke();
    }

    private void Start()
    {
        _health.CurrentValue = _health.StartValue;
    }
}
