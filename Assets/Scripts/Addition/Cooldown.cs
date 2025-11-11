using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float _value = 2f;

    private float _lastResetTime;
    private bool _isReady = true;

    public float Value => _value;

    private void Awake()
    {
        _lastResetTime = -_value;
    }

    private void Update()
    {
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