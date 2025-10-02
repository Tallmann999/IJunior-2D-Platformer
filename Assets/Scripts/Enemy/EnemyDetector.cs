using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Transform> PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerDetected?.Invoke(player.transform);
            Debug.Log("Игрок обнаружен!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerLost?.Invoke();
            Debug.Log("Игрок убежал!");
        }
    }
}
