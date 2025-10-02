using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(EnemyAnimationData.Params.Speed, Mathf.Abs(speed));
    }

    public void TriggerDie()
    {
        _animator.SetTrigger(EnemyAnimationData.Params.Die);
    }

    public void TriggerHit()
    {
        _animator.SetTrigger(EnemyAnimationData.Params.Hit);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(EnemyAnimationData.Params.Attack);
    }
}
