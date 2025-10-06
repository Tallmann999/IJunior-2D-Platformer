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
            _inputReader.Attacking += OnAttackPressed;
        }
    }

    protected override void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.Attacking -= OnAttackPressed;
        }

        base.OnDisable();
    }

    private void OnAttackPressed()
    {
        if (HaveAttack && CurrentTarget != null)
        {
            PerformAttack();
            InvokeAttackEvent(true);
        }
        else
        {
            Debug.Log("Нет цели для атаки");
        }
    }
}