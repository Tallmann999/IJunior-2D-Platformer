using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Speed = nameof(Speed);
    private const string JumpOne = nameof(Jump);

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

    private void Move()
    {
        if (_playerMover.IsMoving == true && _playerMover.IsGrounded == true)
            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(JumpOne);
        }
    }

    private void Idle()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
    }
}
