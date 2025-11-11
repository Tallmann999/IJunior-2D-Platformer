using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode JumpKeyCode = KeyCode.Space;
    private const KeyCode SpecialForceCode = KeyCode.E;

    public event Action<float> HorizontalMovement;
    public event Action Jumping;
    public event Action<bool> SpecialForceUse;
    public event Action Attacking;

    private void Update()
    {
        MoveControl();
        JumpControl();
        AttackControl();
        SpecialForceControl();
    }

    private void SpecialForceControl()
    {
        if (Input.GetKeyDown(SpecialForceCode))
        {
            SpecialForceUse?.Invoke(true);
        }
    }

    private void AttackControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attacking?.Invoke();
        }
    }

    private void JumpControl()
    {
        if (Input.GetKeyDown(JumpKeyCode) )
        {
            Jumping?.Invoke();
        }
    }

    private void MoveControl()
    {
        float horizontalDirection = Input.GetAxis(Horizontal);

        if (horizontalDirection != 0)
        {
            HorizontalMovement?.Invoke(horizontalDirection);
        }
    }
}
