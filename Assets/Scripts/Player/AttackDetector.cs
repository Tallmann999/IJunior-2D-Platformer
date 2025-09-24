using System;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
   //[] private LayerMask _targetLayer;

    public event Action<IDamageble> TargetDetected;

    //  здесь мы отпределяем 
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out IDamageble damageble))
    //    {
    //        TargetDetected?.Invoke(damageble); 
    //    }
    //}

    private void FixedUpdate()
    {
        CheckForTargets();
    }

    private void CheckForTargets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 3f);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out IDamageble damageble))
            {
                TargetDetected?.Invoke(damageble);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position,2f);
    //}
    //// Метод для ручной проверки
    //public bool CheckForTargets()
    //{
    //    Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 0.5f, _targetLayer);
    //    foreach (Collider2D target in targets)
    //    {
    //        if (target.TryGetComponent(out IDamageble damageble))
    //        {
    //            TargetDetected?.Invoke(damageble);
    //            return true;
    //        }
    //    }
    //    return false;
    //}

}
