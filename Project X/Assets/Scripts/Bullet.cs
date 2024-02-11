using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        AsteroidBehaviour asteroid = hitInfo.GetComponent<AsteroidBehaviour>();
        if (hitInfo.GetComponent<Magnet>() == null)
        {
            if (asteroid != null)
            {
                asteroid.TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        Debug.Log(hitInfo.name);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
