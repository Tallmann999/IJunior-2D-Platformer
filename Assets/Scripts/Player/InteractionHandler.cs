using UnityEngine;

[RequireComponent(typeof(Bag),typeof(PlayerNeeds))]
public class InteractionHandler : MonoBehaviour
{
    private Bag _currentBag;
    private PlayerNeeds _playerNeeds;

    private void Awake()
    {
        _currentBag = GetComponent<Bag>();
        _playerNeeds = GetComponent<PlayerNeeds>();
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
            _playerNeeds.AddPotionHealth(health.HealthValue);
            Destroy(collision.gameObject);
        }
    }
}
