using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Minerals : MonoBehaviour, ICollectible
{
    public static event Action mineralCollected;

    public void Collect()
    {
        Debug.Log("Mineral Collected");
        Destroy(gameObject);
        mineralCollected?.Invoke();
    }
}
