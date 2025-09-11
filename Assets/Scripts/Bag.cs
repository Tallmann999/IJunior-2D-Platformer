using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private List<Coin> _coin = new List<Coin>();
    private int _currentCoin= 0;

    public  void AddCoin(Coin coin)
    {
        _coin.Add(coin);
        _currentCoin++;
    }
}
