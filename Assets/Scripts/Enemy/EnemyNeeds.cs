using UnityEngine;

public class EnemyNeeds : MonoBehaviour,IDamageble
{
    [SerializeField]private Need _health;

    public bool IsAlive => _health.CurrentValue > 0;

    public void TakeDamage(int damage)
    {
        Debug.Log($" текущий уровень здоровья   {_health.CurrentValue}");

        _health.Decline(damage);
    }

    private void Start()
    {
        _health.CurrentValue = _health.StartValue;
    }
}
