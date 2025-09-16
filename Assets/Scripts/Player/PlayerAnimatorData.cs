using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int Jump = Animator.StringToHash("Jump");
    }
}
