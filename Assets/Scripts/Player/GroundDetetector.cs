using UnityEngine;
using System;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    private RaycastHit2D[] _raycastHit = new RaycastHit2D[5];

    public event Action<bool> GroundedChanged;

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        int hitCount = Physics2D.RaycastNonAlloc(transform.position,Vector2.down,
            _raycastHit, _groundCheckRadius,_groundLayer);

        bool isGrounded = hitCount > 0;
        GroundedChanged?.Invoke(isGrounded);
    }
}