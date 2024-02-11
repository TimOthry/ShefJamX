using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody2D rigidBody;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelerationTime;
    private float moveSpeed;
    private Vector2 moveStep;
    private Vector2 mouseDifferenceNormalised;
    public float fuel;
    public float maxFuel;
    [SerializeField] private float scaleMultFuel;
    [SerializeField] private float fuelDecay;
    [SerializeField] private float boost;

    [SerializeField] private GameObject destructionEffect;
    [SerializeField] private AudioClip explosionSound;
    

    public float distanceTravelled;
    private Vector2 lastPos;

    public SafeArea safeArea;

    // Start is called before the first frame update
    void Start()
    {
        boost = 1f;
        maxFuel = 500; // This is to be changed when fuel is upgraded
        rigidBody = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boost = 2f;
        }
        else
        {
            boost = 1f;
        }

        Vector3 mousePos = GetMousePos();
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
        mouseDifferenceNormalised = new Vector2(Mathf.Clamp(mousePos.x - transform.position.x, -9f, 9f),
            Mathf.Clamp(mousePos.y - transform.position.y, -9f, 9f)) / 9f;
    }

    private void FixedUpdate()
    {
        distanceTravelled = Vector2.Distance(transform.position, lastPos);
        Debug.Log(distanceTravelled.ToString("F4"));
        lastPos = transform.position;

        if (!safeArea.inRange)
        {
            fuel -= fuelDecay + (distanceTravelled * scaleMultFuel);
        }
        if (fuel <= 0)
        {
            Die();
        }

        if (!Input.GetMouseButton(1))
        {
            if (moveSpeed > 0f) moveSpeed -= Time.fixedDeltaTime / accelerationTime;
            if (moveSpeed < 0f) moveSpeed = 0f;
        }
        else if (moveSpeed < maxMoveSpeed) moveSpeed += Time.fixedDeltaTime / accelerationTime;
        moveStep = mouseDifferenceNormalised * (moveSpeed * boost);
        rigidBody.MovePosition(transform.position + (Vector3)moveStep);
        Debug.Log("Move speed: " + moveSpeed.ToString("F3") + " mouse dif: " + mouseDifferenceNormalised.ToString("F3"));
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Minerals>())
        {
            
        }
        else
        {
            // Player cant die for now
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 2f);
        Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private Vector3 GetMousePos()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
