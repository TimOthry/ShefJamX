using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Minerals : MonoBehaviour, ICollectible
{
    public static event Action mineralCollected;
    //Rigidbody2D rb;

    //bool hasTarget;
    //Vector3 targetPos;
    //float moveSpeed = 5f;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}

    public void Collect()
    {
        Debug.Log("Mineral Collected");
        Destroy(gameObject);
        mineralCollected?.Invoke();
    }

    //private void FixedUpdate()
    //{
    //    if (hasTarget)
    //    {
    //        Debug.Log("Save me");
    //        Vector2 targetDirection = (targetPos - transform.position).normalized;
    //        rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
    //    }
    //}

    //public void SetTarget(Vector3 position)
    //{
    //    targetPos = position;
    //    hasTarget = true;
    //}
}
