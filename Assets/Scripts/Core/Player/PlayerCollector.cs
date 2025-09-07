using System;
using UniRx;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public Subject<Unit> OnCollectedCoin = new Subject<Unit>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            OnCollectedCoin.OnNext(Unit.Default);
        }
    }
}
