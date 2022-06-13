using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroer : MonoBehaviour
{

    public void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}
