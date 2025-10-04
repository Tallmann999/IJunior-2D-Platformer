using UnityEngine;

[System.Serializable]
public class Need
{
    public float MaxValue;
    public float MinValue;
    public float StartValue;
    public float CurrentValue;
    public float RegenRate;
    public float DecayRate;

    public void AddValue(float amount)
    {
        CurrentValue = Mathf.Min(CurrentValue + amount, MaxValue);
    }

    public void DeclineValue(float amount)
    {
        CurrentValue = Mathf.Max(CurrentValue - amount, MinValue);
    }

    public float GetPercent()
    {
        return CurrentValue / MaxValue;
    }

}
