using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    //private const string Vertical = nameof(Vertical);

    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        MoveControl();
        JumpControl();
    }

    private void JumpControl()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _playerMover.Jump();
        }
    }

    private void MoveControl()
    {
        float horizontalDirection = Input.GetAxis(Horizontal);

        if (horizontalDirection != 0)
        {
            _playerMover.Move();
        }
    }
}
