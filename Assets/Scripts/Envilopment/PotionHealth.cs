using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    [SerializeField] private int _healthValue;

    public int HealthValue => _healthValue;
}
