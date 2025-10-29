using System;
using UnityEngine;

public abstract class HealthBarBase : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.CurrentHealth += UpdateHealthView;
    }

    private void OnDisable()
    {
        Health.CurrentHealth -= UpdateHealthView;
    }

    protected abstract void UpdateHealthView(float currentvalue);
}
