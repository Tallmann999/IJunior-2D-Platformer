using UnityEngine;

[RequireComponent(typeof(Bag))]
public class InteractionHandler : MonoBehaviour
{
    private Bag _currentBag;

    private void Awake()
    {
        _currentBag = GetComponent<Bag>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _currentBag.AddCoin(coin);
            Destroy(collision.gameObject);
        }
    }
}
