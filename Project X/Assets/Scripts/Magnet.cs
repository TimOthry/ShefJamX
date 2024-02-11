using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if we collide with the mineral

        if(collision.gameObject.TryGetComponent<Minerals>(out Minerals mineral))
        {
            // tell the mineral to move towards player
            mineral.SetTarget(transform.parent.position);
        }
    }
}
