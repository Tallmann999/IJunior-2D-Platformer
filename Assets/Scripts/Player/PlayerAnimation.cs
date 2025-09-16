using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    //private const string Horizontal = nameof(Horizontal);
    //private const string Speed = nameof(Speed);
    //private const string JumpOne = nameof(Jump);

    private Animator _animator;
    //private float _horizontalMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public  void SetSpeed(float speed)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(speed));
    }

    public void SetVerticalVelocity(float velocity)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.VerticalVelocity, velocity);
    }

    public void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }
}
