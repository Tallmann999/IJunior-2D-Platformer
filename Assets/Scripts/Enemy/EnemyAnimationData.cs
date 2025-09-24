using UnityEngine;

public class EnemyAnimationData 
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Hit = Animator.StringToHash("Hit");
    public static readonly int Dead = Animator.StringToHash("Dead");
}
