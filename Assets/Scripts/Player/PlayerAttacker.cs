using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttacker : Attacker
{
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();

        if (AttackDetector == null)
            AttackDetector = GetComponentInChildren<AttackDetector>(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_inputReader != null)
        {
            _inputReader.Attacking += OnAttackStateChanged;
        }
    }

    protected override void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.Attacking -= OnAttackStateChanged;
        }

        base.OnDisable();
    }

    private void OnAttackStateChanged(bool canAttack)
    {
        if (canAttack)
        {
            PerformAttack();
        }
    }

    protected override void PerformAttack()
    {
        base.PerformAttack();
    }
}