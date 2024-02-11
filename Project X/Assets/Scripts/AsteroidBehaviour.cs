using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AsteroidBehaviour : MonoBehaviour
{
    public int health = 100;

    [SerializeField] private GameObject destructionEffect;
    private GameObject player;

    [SerializeField] private float Angle;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int maxDistance;
    private Vector3 vectorVelocity;
    [SerializeField] private AudioClip explosionSound;
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
        float y = transform.position.y - player.transform.position.y;
        float x = transform.position.x - player.transform.position.x;
        Angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        Angle += Random.Range(-30, 30);
    }

    private void Update()
    {
        Vector2 distanceVector = transform.position - player.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(distanceVector.x), 2) + Mathf.Pow(Mathf.Abs(distanceVector.y), 2));

        speed += Mathf.Floor(distance / 100) * 0.5f; ;

        Vector2 movement = Quaternion.Euler(0, 0, Angle) * Vector2.up;
        transform.position += (Vector3)(movement * (Time.deltaTime * speed));
        

        
        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }

        if (Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x), 2) + Mathf.Pow(Mathf.Abs(transform.position.y), 2)) < 19f)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        } else {
            source.Play();
        }
    }
 

    void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 2f);
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Instantiate(destructionEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
