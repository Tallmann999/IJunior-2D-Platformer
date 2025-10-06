using UnityEngine;

[RequireComponent(typeof(Cooldown))]
public class EnemyAttacker : Attacker
{
    [SerializeField] private Cooldown _cooldown;

    private void Awake()
    {
        _cooldown = GetComponent<Cooldown>();
    }

    public void LoopAttack()
    {
        if (_cooldown.IsExpired())
        {
            PerformAttack();
            _cooldown.Reset();
        }
    }
}
