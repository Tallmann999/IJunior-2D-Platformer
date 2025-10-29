using UnityEngine;

public class TakeDamageButton : ButtonHealth
{
    [SerializeField] private int _damageValue = 20;

    protected override void ChangeHealth()
    {
        if (_damageValue > 0)
        {
            Health.TakeDamage(_damageValue);
        }
    }
}
