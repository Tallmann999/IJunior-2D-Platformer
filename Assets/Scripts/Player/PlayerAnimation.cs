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
    }

    public void Move()
    {
        //float horizontalMove = Input.GetAxis(Horizontal);

        if (_playerMover.IsMoving==true)
            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
        //if (horizontalMove < 0 || horizontalMove > 0)
        //    _animator.SetFloat(Speed, horizontalMove);
    }

    private void Jump()
    {

    }
    public void Idle()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
    }

}
