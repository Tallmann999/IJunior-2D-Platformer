using UnityEngine;

public class AddHealthButton : ButtonHealth
{
    [SerializeField] private float _addHealthValue = 20f;

    protected override void ChangeHealth()
    {
        if (_addHealthValue > 0)
        {
            Health.AddValue(_addHealthValue);
        }
    }
}
