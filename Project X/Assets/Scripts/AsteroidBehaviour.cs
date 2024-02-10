using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public int health = 100;

    public GameObject destructionEffect;
    private GameObject player;

    [SerializeField] private float Angle;
    [SerializeField] private int maxDistance;
    [SerializeField] private int speed;
    public Vector3 vectorVelocity;

    private void Awake()
    {
        player = GameObject.Find("Player");
        float y = gameObject.transform.position.y - player.transform.position.y;
        float x = gameObject.transform.position.x - player.transform.position.x;
        Angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        Angle += Random.Range(-30, 30);
    }

    private void Update()
    {
        Vector2 movement = Quaternion.Euler(0, 0, Angle) * Vector2.up;
        vectorVelocity = movement * Time.deltaTime * speed;
        transform.position += vectorVelocity;


        Vector2 distanceVector = gameObject.transform.position - player.transform.position;
        float distance = Mathf.Pow(Mathf.Pow(Mathf.Abs(distanceVector.x), 2) + Mathf.Pow(Mathf.Abs(distanceVector.y), 2), 0.5f);

        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

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
