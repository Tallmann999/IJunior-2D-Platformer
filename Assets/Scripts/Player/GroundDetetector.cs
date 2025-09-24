using UnityEngine;
using System;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    private RaycastHit2D[] _raycastHit = new RaycastHit2D[5];
    private bool _wasGrounded;

    public event Action<bool> GroundedChanged;

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        int hitCount = Physics2D.RaycastNonAlloc(transform.position, Vector2.down,
            _raycastHit, _groundCheckRadius, _groundLayer);

        bool isGrounded = hitCount > 0;

        if (isGrounded != _wasGrounded)
        {
            _wasGrounded = isGrounded;
            GroundedChanged?.Invoke(isGrounded);
        }
    }
}