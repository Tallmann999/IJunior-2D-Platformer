using System;
using UnityEngine;

public abstract class DetectorBase : MonoBehaviour
{
    [SerializeField] protected LayerMask _targetMask;

    public event Action<IDamageble> TargetDetected;
    public event Action<IDamageble> TargetLost;
    public event Action<IDamageble> TargetInRange;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetDetected?.Invoke(damageble);
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble) && damageble.IsAlive())
        {
            TargetInRange?.Invoke(damageble);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetLost?.Invoke(damageble);
        }
    }

    protected bool IsInLayerMask(int layer)
    {
        return (_targetMask.value & (1 << layer)) != 0;
    }
}