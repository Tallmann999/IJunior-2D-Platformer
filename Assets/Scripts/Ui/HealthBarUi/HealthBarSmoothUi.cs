using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmoothUi : HealthBarSliderUi
{
    private float _smothValue = 2f;
    private float _currentSliderValue;
    private Coroutine _currentCoroutine;

    protected override void UpdateHealthView(float currentvalue)
    {
        _currentSliderValue = Slider.value;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(SmoothViewSlider(currentvalue));
    }

    private IEnumerator SmoothViewSlider(float value)
    {
        float elapsedTime = 0f;
        value = Health.GetPercent();

        while (elapsedTime < _smothValue)
        {
            elapsedTime += Time.deltaTime * _smothValue;
            Slider.value = Mathf.Lerp(_currentSliderValue, value, elapsedTime);
            yield return null;
        }
    }
}
