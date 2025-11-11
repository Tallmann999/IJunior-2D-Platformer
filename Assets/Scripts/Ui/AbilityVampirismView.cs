using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityVampirismView : MonoBehaviour
{
    [SerializeField] private Slider _speciaforceSlider;
    [SerializeField] private Slider _coolDownSlider;
    [SerializeField] private VampirismAbility _vampirismSpecialForce;

    private float _maxSliderValue = 1f;

    private void OnEnable()
    {
        _vampirismSpecialForce.AbilityValueTimer += OnAbilityValueTimer;
        _vampirismSpecialForce.CooldownValueTimer += OnCooldownValueTimer;
    }

    private void OnCooldownValueTimer(float value)
    {
        _coolDownSlider.value = value;

        if (_coolDownSlider.value == 0)
        {
            _coolDownSlider.value = _maxSliderValue;
            _speciaforceSlider.value = _maxSliderValue;
        }
    }

    private void OnAbilityValueTimer(float value)
    {
        _speciaforceSlider.value = value;
    }

    private void OnDisable()
    {
        _vampirismSpecialForce.AbilityValueTimer -= OnAbilityValueTimer;
    }
}