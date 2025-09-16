using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode KeyCoder = KeyCode.Space;

    public event Action<float> HorizontalMovement;
    public event Action<bool> Jumping;

    private void Update()
    {
        MoveControl();
        JumpControl();
    }

    private void JumpControl()
    {
        if (Input.GetKeyDown(KeyCoder) )
        {
            Jumping?.Invoke(true);
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
