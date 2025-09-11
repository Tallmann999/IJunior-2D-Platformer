using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    public const string Speed = "Speed";
    private float _maxMovingValue = 1f;
    private Animator _animator;
    private PlayerMover _playerMover;
    private float _horizontalMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis(Horizontal);
        Idle();
        Move();
        Jump();
    }

    public void Move()
    {
        if (_playerMover.IsMoving==true && _playerMover.IsGrounded==true)
            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
    }

    private void Jump()
    {
        if (_playerMover.IsGrounded ==false)
        {
            _animator.SetTrigger("Jump");
        }
        //else
        //{
        //    _animator.SetBool("Jump",false);

        //}
    }
    public void Idle()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
    }

}
