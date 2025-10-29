using UnityEngine;
using UnityEngine.UI;

public class HealthBarSliderUi : HealthBarBase
{
    [SerializeField] protected Slider Slider;

    protected void Start()
    {
        Slider.value = Health.GetPercent();
    }
    
    protected override void UpdateHealthView(float currentvalue)
    {
        currentvalue = Health.GetPercent();
        Slider.value = currentvalue;
    }
}
