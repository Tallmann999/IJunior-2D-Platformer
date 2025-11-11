using System;
using System.Collections;
using UnityEngine;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private Cooldown _coolDown;
    [SerializeField] private GameObject _areaAbilitySprite;
    [SerializeField] private SpecialForceDetector _specialForceDetector;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _timeLeft = 6f;
    [SerializeField] private float _cooldownTime = 4f;
    [SerializeField] private float _abilityEffectValue;

    private IDamageble _currentTarget;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _healthsmoothTimer;
    private float _healthsmoothValue = 0.5f;
    private bool _IsReadyToUse;
    private bool _isGetToUse;
    private bool _isPresedButton;

    public Action<float> AbilityValueTimer;
    public Action<float> CooldownValueTimer;
    private bool _isOnCooldown;

    protected virtual void OnEnable()
    {
        _specialForceDetector.TargetDetected += OnTargetDetected;
        _specialForceDetector.TargetLost += OnTargetLost;
        _specialForceDetector.TargetInRange += OnTargetInRange;
    }

    private void Awake()
    {
        _healthsmoothTimer = new WaitForSeconds(_healthsmoothValue);
        _coolDown = GetComponent<Cooldown>();
        _areaAbilitySprite.SetActive(false);
    }

    //private void Start()
    //{
    //    _healthsmoothTimer = new WaitForSeconds(_healthsmoothValue);
    //}

    protected virtual void OnDisable()
    {
        _specialForceDetector.TargetDetected -= OnTargetDetected;
        _specialForceDetector.TargetLost -= OnTargetLost;
        _specialForceDetector.TargetInRange -= OnTargetInRange;
    }

    private void OnTargetDetected(IDamageble target)
    {
        _currentTarget = target;
        _IsReadyToUse = true;
    }

    private void OnTargetInRange(IDamageble target)
    {
        _currentTarget = target;
        _IsReadyToUse = true;
    }

    private void OnTargetLost(IDamageble target)
    {
        if (_currentTarget == target)
        {
            _currentTarget = null;
            _IsReadyToUse = false;
        }
    }

    public void OnUseForce(bool isPressedButton)
    {
        if (isPressedButton && !_isGetToUse && _coolDown.IsExpired() && !_isOnCooldown)
        {
            _isPresedButton = true;

            if (_currentCoroutine == null)
            {
                StopCoroutine(ActiveTimer());
            }

            _currentCoroutine = StartCoroutine(ActiveTimer());
        }
    }

    private IEnumerator ActiveTimer()
    {
        if (_isPresedButton && _coolDown.IsExpired())
        {
            _isGetToUse = true;
            _areaAbilitySprite.SetActive(true);

            Coroutine vampirismForceCoroutine = StartCoroutine(VampirismForce());
            Coroutine abilityTimerCoroutine = StartCoroutine(AbilityTimerCoroutine(AbilityValueTimer, _timeLeft));

            yield return abilityTimerCoroutine;

            _isGetToUse = false;
            _isPresedButton = false;
            _areaAbilitySprite.SetActive(false);

            if (vampirismForceCoroutine != null)
                StopCoroutine(vampirismForceCoroutine);

            _isOnCooldown = true;
            _coolDown.Reset();
            Coroutine coolDownTimerCotoutine = StartCoroutine(AbilityTimerCoroutine(CooldownValueTimer, _cooldownTime));

            yield return coolDownTimerCotoutine;
            _isOnCooldown = false;
        }
    }

    private IEnumerator AbilityTimerCoroutine(Action<float> OnValueChange, float totalValueTimer)
    {
        float elapsedTime = 0f;
        float totalTime = totalValueTimer;
        float startValue = 1f;
        float endValue = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / totalTime);

            OnValueChange?.Invoke(currentValue);
            yield return null;
        }

        OnValueChange?.Invoke(0f);
    }

    private bool IsTargetValid(IDamageble target)
    {
        if (target == null)
            return false;

        return true;
    }

    private IEnumerator VampirismForce()
    {
        int damage = (int)_abilityEffectValue;

        while (_isGetToUse)
        {
            if (_currentTarget != null && _IsReadyToUse)
            {
                if (IsTargetValid(_currentTarget))
                {
                    _playerHealth.AddValue(_abilityEffectValue);
                    _currentTarget.TakeDamage(damage);
                }
                else
                {
                    _currentTarget = null;
                    _IsReadyToUse = false;
                }
            }

            yield return _healthsmoothTimer;
        }
    }
}