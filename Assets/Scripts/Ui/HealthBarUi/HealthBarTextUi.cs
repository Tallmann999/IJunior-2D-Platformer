using TMPro;
using UnityEngine;

public class HealthBarTextUi : HealthBarBase
{
    [SerializeField] private TextMeshProUGUI _healthMaxValue;
    [SerializeField] private TextMeshProUGUI _healthCurrentValue;

    private void Start()
    {
        _healthMaxValue.text = Health.MaxValue.ToString();
        _healthCurrentValue.text = Health.CurrentValue.ToString();
    }

    protected override void UpdateHealthView(float currentvalue)
    {
        _healthMaxValue.text = Health.MaxValue.ToString();
        _healthCurrentValue.text = currentvalue.ToString();
    }
}
