using System;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    public event Action<IDamageble> TargetDetected;
    public event Action<IDamageble> TargetLost;
    public event Action<IDamageble> TargetInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetDetected?.Invoke(damageble);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble) && damageble.IsAlive())
        {
            TargetInRange?.Invoke(damageble);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetLost?.Invoke(damageble);
        }
    }

    private bool IsInLayerMask(int layer)
    {
        return (_targetMask.value & (1 << layer)) != 0;
    }
}
