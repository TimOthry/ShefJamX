using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        Debug.Log("Something happened");
        if (collectible != null)
        {
            Destroy(gameObject);
        }
    }
}
