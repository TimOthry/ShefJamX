using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public int health = 100;

    public GameObject destructionEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
