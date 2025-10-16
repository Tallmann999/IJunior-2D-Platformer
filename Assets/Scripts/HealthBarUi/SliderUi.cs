using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderUi : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public event Action<float> OnValueChanged;

    public float SliderValue => _slider.value;

    //private void OnEnable()
    //{
    //    _slider.onValueChanged.AddListener(OnSliderValueChanged);
    //}

    //private void OnDisable()
    //{
    //    _slider.onValueChanged.RemoveListener(OnSliderValueChanged);        
    //}
    //public float SetValue(float value)
    //{
    //    _slider.value = value; 
    //    return _slider.value;
    //}

    //private void OnSliderValueChanged(float value)
    //{
    //    OnValueChanged?.Invoke(value);
    //}
}
