using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float _value = 2f;

    private float _lastResetTime;
    private bool _isReady = true;

    private void Awake()
    {
        _lastResetTime = -_value; // Сразу готов к использованию
    }

    private void Update()
    {
        // Автоматически обновляем статус готовности
        if (!_isReady && Time.time - _lastResetTime >= _value)
        {
            _isReady = true;
        }
    }

    public void Reset()
    {
        _lastResetTime = Time.time;
        _isReady = false;
    }

    public bool IsExpired() => _isReady;
}