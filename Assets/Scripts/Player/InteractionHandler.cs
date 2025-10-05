using UnityEngine;

[RequireComponent(typeof(Bag),typeof(Health))]
public class InteractionHandler : MonoBehaviour
{
    private Bag _currentBag;
    private Health _playerHealth;

    private void Awake()
    {
        _currentBag = GetComponent<Bag>();
        _playerHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _currentBag.AddCoin(coin);
            Destroy(collision.gameObject);
        }

        if (collision.TryGetComponent(out PotionHealth health))
        {
            _playerHealth.AddValue(health.HealthValue);
            Destroy(collision.gameObject);
        }
    }
}
