using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    CoinDestroer coinDestroer;
    public static UnityEvent scoreUp = new UnityEvent();
    private void Awake()
    {
        StartCoroutine(routine: DestroyCountDown(5));
        coinDestroer = GetComponentInParent<CoinDestroer>();
    }

    private IEnumerator DestroyCountDown(float time)
    {
        yield return new WaitForSeconds(time);
        coinDestroer.DestroyCoin();
    }

    private void OnTriggerEnter(Collider other)
    {
        scoreUp?.Invoke();
        coinDestroer.DestroyCoin();
    }
}
