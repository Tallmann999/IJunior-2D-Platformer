using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] private ButtonUi _buttonAddHealth;
    [SerializeField] private ButtonUi _buttonTakeDamage;    
    [SerializeField] private Slider _sliderHealthChanger;    
    [SerializeField] private TextMeshProUGUI _currentHealthValue;
    [SerializeField] private TextMeshProUGUI _maxHealthValue;
    [SerializeField] private Health _playerHealth;

    private int _takeDamageValue =15;
    private float _addHealthvalue= 12f;

    private void Update()
    {
        _currentHealthValue.text = _playerHealth.CurrentValue.ToString();
        _maxHealthValue.text = _playerHealth.MaxValue.ToString();
    }

    private void OnEnable()
    {
        _playerHealth.CurrentHealth += OnHealthChanger;
        _buttonAddHealth.ButtonClick += OnAddHealth;
        _buttonTakeDamage.ButtonClick += OnTakeDamage;
    }
   
    private void OnDisable()
    {
        _buttonAddHealth.ButtonClick -= OnAddHealth;
        _buttonTakeDamage.ButtonClick -= OnTakeDamage;

    }

    private void OnHealthChanger(float value)
    {
        StartCoroutine(ChangeValue(value));
    }

    private void OnTakeDamage()
    {
        _playerHealth.TakeDamage(_takeDamageValue);
    }

    private void OnAddHealth()
    {
        _playerHealth.AddValue(_addHealthvalue);
    }   

    private void ShowTextInfo()
    {
        _currentHealthValue.text = _playerHealth.CurrentValue.ToString();
        _maxHealthValue.text = _playerHealth.MaxValue.ToString();
    }

    private IEnumerator ChangeValue(float value)
    {
        value = _playerHealth.GetPercent();

        while (_sliderHealthChanger.value!= value)
        {
            //ShowTextInfo();
            _sliderHealthChanger.value = Mathf.MoveTowards(_sliderHealthChanger.value,
                value, 0.5f*Time.deltaTime);
            yield return null;
        }
    }
}
