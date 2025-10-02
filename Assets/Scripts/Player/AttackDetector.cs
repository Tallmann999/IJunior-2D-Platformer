using System;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    public event Action<IDamageble> TargetDetected;
    public event Action<IDamageble> TargetLost;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer)) 
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetDetected?.Invoke(damageble);
            Debug.Log("Цель вошла в зону: " + damageble);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer))
            return;

        if (other.TryGetComponent(out IDamageble damageble))
        {
            TargetLost?.Invoke(damageble);
            Debug.Log("Цель покинула зону: " + damageble);
        }
    }

    private bool IsInLayerMask(int layer)
    {
        return (_targetMask.value & (1 << layer)) != 0;
    }
}
